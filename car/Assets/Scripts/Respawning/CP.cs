using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CP : NetworkBehaviour
{
    public Vector3 offset;

    public bool start;

    public bool finish;

    public Timer timer;

    public Vector3 getCPPos()
    {
        return transform.position + offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Player")
        {
            Respawnanator respawnanator = other.transform.parent.gameObject.GetComponent<Respawnanator>();
            if(!finish)respawnanator.currentCP = this;
            //respawnanator.respawn();

            if (timer.OwnerClientId != OwnerClientId) return;

            if (start) timer.startRace();
            if (finish) timer.endRace();
        }
    }
}
