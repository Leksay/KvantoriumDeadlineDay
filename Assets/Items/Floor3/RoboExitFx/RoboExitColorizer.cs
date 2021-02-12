using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboExitColorizer : MonoBehaviour, IActivatable
{
    [SerializeField] private List<LineRenderer> lines;
    [SerializeField] private Gradient colorGradient;
    [SerializeField] private float smoothness;
    private bool isActive;
    void Start()
    {
        isActive = false;
        lines.AddRange(transform.GetComponentsInChildren<LineRenderer>());
    }

    [SerializeField] private float time;
    private float k;
    void Update()
    {
        if (!isActive) return;
        time += Time.deltaTime;
        k = Mathf.Sin(time);
        k *= k;
        Color col = colorGradient.Evaluate(k);
        for(int i=0; i < lines.Count; i++)
        {
            lines[i].startColor = colorGradient.Evaluate(k)/((i+1)/ smoothness);
            lines[i].endColor = colorGradient.Evaluate(k)/ ((i + 1) / smoothness);
        }
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
        Destroy(gameObject, 1f);
    }
}
