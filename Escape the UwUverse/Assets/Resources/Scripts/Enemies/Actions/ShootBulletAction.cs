using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UwUverse
{
    public class ShootBulletAction : IEnemyAction
    {
        private Int32 m_id;
        public Int32 id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        private bool m_canShoot;

        public void CalculateStep(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target)
        {
            me.direction = Vector2Int.zero;

            GetDirection(me);

            CheckCollision(me);

            ValidateShoot(me);
        }

        private void GetDirection(EnemyLogic me)
        {
            if (me.position == me.currentNode.m_grid.GetNearestNode(me.path[(me.m_currentPathNode + 1) % me.path.Length]).position)
                me.m_currentPathNode = (me.m_currentPathNode + 1) % me.path.Length;

            float dirX = me.path[(me.m_currentPathNode + 1) % me.path.Length].x - me.path[me.m_currentPathNode % me.path.Length].x;
            float dirY = me.path[(me.m_currentPathNode + 1) % me.path.Length].y - me.path[me.m_currentPathNode % me.path.Length].y;

            me.direction = Vector2Int.RoundToInt((Mathf.Abs(dirX) > Mathf.Abs(dirY)) ?
                    new Vector2(Mathf.Sign(dirX), 0) : new Vector2(0, Mathf.Sign(dirY)));
        }

        private void CheckCollision(EnemyLogic me)
        {
            const int CheckNum = 4;

            Vector2Int[] offsets = new Vector2Int[CheckNum];

            offsets[0] = new Vector2Int(1, 0);
            offsets[1] = new Vector2Int(-1, 0);
            offsets[2] = new Vector2Int(0, 1);
            offsets[3] = new Vector2Int(0, -1);

            GridNode[] nodes = new GridNode[CheckNum];

            nodes[0] = me.currentNode.GetNeighbour(offsets[0]);
            nodes[1] = me.currentNode.GetNeighbour(offsets[1]);
            nodes[2] = me.currentNode.GetNeighbour(offsets[2]);
            nodes[3] = me.currentNode.GetNeighbour(offsets[3]);

            GameObject[] bullets = new GameObject[CheckNum];

            for (int i = 0; i < CheckNum; i++)
            {
                if (nodes[i] != null && nodes[i].HasObjectOfType<bullet>(ref bullets[i]))
                {
                    if (bullets[i] != null && (bullets[i].GetComponent<bullet>().m_direction == (offsets[i] * -1)))
                    {
                        me.m_isDead = true;
                        bullets[i].GetComponent<bullet>().BulletDestroy();                       
                    }
                }
            }
        }

        private void ValidateShoot(EnemyLogic me)
        {
            if (!me.currentNode.GetNeighbour(me.direction).HasObjectOfType<bullet>())
            {
                m_canShoot = true;
            }
            else
            {
                m_canShoot = false;
            }
        }

        public void ExecuteStep(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target)
        {
            if(m_canShoot == true)
            {
                GameObject bullet = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/bullet"), Vector3.zero, Quaternion.identity);
                bullet.GetComponent<bullet>().createBullet(me.currentNode.GetNeighbour(me.direction), me.direction);
            }
        }
    }
}