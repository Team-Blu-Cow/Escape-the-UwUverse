using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UwUverse
{
    // ACTION QUEUE STRUCT **************************************************************************************************************************
    // Struct to store all available actions in both a list and queue format
    [SerializeField]
    public struct ActionQueue
    {
        public Queue<IEnemyAction>[] m_actionQueues;
        [SerializeField] public List<IEnemyAction>[] m_actionLists;

        public void Initialise(int numberOfQueues)
        {
            m_actionLists = new List<IEnemyAction>[numberOfQueues];
            
            for (int i = 0; i < numberOfQueues; i++)
            {
                m_actionLists[i] = new List<IEnemyAction>();
            }
        }

        public void AddAction(int listIndex, IEnemyAction action)
        {
            m_actionLists[listIndex].Add(action);
        }

        public IEnemyAction GetAction(int listIndex, int actionIndex)
        {
            return m_actionLists[listIndex][actionIndex];
        }

        public void AssignQueues()
        {
            m_actionQueues = new Queue<IEnemyAction>[m_actionLists.Length];

            for (int i = 0; i < m_actionLists.Length; i++)
            {
                m_actionQueues[i] = new Queue<IEnemyAction>();
                foreach (var item in m_actionLists[i])
                {
                    m_actionQueues[i].Enqueue(item);
                }
            }
        }

        public void ResetQueue(int queueIndex)
        {
            m_actionQueues[queueIndex].Clear();
            foreach (var item in m_actionLists[queueIndex])
            {
                m_actionQueues[queueIndex].Enqueue(item);
            }
        }

        public IEnemyAction NextAction(int queueIndex)
        {
            IEnemyAction action = m_actionQueues[queueIndex].Dequeue();
            m_actionQueues[queueIndex].Enqueue(action);
            return action;
        }
    }

    // ENEMY LOGIC CLASS ****************************************************************************************************************************
    // The base class to represent the AI logic of any enemy in the game
    public class EnemyLogic : MonoBehaviour
    {
        // ***** CLASS MEMBERS *****

        // Debug editor members
#if UNITY_EDITOR
        public bool drawPathGizmo;
        public bool drawDangerShapes;
#endif
        // Action related members
        protected ActionQueue m_actionQueue;
        protected IEnemyAction m_currentAction;

        public ActionQueue actionQueue { get { return m_actionQueue; } }

        // Path related members, getters & setters
        [SerializeField] protected Vector2[] m_path;
        [HideInInspector] public int m_currentPathNode = 0;
        public Vector2[] path
        {
            get { return m_path; }
            set { path = value; }
        }

        // Controller reference and shortcut getters & setters
        protected EnemyController m_controller;
        public EnemyController controller 
        { get { return m_controller; } set { m_controller = value; } }
        public Vector2Int position
        { get { return m_controller.CurrentNode.position; } }
        public GridNode currentNode 
        { get { return m_controller.CurrentNode; }set { m_controller.CurrentNode = value; } }
        
        // death flag
        [HideInInspector] public bool m_isDead = false;

        // ***** VIRTUAL METHODS *****

        // Method to initialize the action queue of a given enemy type
        virtual public void SetActions() 
        {
            m_actionQueue.Initialise(1);
            m_actionQueue.AddAction(0, new WaitAction());
        }

        // Method run on initialization of controller class
        virtual public void Initialise(EnemyController in_controller)
        {
            m_controller = in_controller;
            SetActions();
            m_actionQueue.AssignQueues();
            m_currentPathNode = 0;
        }

        // method run during the pre step phase
        // IMPORTANT: Always Apply the move in the step controller
        // at the end of this method. behavior will not work otherwise
        virtual public void PreStep()
        {
            m_currentAction = m_actionQueue.NextAction(0);
            m_currentAction.CalculateStep(null, null, this, null);
            GameController.StepController().ApplyMove();
        }

        // method run during the main step
        virtual public void Step()
        {
            // do this at the end of the step
            m_currentAction.ExecuteStep(null, null, this, null);
        }

        // method to check if an enemy is dead
        virtual public void CheckIfDead() { }

        // method to determine behavior upon death
        virtual public void KillEnemy() { }
    }
}