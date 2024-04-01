using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public GameObject wheel1;
    public GameObject wheel2;
    public GameObject wheel3;
    public GameObject wheel4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float ang = 0;
        if (Input.GetKey(KeyCode.A)) ang -= 11;
        if (Input.GetKey(KeyCode.D)) ang += 11;
        Vector3 rot = wheel1.transform.localEulerAngles;
        rot.y = ang;
        wheel1.transform.localEulerAngles = rot;
        wheel2.transform.localEulerAngles = rot;
        wheel3.transform.localEulerAngles = -rot;
        wheel4.transform.localEulerAngles = -rot;
    }
}
