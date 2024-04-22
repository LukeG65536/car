using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMotionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public WheelManager FR;
    public WheelManager FL;
    public WheelManager BR;
    public WheelManager BL;

    public float turn;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
