using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UwUverse
{
    [SerializeField]
    public struct ActionQueue
    {
        public Queue<IEnemyAction> m_actionQueue;
        [SerializeField] public IEnemyAction[] m_actionList;

        public void AssignQueue()
        {
            m_actionQueue = new Queue<IEnemyAction>();
            foreach(var item in m_actionList)
            {
                m_actionQueue.Enqueue(item);
            }
        }

        public IEnemyAction NextAction()
        {
            IEnemyAction action = m_actionQueue.Dequeue();
            m_actionQueue.Enqueue(action);
            return action;
        }
    }

    public class EnemyLogic : MonoBehaviour
    { 
        [SerializeField] protected ActionQueue m_actionQueue;

        private GridNode m_currentNode;
        public Vector3 m_targetPosition;
        public Vector2Int position{get { return m_currentNode.position;}}
        public GridNode currentNode 
        {
            get { return m_currentNode; }
            set { m_currentNode = value; }
        }

        [SerializeField] private Vector2[] m_path;
        public int m_currentPathNode = 0;
        public bool m_isDead = false;

        private IEnemyAction m_currentAction;

        public Vector2[] path
        {
            get { return m_path; }
            set { path = value; }
        }

        virtual public void SetActions() { }

        virtual public void Initialise()
        {
            SetActions();
            m_actionQueue.AssignQueue();
            m_currentPathNode = 0;
        }

        virtual public void PreStep()
        {
            m_currentAction = m_actionQueue.NextAction();
            m_currentAction.CalculateStep();
        }

        virtual public void Step()
        {
            // do this at the end of the step
            m_currentAction.ExecuteStep(null, null, this, null);
        }

        virtual public void CheckIfDead() { }
        virtual public void KillEnemy() { }
    }
}