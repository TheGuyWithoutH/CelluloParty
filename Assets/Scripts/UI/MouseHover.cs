using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 v = gameObject.transform.localScale;
        v = new Vector3(v.x * 1.1f, v.y * 1.1f, v.z * 1.1f);
        gameObject.transform.localScale = v;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Vector3 v = gameObject.transform.localScale;
        v = new Vector3(v.x / 1.1f, v.y / 1.1f, v.z / 1.1f);
        gameObject.transform.localScale = v;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 v = gameObject.transform.localScale;
        v = new Vector3(v.x / 1.1f, v.y / 1.1f, v.z / 1.1f);
        gameObject.transform.localScale = v;
    }
}