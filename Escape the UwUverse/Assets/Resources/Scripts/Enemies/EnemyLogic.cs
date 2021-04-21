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
        //public Vector2 m_currentGridPos;
        //public Vector2 m_nextNodeGridPos;
        //public Vector2Int m_debugVector;


        public Vector2Int position{get { return m_currentNode.position;}}
        public GridNode currentNode 
        {
            get { return m_currentNode; }
            set { m_currentNode = value; }
        }

        [SerializeField] private Vector2[] m_path;
        public int m_currentPathNode = 0;

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

        public void Step()
        {
            m_actionQueue.NextAction().ExecuteAction(null, null, this, null);
        }
    }
}