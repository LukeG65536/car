using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class Timer : NetworkBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI board;
    public float globalBest = 9999f;
    public float bestTime = 9999f;
    public bool inRace = false;
    public float currentTime = 0f;

    public bool multiplayer = false;

    public ColorManager colorManager;
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();

        board = GameObject.Find("Scoreboard").GetComponent<TextMeshProUGUI>();

        if (!IsOwner) return;

        colorManager = GetComponent<ColorManager>();

        foreach(var obj in GameObject.FindGameObjectsWithTag("Respawn"))
        {
            obj.GetComponent<CP>().timer = this;
        }
    }

    public void startRace()
    {
        if (!IsOwner) return;
        inRace = true;
    }

    public void endRace()
    {
        if (!IsOwner) return;

        
        inRace = false;

        if (currentTime == 0) return;


        if (currentTime < bestTime)
        {
            bestTime = currentTime;

            if(bestTime < globalBest)
            {
                updateGlobalTimeServerRpc(colorManager.color.r, colorManager.color.g, colorManager.color.b, bestTime);
            }
        }

        currentTime = 0f;
    }

    [ServerRpc]
    public void updateGlobalTimeServerRpc(float r, float g, float b, float time)
    {
        updateGlobalTimeClientRpc(r, g, b, time);
    }

    [ClientRpc]
    public void updateGlobalTimeClientRpc(float r, float g, float b, float time)
    {
        globalBest = time;
        board.text = "Best Time" + time.ToString();
        board.color = new Color(r, g, b);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        if (inRace) currentTime += Time.deltaTime;
        if(currentTime > 0) text.text = "Current Time: " + currentTime.ToString();
        else text.text = "PB: " + bestTime.ToString();
    }
}
