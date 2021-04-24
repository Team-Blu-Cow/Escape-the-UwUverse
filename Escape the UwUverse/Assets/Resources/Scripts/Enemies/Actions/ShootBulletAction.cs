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

        private Vector2Int m_direction;
        private bool m_canShoot;

        public void CalculateStep(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target)
        {
            m_direction = Vector2Int.zero;

            GetDirection(me);

            ValidateShoot(me);
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

        private void ValidateShoot(EnemyLogic me)
        {
            if (!me.currentNode.GetNeighbour(m_direction).HasObjectOfType<bullet>())
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
                bullet.GetComponent<bullet>().createBullet(me.currentNode, m_direction);
            }
        }
    }
}