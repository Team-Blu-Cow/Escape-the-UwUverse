using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UwUverse
{
    public class RangedEnemy : EnemyLogic
    {

        public enum ActionIDS : Int32 
        {
            MoveAction = 1,
            ShootAction = 2
        }

        public override void Initialise(EnemyController in_controller)
        {
            base.Initialise(in_controller);
        }

        public override void SetActions()
        {
            m_actionQueue.Initialise(1);

            m_actionQueue.AddAction(0, new MoveAlongPathAction());
            m_actionQueue.AddAction(0, new MoveAlongPathAction());
            m_actionQueue.AddAction(0, new MoveAlongPathAction());
            m_actionQueue.AddAction(0, new ShootBulletAction());
        }

        public override void Step()
        {
            m_currentAction.ExecuteStep(null, null, this, null);
            CheckIfDead();
        }

        public override void CheckIfDead()
        {
            if(m_isDead)
            {
                Destroy(gameObject);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(drawPathGizmo)
            {
                Gizmos.color = Color.red;
                for(int i = 1; i < m_path.Length + 1; i++)
                {
                    if (i == 1)
                    {
                        Gizmos.DrawSphere(m_path[i - 1] - (Vector2.one * 0.5f), 0.1f);
                    }
                    else
                    {
                        Gizmos.DrawWireSphere(m_path[i - 1] - (Vector2.one * 0.5f), 0.1f);
                    }

                    Gizmos.DrawLine(m_path[(i - 1) % m_path.Length] - (Vector2.one * 0.5f), m_path[i % m_path.Length] - (Vector2.one * 0.5f));
                }
            }            
        }
#endif
    }
}