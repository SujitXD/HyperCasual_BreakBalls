using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    public bool x, y,point1Reached,point2Reached;

    public float point1, point2,speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (x)
        {
            if (transform.position.x != point1 && point2Reached)
            {
                if (transform.position.x > point1)
                {
                    point1Reached = true;
                    point2Reached = false;
                }
                transform.Translate(speed * Time.deltaTime, 0f,  0f, Space.World);

            }

            if (transform.position.x != point2 && point1Reached)
            {
                if (transform.position.x < point2)
                {
                    point2Reached = true;
                    point1Reached = false;
                }
                transform.Translate(-speed * Time.deltaTime,0f,  0f, Space.World);

            }
        }
        else if (y)
        {
            if (transform.position.y != point1 && point2Reached)
            {
                if (transform.position.y > point1)
                {
                    point1Reached = true;
                    point2Reached = false;
                }
                transform.Translate(0f, speed*Time.deltaTime, 0f, Space.World);
             
            }
            
            if (transform.position.y != point2 && point1Reached)
            {
                if (transform.position.y < point2)
                {
                    point2Reached = true;
                    point1Reached = false;
                }
                transform.Translate(0f, -speed * Time.deltaTime, 0f, Space.World);
               
            }
        }
    }
}
