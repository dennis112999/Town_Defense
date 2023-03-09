using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WayPoint))]
public class WayPointEditor : Editor
{
    WayPoint WayPoint => target as WayPoint;

    private void OnSceneGUI()
    {
        Handles.color = Color.red;

        for (int i=0; i<WayPoint.Points.Length; i++)
        {
            //Create Handles
            Vector3 currentWayPoint = WayPoint.CurrentPos + WayPoint.Points[i];
            Vector3 newWayPoint = Handles.FreeMoveHandle(currentWayPoint, 0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);

            //Create text
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.fontSize = 16;
            textStyle.normal.textColor = Color.yellow;
            Vector3 textAlligment = Vector3.down * 0.35f + Vector3.right * 0.35f;
            Handles.Label(WayPoint.CurrentPos + WayPoint.Points[i] + textAlligment, text:$"{i+1}");


            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, name:"Free Move Handle");
                WayPoint.Points[i] = newWayPoint - WayPoint.CurrentPos;
            }

        }

    }
}
