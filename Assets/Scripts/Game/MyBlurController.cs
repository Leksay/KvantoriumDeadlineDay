using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBlurController : MonoBehaviour
{
    public RenderTexture inputOutput;
    public Material BlurMaterial;

    private Camera cam;
    private void Awake()
    {
        cam = GetComponent<Camera>();
        cam.targetTexture = inputOutput;
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(inputOutput, inputOutput, BlurMaterial);
    }
}
