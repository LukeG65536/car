using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NonMultiCP : MonoBehaviour
{
    public Vector3 offset;

    public bool start;

    public bool finish;

    public NonMultiTimer timer;

    public CP cp;

    public Vector3 getCPPos()
    {
        return transform.position + offset;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Respawnanator respawnanator = other.transform.parent.gameObject.GetComponent<Respawnanator>();

            if (!finish) respawnanator.currentCP = cp;
            //respawnanator.respawn();


            if (start) timer.startRace();
            if (finish) timer.endRace();
        }
    }
}
