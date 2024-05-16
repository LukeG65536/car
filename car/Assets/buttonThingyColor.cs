using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonThingyColor : MonoBehaviour
{
    public ColorManager colorManager;
    public void randomColorFr()
    {
        colorManager.setColorRandom();
    }
}
