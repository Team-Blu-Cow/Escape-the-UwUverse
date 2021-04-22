using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UwUverse
{
    public class TestEnemy : EnemyLogic
    {
        public enum ActionIDS : Int32
        {
            MoveAction = 1,
            BlockAction = 2
        }

        public override void Initialise()
        {
            base.Initialise();

            //m_path = new EnemyPath();
        }

        public override void SetActions()
        {
            m_actionQueue.m_actionList = new IEnemyAction[3];
            m_actionQueue.m_actionList[0] = new MoveAlongPathAction();
            m_actionQueue.m_actionList[0].id = (Int32)ActionIDS.MoveAction;
            m_actionQueue.m_actionList[1] = new MoveAlongPathAction();
            m_actionQueue.m_actionList[1].id = (Int32)ActionIDS.MoveAction;
            m_actionQueue.m_actionList[2] = new MoveAlongPathAction();
            m_actionQueue.m_actionList[2].id = (Int32)ActionIDS.MoveAction;
            //m_actionQueue.m_actionList[3] = new TestBlockAction();
            //m_actionQueue.m_actionList[3].id = (Int32)ActionIDS.BlockAction;
        }

        public override void Step()
        {
            IEnemyAction action = m_actionQueue.NextAction();

            action.ExecuteStep(null, null, this, null);

            if (action.id != (Int32)ActionIDS.BlockAction)
                CheckIfDead();
            //base.Step();
        }

        public override void CheckIfDead()
        {
            // steps:

            // 1: check current node
            // 2: check node that is being moved to for a bullet
        }
    }
}
