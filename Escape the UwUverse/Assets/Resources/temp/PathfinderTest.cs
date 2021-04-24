using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderTest : MonoBehaviour
{
    private GridNode m_start = null;
    private GridNode m_end = null;

    public Vector2Int m_startPoint;
    public Vector2Int m_endPoint;

    private TileGrid m_grid;

    private void Start()
    {
        m_grid = GameObject.Find("Grid").GetComponent<TileGrid>();
    }

    private void FixedUpdate()
    {
        m_start = m_grid.GetNode(m_startPoint);
        m_end = m_grid.GetNode(m_endPoint);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position, 1);
        if (m_grid != null && m_start != null && m_end != null)
        {
            List<GridNode> nodepath = m_grid.FindPath(m_start, m_end);

            if (nodepath != null)
            {
                foreach (GridNode node in nodepath)
                {
                    if (node != null)
                    {
                        Gizmos.DrawWireSphere(node.worldPosition, 1);
                    }
                }
            }
        }
    }
}