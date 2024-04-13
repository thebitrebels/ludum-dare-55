using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class Clickable : MonoBehaviour
{

    public void OnMouseEnter()
    {
        MouseController.instance.Clickable();
    }

    public void OnMouseExit()
    {
        MouseController.instance.Default();
    }
}
