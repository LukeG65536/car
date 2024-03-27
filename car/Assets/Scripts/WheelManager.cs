using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelManager : MonoBehaviour
{
    private Vector3 currentForce = Vector3.zero;
    private Vector3 lastFramePos;
    private Vector3 currentVel = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        lastFramePos = transform.position;
    }

    private void Update()
    {

        currentVel = 

        lastFramePos = transform.position;
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
        currentForce = Vector3.forward;
    }
}
