using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[System.Serializable]
public class Move
{
    public MoveType Type;
    public GameSetups.GameKeys.ActionType EnterActionType;
    public KeyCode EnterKey;
    [HideInInspector] public AnimatorControllerParameter animatorParametr;
    [HideInInspector] public int paramentrEnumIndex;
    public enum MoveType
    {
        Enter, Middle, Exit
    }
}
