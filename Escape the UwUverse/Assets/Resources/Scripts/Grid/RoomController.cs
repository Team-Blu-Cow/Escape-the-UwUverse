using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    private MasterInput m_input;

    private List<GameObject> m_zoomedRooms = new List<GameObject>();
    private List<GameObject> m_unzoomedRooms = new List<GameObject>();

    private GameObject m_closest;
    private GameObject m_player;

    private GameObject camLookPoint = null;

    [SerializeField]
    private bool m_cameraIsZoomed = false;

    [SerializeField]
    private float m_innerZoomLevel = 5;

    [SerializeField]
    private float m_outerZoomLevel = 10;

    [SerializeField]
    private float m_zoomTime = 1f;

    private float m_targetZoomLevel = 10f;

    private void Awake()
    {
        camLookPoint = GameObject.Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
        m_input = new MasterInput();

        m_input.PlayerMovement.ZoomIn.performed += ctx => m_cameraIsZoomed = true;
        m_input.PlayerMovement.ZoomOut.performed += ctx => m_cameraIsZoomed = false;
    }

    private void OnEnable()
    {
        m_input.Enable();
    }

    private void OnDisable()
    {
        m_input.Disable();
    }

    private void OnDestroy()
    {
        GameObject.Destroy(camLookPoint);
    }

    private void Start()
    {
        m_player = GameObject.Find("Player");

        if (m_cameraIsZoomed)
        {
            GameController.Instance.vCamera.m_Lens.OrthographicSize = m_innerZoomLevel;
            m_targetZoomLevel = m_innerZoomLevel;
        }
        else
        {
            GameController.Instance.vCamera.m_Lens.OrthographicSize = m_outerZoomLevel;
            m_targetZoomLevel = m_outerZoomLevel;
        }
    }

    public void AddRoom(GameObject roomObject)
    {
        if (roomObject.GetComponent<Room>().zoomedCamera)
        {
            if (!m_zoomedRooms.Contains(roomObject))
                m_zoomedRooms.Add(roomObject);
        }
        else
        {
            if (!m_unzoomedRooms.Contains(roomObject))
                m_unzoomedRooms.Add(roomObject);
        }
    }

    public void RemoveRoom(GameObject roomObject)
    {
        if (roomObject.GetComponent<Room>().zoomedCamera)
        {
            m_zoomedRooms.Remove(roomObject);
        }
        else
        {
            m_unzoomedRooms.Remove(roomObject);
        }
    }

    private void FixedUpdate()
    {
        GameObject closest;

        if (m_cameraIsZoomed)
        {
            m_targetZoomLevel = m_innerZoomLevel;
        }
        else
        {
            m_targetZoomLevel = m_outerZoomLevel;
        }

        if (m_cameraIsZoomed)
        {
            closest = ClosestZoomed();
        }
        else
        {
            closest = ClosestUnzoomed();
        }

        if (closest != m_closest)
        {
            if (m_closest != null)
                m_closest.GetComponent<Room>().m_focused = false;

            if (closest != null)
                closest.GetComponent<Room>().m_focused = true;

            m_closest = closest;
        }
    }

    private GameObject ClosestZoomed()
    {
        float shortest = 0f;
        GameObject closest = null;

        foreach (GameObject obj in m_zoomedRooms)
        {
            float dist = Vector3.Distance(obj.transform.position, m_player.transform.position);
            if (closest == null)
            {
                closest = obj;
                shortest = dist;
                continue;
            }

            if (dist < shortest)
            {
                closest = obj;
                shortest = dist;
            }
        }

        return closest;
    }

    private GameObject ClosestUnzoomed()
    {
        float shortest = 0f;
        GameObject closest = null;

        foreach (GameObject obj in m_unzoomedRooms)
        {
            float dist = Vector3.Distance(obj.transform.position, m_player.transform.position);
            if (closest == null)
            {
                closest = obj;
                shortest = dist;
                continue;
            }

            if (dist < shortest)
            {
                closest = obj;
                shortest = dist;
            }
        }

        return closest;
    }

    private void Update()
    {
        if (m_player == null || m_closest == null)
            return;

        Vector3 diff = m_player.transform.position - m_closest.transform.position;
        diff = new Vector3(diff.x * 0.1f, diff.y * 0.1f, 0);

        Vector3 target = m_closest.transform.position + diff;
        camLookPoint.transform.position = target;
        GameController.Instance.vCamera.Follow = camLookPoint.transform;

        float zoomDist = Mathf.Abs(m_outerZoomLevel - m_innerZoomLevel);
        float zoomSpeed = (zoomDist / m_zoomTime) * Time.deltaTime;

        float orthosize = GameController.Instance.vCamera.m_Lens.OrthographicSize;
        if (m_targetZoomLevel < orthosize)
        {
            orthosize -= zoomSpeed;
            if (m_targetZoomLevel > orthosize)
            {
                orthosize = m_targetZoomLevel;
            }
        }
        else if (m_targetZoomLevel > orthosize)
        {
            orthosize += zoomSpeed;
            if (m_targetZoomLevel < orthosize)
            {
                orthosize = m_targetZoomLevel;
            }
        }
        else
        {
            //do nothing
        }

        GameController.Instance.vCamera.m_Lens.OrthographicSize = orthosize;
    }
}