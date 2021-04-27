using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : GridEntity
{
    [HideInInspector] public Vector2Int m_direction = Vector2Int.zero;
    [HideInInspector] public bool m_hasMoved = false;

    public void createBullet(GridNode in_node, Vector2Int in_direction)
    {
        m_currentNode = in_node;
        m_currentNode.AddObject(gameObject);
        m_direction = in_direction;
        transform.position = m_gridRef.GridCoordToWorldCoord(m_currentNode.position);
        GameController.StepController().PreStepEvent += BeginStep;
        GameController.StepController().StepEvent += Move;
        GameController.StepController().AddEntity();
        GameController.StepController().ApplyMove();
    }

    private void Awake()
    {
        //m_direction = Vector2Int.zero;

        
        m_gridRef = GameObject.Find("Grid").GetComponent<TileGrid>();
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
            LeanTween.move(gameObject, m_gridRef.GridCoordToWorldCoord(m_currentNode.position), 0.1f);
        }
        else
        {
            BulletDestroy();
            //Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        m_currentNode.RemoveObject(gameObject);
        //Debug.Log("bullet destroyed");
        //Player.m_PlayerMoved -= Move;
        //GameController.StepController().AddEntity(); // silly goose @jay :3
        if (GameController.StepController() != null)
        {
            GameController.StepController().PreStepEvent -= BeginStep;
            GameController.StepController().StepEvent -= Move;
            GameController.StepController().RemoveEntity();
        }
    }

    public void BulletDestroy()
    {
        Destroy(gameObject);
    }
}