using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public class Combo : MonoBehaviour
{
    public string name;
    [HideInInspector] public List<Move> moves;
    public float delayTime;
    public Animator CharacterAnimator;
    public UnityEditor.Animations.AnimatorController animatorController;
    public float attackTime = 0.75f;
    private void Update()
    {
#if UNITY_EDITOR
        var parametres = animatorController.parameters;
        foreach(var param in parametres)
        {
            
        }
        moves.ForEach(m =>
        {
            m.EnterKey = GameDataHolder.instance.Settups.Keys.ActionTypeToKey(m.EnterActionType);
        });
#endif
    }
}