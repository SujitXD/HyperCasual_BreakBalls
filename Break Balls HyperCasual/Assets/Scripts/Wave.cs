using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Wave : MonoBehaviour
{
    public float bobSpeed = 1f;
    public float bobHeight = 0.5f;

    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        transform.position = _startPosition + new Vector3(0f, Mathf.Sin(Time.time * bobSpeed) * bobHeight, 0f);
    }
}
