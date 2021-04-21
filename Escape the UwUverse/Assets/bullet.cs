using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [HideInInspector] public Vector2Int m_direction = Vector2Int.zero;
    [HideInInspector] public TileGrid in_grid = null;
    [HideInInspector] public GridNode m_currentNode = null;

    public void createBullet(GridNode in_node, Vector2Int in_direction)
    {
        m_currentNode = in_node;
        m_currentNode.AddObject(gameObject);
        m_direction = in_direction;
        transform.position = in_grid.GridCoordToWorldCoord(m_currentNode.position);
    }

    private void Awake()
    {
        Player.m_PlayerMoved += Move;
        in_grid = GameObject.Find("Grid").GetComponent<TileGrid>();
    }

    private void Move()
    {
        GridNode targetNode = m_currentNode.GetNeighbour(m_direction);

        m_currentNode.RemoveObject(gameObject);
        if (targetNode != null && !targetNode.isWall)
        {
            m_currentNode = targetNode;
            m_currentNode.AddObject(gameObject);

            LeanTween.move(gameObject, in_grid.GridCoordToWorldCoord(m_currentNode.position), 0.1f);
        }
        else
        {
            Debug.Log("bullet destroyed");
            Player.m_PlayerMoved -= Move;
            Destroy(gameObject);
        }
    }
}