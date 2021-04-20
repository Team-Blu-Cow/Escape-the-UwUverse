using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UwUverse
{
    public interface IEnemyAction
    {
        public void RunAction(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target);
    }
}
