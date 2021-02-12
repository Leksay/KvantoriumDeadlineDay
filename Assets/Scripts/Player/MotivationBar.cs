using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private float smoothness = 0.01f;
    private Slider motivationBar;

    private float curValue, destinationValue;
    void Start()
    {
        motivationBar = GetComponent<Slider>();
        curValue = destinationValue = motivationBar.value;
    }

    
    void Update()
    {

        if (destinationValue != curValue)
        {
            motivationBar.value = Mathf.Lerp(curValue, destinationValue, smoothness);
        }
    }

    void FixedUpdate()
    {
        curValue = motivationBar.value;
    }

    public void SetBarValuePercent(float percentValue)
    {
        if (percentValue < 0 )return;
        else if (percentValue > 1)
        {
            percentValue = 1;
        }
        destinationValue = percentValue;
    }
}
