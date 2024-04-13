using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class Clickable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseController.instance.Clickable();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseController.instance.Default();
    }
}
