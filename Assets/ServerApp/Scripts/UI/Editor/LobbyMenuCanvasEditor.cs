using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Carousel{

namespace BaselineAgent{

[CustomEditor(typeof(LobbyMenuCanvas))]
public class LobbyMenuCanvasEditor : Editor

{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LobbyMenuCanvas c = (LobbyMenuCanvas)target;
    
		if (GUILayout.Button("Host"))
        {

           c.Host();
        }
        if (GUILayout.Button("Join"))
        {

           c.ShowServerList();
        }
		if (GUILayout.Button("Settings"))
        {

           c.ShowSettings();
        }
		if (GUILayout.Button("Back"))
        {

           c.ShowMain();
        }
		if (GUILayout.Button("ExitGame"))
        {

           c.ExitGame();
        }
    }
}
}
}