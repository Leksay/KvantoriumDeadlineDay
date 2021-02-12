using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class PlayerNanoSetup : MonoBehaviour
{
    public NanoSetup.NanoSetupStruct startSettings;
    public bool settuped;
    private PlayerController player;
    private Camera cam;
    private void Start()
    {
        settuped = false;
        startSettings = new NanoSetup.NanoSetupStruct();
        player = GetComponent<PlayerController>();
        startSettings.FOV = 5;
        startSettings.scaleMultiplier = transform.localScale.x;
        startSettings.speedMultiplier = player.speed;
        startSettings.attackRadius = player.radius;
        cam = Camera.main;
    }
    public void SetupNanoSettings(NanoSetup.NanoSetupStruct settings)
    {
        if (settuped) return;
        player.speed *= settings.speedMultiplier;
        player.radius *= 0.1f;
        print(settings.speedMultiplier);
        WaitForNanoSetup(player.transform.localScale.x * settings.scaleMultiplier, settings.FOV);
    }
    public void RestoreNanoSettings ()
    {
        if (!settuped) return;
        player.speed = startSettings.speedMultiplier;
        player.radius = startSettings.attackRadius;
        WaitForNanoReSetup(startSettings.scaleMultiplier, startSettings.FOV);
    }
    private async void WaitForNanoSetup(float newScale, float newFov)
    {
        bool scaled = false;
        bool foved = false;
        while (!scaled || !foved)
        {
            if(!scaled)
            {
                player.transform.localScale -= Vector3.one * Time.deltaTime;
                if (player.transform.localScale.x <= newScale)
                {
                    scaled = true;
                    player.transform.localScale = Vector3.one * newScale;
                }
            }
            if(!foved)
            {
                cam.orthographicSize -= 2 * Time.deltaTime;
                if(Camera.main.orthographicSize <= newFov)
                    foved = true;
            }
            await Task.Yield();
        }
        settuped = true;
    }

    private async void WaitForNanoReSetup(float newScale, float newFov)
    {
        bool scaled = false;
        bool foved = false;
        while (!scaled || !foved)
        {
            if (player == null)
            {
                OnDestroy();
                return;
            }
            if (!scaled)
            {
                player.transform.localScale += 2 * Vector3.one * Time.deltaTime;
                if (player.transform.localScale.x >= newScale)
                    scaled = true;
            }
            if (!foved && cam != null)
            {
                cam.orthographicSize += 2 * Time.deltaTime;
                if (cam.orthographicSize >= newFov)
                    foved = true;
            }
            await Task.Yield();
            settuped = false;
        }
    }
    private void OnDestroy()
    {
        if(cam != null)
        {
            cam.orthographicSize = 5f;
        }
        RestoreNanoSettings();
    }
}
