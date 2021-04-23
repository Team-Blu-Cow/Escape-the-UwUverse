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
        private Transform m_player;

        public override void Initialise(EnemyController in_controller)
        {
            base.Initialise(in_controller);

            state = State.IDLE;

            m_player = GameObject.Find("Player").transform;
            if (m_player == null)
                Debug.LogError("No Player Found");
        }

        public override void SetActions()
        {
            m_actionQueue.Initialise(2);

            m_actionQueue.AddAction((int)State.IDLE, new WaitAction());

            m_actionQueue.AddAction((int)State.FIRING, new MortarPickTargetAction());
            m_actionQueue.AddAction((int)State.FIRING, new WaitAction());
            m_actionQueue.AddAction((int)State.FIRING, new MortarFireAction());
        }

        public override void PreStep()
        {
            if (PlayerInFiringRange())
                state = State.FIRING;

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
            return Vector3.Distance(transform.position, m_player.position) <= attackRadius + 0.25f;
        }

        private void OnDrawGizmos()
        {
            if(drawDangerShapes)
            {
                Gizmos.color = (m_player != null && PlayerInFiringRange()) ? Color.red : Color.cyan;
                Gizmos.DrawWireSphere(transform.position, attackRadius);
            }
        }
    }
}
