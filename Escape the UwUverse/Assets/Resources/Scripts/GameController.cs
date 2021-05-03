using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UwUverse;

public class GameController : MonoBehaviour
{
    // make game controller a singleton
    private static GameController _Instance;

    private FileIO.UserData m_userData = null;

    public FileIO.UserData userData
    { get { return m_userData; } }

    [SerializeField]
    private Camera m_camera;

    private LevelLoader m_levelLoader;

    public LevelLoader LevelLoader
    {
        get { return m_levelLoader; }
        set { m_levelLoader = value; }
    }

    private struct Level
    {
        public bool complete;
        public int number;
    }

    private Level[] m_levels;
    [SerializeField] private int m_levelAmount;

    public int currentLevel = 0;

    public Camera camera
    {
        get
        {
            if (m_camera != null)
                return m_camera;
            m_camera = GameObject.Find("Camera").GetComponent<Camera>();
            return m_camera;
        }
    }

    public Cinemachine.CinemachineVirtualCamera vCamera
    { get { return camera.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>(); } }

    [SerializeField] private StepController m_stepController;
    public StepController stepController
    { get { return m_stepController; } }
    public static StepController StepController()
    { return Instance.stepController; }

    [SerializeField] private LevelStatistics m_levelStatistics;
    public LevelStatistics levelStatistics
    { get { return m_levelStatistics; } }
    public static LevelStatistics LevelStats()
    { return Instance.levelStatistics; }
    

    public static GameController Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new GameController();
            }
            return _Instance;
        }
    }

    public bool isPaused;

    private void Awake()
    {
        isPaused = false;

        if (_Instance != null && _Instance != this)
            Destroy(this.gameObject);
        else
        {
            _Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        m_camera = GameObject.Find("Camera").GetComponent<Camera>();
        m_levelLoader = FindObjectOfType<LevelLoader>();

        m_stepController = new StepController();
        m_userData = new FileIO.UserData();
        m_levels = new Level[m_levelAmount];

        for (int i = 0; i < m_levelAmount; i++)
        {
            m_levels[i].complete = false;
            m_levels[i].number = i + 1;
        }
    }

    private string m_applicationPath = null;

    public string applicationPath
    {
        get
        {
            if (m_applicationPath == null)
            {
                m_applicationPath = Application.persistentDataPath;
            }
            return m_applicationPath;
        }
    }

    static public string ReadFile(string fileName)
    {
        var sr = new StreamReader(Application.dataPath + "/" + fileName);
        var fileContents = sr.ReadToEnd();
        sr.Close();

        return fileContents;
    }

    public void SwitchScene(string in_scene)
    {
        m_levelLoader.SwitchScene(in_scene);
    }

    public void SwitchLevel(int in_level)
    {
        currentLevel = in_level;
        m_levelLoader.SwitchScene("Level-" + in_level);
    }

    public void SetLevelComplete(int in_level)
    {
        m_levels[in_level].complete = true;
    }

    public bool GetLevelComplete(int in_level)
    {
        return m_levels[in_level].complete;
    }

    public int GetLevelAmount()
    {
        return m_levelAmount;
    }
}