using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UwUverse
{
    public class BlockAction : IEnemyAction
    {
        private Int32 m_id;

        private const int CheckNum = 5;

        public Int32 id
        {
            get { return m_id; }
            set { m_id = value; }
        }
        public void CalculateStep(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target)
        {
            me.direction = Vector2Int.zero;

            ReflectIncoming(me);
        }

        public void ReflectIncoming(EnemyLogic me)
        {
            

            Vector2Int[] offsets = GetOffsets();
            //Vector2Int[] offsets = new Vector2Int[CheckNum];

            //offsets[0] = new Vector2Int( 1, 0);
            //offsets[1] = new Vector2Int(-1, 0);
            //offsets[2] = new Vector2Int( 0, 1);
            //offsets[3] = new Vector2Int( 0,-1);

            GridNode[] nodes = GetNodes(me.currentNode, offsets);
            //GridNode[] nodes = new GridNode[CheckNum];

            //nodes[0] = me.currentNode.GetNeighbour(offsets[0]);
            //nodes[1] = me.currentNode.GetNeighbour(offsets[1]);
            //nodes[2] = me.currentNode.GetNeighbour(offsets[2]);
            //nodes[3] = me.currentNode.GetNeighbour(offsets[3]);

            GameObject[] bullets = new GameObject[CheckNum];

            for (int i = 0; i < CheckNum; i++)
            {
                if (nodes[i].HasObjectOfType<bullet>(ref bullets[i]))
                {
                    if (bullets[i] != null && (bullets[i].GetComponent<bullet>().m_direction == (offsets[i] * -1)))
                    {
                        bullets[i].GetComponent<bullet>().m_direction *= -1;
                    }
                }
            }
        }

        private Vector2Int[] GetOffsets()
        {
            Vector2Int[] offsets = new Vector2Int[CheckNum];

            offsets[0] = new Vector2Int(1, 0);
            offsets[1] = new Vector2Int(-1, 0);
            offsets[2] = new Vector2Int(0, 1);
            offsets[3] = new Vector2Int(0, -1);
            offsets[4] = new Vector2Int(0, 0);

            return offsets;
        }

        private GridNode[] GetNodes(GridNode currentNode, Vector2Int[] offsets)
        {
            GridNode[] nodes = new GridNode[CheckNum];

            nodes[0] = currentNode.GetNeighbour(offsets[0]);
            nodes[1] = currentNode.GetNeighbour(offsets[1]);
            nodes[2] = currentNode.GetNeighbour(offsets[2]);
            nodes[3] = currentNode.GetNeighbour(offsets[3]);
            nodes[4] = currentNode;

            return nodes;
        }

        private Vector2Int[] GetDangerVectors(Vector2Int[] offsets)
        {
            for (int i = 0; i < offsets.Length; i++)
            {
                offsets[i] *= -1;
            }

            return offsets;
        }

        public void ExecuteStep(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target)
        {
            me.StartCoroutine(BlockColourFlash(me.gameObject, 0.1f));
            //MoveAlongPathAction.GetDirection(me);
            //me.controller.CheckForCollisionsStationary(GetNodes(me.currentNode,GetOffsets()),GetDangerVectors(GetOffsets()));
            ReflectIncoming(me);
            //me.CheckTileForDanger();
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
