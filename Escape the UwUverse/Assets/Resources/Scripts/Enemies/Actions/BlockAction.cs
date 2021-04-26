using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UwUverse
{
    public class BlockAction : IEnemyAction
    {
        private Int32 m_id;

        public Int32 id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        public void CalculateStep(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target)
        {
            me.direction = Vector2Int.zero;

            const int CheckNum = 4;

            Vector2Int[] offsets = new Vector2Int[CheckNum];

            offsets[0] = new Vector2Int( 1, 0);
            offsets[1] = new Vector2Int(-1, 0);
            offsets[2] = new Vector2Int( 0, 1);
            offsets[3] = new Vector2Int( 0,-1);

            GridNode[] nodes = new GridNode[CheckNum];

            nodes[0] = me.currentNode.GetNeighbour(offsets[0]);
            nodes[1] = me.currentNode.GetNeighbour(offsets[1]);
            nodes[2] = me.currentNode.GetNeighbour(offsets[2]);
            nodes[3] = me.currentNode.GetNeighbour(offsets[3]);

            GameObject[] bullets = new GameObject[CheckNum];

            for (int i = 0; i < CheckNum; i++)
            {
                if (nodes[i].HasObjectOfType<bullet>(ref bullets[i]))
                {
                    if (bullets[i] != null && (bullets[i].GetComponent<bullet>().m_direction == (offsets[i]*-1)))
                    {
                        bullets[i].GetComponent<bullet>().m_direction *= -1;
                    }
                }
            }
        }

        public void ExecuteStep(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target)
        {
            me.StartCoroutine(BlockColourFlash(me.gameObject, 0.1f));
            MoveAlongPathAction.GetDirection(me);
        }

        public IEnumerator BlockColourFlash(GameObject meGObj, float time)
        {
            meGObj.GetComponent<SpriteRenderer>().color = Color.blue;

            float timer = 0;

            while (timer < time)
            {
                timer += Time.deltaTime;

                yield return null;

                meGObj.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.blue, Color.white, timer / time);
            }
        }
    }
}
