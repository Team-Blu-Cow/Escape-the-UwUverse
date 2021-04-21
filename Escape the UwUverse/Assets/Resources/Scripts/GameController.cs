using UnityEngine;

public class GameController : MonoBehaviour
{
    // make game controller a singleton
    private static GameController _Instance;

    [SerializeField]
    private Camera m_camera;

    public Camera camera
    { get { return m_camera; } }

    public Cinemachine.CinemachineVirtualCamera vCamera
    { get { return m_camera.GetComponent<Cinemachine.CinemachineVirtualCamera>(); } }

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

    //private FileIO.UserData m_userData;

    //public FileIO.UserData userData
    //{
    //    get { return m_userData; }
    //    set { m_userData = value; }
    //}

    // Start is called before the first frame update
    private void Start()
    {
        //m_applicationPath = Application.persistentDataPath;
        //m_userData = new FileIO.UserData();
    }
}