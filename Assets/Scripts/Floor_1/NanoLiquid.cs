using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NanoLiquid : MonoBehaviour
{
    [SerializeField] private Color negativeColor;
    [SerializeField] private Color positiveColor;

    [Range(0,50)]
    [SerializeField] private float DPS; // Damage or 

    private SpriteRenderer spriteRenderer;
    private List<IAtackable> targets;
    public enum LiqidType
    {
        Neutral, Positive, Negative
    }
    private LiqidType currentType;
    private float attackSign;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentType = LiqidType.Neutral;
        targets = new List<IAtackable>();
    }
    public void ChangeLiquidType(NanoChemical.TypeOfChemical type)
    {
        if(type == NanoChemical.TypeOfChemical.Negative)
        {
            spriteRenderer.color = negativeColor;
            attackSign = 1f;
        }
        else
        {
            spriteRenderer.color = positiveColor;
            attackSign = -1f;
        }
        currentType = CalculateLiquidType(type);
    }

    private LiqidType CalculateLiquidType(NanoChemical.TypeOfChemical inputLiquidType)
    {
        if (inputLiquidType == NanoChemical.TypeOfChemical.Negative) return LiqidType.Negative;
        if (inputLiquidType == NanoChemical.TypeOfChemical.Positive) return LiqidType.Positive;
        return LiqidType.Neutral;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var nanoChemical = collision.GetComponent<NanoChemical>();
        var inputAttackable = collision.GetComponent<IAtackable>();
        if(nanoChemical != null)
        { 
            ChangeLiquidType(nanoChemical.type);
            Destroy(collision.gameObject);
            return;
        }
        if(inputAttackable != null && currentType != LiqidType.Neutral && !targets.Contains(inputAttackable))
        {
            targets.Add(inputAttackable);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(targets.Count > 0)
        {
            targets.ForEach(t => {
                if (t == null) targets.Remove(t);
                t?.GetDamage(DPS * Time.deltaTime * attackSign);
            });
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var outcomingAttackable = collision.GetComponent<IAtackable>();
        if (outcomingAttackable != null && targets.Contains(outcomingAttackable))
        {
            targets.Remove(outcomingAttackable);
        }
    }
}


