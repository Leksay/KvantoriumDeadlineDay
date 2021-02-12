using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DroneRacingTMP : MonoBehaviour
{
    [SerializeField] private static TextMeshProUGUI textMesh;
    void Start()
    {
        if(textMesh == null)
            textMesh = GetComponent<TextMeshProUGUI>();
    }

    public static void SetText(string text)
    {
        textMesh.text = text;
    }
}
