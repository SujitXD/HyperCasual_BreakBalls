using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight : MonoBehaviour
{
    public bool rotate;
    [SerializeField] private float rotationMultiplier;
    [SerializeField] private float rotationDist;
    public int remainigConnetions;

    public List<Rope> ropes; //support Ropes

    private void Update()
    {
        if (rotate)
        {
            Vector3 rot = transform.eulerAngles;
            rot.x = Mathf.Sin(Time.time * rotationMultiplier) * rotationDist;
            transform.eulerAngles = rot;
        }
    }

    public float distanceFromChainEnd = 0.6f;

    public void ConnectRopeEnd(Rigidbody endRB, Vector3 anchor)
    {
        HingeJoint joint = gameObject.AddComponent<HingeJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedBody = endRB;
        joint.anchor = anchor;
        joint.connectedAnchor = new Vector3(0f, -distanceFromChainEnd, 0f);

        remainigConnetions--;

        if (remainigConnetions == 0)
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
