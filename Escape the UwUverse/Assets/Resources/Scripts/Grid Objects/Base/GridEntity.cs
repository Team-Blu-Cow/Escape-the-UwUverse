using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEntity : MonoBehaviour
{
    protected GridNode m_targetNode;
    protected TileGrid m_gridRef = null;
    protected GridNode m_currentNode = null;
    protected Vector2Int m_direction = Vector2Int.zero;

    public GridNode CurrentNode
    {
        get { return m_currentNode; }
        set { m_currentNode = value; }
    }

    public GridNode TargetNode
    {
        get { return m_targetNode; }
        set { m_targetNode = value; }
    }

    public Vector2Int Position
    {
        get { return m_currentNode.position; }
    }

    public Vector2Int Direction
    {
        get { return m_direction; }
        set { m_direction = value; }
    }

    public TileGrid grid
    { get { return m_gridRef; } }

    public virtual void Hit(GameObject obj, int damage)
    {
        //Debug.LogWarning("Default hit called!");
        Debug.Log("I've been Hit");
    }

    // TODO: @Jay probably redundant
    public virtual void CheckForCollisionsMoving(GridNode[] nodes, Vector2Int[] dangerVectors)
    {
        /* Offsets & Nodes diagram
        * 
        * P = player
        * n = index in list
        * ┌───┐
        * │ n │ = nodes[n]
        * └───┘
        * ✢n✢ = dangerVectors[n]
        * 
        *      ┌───┐
        *      │↓1↓│
        *      └───┘
        *┌───┐ ┌───┐ ┌───┐ 
        *│→0→│ │↓3↓│ │←2←│ 
        *└───┘ └───┘ └───┘
        *      ╔═══╗
        *      ║↑P↑║
        *      ╚═══╝
        */

        int CheckNum = nodes.Length;

        GameObject[] bullets = new GameObject[CheckNum];

        for (int i = 0; i < CheckNum; i++)
        {
            // check nodes for a bullet
            if (nodes[i] != null && nodes[i].HasObjectOfType<bullet>(ref bullets[i]))
            {
                // check if the bullet is heading towards the player's next position
                if (bullets[i] != null && (bullets[i].GetComponent<bullet>().m_direction == dangerVectors[i]))
                {
                    Hit(bullets[i], 1);
                }
            }
        }
    }

    public virtual void CheckForCollisionsStationary(GridNode[] nodes, Vector2Int[] dangerVectors)
    {
        int CheckNum = nodes.Length;

        GameObject[] bullets = new GameObject[CheckNum];

        for (int i = 0; i < CheckNum; i++)
        {
            // check nodes for a bullet
            if (nodes[i] != null && nodes[i].HasObjectOfType<bullet>(ref bullets[i]))
            {
                // check if the bullet is heading towards the player's next position
                if (bullets[i] != null && (bullets[i].GetComponent<bullet>().m_direction == dangerVectors[i]))
                {
                    Hit(bullets[i], 1);
                }
            }
        }
    }

}