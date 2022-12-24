using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.HelpBox("This is create and Joing Rooms", MessageType.Info);

        RoomManager roomManager = (RoomManager)target;
        
        if(GUILayout.Button("Join CityRoom"))
        {
            roomManager.OnEnterButtonClicked_CityScene();
        }
        
        if(GUILayout.Button("Join OutDoorRoom"))
        {
            roomManager.OnEnterButtonClicked_OutDoor();
        }
    }
}
