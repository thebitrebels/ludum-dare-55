using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomEvents : EventTrigger

{
    public override void OnPointerEnter(PointerEventData eventData)
    {
        MouseController.instance.Clickable();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        MouseController.instance.Default();
    }

}
