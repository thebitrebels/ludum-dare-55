using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcePanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RewriteText(int newValue)
    {
        TextMeshProUGUI textMesh = GetComponentInChildren<TextMeshProUGUI>();
        textMesh.text = newValue.ToString();
    }
}
