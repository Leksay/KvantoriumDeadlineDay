using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(ComboTool))]
public class ComboTool:EditorWindow
{
    private static ComboData currentCombo;
    private static GameSetups settups;


    [MenuItem("Window/ComboTool")]
    public static void ShowWindow()
    {
        GetWindow<ComboTool>("ComboTool");
    }

    private void OnGUI()
    {
        if(GUILayout.Button("CreateNewCombo"))
        {
            currentCombo = new ComboData();
            currentCombo.moves = new List<Move>();
        }
        if(settups == null)
        {
            settups = FindObjectOfType<GameDataHolder>().Settups;
        }
        if(currentCombo != null)
        {
            if (settups == null)
            {
                GUILayout.Label("Place Game Settups Object On Scene");
            }
            DrawComboGUI();
        }
    }

    private void DrawComboGUI()
    {
        // Combo name
        GUILayout.Label($"Combo name '{currentCombo.name} ' ");
        currentCombo.name = EditorGUILayout.TextField(currentCombo.name);
        // Moves 
        DrawMoveGUI();
        if(GUILayout.Button("Create Move"))
        {
            currentCombo.moves.Add(new Move());
        }
    }

    private void DrawMoveGUI()
    {
        currentCombo.moves.ForEach(c =>
            {
                c.Type = (Move.MoveType)EditorGUILayout.EnumPopup("Move type", c.Type);
                c.EnterActionType = (GameSetups.GameKeys.ActionType)EditorGUILayout.EnumPopup("Combo Enter Key", c.EnterActionType);
            });
    }
}
