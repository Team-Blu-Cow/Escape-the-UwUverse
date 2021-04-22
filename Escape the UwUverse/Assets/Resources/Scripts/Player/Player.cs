using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UwUverse;

public class Player : MonoBehaviour
{
    private TileGrid in_grid;

    private GridNode m_currentNode;
    private GridNode m_targetNode;
    private Vector2Int m_gridPosition = Vector2Int.zero;
    private Vector2Int m_shotDirection = Vector2Int.zero;
    private MasterInput m_input;
    private Vector2Int m_inDirection;

    private bool m_hasShot = false;

    private void Awake()
    {
        in_grid = GameObject.Find("Grid").GetComponent<TileGrid>();

        if (in_grid == null)
        {
            Debug.LogError("No grid could be found in scene, wrong name in hierarchy maybe?");
        }

        m_input = new MasterInput();
        m_input.PlayerMovement.Move.performed += ctx => BeginStep(new Vector2Int((int)ctx.ReadValue<Vector2>().x, (int)ctx.ReadValue<Vector2>().y));//Move(new Vector2Int((int)ctx.ReadValue<Vector2>().x, (int)ctx.ReadValue<Vector2>().y));
        m_input.PlayerMovement.Idle.performed += ctx => BeginStep(Vector2Int.zero);
        m_input.PlayerShoot.Direction.performed += ctx => SetShot(new Vector2Int((int)ctx.ReadValue<Vector2>().x, (int)ctx.ReadValue<Vector2>().y));
        m_input.PlayerShoot.Mouse.performed += ctx => SetShot(DirectionFromMouse());
        m_input.PlayerShoot.Undo.performed += ctx => UndoShot();
    }

    private void OnDestroy()
    {
        GameController.StepController().RemoveEntity();
        GameController.StepController().StepEvent -= Move;
    }

    // Start is called before the first frame update
    private void Start()
    {
        GameController.StepController().StepEvent += Move;
        GameController.StepController().AddEntity();

        m_currentNode = in_grid.GetNearestNode(transform.position);
        m_currentNode.AddObject(gameObject);
        m_gridPosition = m_currentNode.position;
    }

    private void OnEnable()
    {
        m_input.Enable();
    }

    private void OnDisable()
    {
        m_input.Disable();
    }

    public void BeginStep(Vector2Int direction)
    {
        m_targetNode = m_currentNode.GetNeighbour(direction);

        if (m_targetNode != null && !m_targetNode.isWall && !m_targetNode.isHole)
        {
            if (m_hasShot)
            {
                // shoot if: node had no bullets already, wont be occupied by the player or a wall next step

                if (m_shotDirection == direction && !m_currentNode.GetNeighbour(m_shotDirection).HasObjectOfType<bullet>())  // TODO @me:
                    Shoot(m_currentNode.GetNeighbour(m_shotDirection));                                             //   fix this <3
                else if (!m_currentNode.GetNeighbour(m_shotDirection).HasObjectOfType<bullet>() && !m_currentNode.HasObjectOfType<bullet>())                     //  its broken
                    Shoot(m_currentNode);                                                                              //       thanks

                m_hasShot = false;
            }

            GameController.StepController().ApplyMove();
            GameController.Instance.stepController.BeginStep();
        }
    }

    private void Move()
    {
        m_currentNode.RemoveObject(gameObject);
        m_gridPosition = m_targetNode.position;
        m_currentNode = m_targetNode;
        m_currentNode.AddObject(gameObject);
        LeanTween.move(gameObject, in_grid.GridCoordToWorldCoord(m_currentNode.position), 0.1f);
    }

    private void SetShot(Vector2Int in_direction)
    {
        if (!m_hasShot)
            m_hasShot = true;

        m_shotDirection = in_direction;
    }

    private void UndoShot()
    {
        m_hasShot = false;
        m_shotDirection = Vector2Int.zero;
    }

    private void Shoot(GridNode in_Node)
    {
        GameObject bullet = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/bullet"), Vector3.zero, Quaternion.identity);
        bullet.GetComponent<bullet>().createBullet(in_Node, m_shotDirection);
    }

    private Vector2Int DirectionFromMouse()
    {
        Vector2 directionf = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
        directionf.Normalize();
        Vector2Int directioni = Vector2Int.RoundToInt(directionf);
        return directioni;
    }
}