using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UwUverse
{
    public class MortarEnemy : EnemyLogic
    {
        public enum State
        {
            IDLE    = 0,
            FIRING  = 1
        }

        public State state;
        public int attackRadius;
        private Player m_player;
        private Vector2Int m_targetNode;

        public bool m_drawGizmos = false;

        private GameObject m_targetIndicatorPrefab;

        public Player player { get { return m_player; } }
        public Vector2Int targetNode 
        { get { return m_targetNode; } set { m_targetNode = value; } }

        public TileGrid grid
        { get { return controller.grid; } }

        public override void Initialise(EnemyController in_controller)
        {
            base.Initialise(in_controller);

            m_targetIndicatorPrefab = Resources.Load<GameObject>("Prefabs/Enemies/Misc/MortarTarget");

            state = State.IDLE;

            m_player = GameObject.FindObjectOfType<Player>();
            if (m_player == null)
                Debug.LogError("No Player Found");
        }

        public override void SetActions()
        {
            m_actionQueue.Initialise(2);

            m_actionQueue.AddAction((int)State.IDLE, new WaitAction());

            m_actionQueue.AddAction((int)State.FIRING, new WaitAction());
            m_actionQueue.AddAction((int)State.FIRING, new MortarPickTargetAction());
            m_actionQueue.AddAction((int)State.FIRING, new WaitAction());
            m_actionQueue.AddAction((int)State.FIRING, new MortarFireAction());
        }

        public override void PreStep()
        {
            if (PlayerInFiringRange())
            {
                state = State.FIRING;
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }


            m_currentAction = m_actionQueue.NextAction((int)state);
            m_currentAction.CalculateStep(null, null, this, null);
            GameController.StepController().ApplyMove();
        }

        public override void Step()
        {
            m_currentAction.ExecuteStep(null, null, this, null);
        }

        private bool PlayerInFiringRange()
        {
            return Vector3.Distance(transform.position, m_player.transform.position) <= attackRadius + 0.25f;
        }

        public void CreateIndicators()
        {
            GameObject centreTarget = GameObject.Instantiate(m_targetIndicatorPrefab, grid.GridCoordToWorldCoord(m_targetNode), Quaternion.identity);
            centreTarget.transform.parent = transform;

            GameObject rightTarget = GameObject.Instantiate(m_targetIndicatorPrefab, grid.GetNode(m_targetNode).GetNeighbour(1,0).worldPosition, Quaternion.identity);
            rightTarget.transform.parent = transform;

            GameObject leftTarget = GameObject.Instantiate(m_targetIndicatorPrefab, grid.GetNode(m_targetNode).GetNeighbour(-1, 0).worldPosition, Quaternion.identity);
            leftTarget.transform.parent = transform;

            GameObject upTarget = GameObject.Instantiate(m_targetIndicatorPrefab, grid.GetNode(m_targetNode).GetNeighbour(0, 1).worldPosition, Quaternion.identity);
            upTarget.transform.parent = transform;

            GameObject downTarget = GameObject.Instantiate(m_targetIndicatorPrefab, grid.GetNode(m_targetNode).GetNeighbour(0, -1).worldPosition, Quaternion.identity);
            downTarget.transform.parent = transform;
        }

        private void OnDrawGizmos()
        {
            if (drawDangerShapes)
            {
                Gizmos.color = (m_player != null && PlayerInFiringRange()) ? Color.red : Color.cyan;
                Gizmos.DrawWireSphere(transform.position, attackRadius);


                if (controller != null && state == State.FIRING && m_drawGizmos)
                {
                    Gizmos.color = Color.magenta;
                    Gizmos.DrawWireCube(grid.GridCoordToWorldCoord(m_targetNode), Vector3.one * 0.8f);
                    Gizmos.DrawWireCube(grid.GridCoordToWorldCoord(m_targetNode + new Vector2Int(1, 0)), Vector3.one * 0.8f);
                    Gizmos.DrawWireCube(grid.GridCoordToWorldCoord(m_targetNode + new Vector2Int(-1, 0)), Vector3.one * 0.8f);
                    Gizmos.DrawWireCube(grid.GridCoordToWorldCoord(m_targetNode + new Vector2Int(0, 1)), Vector3.one * 0.8f);
                    Gizmos.DrawWireCube(grid.GridCoordToWorldCoord(m_targetNode + new Vector2Int(0, -1)), Vector3.one * 0.8f);
                }
            }
        }
    }
}
