using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEntity : MonoBehaviour
{
    protected GridNode m_targetNode;
    protected TileGrid m_gridRef = null;
    protected GridNode m_currentNode = null;
    protected Vector2Int m_direction = Vector2Int.zero;

    public GridNode CurrentNode
    {
        get { return m_currentNode; }
        set { m_currentNode = value; }
    }

    public GridNode TargetNode
    {
        get { return m_targetNode; }
        set { m_targetNode = value; }
    }

    public Vector2Int Position
    {
        get { return m_currentNode.position; }
    }

    public Vector2Int Direction
    {
        get { return m_direction; }
        set { m_direction = value; }
    }

    public TileGrid grid
    { get { return m_gridRef; } }

    public virtual void Hit(GameObject obj, int damage)
    {
        //Debug.LogWarning("Default hit called!");
        Debug.Log("I've been Hit");
    }
}