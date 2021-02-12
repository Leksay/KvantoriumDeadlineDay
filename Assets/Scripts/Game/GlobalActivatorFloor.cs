using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class GlobalActivatorFloor : MonoBehaviour, IGlobalActivator
{
    public List<IActivatable> Activables { get; set; }
    [SerializeField] private Transform floor_root;

    public void Start()
    {
        Activables = new List<IActivatable>();
        Activables.AddRange(floor_root.GetComponentsInChildren<IActivatable>());
    }
    public void ActivateAll() => Activables.ForEach(a => a.Activate());

    public void DeactivateAll() => Activables?.ForEach(a => a?.Deactivate());

    public void TurnOffLights()
    {
        SpriteRenderer[] firstFloor_sprites = floor_root.GetComponentsInChildren<SpriteRenderer>();
        foreach (var sprite in firstFloor_sprites)
        {
            sprite.color = new Color(1, 1, 1, 0.35f);
        }
    }
}

