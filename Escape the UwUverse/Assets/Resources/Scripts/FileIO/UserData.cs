using UnityEngine;

namespace FileIO
{
    public class UserData
    {
        // container that will be serialized and written to disk
        [System.Serializable]
        protected class DiskUserData
        {
            public SoundSettings m_soundSettings = null;
            public LevelData m_levelData = null;
        }

        private DiskUserData m_userData = null;
        private FileLoader<DiskUserData> m_userFile;

        public SoundSettings soundSettings
        {
            get { return m_userData.m_soundSettings; }
            set { m_userData.m_soundSettings = value; }
        }

        public LevelData m_levelData
        {
            get { return m_userData.m_levelData; }
            set { m_userData.m_levelData = value; }
        }

        // read data from disk if available
        // otherwise generate new data
        public UserData()
        {
            // file extension uud (uwu user data)
            m_userFile = new FileLoader<DiskUserData>(GameController.Instance.applicationPath + "/savedata/userdata.uud");

            m_userFile.CreateDirectory(GameController.Instance.applicationPath + "/savedata/");
            if (m_userFile.FileExists())
            {
                if (!m_userFile.ReadData(out m_userData))
                {
                    Debug.Log("failed to read userdata from disk");
                }
            }
            else
            {
                GenerateNewData();
            }
        }

        //private bool ReadFromDisk()
        //{
        //    return m_userFile.ReadData(out m_userData);
        //}

        public bool WriteToDisk()
        {
            bool r1 = m_userFile.WriteData(m_userData);
            return r1;
        }

        private bool GenerateNewData()
        {
            m_userData = new DiskUserData();

            // sets default volume
            m_userData.m_soundSettings = new SoundSettings();
            m_userData.m_levelData = new LevelData();

            return m_userFile.WriteData(m_userData);
        }

        public void ClearAllData()
        {
            m_userFile.DestroyDirectory(GameController.Instance.applicationPath + "/savedata/leveldata/");
            GenerateNewData();
        }
    }
}