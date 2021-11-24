using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelDesignerWindow : EditorWindow {

    public GameObject FirstGridObject;


    [MenuItem("Window/Level Designer")]
    static void OpenWindow()
    {
        LevelDesignerWindow window = (LevelDesignerWindow)GetWindow(typeof(LevelDesignerWindow));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }

    // this calls when this window is enabled
    // this is similar to Start or Awake
    private void OnEnable()
    {
          
    }

    //This is similar to Update but it is called when a
    // UI interaction is happened rather then every frame
    private void OnGUI()
    {
        GUILayout.Label("This is showing");
    }
}
