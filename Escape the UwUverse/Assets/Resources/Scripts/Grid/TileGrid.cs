using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TileGrid : MonoBehaviour
{
    private GridNode[,] m_nodes;

    [SerializeField]
    private Tilemap m_hole;

    [SerializeField]
    private Tilemap m_walls;

    [SerializeField]
    private Tilemap m_background;

    // TODO @matthew - take this value from m_forground
    [SerializeField]
    private int m_width;

    [SerializeField]
    private int m_height;

    private int m_startX = 0;
    private int m_startY = 0;

    public int width
    { get { return m_width; } }

    public int height
    { get { return m_height; } }

    private void Awake()
    {
        m_startX = -(m_width / 2);
        m_startY = -(m_height / 2);
        m_nodes = new GridNode[width, height];

        //set x,y coords
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                m_nodes[x, y] = new GridNode(x, y);
                m_nodes[x, y].m_grid = this;
            }
        }

        {
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    Vector3 pos = GridCoordToWorldCoord(w, h);

                    {
                        Vector3Int cell = m_walls.WorldToCell(pos);
                        if (cell != null && m_walls.HasTile(cell))
                        {
                            GetNode(w, h).isWall = true;
                        }
                    }

                    {
                        Vector3Int cell = m_hole.WorldToCell(pos);
                        if (cell != null && m_hole.HasTile(cell))
                        {
                            GetNode(w, h).isHole = true;
                        }
                    }
                }
            }
        }
    }

    public GridNode GetNode(int x, int y)
    {
        if (x >= m_width || y >= m_height)
            return null;

        if (x < 0 || y < 0)
            return null;

        return m_nodes[x, y];
    }

    public GridNode GetNode(Vector2Int pos)
    {
        return GetNode(pos.x, pos.y);
    }

    // very inefficent
    public GridNode FindObjectOnGrid(GameObject obj)
    {
        foreach (GridNode node in m_nodes)
        {
            foreach (GameObject it_obj in node.AllObjects())
            {
                if (obj == it_obj)
                {
                    return node;
                }
            }
        }

        return null;
    }

    public Vector3 GridCoordToWorldCoord(Vector2Int pos)
    {
        return GridCoordToWorldCoord(pos.x, pos.y);
    }

    public Vector3 GridCoordToWorldCoord(int x, int y)
    {
        return new Vector3(m_startX + x + 0.5f, m_startY + y + 0.5f, 0);
    }

    public GridNode GetNearestNode(float x, float y)
    {
        int ix = Mathf.FloorToInt(x - 0.5f);
        int iy = Mathf.FloorToInt(y - 0.5f);

        ix += m_startX;
        iy += m_startY;

        return GetNode(ix + width, iy + height);
    }

    //     public GridNode GetNearestNode(float x, float y)
    //     {
    //         int ix = Mathf.RoundToInt(x) + Mathf.Abs(m_startX);
    //         int iy = Mathf.RoundToInt(y) + Mathf.Abs(m_startY);
    //
    //         return GetNode(ix, iy);
    //     }

    public GridNode GetNearestNode(Vector2 pos)
    {
        return GetNearestNode(pos.x, pos.y);
    }

    public List<GridNode> FindPath(GridNode start, GridNode end)
    {
        return GridPathfinder.FindPath(start, end);
    }
}

//
internal class GridPathfinderNode
{
    public int x;
    public int y;

    public bool IsTraversable;
    public GridNode node;

    public int gCost;
    public int hCost;

    public int fCost
    { get { return gCost + hCost; } }

    public GridPathfinderNode parent = null;
}

internal class GridPathfinder
{
    // ported from lil-guy-big-adventure : PathFinder.cs written by @jay
    static public List<GridNode> FindPath(GridNode start, GridNode end)
    {
        bool pathSuccess = false;

        List<GridPathfinderNode> openSet = new List<GridPathfinderNode>();
        List<GridPathfinderNode> closedSet = new List<GridPathfinderNode>();

        // TODO - nullcheck
        GridPathfinderNode startNode = (GridPathfinderNode)CreatePathfinderNode(start);
        GridPathfinderNode targetNode = (GridPathfinderNode)CreatePathfinderNode(end);

        if (startNode.IsTraversable && targetNode.IsTraversable)
        {
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                GridPathfinderNode currentNode = openSet[0];
                openSet.RemoveAt(0);

                closedSet.Add(currentNode);

                if (currentNode.x == targetNode.x && currentNode.y == targetNode.y)
                {
                    pathSuccess = true;
                    break;
                }

                List<GridPathfinderNode> neighbourList = FindNeighbours(currentNode);

                for (int i = 0; i < neighbourList.Count; i++)
                {
                    GridPathfinderNode neighbour = neighbourList[i];

                    if (!neighbour.IsTraversable || ListContainsNode(closedSet, neighbour))
                        continue;

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !ListContainsNode(openSet, neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!ListContainsNode(openSet, neighbour))
                            openSet.Add(neighbour);
                        else
                            UpdateItemInList(neighbour, openSet);
                    }
                }
            }
        }
        if (pathSuccess)
        {
            return RetracePath(openSet[0], openSet[openSet.Count - 1]);
        }
        return null;
    }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

    static private GridPathfinderNode? CreatePathfinderNode(GridNode inNode)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    {
        if (inNode == null)
            return null;

        GridPathfinderNode outNode = new GridPathfinderNode();
        outNode.x = inNode.x;
        outNode.y = inNode.y;
        outNode.IsTraversable = inNode.isTraversable;
        outNode.node = inNode;
        return outNode;
    }

    static private List<GridPathfinderNode> FindNeighbours(GridPathfinderNode node)
    {
        List<GridPathfinderNode> neighbours = new List<GridPathfinderNode>();
        GridPathfinderNode? n = null;

        n = CreatePathfinderNode(node.node.GetNeighbour(1, 0));
        if (n != null) { neighbours.Add((GridPathfinderNode)n); }

        n = CreatePathfinderNode(node.node.GetNeighbour(-1, 0));
        if (n != null) { neighbours.Add((GridPathfinderNode)n); }

        n = CreatePathfinderNode(node.node.GetNeighbour(0, 1));
        if (n != null) { neighbours.Add((GridPathfinderNode)n); }

        n = CreatePathfinderNode(node.node.GetNeighbour(0, -1));
        if (n != null) { neighbours.Add((GridPathfinderNode)n); }

        return neighbours;
    }

    static private int GetDistance(GridPathfinderNode nodeA, GridPathfinderNode nodeB)
    {
        int dstX = Mathf.Abs(nodeA.x - nodeB.x);
        int dstY = Mathf.Abs(nodeA.y - nodeB.y);

        return dstX + dstY;
    }

    static private void UpdateItemInList(GridPathfinderNode node, List<GridPathfinderNode> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].x == node.x && list[i].y == node.y)
            {
                list[i] = node;
            }
        }
        return;
    }

    static private List<GridNode> RetracePath(GridPathfinderNode startNode, GridPathfinderNode endNode)
    {
        List<GridNode> path = new List<GridNode>();
        GridPathfinderNode currentNode = endNode;

        while (currentNode != null)
        {
            if (currentNode.node == null)
            {
                break;
            }
            path.Add(currentNode.node);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    static private bool ListContainsNode(List<GridPathfinderNode> path, GridPathfinderNode node)
    {
        foreach (GridPathfinderNode pnode in path)
        {
            if (pnode.x == node.x && pnode.y == node.y)
                return true;
        }

        return false;
    }
}