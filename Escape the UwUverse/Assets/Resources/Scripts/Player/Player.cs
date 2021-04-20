using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static event Action m_PlayerMoved;

    [SerializeField] private TileGrid in_grid;

    private GridNode m_currentNode;
    private Vector2Int m_gridPosition = Vector2Int.zero;
    private MasterInput m_input;

    private void Awake()
    {
        m_input = new MasterInput();
        m_input.PlayerMovement.Move.performed += ctx => Move(new Vector2Int((int)ctx.ReadValue<Vector2>().x, (int)ctx.ReadValue<Vector2>().y));
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
            m_gridPosition = targetNode.position;
            m_currentNode = targetNode;
            m_PlayerMoved?.Invoke();
        }
    }
}