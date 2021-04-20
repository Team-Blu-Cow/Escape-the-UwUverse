using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static event Action m_PlayerMoved;

    private TileGrid in_grid;

    private GridNode m_currentNode;
    private Vector2Int m_gridPosition = Vector2Int.zero;
    private Vector2Int m_shotDirection = Vector2Int.zero;
    private MasterInput m_input;

    private bool m_hasShot = false;

    private void Awake()
    {
        in_grid = GameObject.Find("Grid").GetComponent<TileGrid>();

        if (in_grid == null)
        {
            Debug.LogError("No grid could be found in scene, wrong name in hierarchy maybe?");
        }

        m_input = new MasterInput();
        m_input.PlayerMovement.Move.performed += ctx => Move(new Vector2Int((int)ctx.ReadValue<Vector2>().x, (int)ctx.ReadValue<Vector2>().y));
        m_input.PlayerShoot.Direction.performed += ctx => SetShot(new Vector2Int((int)ctx.ReadValue<Vector2>().x, (int)ctx.ReadValue<Vector2>().y));
        m_input.PlayerShoot.Mouse.performed += ctx => SetShot(DirectionFromMouse());
        m_input.PlayerShoot.Undo.performed += ctx => UndoShot();
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_currentNode = in_grid.GetNearestNode(transform.position);
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

    private void Update()
    {
        transform.position = in_grid.GridCoordToWorldCoord(m_gridPosition);
    }

    private void Move(Vector2Int direction)
    {
        GridNode targetNode = m_currentNode.GetNeighbour(direction);

        if (!targetNode.isWall && targetNode != null)
        {
            if (m_hasShot)
            {
                if (m_shotDirection == direction)
                    Shoot(m_currentNode.GetNeighbour(m_shotDirection));
                else
                    Shoot(m_currentNode);

                m_hasShot = false;
            }

            m_gridPosition = targetNode.position;
            m_currentNode = targetNode;

            m_PlayerMoved?.Invoke();
        }
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