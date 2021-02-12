using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public int FloorNumber;

    [SerializeField] private Transform firstFloor;

    [SerializeField] private Transform secondFloor;

    [SerializeField] private Transform thirdFloor;
    [SerializeField] private GlobalActivatorFloor floor1GlobalActivator;
    void Start()
    {
        FloorNumber = 0;
        SpriteRenderer[] secondFloor_sprites = secondFloor.GetComponentsInChildren<SpriteRenderer>();
        foreach (var sprite in secondFloor_sprites)
        {
            sprite.color = new Color(1, 1, 1, 0.35f);
        }
    }

    public void NextFloor()
    {
        FloorNumber++;

        if (FloorNumber == 1)
        {
            SpriteRenderer[] firstFloor_sprites = firstFloor.GetComponentsInChildren<SpriteRenderer>();
            if (floor1GlobalActivator != null) floor1GlobalActivator.DeactivateAll();
            foreach (var sprite in firstFloor_sprites)
            {
                sprite.color = new Color(1,1,1,0.35f);
            }
            SpriteRenderer[] secondFloor_sprites = secondFloor.GetComponentsInChildren<SpriteRenderer>();
            foreach (var sprite in secondFloor_sprites)
            {
                sprite.color = Color.white;
            }
        }
        else if(FloorNumber == 2)
        {
            SpriteRenderer[] firstFloor_sprites = secondFloor.GetComponentsInChildren<SpriteRenderer>();
            if (floor1GlobalActivator != null) floor1GlobalActivator.DeactivateAll();
            foreach (var sprite in firstFloor_sprites)
            {
                sprite.color = new Color(1, 1, 1, 0.35f);
            }
        }
    }
}
