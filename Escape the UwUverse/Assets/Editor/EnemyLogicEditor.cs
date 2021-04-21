using UnityEngine;
using UnityEditor;
using UwUverse;

[CustomEditor(typeof(EnemyLogic),true)]
public class EnemyLogicEditor : Editor
{
    private EnemyLogic eLogic;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();

        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        eLogic = (EnemyLogic)target;

        var positions = eLogic.path;
        if (positions.Length > 1)
        {
            TileGrid grid = GameObject.FindObjectOfType<TileGrid>();

            Handles.color = Color.red;

            float dirX = positions[1].x - positions[0].x;
            float dirY = positions[1].y - positions[0].y;

            float thickness = 0.2f;

            Vector2 dir = (Mathf.Abs(dirX) > Mathf.Abs(dirY)) ? new Vector2(Mathf.Sign(dirX), 0) : new Vector2(0,Mathf.Sign(dirY));

            Vector3[] points = new Vector3[3];
            points[0] = positions[0] - (Vector2.one * 0.5f) + (dir*0.5f);
            points[1] = positions[0] - (Vector2.one * 0.5f) + (new Vector2(dir.y, dir.x)* thickness) + (dir * 0.06f);
            points[2] = positions[0] - (Vector2.one * 0.5f) + (new Vector2(-dir.y, -dir.x)* thickness) + (dir * 0.06f);

            Handles.DrawSolidDisc(positions[0] - (Vector2.one * 0.5f), Vector3.back, thickness);
            Handles.DrawAAConvexPolygon(points);
            Handles.color = Color.cyan;

            for (int i = 1; i < positions.Length + 1; i++)
            {
                var previousPoint = positions[i - 1];
                var currentPoint = positions[i % positions.Length];

                Handles.DrawDottedLine(previousPoint - (Vector2.one*0.5f), currentPoint - (Vector2.one * 0.5f), 4f);

                positions[i - 1] = Handles.PositionHandle(positions[i - 1] - (Vector2.one * 0.5f), Quaternion.identity) + (Vector3.one * 0.5f);

                positions[i - 1].x = (float)Mathf.RoundToInt(positions[i - 1].x);
                positions[i - 1].y = (float)Mathf.RoundToInt(positions[i - 1].y);
            }
        }
    }
}
