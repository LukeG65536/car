using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CP : MonoBehaviour
{
    public Vector3 offset;

    public Vector3 getCPPos()
    {
        return transform.position + offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.parent.gameObject.GetComponent<Respawnanator>().currentCP = this;
        }
    }
}
