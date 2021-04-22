using UnityEngine;
using UwUverse;

public class GameController : MonoBehaviour
{
    // make game controller a singleton
    private static GameController _Instance;

    [SerializeField]
    private Camera m_camera;

    public Camera camera
    { get { return m_camera; } }

    public Cinemachine.CinemachineVirtualCamera vCamera
    { get { return m_camera.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>(); } }

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

        m_stepController = new StepController();
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
}