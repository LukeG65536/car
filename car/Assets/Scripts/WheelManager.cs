using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelManager : MonoBehaviour
{
    private Vector3 currentForce = Vector3.zero;
    private Vector3 lastFramePos;
    private Vector3 currentVelGlobal = Vector3.zero;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        lastFramePos = transform.position;
    }

    private void FixedUpdate()
    {

        currentVelGlobal = (transform.position - lastFramePos) / Time.deltaTime;

        Debug.Log(currentVelGlobal);

        lastFramePos = transform.position;


        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1.1f)){
            return;
        }


        Vector3 localVel = transform.InverseTransformDirection(currentVelGlobal);

        float sidewaysForce = localVel.z;

        Vector3 localForce = new Vector3(1, 0, -100f * sidewaysForce);

        Vector3 globalForce = transform.TransformDirection(localForce);

        rb.AddForceAtPosition(globalForce, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");
        currentForce = Vector3.zero;
    }

    private void OnTriggerStay(Collider other)
    {

    }
}
