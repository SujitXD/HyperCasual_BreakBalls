using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    #region Singleton
    public static CameraShake instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    public float xMin, xMax, yMin, yMax;
    public Vector3 originalPos;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //StartCoroutine(Shake(0.15f, 0.4f, 1f));
        }
        originalPos = transform.position;
    }
    public IEnumerator Shake(float duration, float magnitudeX, float magnitudeY)
    {
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(xMin, xMax) * magnitudeX;
            float y = Random.Range(yMin, yMax) * magnitudeY;
            
            //float x = Mathf.PerlinNoise(-1f, 1f) * magnitude;
            //float y = Mathf.PerlinNoise(-1f, 1f) * magnitude;

            transform.position = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPos;

    }
}