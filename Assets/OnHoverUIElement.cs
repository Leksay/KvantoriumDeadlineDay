using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class OnHoverUIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float hoverSize = 1.5f;
    [SerializeField] private float startSize;
    [SerializeField] private float smothness = 0.01f;
    [SerializeField] private bool pointerInside;
    
    private Transform myTransform;

    void Start()
    {
        myTransform = transform;
        startSize = myTransform.localScale.x;
        hoverSize = startSize + hoverSize;
    }

    void Update()
    {
        if (pointerInside )
        {
            myTransform.localScale = Vector3.Lerp(myTransform.localScale, Vector3.one * hoverSize, smothness);
        } 
        else if (!pointerInside && myTransform.localScale.x - startSize > 0.01f)
        {
            myTransform.localScale = Vector3.Lerp(myTransform.localScale, Vector3.one * startSize, smothness);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerInside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerInside = false;
    }
}
