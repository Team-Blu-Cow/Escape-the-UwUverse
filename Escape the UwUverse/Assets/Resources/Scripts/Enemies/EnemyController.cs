using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UwUverse
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] public EnemyLogic m_brain;

        TileGrid in_grid;

        private void Awake()
        {
            in_grid = GameObject.Find("Grid").GetComponent<TileGrid>();

            Player.m_PlayerMoved += OnStep;
        }

        private void Start()
        {
            m_brain.Initialise();

            

            if (m_brain.path.Length > 0)
            {
                transform.position = in_grid.GridCoordToWorldCoord(in_grid.GetNearestNode(m_brain.path[0]).position);
                m_brain.currentNode = in_grid.GetNearestNode(transform.position);
            }
            else
            {
                m_brain.currentNode = in_grid.GetNearestNode(transform.position);
                transform.position = in_grid.GridCoordToWorldCoord(m_brain.position);
            }
            m_brain.m_targetPosition = transform.position;
        }

        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                OnStep();
            }
        }

        public void OnStep()
        {
            m_brain.Step();
        }


    }
}
