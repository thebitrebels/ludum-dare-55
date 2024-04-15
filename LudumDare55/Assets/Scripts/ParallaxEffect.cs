using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{

    public float strength = 1f;

    private float _currentCameraX;
    
    // Start is called before the first frame update
    private void Start()
    {
        _currentCameraX = Camera.main.transform.position.x;
    }

    // Update is called once per frame
    private void Update()
    {
        var newDeltaX = Camera.main.transform.position.x;
        var deltaX = _currentCameraX - newDeltaX;
        transform.Translate(deltaX * strength, 0f, 0f);
        _currentCameraX = newDeltaX;
    }
}
