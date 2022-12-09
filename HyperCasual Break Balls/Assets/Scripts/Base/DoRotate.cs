using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoRotate : MonoBehaviour
{
    public bool upwardRot, xRot, yRot, zRot,hammerRot, hammerRot2;
    public float rotSpeed, moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (hammerRot)
        {            
            if (transform.localEulerAngles.z == 300f)
            {
                transform.DOLocalRotate(new Vector3(0f, 0f, 60f), 2f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

            }
            else
            {
                transform.DOLocalRotate(new Vector3(0f, 0f, -60f), 2f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

            }

        }
        if (hammerRot2)
        {
            //transform.DOLocalRotate(new Vector3(60f, 0f, -60f), 2f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            //transform.DOLocalRotate(new Vector3(-30f, transform.localEulerAngles.y, 60f), 2f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            float angle = transform.localEulerAngles.z;
            DOTween.To(() => angle, x => angle = x, 60f, 1f);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (xRot)
        {
            transform.Rotate(rotSpeed * Time.deltaTime, 0f,  0f);
            //transform.Rotate(Vector3.right * rotSpeed * Time.deltaTime);

            //transform.DOLocalRotate(new Vector3(360f, 0f, 0f), 2f, RotateMode.Fast).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        }
        if (yRot)
        {
            transform.Rotate(0f, rotSpeed * Time.deltaTime, 0f);
           
        }
        if (zRot)
        {
           transform.Rotate(0f, 0f, rotSpeed * Time.deltaTime);
           
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (upwardRot)
            {
                transform.DORotate(new Vector3(0f, 0f, 0f), moveSpeed);
            }
            else
            {
                transform.DORotate(new Vector3(180f, 0f, 0f), moveSpeed);
            }
            
        }

    }
}
