using UnityEngine.EventSystems;

public class CustomEvents : EventTrigger

{
    public override void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<MouseController>().Clickable();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<MouseController>().Default();
    }

}
