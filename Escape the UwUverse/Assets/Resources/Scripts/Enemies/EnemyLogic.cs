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

    // ENEMY LOGIC CLASS ****************************************************************************************************************************
    // The base class to represent the AI logic of any enemy in the game
    public class EnemyLogic : MonoBehaviour
    {
        // ***** CLASS MEMBERS *****

        // Debug editor members
#if UNITY_EDITOR
        public bool drawPathGizmo;
        public bool drawDangerSquares;
#endif
        // Action related members
        protected ActionQueue m_actionQueue;
        protected IEnemyAction m_currentAction;

        // Path related members, getters & setters
        [SerializeField] protected Vector2[] m_path;
        public int m_currentPathNode = 0;
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
        { get { return m_controller.CurrentNode.position;} }
        public GridNode currentNode 
        { get { return m_controller.CurrentNode; }set { m_controller.CurrentNode = value; } }
        
        // death flag
        public bool m_isDead = false;

        public int m_queueIndex = 0;

        // ***** VIRTUAL METHODS *****

        // Method to initialize the action queue of a given enemy type
        virtual public void SetActions() { }

        // Method run on initialization of controller class
        virtual public void Initialise(EnemyController in_controller)
        {
            m_controller = in_controller;
            SetActions();
            m_actionQueue.AssignQueue();
            m_currentPathNode = 0;
        }

        // method run during the pre step phase
        // IMPORTANT: Always Apply the move in the step controller
        // at the end of this method. behavior will not work otherwise
        virtual public void PreStep()
        {
            m_currentAction = m_actionQueue.NextAction();
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