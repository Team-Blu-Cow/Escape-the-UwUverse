using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    private GridNode[,] m_nodes;

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
                m_nodes[x, y].x = x;
                m_nodes[x, y].y = y;
                m_nodes[x, y].m_grid = this;
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

    // very inefficent
    public GridNode FindObjectOnGrid(GameObject obj)
    {
        foreach (GridNode node in m_nodes)
        {
            foreach (GameObject it_obj in node.m_objectsInNode)
            {
                if (obj == it_obj)
                {
                    return node;
                }
            }
        }

        return null;
    }

    public Vector3 GridCoordToWorldCoord(int x, int y)
    {
        return new Vector3(m_startX + x + 0.5f, m_startY + y + 0.5f, 0);
    }
}