using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : NetworkBehaviour
{
    public Material mat;
    public Color color;

    public MeshRenderer[] meshRenderers;
    // Start is called before the first frame update
    void Start()
    {
        mat = new Material(mat);
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
        color = UnityEngine.Random.ColorHSV();
        color.a = 1;
        mat.color = color;
        setColorServerRpc(color.r, color.g, color.b, NetworkObjectId);
    }

    [ServerRpc(RequireOwnership =false)]
    public void setColorServerRpc(float r, float g, float b,ulong id)
    {
        setColorClientRpc(r, g, b, id);
    }

    [ClientRpc]
    public void setColorClientRpc(float r, float g, float b, ulong id)
    {
        if (NetworkObjectId != id) return;
        color = new Color(r, g, b);
        mat.color = color;

        foreach(MeshRenderer renderer in meshRenderers)
        {
            renderer.material = mat;
        }
    }
}
