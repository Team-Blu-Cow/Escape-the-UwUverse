namespace FileIO
{
    public class SoundSettings
    {
        private float m_masterVolume = 1f;
        private float m_musicVolume = 1f;
        private float m_sfxVolume = 1f;

        public float masterVolume
        {
            get { return m_masterVolume; }
            set { m_masterVolume = value; }
        }

        public float musicVolume
        {
            get { return m_musicVolume; }
            set { m_musicVolume = value; }
        }

        public float sfxVolume
        {
            get { return m_sfxVolume; }
            set { m_sfxVolume = value; }
        }
    }
}