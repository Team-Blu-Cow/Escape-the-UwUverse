using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UwUverse
{
    public class TestBlockAction : IEnemyAction
    {
        private Int32 m_id;

        public Int32 id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        public void CalculateStep(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target)
        {

        }

        public void ExecuteStep(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target)
        {
            Debug.Log("Block Action");
        }
    }
}
