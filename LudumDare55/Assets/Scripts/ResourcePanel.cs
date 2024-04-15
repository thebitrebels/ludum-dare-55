using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcePanel : MonoBehaviour
{
    public void RewriteText(int newValue)
    {
        var textMesh = GetComponentInChildren<TextMeshProUGUI>();
        textMesh.text = newValue.ToString();
    }
}
