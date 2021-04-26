using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridEntity), true)]
public class GridEntityEditor : Editor
{
    private GridEntity entity;

    private void OnSceneGUI()
    {
        entity = (GridEntity)target;

        entity.transform.position = new Vector3(Mathf.Floor(entity.transform.position.x) + 0.5f, Mathf.Floor(entity.transform.position.y) + 0.5f, Mathf.Floor(entity.transform.position.y) + 0.5f);
    }
}
