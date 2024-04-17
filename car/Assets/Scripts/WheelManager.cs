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

        Debug.Log(transform.InverseTransformDirection(rb.velocity));

        lastFramePos = transform.position;

        RaycastHit hit;

        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.5f)){
            
            return;
        }

        //Vector3 localVel = transform.InverseTransformDirection(currentVelGlobal);

        Vector3 localVel = transform.InverseTransformDirection(rb.GetPointVelocity(transform.position));

        float sidewaysForce = localVel.z;

        Debug.Log(sidewaysForce);

        float accel = Input.GetAxis("Vertical");

        if (accel == 0) accel = -localVel.x * 0.1f;

        accel = Mathf.Clamp(accel, -1, 1) * (hit.distance < 1.2 ? 1:0);

        Vector3 localForce = new Vector3(2 * accel, 1.5f-hit.distance-0.2f, -sidewaysForce);

        Vector3 globalForce = transform.TransformDirection(localForce);


        rb.AddForceAtPosition(globalForce, transform.position, ForceMode.Acceleration);


        //rb.AddRelativeTorque(que);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("trigger enter");
    }
    
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("exit");
        currentForce = Vector3.zero;
    }

    private void OnTriggerStay(Collider other)
    {

    }
}
