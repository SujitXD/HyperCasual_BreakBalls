using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPosition : MonoBehaviour
{
    float tRot = 0.0f; // lerp val 

    public float minPos = -90.0F;
    public float maxPos = 90.0F;
    [Range(0f, 5f)]
    public float smoothPosLerp;
    public bool smoothTranslateX , smoothTranslateY, smoothTranslateZ, autoLoopLerp;
    public float switchLerpTime = 1f;

    void Update()
    {
        if (smoothTranslateX)
        {
            transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, maxPos, tRot), transform.localPosition.y, transform.localPosition.z);

            tRot += smoothPosLerp * Time.deltaTime;

            if (tRot > switchLerpTime)
            {
                tRot = 0.0f;
                if(autoLoopLerp)
                {
                    float tempRot = maxPos;
                    maxPos = minPos;
                    minPos = tempRot;
                    //smoothTranslateX = false;
                }
            }
        }
        else if (smoothTranslateY)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, maxPos, tRot), transform.localPosition.z);

            tRot += smoothPosLerp * Time.deltaTime;

            if (tRot > switchLerpTime)
            {
                tRot = 0.0f;
                if (autoLoopLerp)
                {
                    float tempRot = maxPos;
                    maxPos = minPos;
                    minPos = tempRot;
                    //smoothTranslateZ = false;
                }
            }
        }
        else if (smoothTranslateZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(transform.position.z, maxPos, tRot));

            tRot += smoothPosLerp * Time.deltaTime;

            if (tRot > switchLerpTime)
            {
                tRot = 0.0f;
                if (autoLoopLerp)
                {
                    float tempRot = maxPos;
                    maxPos = minPos;
                    minPos = tempRot;
                    //smoothTranslateZ = false;
                }
            }
        }
    }
}
