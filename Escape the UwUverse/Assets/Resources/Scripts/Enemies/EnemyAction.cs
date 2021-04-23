using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UwUverse
{
    public interface IEnemyAction
    {
        // integer that Enemy Logic class can use for whatever purpose they wish
        public Int32 id
        {
            get;
            set;
        }

        // method run during pre step event
        public void CalculateStep(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target);

        // method run on step event
        public void ExecuteStep(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target);
    }
}
