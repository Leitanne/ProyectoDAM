using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    Waypoint WPTarget => target as Waypoint;

    private void OnSceneGUI()
    {
        Handles.color = Color.red;
        if(WPTarget.Points == null)
        {
            return;
        }

        for (int i = 0; i < WPTarget.Points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();

            //Create Handle
            Vector3 currentPoint = WPTarget.currentPosition + WPTarget.Points[i];
            Vector3 newPoint = Handles.FreeMoveHandle(currentPoint, Quaternion.identity, 0.7f, 
                new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap) ;

            //Create Text
            GUIStyle text = new GUIStyle();
            text.fontStyle = FontStyle.Bold;
            text.fontSize = 16;
            text.normal.textColor = Color.black;
            Vector3 alignment = Vector3.down * 0.3f + Vector3.right * 0.3f;
            Handles.Label(WPTarget.currentPosition + WPTarget.Points[i] + alignment, 
                $"{i + 1}", text);

            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Free Move Handle");
                WPTarget.Points[i] = newPoint - WPTarget.currentPosition;
            }
        }
    }
}
