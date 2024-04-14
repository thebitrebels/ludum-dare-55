using UnityEngine;
using UnityEngine.Events;

public class ResourceManager : MonoBehaviour
{
    public int RedResource = 0;
    public int YellowResource = 0;
    public int BlueResource = 0;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
