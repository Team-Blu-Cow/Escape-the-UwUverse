using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [HideInInspector] public Vector2Int m_direction = Vector2Int.zero;
    [HideInInspector] public TileGrid in_grid = null;
    [HideInInspector] public GridNode m_currentNode = null;
    [HideInInspector] public bool m_hasMoved = false;

    public void createBullet(GridNode in_node, Vector2Int in_direction)
    {
        m_currentNode = in_node;
        m_currentNode.AddObject(gameObject);
        m_direction = in_direction;
        transform.position = in_grid.GridCoordToWorldCoord(m_currentNode.position);
    }

    private void Awake()
    {
        //Player.m_PlayerMoved += Move;
        GameController.StepController().PreStepEvent    += BeginStep;
        GameController.StepController().StepEvent       += Move;
        in_grid = GameObject.Find("Grid").GetComponent<TileGrid>();

        GameController.StepController().AddEntity();
    }

    public void BeginStep()
    {
        GameController.StepController().ApplyMove();
    }

    public void Move()
    {
        GridNode targetNode = m_currentNode.GetNeighbour(m_direction);

        if (targetNode != null && !targetNode.isWall)
        {
            m_currentNode.RemoveObject(gameObject);
            m_currentNode = targetNode;
            m_currentNode.AddObject(gameObject);

            m_hasMoved = true;
            LeanTween.move(gameObject, in_grid.GridCoordToWorldCoord(m_currentNode.position), 0.1f);
        }
        else
        {
            BulletDestroy();
        }
    }

    public void BulletDestroy()
    {
        m_currentNode.RemoveObject(gameObject);
        Debug.Log("bullet destroyed");
        //Player.m_PlayerMoved -= Move;
        GameController.StepController().PreStepEvent    -= BeginStep;
        GameController.StepController().StepEvent       -= Move;
        GameController.StepController().AddEntity();
        Destroy(gameObject);
    }
}