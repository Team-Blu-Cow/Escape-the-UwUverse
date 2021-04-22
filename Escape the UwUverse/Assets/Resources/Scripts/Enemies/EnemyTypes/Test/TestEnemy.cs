using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UwUverse
{
    public class TestEnemy : EnemyLogic
    {
        public enum ActionIDS : Int32
        {
            MoveAction = 1,
            BlockAction = 2
        }

        public override void Initialise()
        {
            base.Initialise();

            //m_path = new EnemyPath();
        }

        public override void SetActions()
        {
            m_actionQueue.m_actionList = new IEnemyAction[3];
            m_actionQueue.m_actionList[0] = new MoveAlongPathAction();
            m_actionQueue.m_actionList[0].id = (Int32)ActionIDS.MoveAction;
            m_actionQueue.m_actionList[1] = new MoveAlongPathAction();
            m_actionQueue.m_actionList[1].id = (Int32)ActionIDS.MoveAction;
            m_actionQueue.m_actionList[2] = new MoveAlongPathAction();
            m_actionQueue.m_actionList[2].id = (Int32)ActionIDS.MoveAction;
            //m_actionQueue.m_actionList[3] = new TestBlockAction();
            //m_actionQueue.m_actionList[3].id = (Int32)ActionIDS.BlockAction;
        }

        public override void PreStep()
        {
            m_currentAction = m_actionQueue.NextAction();
            m_currentAction.CalculateStep(null, null, this, null);
        }

        public override void Step()
        {
            m_currentAction.ExecuteStep(null, null, this, null);

            //if (m_currentAction.id != (Int32)ActionIDS.BlockAction)
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
            if (drawPathGizmo)
            {
                Gizmos.color = Color.red;
                for (int i = 1; i < m_path.Length + 1; i++)
                {
                    if (i == 1)
                        Gizmos.DrawSphere(m_path[i - 1] - (Vector2.one * 0.5f), 0.1f);
                    else
                        Gizmos.DrawWireSphere(m_path[i - 1] - (Vector2.one * 0.5f), 0.1f);

                    Gizmos.DrawLine(m_path[(i - 1) % m_path.Length] - (Vector2.one * 0.5f), m_path[i % m_path.Length] - (Vector2.one * 0.5f));
                }
            }

            if(drawDangerSquares)
            {
                int pathNodeIndex = m_currentPathNode;

                if (currentNode != null && position == currentNode.m_grid.GetNearestNode(path[(m_currentPathNode + 1) % path.Length]).position)
                    pathNodeIndex = (m_currentPathNode + 1) % path.Length;


                float dirX = path[(pathNodeIndex + 1) % path.Length].x - path[pathNodeIndex % path.Length].x;
                float dirY = path[(pathNodeIndex + 1) % path.Length].y - path[pathNodeIndex % path.Length].y;

                Vector2Int direction = Vector2Int.RoundToInt((Mathf.Abs(dirX) > Mathf.Abs(dirY)) ?
                        new Vector2(Mathf.Sign(dirX), 0) : new Vector2(0, Mathf.Sign(dirY)));

                Vector2Int[] offsets = new Vector2Int[4];

                offsets[0] = new Vector2Int(direction.x - direction.y, direction.x + direction.y);
                offsets[1] = new Vector2Int(direction.x * 2, direction.y * 2);
                offsets[2] = new Vector2Int(direction.x + direction.y, direction.y - direction.x);
                offsets[3] = direction;

                Gizmos.color = Color.red;
                foreach(var offset in offsets)
                {
                    Gizmos.DrawWireCube(m_targetPosition + new Vector3(offset.x,offset.y,0), Vector3.one * 0.8f);
                }
            }
        }
#endif
    }
}
