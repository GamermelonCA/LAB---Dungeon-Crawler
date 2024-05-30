using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemieMainControll : MonoBehaviour
{
    public void BroadcastStopAI()
    {
        BroadcastMessage("Deactivate");
    }

    public void BroadcastStartAI()
    {
        BroadcastMessage("Activate");
    }

    public void LevelStart()
    {

    }

    public void LevelReset()
    {
        
    }
}
