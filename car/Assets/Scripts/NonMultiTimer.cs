using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class NonMultiTimer : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI board;
    public float globalBest = 9999f;
    public float bestTime = 9999f;
    public float currentTime = 0f;
    public bool inRace = false;

    public bool multiplayer = false;

    public ColorManager colorManager;
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();



        colorManager = GetComponent<ColorManager>();

        foreach (var obj in GameObject.FindGameObjectsWithTag("Respawn"))
        {
            obj.GetComponent<NonMultiCP>().timer = this;
            obj.GetComponent<CP>().timer2 = this;
        }
    }

    public void startRace()
    {
        inRace = true;
    }

    public void endRace()
    {

        inRace = false;

        if (currentTime == 0) return;


        if (currentTime < bestTime)
        {
            bestTime = currentTime;
        }

        currentTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRace) currentTime += Time.deltaTime;
        if (currentTime > 0) text.text = "Current Time: " + currentTime.ToString();
        else text.text = "PB: " + bestTime.ToString();
    }
}
