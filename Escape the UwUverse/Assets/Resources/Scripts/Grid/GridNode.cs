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

    public TileGrid m_grid;

    private bool m_isWall = false;
    private bool m_isHole = false;

    public GridNode GetNeighbour(int in_x, int in_y)
    { return m_grid.GetNode(x + in_x, y + in_y); }

    public GridNode GetNeighbour(Vector2Int pos)
    { return GetNeighbour(pos.x, pos.y); }

    public bool isWall
    {
        get { return m_isWall; }
        set { m_isWall = value; }
    }

    public bool isHole
    {
        get { return m_isHole; }
        set { m_isHole = value; }
    }

    public int x
    {
        get { return m_posX; }
    }

    public int y
    {
        get { return m_posY; }
    }

    public Vector2Int position
    {
        get { return new Vector2Int(x, y); }
    }

    public Vector3 worldPosition
    { get { return m_grid.GridCoordToWorldCoord(x, y); } }

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

    public bool HasObjectOfType(GameObject obj)
    {
        return m_objectsInNode.Contains(obj);
    }
    public bool HasObjectOfType<T>()
    {
        foreach (GameObject obj in m_objectsInNode)
        {
            if(obj != null && obj.GetComponent<T>() != null)
            {
                return true;
            }
        }
        return false;
    }

    public List<GameObject> AllObjects()
    {
        return m_objectsInNode;
    }
}