using UnityEngine;

public class SummonHint : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        FindObjectOfType<PlayerController>().freezePlayer = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Input.GetKeyUp(KeyCode.Space) && !Input.GetKeyUp(KeyCode.Return)) return;
        FindObjectOfType<PlayerController>().freezePlayer = false;
        Destroy(gameObject);
    }
}
