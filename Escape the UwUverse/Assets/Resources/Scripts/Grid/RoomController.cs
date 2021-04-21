using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    private List<GameObject> rooms = new List<GameObject>();

    private GameObject m_closest;
    private GameObject m_player;

    private GameObject camLookPoint = null;

    private void Awake()
    {
        camLookPoint = GameObject.Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
    }

    private void OnDestroy()
    {
        GameObject.Destroy(camLookPoint);
    }

    private void Start()
    {
        m_player = GameObject.Find("Player");
    }

    public void AddRoom(GameObject roomObject)
    {
        if (!rooms.Contains(roomObject))
        {
            rooms.Add(roomObject);
        }
    }

    public void RemoveRoom(GameObject roomObject)
    {
        rooms.Remove(roomObject);
    }

    private void FixedUpdate()
    {
        float shortest = 0f;
        GameObject closest = null;

        foreach (GameObject obj in rooms)
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

        if (closest != m_closest)
        {
            if (m_closest != null)
                m_closest.GetComponent<Room>().m_focused = false;

            if (closest != null)
                closest.GetComponent<Room>().m_focused = true;

            m_closest = closest;
        }
    }

    // TODO @matthew - conver this over to lean twean
    private void Update()
    {
        //Vector3 cameraPos = Camera.main.transform.position;
        //
        if (m_player == null || m_closest == null)
            return;

        Vector3 diff = m_player.transform.position - m_closest.transform.position;
        diff = new Vector3(diff.x * 0.1f, diff.y * 0.1f, 0);

        Vector3 target = m_closest.transform.position + diff;

        camLookPoint.transform.position = target;

        GameController.Instance.vCamera.Follow = camLookPoint.transform;
        //cameraPos = Vector3.Lerp(cameraPos, target, 0.05f);
        //Camera.main.transform.position = cameraPos;
    }
}