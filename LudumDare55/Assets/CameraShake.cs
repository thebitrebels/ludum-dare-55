using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = 0.5f;
    public float magnitude = 0.5f;
    public IEnumerator Shake()
    {
        var originalPos = transform.localPosition;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            var xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            var yOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            transform.localPosition = new Vector3(xOffset, yOffset, originalPos.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
