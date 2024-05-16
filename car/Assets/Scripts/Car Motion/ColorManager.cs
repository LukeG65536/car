using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    public Material mat;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("ColorManager").GetComponent<buttonThingyColor>().colorManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setColorRandom()
    {
        color = Random.ColorHSV();
        color.a = 1;
        mat.color = color;
        setColorServer(color.r, color.g, color.b);
    }

    [ServerRpc]
    public void setColorServer(float r, float g, float b)
    {
        setColorClient(r, g, b);
    }

    [ClientRpc]
    public void setColorClient(float r, float g, float b)
    {
        color = new Color(r, g, b);
        mat.color = color;
    }


}
