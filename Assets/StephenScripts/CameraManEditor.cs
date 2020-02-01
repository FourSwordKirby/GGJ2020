using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraMan))]
public class CameraManEditor : Editor
{
    void OnEnable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CameraMan cm = (CameraMan)target;

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("To Default"))
        {
            cm.MoveCameraToDefault();
        }

        if (GUILayout.Button("To LowZ"))
        {
            cm.MoveCameraToLowZ();
        }

        if (GUILayout.Button("To HighZ"))
        {
            cm.MoveCameraToHighZ();
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save Default"))
        {
            cm.SaveCameraValuesToDefault();
        }

        if (GUILayout.Button("Save LowZ"))
        {
            cm.SaveCameraValuesToLowZ();
        }

        if (GUILayout.Button("Save HighZ"))
        {
            cm.SaveCameraValuesToHighZ();
        }
        EditorGUILayout.EndHorizontal();
    }
}
