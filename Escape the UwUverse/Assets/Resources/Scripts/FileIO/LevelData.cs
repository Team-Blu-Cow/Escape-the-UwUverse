namespace FileIO
{
    [System.Serializable]
    public class LevelData
    {
        private System.Int64 m_levelsBits = 0;

        public bool IsLevelCompleted(int level)
        {
            System.Int64 one = 1;
            return (m_levelsBits & (one << level)) != 0;
        }

        public void SetLevelCompleted(int level, bool b)
        {
            System.Int64 one = 1;
            System.Int64 mask = m_levelsBits | (one << level);

            if (b)
            {
                m_levelsBits |= mask;
            }
            else
            {
                m_levelsBits &= ~mask;
            }
        }
    }
}