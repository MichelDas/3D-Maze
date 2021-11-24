using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelBuildManager))]
public class LevelBuilder : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //DrawDefaultInspector();


        LevelBuildManager levelBuildManager = (LevelBuildManager)target;
        if(GUILayout.Button("Build Level"))
        {
            levelBuildManager.BuildGround();
        }

        if(GUILayout.Button("Destroy Level"))
        {
            levelBuildManager.ClearFloor();
        }
    }

}
