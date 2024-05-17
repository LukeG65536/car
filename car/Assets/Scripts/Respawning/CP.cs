using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CP : MonoBehaviour
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
            
            if (start) timer.startRace();
            if (finish) timer.endRace();
        }
    }
}
