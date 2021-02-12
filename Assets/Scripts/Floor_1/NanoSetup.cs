using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanoSetup : MonoBehaviour
{
    [SerializeField] private NanoSetupStruct settings;
    private bool settuped;
    private NanoSetupStruct startParams;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (settuped) return;
        var player = collision.GetComponent<PlayerNanoSetup>();
        if (player == null) return;
        startParams = new NanoSetupStruct();
        startParams.FOV = 5;
        startParams.speedMultiplier = player.transform.GetComponent<PlayerController>().speed;
        startParams.speedMultiplier = player.transform.localScale.x;
        

        if(player != null)
        {
            player.SetupNanoSettings(settings);
            settuped = true;
        }
    }



    [Serializable]
    public struct NanoSetupStruct
    {
        public float scaleMultiplier;
        public float FOV;
        public float speedMultiplier;
        public float attackRadius;
    }
}
