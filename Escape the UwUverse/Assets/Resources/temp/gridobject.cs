using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridobject : MonoBehaviour
{
    public int x = 0;
    public int y = 0;

    [SerializeField]
    private TileGrid m_grid;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        Transform trans = GetComponent<Transform>();

        Vector3 pos = m_grid.GridCoordToWorldCoord(x, y);
        trans.position = pos;
    }
}