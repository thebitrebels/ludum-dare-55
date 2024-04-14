using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Clickable : MonoBehaviour
{

    public void OnMouseEnter()
    {
        FindObjectOfType<MouseController>().Clickable();
    }

    public void OnMouseExit()
    {
        FindObjectOfType<MouseController>().Default();
    }
}
