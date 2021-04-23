using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEntity : MonoBehaviour
{
    protected GridNode m_targetNode;
    protected TileGrid m_gridRef = null;
    protected GridNode m_currentNode = null;

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
}