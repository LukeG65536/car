using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggleanator : MonoBehaviour
{
    public bool current = true;
    public KeyCode key = KeyCode.Escape;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            current = !current;
            foreach(var obj in transform.GetComponentsInChildren<Button>())
            {
                obj.enabled = current;
                obj.GetComponent<Image>().enabled = current;
                obj.transform.GetChild(0).gameObject.SetActive(current);
            }
        }
    }
}
