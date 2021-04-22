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

        private Vector2Int m_direction;

        public void CalculateStep(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target)
        {
            m_direction = Vector2Int.zero;

            // Step 1: get direction of movement
            GetDirection(me);

            //Step 2: check if move is valid
            ValidateMove(me);

            GameController.StepController().ApplyMove();
        }

        private void GetDirection(EnemyLogic me)
        {
            if (me.position == me.currentNode.m_grid.GetNearestNode(me.path[(me.m_currentPathNode + 1) % me.path.Length]).position)
                me.m_currentPathNode = (me.m_currentPathNode + 1) % me.path.Length;

            float dirX = me.path[(me.m_currentPathNode + 1) % me.path.Length].x - me.path[me.m_currentPathNode % me.path.Length].x;
            float dirY = me.path[(me.m_currentPathNode + 1) % me.path.Length].y - me.path[me.m_currentPathNode % me.path.Length].y;

            m_direction = Vector2Int.RoundToInt((Mathf.Abs(dirX) > Mathf.Abs(dirY)) ?
                    new Vector2(Mathf.Sign(dirX), 0) : new Vector2(0, Mathf.Sign(dirY)));
        }

        private void ValidateMove(EnemyLogic me)
        {
            bool moveSafe = true;

            if (me.currentNode.HasObjectOfType<bullet>())
            {
                moveSafe = false;
                return;
            }

            const int CheckNum = 4;

            // get node offsets
            Vector2Int[] offsets = new Vector2Int[CheckNum-1];

            offsets[0] = new Vector2Int(m_direction.x - m_direction.y, m_direction.x + m_direction.y);
            offsets[1] = new Vector2Int(m_direction.x * 2, m_direction.y * 2);
            offsets[2] = new Vector2Int(m_direction.x + m_direction.y, m_direction.y - m_direction.x);

            // get nodes at offsets
            GridNode[] nodes = new GridNode[CheckNum];

            nodes[0] = me.currentNode.GetNeighbour(offsets[0]);
            nodes[1] = me.currentNode.GetNeighbour(offsets[1]);
            nodes[2] = me.currentNode.GetNeighbour(offsets[2]);
            nodes[3] = me.currentNode.GetNeighbour(m_direction);

            // get danger vectors for each node
            Vector2Int[] dangerVectors = new Vector2Int[CheckNum];

            dangerVectors[0] = new Vector2Int(m_direction.y, -m_direction.x);
            dangerVectors[1] = new Vector2Int(-m_direction.x, -m_direction.y);
            dangerVectors[2] = new Vector2Int(-m_direction.y, m_direction.x);
            dangerVectors[3] = dangerVectors[1];

            // check nodes for bullets with certain velocity
            GameObject[] bullets = new GameObject[CheckNum];

            for(int i = 0; i < CheckNum; i++)
            {
                if(nodes[i].HasObjectOfType<bullet>(ref bullets[i]))
                {
                    if(bullets[i] != null && (bullets[i].GetComponent<bullet>().m_direction == dangerVectors[i]))
                    {
                        moveSafe = false;
                        bullets[i].GetComponent<bullet>().BulletDestroy();
                    }
                }
            }

            // determine whether move is safe
            me.m_isDead = !moveSafe;
        }

        public void ExecuteStep(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target)
        {
            cur_node = me.currentNode;
            tar_node = cur_node.GetNeighbour(Vector2Int.RoundToInt(m_direction));

            me.currentNode.RemoveObject(me.gameObject);
            me.currentNode = tar_node;
            me.m_targetPosition = tar_node.worldPosition;
            me.currentNode.AddObject(me.gameObject);

            //me.gameObject.GetComponent<EnemyController>().StartCoroutine(SmoothMove(0.2f, me));
            LeanTween.move(me.gameObject, GameObject.Find("Grid").GetComponent<TileGrid>().GridCoordToWorldCoord(me.currentNode.position), 0.1f);
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
