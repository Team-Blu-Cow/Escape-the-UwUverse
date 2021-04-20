using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGrid : MonoBehaviour
{
    private GridNode[,] m_nodes;

    [SerializeField]
    private Tilemap m_foreground;

    [SerializeField]
    private Tilemap m_background;

    // TODO @matthew - take this value from m_forground
    [SerializeField]
    private int m_sizeX;

    [SerializeField]
    private int m_sizeY;

    private int m_startX = 0;
    private int m_startY = 0;

    public int sizeX
    { get { return m_sizeX; } }

    public int sizeY
    { get { return m_sizeY; } }

    private void Awake()
    {
        m_startX = -(m_sizeX / 2);
        m_startY = -(m_sizeY / 2);
        m_nodes = new GridNode[sizeX, sizeY];

        //set x,y coords
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                m_nodes[x, y] = new GridNode(x, y);
                m_nodes[x, y].m_grid = this;
            }
        }

        // mark tiles in m_foreground as walls
        // adapted from
        // https://gamedev.stackexchange.com/a/150949

        m_foreground.CompressBounds();
        BoundsInt bounds = m_foreground.cellBounds;
        TileBase[] allTiles = m_foreground.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Vector3 worldpos = m_foreground.CellToWorld(new Vector3Int(x, y, 0));

                    GridNode node = GetNearestNode(worldpos.x, worldpos.y);
                    if (node != null)
                    {
                        node.isWall = true;
                    }

                    // Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                }
            }
        }
    }

    public GridNode GetNode(int x, int y)
    {
        if (x >= m_sizeX || y >= m_sizeY)
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
        int ix = (int)(x + 0.5f);
        int iy = (int)(y + 0.5f);

        ix -= m_startX;
        iy -= m_startY;

        return GetNode(ix, iy);
    }
}