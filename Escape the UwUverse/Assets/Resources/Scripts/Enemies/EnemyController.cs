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
        }

        private void Start()
        {
            m_brain.Initialise();

            m_brain.currentNode = in_grid.GetNearestNode(transform.position);
            transform.position = in_grid.GridCoordToWorldCoord(m_brain.position);
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
