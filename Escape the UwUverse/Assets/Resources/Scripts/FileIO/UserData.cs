using UnityEngine;

public class UserData
{
    // container that will be serialized and written to disk
    [System.Serializable]
    protected class DiskUserData
    {
        public float m_masterVolume = 1f;
        public float m_musicVolume = 1f;
        public float m_sfxVolume = 1f;
    }

    private DiskUserData m_userData = null;
    private FileLoader<DiskUserData> m_userFile;

    public float masterVolume
    {
        get { return m_userData.m_masterVolume; }
        set { m_userData.m_masterVolume = value; }
    }

    public float musicVolume
    {
        get { return m_userData.m_musicVolume; }
        set { m_userData.m_musicVolume = value; }
    }

    public float sfxVolume
    {
        get { return m_userData.m_sfxVolume; }
        set { m_userData.m_sfxVolume = value; }
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
        m_userData.m_masterVolume = 0.5f;
        m_userData.m_musicVolume = 0.5f;
        m_userData.m_sfxVolume = 0.5f;

        return m_userFile.WriteData(m_userData);
    }

    public void ClearAllData()
    {
        m_userFile.DestroyDirectory(GameController.Instance.applicationPath + "/savedata/leveldata/");
        GenerateNewData();
    }
}