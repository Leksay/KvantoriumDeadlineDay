using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] private Combo combo;
    [Header("Attack Parametres")]
    [SerializeField] private float notComboAttackTime;
    [SerializeField] private float damage;
    [SerializeField] private float radius;
    [SerializeField] private Transform attackPoint;

    private float timer;
    private bool inCombo;
    private float nextAttackTime;

    private bool run;
    private int moveIndexProperty;
    private int moveIndex
    {
        get { return moveIndexProperty; }
        set { 
            if (value >= 3)
            {
                if (Time.time >= nextAttackTime)
                    moveIndexProperty = 0; 
            }
            else moveIndexProperty = value;
        }
    }
    #region MonoBehaviour
    private void Start()
    {
        if(combo == null)
        {
            if(!TryGetComponent<Combo>(out combo))
            {
                throw new MissingReferenceException("Combo not settuped");
            }
        }
    }
    private void Update()
    {
        run = combo.CharacterAnimator.GetFloat("speed") > 0.25f;
        if(run)
        {
            if (inCombo) 
                StopCombo();
            if(AttackCondition())
            {
                combo.CharacterAnimator.SetTrigger("attack");
                nextAttackTime = Time.time + notComboAttackTime;
                //Attack();
            }
        }
        else if(ComboAttackCondition())
        {
            if (combo.CharacterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Motion") || combo.CharacterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
                moveIndex = 0;
            inCombo = true;
            SetBoolOrTriggerParam(moveIndex, true);
            combo.CharacterAnimator.SetInteger("attackIndex", moveIndex);
            nextAttackTime = Time.time + combo.attackTime;
            timer = combo.delayTime;
            moveIndex++;
            //Attack();
        }
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer <= 0)
        {
            StopCombo();
        }
    }
    #endregion
    // Called from attack animation event
    private void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, radius);
        foreach (var collider in colliders)
        {
            var atakable = collider.GetComponent<IAtackable>();
            if (atakable == null)
            {
                atakable = collider.GetComponentInParent<IAtackable>();
            }
            if (atakable != null && collider.tag == "CanAttack")
            {
                atakable.GetDamage(damage);
            }
        }
    }

    private void AttackWithMultipier(float multiplier)
    {
        float tempAttack = damage;
        damage *= multiplier;
        Attack();
        damage = tempAttack;
    }

    private bool AttackCondition()
    {
        return Time.time >= nextAttackTime && Input.GetKeyDown(GameDataHolder.instance.Settups.Keys.AttackKey);
    }

    private void StopCombo()
    {
        inCombo = false;
        timer = 0;
        moveIndex = 0;
        combo.CharacterAnimator.SetBool("inCombo", false);
        combo.CharacterAnimator.SetInteger("attackIndex", 0);

    }
    private bool ComboAttackCondition()
    {
        return Time.time >= nextAttackTime && Input.GetKeyDown(combo.moves[0].EnterKey);
    }

    private AnimatorControllerParameterType GetParameterType(int _moveIndex)
    {
        return combo.animatorController.parameters[combo.moves[_moveIndex].paramentrEnumIndex].type;
    }

    private void SetBoolOrTriggerParam(int _moveIndex, bool value)
    {
        var paramType = GetParameterType(_moveIndex);
        switch(paramType)
        {
            case AnimatorControllerParameterType.Trigger:
                combo.CharacterAnimator.SetTrigger(combo.animatorController.parameters[combo.moves[_moveIndex].paramentrEnumIndex].name);
                break;
            case AnimatorControllerParameterType.Bool:
                combo.CharacterAnimator.SetBool(combo.animatorController.parameters[combo.moves[_moveIndex].paramentrEnumIndex].name, value);
                break;
        }
    }

    public void MultiplyAttackRadious(float newRadius)
    {
        radius *= newRadius;
    }
    public float GetRadious() => radius;
    public float SetRadious(float newRadious) => radius = newRadious;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }

}
