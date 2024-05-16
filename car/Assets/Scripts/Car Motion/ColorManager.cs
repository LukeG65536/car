using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : NetworkBehaviour
{
    public Material mat;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner) return;
        GameObject.Find("ColorManager").GetComponent<buttonThingyColor>().colorManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setColorRandom()
    {
        if (!IsOwner) return;
        color = Random.ColorHSV();
        color.a = 1;
        mat.color = color;
        setColorServerRpc(color.r, color.g, color.b);
    }

    [ServerRpc]
    public void setColorServerRpc(float r, float g, float b)
    {
        setColorClientRpc(r, g, b);
    }

    [ClientRpc]
    public void setColorClientRpc(float r, float g, float b)
    {
        if (IsOwner) return;
        color = new Color(r, g, b);
        mat.color = color;
    }


}
