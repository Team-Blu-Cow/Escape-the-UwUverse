using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private RoomController m_controller;

    public bool m_focused = false;

    // Start is called before the first frame update
    private void Awake()
    {
        GameObject gridObject = GameObject.Find("Grid");
        m_controller = gridObject.GetComponent<RoomController>();

        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    private void OnEnable()
    {
        m_controller.AddRoom(gameObject);
    }

    private void OnDisable()
    {
        m_controller.RemoveRoom(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (m_focused)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}