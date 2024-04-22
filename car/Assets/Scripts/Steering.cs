using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public GameObject wheel1;
    public GameObject wheel2;
    public GameObject wheel3;
    public GameObject wheel4;
    public float turnSpeed = 0.2f;
    private Rigidbody rb;
    public float tireAngle = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {

        float speed = transform.InverseTransformDirection(rb.velocity).x;

        float rotPer2nd = 0.2f;

        float c = speed * rotPer2nd;

        float r = c / 2 * Mathf.PI;

        Debug.Log("speed, radius:  " + speed + ", " + r);

        float angleRad = Mathf.Atan(2/r);

        tireAngle = Mathf.Rad2Deg * angleRad;

        //Debug.Log(tireAngle);
    }

    private void FixedUpdate()
    {
        updateTires();
    }


    private void updateTires()
    {
        Vector3 rot = wheel1.transform.localEulerAngles;
        

        float input = Input.GetAxis("Horizontal");

        rot.y = input * turnSpeed * Mathf.Clamp(tireAngle, -20, 20) + input/2;

        wheel1.transform.localEulerAngles = rot;
        wheel2.transform.localEulerAngles = rot;
        wheel3.transform.localEulerAngles = -rot;
        wheel4.transform.localEulerAngles = -rot;
    }
}
