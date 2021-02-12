using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameDataHolder : MonoBehaviour
{
    public GameSetups Settups;
    public static GameDataHolder instance;
    private void OnEnable()
    {
        if (instance == null)
            instance = this;
    }
}
