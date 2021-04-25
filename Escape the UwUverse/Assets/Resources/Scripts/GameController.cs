using UnityEngine;
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

    private struct Level
    {
        public bool complete;
        public int number;
    }

    private Level[] m_levels;
    [SerializeField] private int m_levelAmount;

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

    public static GameController Instance
    {
        get
        {
            return _Instance;
        }
    }

    private void Awake()
    {
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

    public void SwitchScene(string in_scene)
    {
        m_levelLoader.SwitchScene(in_scene);
        m_levelLoader = FindObjectOfType<LevelLoader>();
    }

    public void SwitchLevel(int in_level)
    {
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