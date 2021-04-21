using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UwUverse
{
    public class TestEnemy : EnemyLogic
    {
        public override void Initialise()
        {
            base.Initialise();

            //m_path = new EnemyPath();
        }

        public override void SetActions()
        {
            m_actionQueue.m_actionList = new IEnemyAction[4];
            m_actionQueue.m_actionList[0] = new TestMoveAction();
            m_actionQueue.m_actionList[1] = new TestMoveAction();
            m_actionQueue.m_actionList[2] = new TestMoveAction();
            m_actionQueue.m_actionList[3] = new TestAttackAction();
            
        }
    }
}
