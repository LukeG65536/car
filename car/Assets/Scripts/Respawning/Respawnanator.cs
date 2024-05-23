using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawnanator : MonoBehaviour
{
    public CP currentCP;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            respawn();
        }

        if (Input.GetKeyDown(KeyCode.Delete)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void respawn()
    {
        if (currentCP == null)
        {
            Debug.Log("no cp");
            return;
        }

        currentCP.timer.currentTime = 0f;

        transform.position = currentCP.getCPPos();

        transform.rotation = currentCP.transform.rotation;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
