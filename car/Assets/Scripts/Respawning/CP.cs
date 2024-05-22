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

    public NonMultiTimer timer2;

    public bool multiplayer;

    public Vector3 getCPPos()
    {
        return transform.position + offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Player")
        {
            if(!multiplayer)
            {
                Respawnanator respawnanator2 = other.transform.parent.gameObject.GetComponent<Respawnanator>();

                if (!finish) respawnanator2.currentCP = this;

                if (start) timer2.startRace();
                if (finish) timer2.endRace();
                return;
            }

            Respawnanator respawnanator = other.transform.parent.gameObject.GetComponent<Respawnanator>();
            if(!finish)respawnanator.currentCP = this;
            //respawnanator.respawn();

            if (timer.OwnerClientId != OwnerClientId) return;

            if (start) timer.startRace();
            if (finish) timer.endRace();
        }
    }
}
