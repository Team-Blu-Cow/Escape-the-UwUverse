using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode
{
    private List<GameObject> m_objectsInNode = new List<GameObject>();
    private int m_posX = 0;
    private int m_posY = 0;

    public bool m_isWall = false;

    public TileGrid m_grid;

    public GridNode getAbove
    { get { return m_grid.GetNode(x, y + 1); } }

    public GridNode getBelow
    { get { return m_grid.GetNode(x, y - 1); } }

    public GridNode getLeft
    { get { return m_grid.GetNode(x - 1, y); } }

    public GridNode getRight
    { get { return m_grid.GetNode(x + 1, y); } }

    public bool isWall
    {
        get { return isWall; }
        set { isWall = value; }
    }

    public int x
    {
        get { return m_posX; }
        set { m_posX = value; }
    }

    public int y
    {
        get { return m_posY; }
        set { m_posY = value; }
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