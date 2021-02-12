using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Combo))]
[CanEditMultipleObjects]
public class ComboEditor : Editor
{
    [HideInInspector] public UnityEditor.Animations.AnimatorController controller;
    private int index;
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Setup Moves"))
        {
            MovesSetupWindow.Init((Combo)serializedObject.targetObject);
        }
        base.OnInspectorGUI();

        Combo combo = (Combo)target;
        controller = combo.animatorController;
        index = EditorGUILayout.Popup(index, GetControllersParametres());
    }

    public string[] GetControllersParametres()
    {
        string[] parametres = new string[controller.parameters.Length];
        for(int i =0; i < parametres.Length; i++)
        {
            parametres[i] = controller.parameters[i].name;
        }
        return parametres;
    }
}
