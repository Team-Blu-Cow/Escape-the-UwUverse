using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UwUverse
{
    public class TestMoveAction : IEnemyAction
    {
        public void ExecuteAction(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target)
        {
            if (me.position == me.currentNode.m_grid.GetNearestNode(me.path[(me.m_currentPathNode + 1) % me.path.Length]).position)
            {
                me.m_currentPathNode = (me.m_currentPathNode + 1) % me.path.Length;
            }

            Vector2 dir = Vector2.zero;

            float dirX = me.path[(me.m_currentPathNode + 1) % me.path.Length].x - me.path[me.m_currentPathNode % me.path.Length].x;
            float dirY = me.path[(me.m_currentPathNode + 1) % me.path.Length].y - me.path[me.m_currentPathNode % me.path.Length].y;

            dir = new Vector2(dirX, dirY).normalized;

            cur_node = me.currentNode;
            tar_node = cur_node.GetNeighbour(Vector2Int.RoundToInt(dir));

            me.currentNode = tar_node;
            me.m_targetPosition = tar_node.worldPosition;

            me.gameObject.GetComponent<EnemyController>().StartCoroutine(SmoothMove(0.4f, me));
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
