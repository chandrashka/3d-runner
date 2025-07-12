using System.Collections;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private float shrinkDuration = 0.2f;
    [SerializeField] private float riseHeight = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        StartCoroutine(RiseAndShrink());
    }

    private IEnumerator RiseAndShrink()
    {
        var originalScale = transform.localScale;
        var originalPos = transform.position;
        var elapsed = 0f;

        while (elapsed < shrinkDuration)
        {
            var t = elapsed / shrinkDuration;
            var yOffset = Mathf.Lerp(0f, riseHeight, t);
            var newScale = Vector3.Lerp(originalScale, Vector3.zero, t);

            transform.localScale = newScale;
            transform.position = originalPos + Vector3.up * yOffset;

            elapsed += Time.deltaTime;
            yield return null;
        }
        
        transform.localScale = Vector3.zero;
        transform.position = originalPos + Vector3.up * riseHeight;
        
        gameObject.SetActive(false);
    }
}