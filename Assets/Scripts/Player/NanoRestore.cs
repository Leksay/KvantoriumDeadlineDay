using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanoRestore : MonoBehaviour
{
    private NanoSetup.NanoSetupStruct startParams;
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerNanoSetup>();
        if (player == null || !player.settuped) return;
        player.RestoreNanoSettings();
        player.settuped = false;
    }
}
