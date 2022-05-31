using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsedTime = 0;

        while(elapsedTime < duration)
        {
            Vector2 randomValue = Random.insideUnitCircle * magnitude;
            transform.localPosition = new Vector3(randomValue.x, randomValue.y, originalPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
