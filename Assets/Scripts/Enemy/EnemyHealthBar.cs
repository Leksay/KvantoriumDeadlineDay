using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class MotivationBar : MonoBehaviour
{
    [SerializeField] private float smoothness = 0.01f;
    private Slider enemyBar;

    private float curValue, destinationValue;
    void Start()
    {
        enemyBar = GetComponent<Slider>();
        curValue = destinationValue = enemyBar.value;
    }

    
    void Update()
    {

        if (destinationValue != curValue)
        {
            enemyBar.value = Mathf.Lerp(curValue, destinationValue, smoothness);
        }
    }

    void FixedUpdate()
    {
        curValue = enemyBar.value;
    }

    public void SetBarValuePercent(float percentValue)
    {
        if (percentValue < 0)
        {   
            return;
        }

        if (percentValue > 1) percentValue = 1;
        destinationValue = percentValue;
    }
}
