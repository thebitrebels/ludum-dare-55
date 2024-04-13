using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Texture2D defaultCursor, clickableCursor, summoningCursor;

    public static MouseController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
       
        }
        
    }

    private void Start()
    {
        Default();
    }

    public void Clickable()
    {
        Cursor.SetCursor(clickableCursor, Vector2.zero, CursorMode.Auto);
    }

    public void Default()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    public void Summoning()
    {
        Cursor.SetCursor(summoningCursor, Vector2.zero, CursorMode.Auto);
    }
}
