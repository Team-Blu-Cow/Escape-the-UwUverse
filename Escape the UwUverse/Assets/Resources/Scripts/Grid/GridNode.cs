using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode
{
    public List<GameObject> m_objectsInNode = new List<GameObject>();
    private int m_posX = 0;
    private int m_posY = 0;

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
}