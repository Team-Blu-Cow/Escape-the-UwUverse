using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UwUverse
{
    public class TestEnemy : EnemyLogic
    {
        public override void SetActions()
        {
            m_actionQueue.m_actionList = new IEnemyAction[2];
            m_actionQueue.m_actionList[0] = new TestAttackAction();
            m_actionQueue.m_actionList[1] = new TestMoveAction();
        }
    }
}
