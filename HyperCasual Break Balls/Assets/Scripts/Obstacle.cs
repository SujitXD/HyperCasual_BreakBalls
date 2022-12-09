using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float lifeTime = 5f;
    public float magnitude;

    public void Shatter()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);

        Destroy(gameObject, lifeTime);
    }
}
