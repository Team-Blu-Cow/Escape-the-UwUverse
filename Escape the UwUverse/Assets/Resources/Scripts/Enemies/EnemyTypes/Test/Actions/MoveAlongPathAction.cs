using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UwUverse
{
    public class MoveAlongPathAction : IEnemyAction
    {
        private Int32 m_id;
        public Int32 id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        public void CalculateStep()
        {

        }

        public void ExecuteStep(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target)
        {
            if (me.position == me.currentNode.m_grid.GetNearestNode(me.path[(me.m_currentPathNode + 1) % me.path.Length]).position)
            {
                me.m_currentPathNode = (me.m_currentPathNode + 1) % me.path.Length;
            }

            float dirX = me.path[(me.m_currentPathNode + 1) % me.path.Length].x - me.path[me.m_currentPathNode % me.path.Length].x;
            float dirY = me.path[(me.m_currentPathNode + 1) % me.path.Length].y - me.path[me.m_currentPathNode % me.path.Length].y;

            Vector2 dir = (Mathf.Abs(dirX) > Mathf.Abs(dirY)) ? new Vector2(Mathf.Sign(dirX), 0) : new Vector2(0, Mathf.Sign(dirY));

            cur_node = me.currentNode;
            tar_node = cur_node.GetNeighbour(Vector2Int.RoundToInt(dir));

            me.currentNode.RemoveObject(me.gameObject);
            me.currentNode = tar_node;
            me.m_targetPosition = tar_node.worldPosition;
            me.currentNode.AddObject(me.gameObject);

            me.gameObject.GetComponent<EnemyController>().StartCoroutine(SmoothMove(0.2f, me));
        }

        public IEnumerator SmoothMove(float duration, EnemyLogic me)
        {
            float n = 0;

            duration = (duration <= 0) ? 1 : duration;

            Vector3 startPos = me.transform.position;

            while (true)
            {
                n += Time.deltaTime;

                if (n >= duration)
                    break;

                float m = JaysUnityUtils.JUtils.SmoothStep(0, duration, n);

                me.transform.position = Vector3.Lerp(startPos, me.m_targetPosition, m);

                yield return null;
            }
        }
    }
}
