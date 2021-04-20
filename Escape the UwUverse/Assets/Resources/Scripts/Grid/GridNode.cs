using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode
{
    public GridNode(int in_x, int in_y)
    {
        m_posX = in_x;
        m_posY = in_y;
    }

    private List<GameObject> m_objectsInNode = new List<GameObject>();
    private int m_posX = 0;
    private int m_posY = 0;

    public bool m_isWall = false;

    public TileGrid m_grid;

    public GridNode getAbove()
    { return m_grid.GetNode(x, y + 1); }

    public GridNode getBelow()
    { return m_grid.GetNode(x, y - 1); }

    public GridNode getLeft()
    { return m_grid.GetNode(x - 1, y); }

    public GridNode getRight()
    { return m_grid.GetNode(x + 1, y); }

    public GridNode GetNeighbour(int in_x, int in_y)
    { return m_grid.GetNode(x + in_x, y + in_y); }

    public GridNode GetNeighbour(Vector2Int pos)
    { return GetNeighbour(pos.x, pos.y); }

    public bool isWall
    {
        get { return isWall; }
        set { isWall = value; }
    }

    public int x
    {
        get { return m_posX; }
    }

    public int y
    {
        get { return m_posY; }
    }

    public void AddObject(GameObject obj)
    {
        if (!m_objectsInNode.Contains(obj))
        {
            m_objectsInNode.Add(obj);
        }
    }

    public void RemoveObject(GameObject obj)
    {
        m_objectsInNode.Remove(obj);
    }

    public bool HasObject(GameObject obj)
    {
        return m_objectsInNode.Contains(obj);
    }

    public List<GameObject> AllObjects()
    {
        return m_objectsInNode;
    }
}