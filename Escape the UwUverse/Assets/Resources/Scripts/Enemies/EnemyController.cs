using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UwUverse
{
    public class EnemyController : GridEntity
    {
        [SerializeField] public EnemyLogic m_brain;

        public int hp
        {
            get { return m_brain.hp; }
            set { m_brain.hp = value; }
        }


        private void Awake()
        {
            m_gridRef = GameObject.Find("Grid").GetComponent<TileGrid>();
            m_brain = gameObject.GetComponent<EnemyLogic>();
        }

        private void Start()
        {
            GameController.StepController().StepEvent += OnStep;
            GameController.StepController().PreStepEvent += OnBeginStep;

            m_brain.Initialise(this);

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

            GameController.StepController().AddEntity();
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
            if (GameController.Instance.stepController != null)
            {
                GameController.StepController().StepEvent -= OnStep;
                GameController.StepController().PreStepEvent -= OnBeginStep;
                GameController.StepController().RemoveEntity();
            }
        }

        public override void Hit(GameObject obj, int damage)
        {
            m_brain.hp -= damage;
        }
    }
}