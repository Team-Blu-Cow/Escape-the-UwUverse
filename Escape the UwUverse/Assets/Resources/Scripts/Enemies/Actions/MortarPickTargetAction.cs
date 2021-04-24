using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UwUverse
{
    public class MortarPickTargetAction : IEnemyAction
    {
        private Int32 m_id;
        public Int32 id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        public void CalculateStep(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target)
        {
            MortarEnemy Me = (MortarEnemy)me;

            Me.targetNode = Me.player.CurrentNode.position;
        }

        public void ExecuteStep(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target)
        {
            MortarEnemy Me = (MortarEnemy)me;

            Me.CreateIndicators();
            Me.m_drawGizmos = true;
        }
    }
}

