using UnityEngine;
using UnityEditor;
using System;

public class MovesSetupWindow:EditorWindow
{
    public static MovesSetupWindow instance;
    public static   Combo sourceCombo;

    public static void Init(Combo combo)
    {
        sourceCombo = combo;
        ShowWindow();
    }

    [MenuItem("MyTools/MovesSetup")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<MovesSetupWindow>("Moves Setup");
    }

    private void OnGUI()
    {
        sourceCombo = (Combo)EditorGUILayout.ObjectField("Combo",sourceCombo, typeof(Combo), true);
        if(sourceCombo != null)
        {
            DrawMovesGUI();
        }
    }

    private void DrawMovesGUI()
    {
        sourceCombo.delayTime = EditorGUILayout.FloatField("Delay Time",sourceCombo.delayTime);
        sourceCombo.attackTime = EditorGUILayout.FloatField("Attack Time",sourceCombo.attackTime);
        for(int i = 0; i < sourceCombo.moves.Count; i++)
        {
            var move = sourceCombo.moves[i];
            GUILayout.Label($"Move {i+1}");
            move.Type = (Move.MoveType)EditorGUILayout.EnumPopup("Type",move.Type);
            move.EnterActionType = (GameSetups.GameKeys.ActionType)EditorGUILayout.EnumPopup("Enter Action", move.EnterActionType);
            EditorGUILayout.EnumPopup("Enter KeyCode (ReadOnly)",move.EnterKey);
            move.EnterKey = GameDataHolder.instance.Settups.Keys.ActionTypeToKey(move.EnterActionType);
            move.paramentrEnumIndex = EditorGUILayout.Popup(move.paramentrEnumIndex, GetControllersParametres(sourceCombo.animatorController));
            move.animatorParametr = GetControllersParametres(sourceCombo.animatorController,move.paramentrEnumIndex);

            if (GUILayout.Button("Remove Move"))
            {
                sourceCombo.moves.Remove(move);
            }
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
        }
        if(GUILayout.Button("Add Move"))
        {
            sourceCombo.moves.Add(new Move());
        }
    }


    public string[] GetControllersParametres(UnityEditor.Animations.AnimatorController controller)
    {
        string[] parametres = new string[controller.parameters.Length];
        for (int i = 0; i < parametres.Length; i++)
        {
            parametres[i] = controller.parameters[i].name;
        }
        return parametres;
    }

    public AnimatorControllerParameter GetControllersParametres(UnityEditor.Animations.AnimatorController controller,int index)
    {
        if (index < controller.parameters.Length)
            return controller.parameters[index];
        else
            throw new IndexOutOfRangeException("Out of animator controller parametres length");
    }
}