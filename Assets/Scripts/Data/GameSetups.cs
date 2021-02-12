using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UI;
[CreateAssetMenu(fileName ="GameSetup",menuName ="Data/GameSetup")]
public class GameSetups : ScriptableObject
{

    public GameKeys Keys;   
    [System.Serializable]
    public struct GameKeys
    {
        public KeyCode AttackKey;
        public KeyCode JumpKey;

        public enum ActionType
        {
            Attack, Jump
        }

        public KeyCode ActionTypeToKey(ActionType type)
        {
            switch(type)
            {
                case ActionType.Attack:
                    return AttackKey;
                case ActionType.Jump:
                    return JumpKey;
                default:
                    return KeyCode.None;
            }
        }
    }

}
