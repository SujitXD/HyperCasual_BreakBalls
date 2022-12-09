using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public float dragSpeed;
    public bool isHolding;
   
    public void Start()
    {



    }
    public void Update()
    {
        Vector3 pos = transform.localPosition;

        //The pos.x value works perfectly and is not an issue while moving forward
        pos.x = Mathf.Clamp(pos.x, -90f, 380f);

        transform.localPosition = pos;


        if (Input.GetMouseButtonDown(0))
        {
            isHolding = true;

        }
        
        if (Input.GetMouseButtonUp(0))
        {

            isHolding = false;
        }

        //float horizontalDrag = Mathf.Clamp(-Input.GetAxis("Mouse X"), -90f, 380f);

        if (isHolding  )
        {
            transform.position += new Vector3( 0f, 0f, -Input.GetAxis("Mouse X") * dragSpeed);
        }

        
    }
}
