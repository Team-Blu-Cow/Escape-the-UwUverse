using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UwUverse
{
    public class TestMoveAction : IEnemyAction
    {
        public void RunAction(GridNode cur_node, GridNode tar_node, EnemyLogic me, GameObject target)
        {
            Debug.Log("Move Action");
            me.transform.position += new Vector3(0, 1, 0);
        }
    }
}
