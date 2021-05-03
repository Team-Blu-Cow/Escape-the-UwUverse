using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UwUverse
{
    [System.Serializable]
    public class LevelStatistics
    {
        [SerializeField] private int m_moves         = 0;
        [SerializeField] private int m_enemiesKilled = 0;
        [SerializeField] private int m_shots         = 0;

        public int moves
        { get { return m_moves; } }
        public int enemiesKilled
        { get { return m_enemiesKilled; } }
        public int shots
        { get { return m_shots; } }

        public void AddMove() => m_moves++;
        public void AddKill() => m_enemiesKilled++;
        public void AddShot() => m_shots++;

        public void Reset()
        {
            m_moves         = 0;
            m_enemiesKilled = 0;
            m_shots         = 0;
        }
    }
}
