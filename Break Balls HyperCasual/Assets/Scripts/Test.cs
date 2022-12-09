using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    public float point1, point2, speed;
    public float xOffset;
    
    // Start is called before the first frame update
    void Start()
    {
      

        //transform.DOLocalMoveX(transform.localPosition.x + xOffset, speed).SetEase(Ease.InOutBounce).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.x < point2)
        {
           transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition + new Vector3(xOffset, 0f, 0f),speed*Time.deltaTime);

        }
    }
}
