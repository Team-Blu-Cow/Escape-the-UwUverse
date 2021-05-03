using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UwUverse
{
    public class BlockingEnemy : EnemyLogic
    {
        public enum ActionIDS : Int32
        {
            MoveAction  = 1,
            BlockAction = 2
        }

        public override void Initialise(EnemyController in_controller)
        {
            base.Initialise(in_controller);
        }

        public override void SetActions()
        {
            m_actionQueue.Initialise(1);

            m_actionQueue.AddAction(0, new MoveAlongPathAction());
            m_actionQueue.GetAction(0, 0).id = (int)ActionIDS.MoveAction;
            m_actionQueue.AddAction(0, new MoveAlongPathAction());
            m_actionQueue.GetAction(0, 1).id = (int)ActionIDS.MoveAction;
            m_actionQueue.AddAction(0, new MoveAlongPathAction());
            m_actionQueue.GetAction(0, 2).id = (int)ActionIDS.MoveAction;
            m_actionQueue.AddAction(0, new BlockAction());
            m_actionQueue.GetAction(0, 3).id = (int)ActionIDS.BlockAction;
        }

        public override void Step()
        {
            m_currentAction.ExecuteStep(null, null, this, null);

            m_currentAction     = new MoveAlongPathAction();
            m_currentAction.id  = (int)ActionIDS.MoveAction;

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

        public void Update()
        {
            if(m_currentAction != null && m_currentAction.id == (int)ActionIDS.MoveAction)
                CheckTileForDanger();
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

            if(drawDangerShapes)
            {
                int pathNodeIndex = m_currentPathNode;

                if (m_controller != null && currentNode != null && position == currentNode.m_grid.GetNearestNode(path[(m_currentPathNode + 1) % path.Length]).position)
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
                    //Gizmos.DrawWireCube(m_targetPosition + new Vector3(offset.x,offset.y,0), Vector3.one * 0.8f);
                    if(m_controller != null && currentNode != null)
                        Gizmos.DrawWireCube(currentNode.worldPosition + new Vector3(offset.x,offset.y,0), Vector3.one * 0.8f);
                }
            }
        }
#endif
    }
}
