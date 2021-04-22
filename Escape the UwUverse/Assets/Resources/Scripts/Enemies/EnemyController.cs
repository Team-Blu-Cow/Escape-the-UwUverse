using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UwUverse
{
    public class EnemyController : GridEntity
    {
        [SerializeField] public EnemyLogic m_brain;

        private TileGrid m_gridRef;

        private void Awake()
        {
            m_gridRef = GameObject.Find("Grid").GetComponent<TileGrid>();

            //Player.m_PlayerMoved += OnStep;
            GameController.StepController().StepEvent += OnStep;
            GameController.StepController().PreStepEvent += OnBeginStep;
        }

        private void Start()
        {
            m_brain.Initialise();

            if (m_brain.path.Length > 0)
            {
                transform.position = m_gridRef.GridCoordToWorldCoord(m_gridRef.GetNearestNode(m_brain.path[0]).position);
                m_brain.currentNode = m_gridRef.GetNearestNode(transform.position);
            }
            else
            {
                m_brain.currentNode = m_gridRef.GetNearestNode(transform.position);
                transform.position = m_gridRef.GridCoordToWorldCoord(m_brain.position);
            }

            m_brain.currentNode.AddObject(gameObject);
            m_brain.m_targetPosition = transform.position;

            //GameController.Instance.stepController.AddEntity();
            GameController.StepController().AddEntity();
        }

        private void Update()
        {
        }

        public void OnBeginStep()
        {
            m_brain.PreStep();
        }

        public void OnStep()
        {
            m_brain.Step();
        }

        public void OnDestroy()
        {
            m_brain.currentNode.RemoveObject(gameObject);
            //Player.m_PlayerMoved -= OnStep;
            GameController.StepController().StepEvent -= OnStep;
            GameController.StepController().PreStepEvent -= OnBeginStep;
            //GameController.Instance.stepController.RemoveEntity();
            GameController.StepController().RemoveEntity();
        }
    }
}