using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UwUverse
{
    public interface IEnemyAction
    {
        public Int32 id
        {
            get;
            set;
        }


        public void ExecuteAction(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target);
    }
}
