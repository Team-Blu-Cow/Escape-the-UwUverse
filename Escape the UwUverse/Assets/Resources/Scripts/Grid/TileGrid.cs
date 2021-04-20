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

                    GridNode node = GetNearestNode(worldpos.x + 1f, worldpos.y - 1f);
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
}