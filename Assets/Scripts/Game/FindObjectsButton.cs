using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
[CustomEditor(typeof(GlobalActivatorFloor))]
public class FindObjectsButton : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GlobalActivatorFloor FindTargetScript = (GlobalActivatorFloor)target;
        if (GUILayout.Button("FindObjects"))
        {
            FindTargetScript.Start();
        }
    }
}
#endif