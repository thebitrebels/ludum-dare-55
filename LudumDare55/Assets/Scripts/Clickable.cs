using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class Clickable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<MouseController>().Clickable();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<MouseController>().Default();
    }
}
