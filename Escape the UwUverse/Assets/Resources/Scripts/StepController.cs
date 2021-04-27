using System.Collections;
using System;
using UnityEngine;

namespace UwUverse
{
    [Serializable]
    public class StepController
    {
        public event Action PreStepEvent;

        public event Action StepEvent;

        public void BeginStep() => PreStepEvent?.Invoke();

        [SerializeField] private int m_entityCount = 0;
        [SerializeField] private int m_entitiesMoved = 0;

        public int entityCount
        { get { return m_entityCount; } }

        public int entitiesMoved
        { get { return m_entitiesMoved; } }

        public void AddEntity() => m_entityCount++;

        public void RemoveEntity() => m_entityCount--;

        public void ResetEntities() => m_entityCount = 0;

        public void ApplyMove() => m_entitiesMoved++;

        public void ClearMoves() => m_entitiesMoved = 0;

        public StepController() => Initialise();

        ~StepController() => PreStepEvent -= PreStep;

        public void Initialise()
        {
            PreStepEvent += PreStep;
            int m_entityCount = 0;
            int m_entitiesMoved = 0;
        }

        public void PreStep()
        {
            GameController.Instance.StartCoroutine(WaitForAllActions());
        }

        public IEnumerator WaitForAllActions()
        {
            while (m_entitiesMoved < m_entityCount)
            {
                yield return null;
            }

            ClearMoves();
            StepEvent?.Invoke();
        }
    }
}