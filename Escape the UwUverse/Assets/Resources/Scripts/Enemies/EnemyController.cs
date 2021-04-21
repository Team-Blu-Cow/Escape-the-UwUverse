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

            //Player.m_PlayerMoved += OnStep;
            GameController.StepController().StepEvent       += OnStep;
            GameController.StepController().PreStepEvent    += OnBeginStep;
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

            m_brain.currentNode.AddObject(gameObject);
            m_brain.m_targetPosition = transform.position;

            //GameController.Instance.stepController.AddEntity();
            GameController.StepController().AddEntity();

        }

        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                OnStep();
            }
        }

        public void OnBeginStep()
        {
            m_brain.PreStep();
            Debug.Log("beginStep");
            StartCoroutine(DebugWait());
        }

        IEnumerator DebugWait()
        {
            yield return new WaitForSeconds(1f);
            //GameController.Instance.stepController.ApplyMove();
            GameController.StepController().ApplyMove();
        }

        public void OnStep()
        {
            m_brain.Step();
            Debug.Log("Step");
        }

        public void OnDestroy()
        {
            m_brain.currentNode.RemoveObject(gameObject);
            //Player.m_PlayerMoved -= OnStep;
            GameController.StepController().StepEvent       -= OnStep;
            GameController.StepController().PreStepEvent    -= OnBeginStep;
            //GameController.Instance.stepController.RemoveEntity();
            GameController.StepController().RemoveEntity();
        }

    }
}
