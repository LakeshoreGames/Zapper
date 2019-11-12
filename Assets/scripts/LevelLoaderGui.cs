
#if (UNITY_EDITOR) 


using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(levelloader))]
public class LevelLoaderGui : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        levelloader myTarget = (levelloader)target;

        if (GUILayout.Button("build level"))
        {
            myTarget.buildlvl();
        }
    }
}

#endif