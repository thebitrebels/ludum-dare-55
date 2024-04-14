using System;
using UnityEngine;
using UnityEngine.Events;

public class ResourceManager : MonoBehaviour
{
    public int RedResource = 5;
    public int YellowResource = 5;
    public int BlueResource = 5;

    public UnityEvent<int> RedEvent;
    public UnityEvent<int> YellowEvent;
    public UnityEvent<int> BlueEvent;

    public static ResourceManager instance;

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
        SetRed(RedResource);
        SetYellow(YellowResource);
        SetBlue(BlueResource);
    }


    public void SetRed(int red)
    {
        RedResource = red;
        RedEvent.Invoke(RedResource);
    }

    public void SetYellow(int yellow) {  
        YellowResource = yellow;
        YellowEvent.Invoke(YellowResource);
    }

    public void SetBlue(int blue)
    {
        BlueResource = blue;
        BlueEvent.Invoke(BlueResource);
    }

    public bool CanPerformSummoning(int costRed, int costYellow, int costBlue)
    {
        return RedResource >= costRed
            && YellowResource >= costYellow
            && BlueResource >= costBlue;
    }

    public void PerformSummoning(int costRed, int costYellow, int costBlue)
    {
        SetRed(RedResource - costRed); 
        SetYellow(YellowResource - costYellow); 
        SetBlue(BlueResource - costBlue);
    }
}
