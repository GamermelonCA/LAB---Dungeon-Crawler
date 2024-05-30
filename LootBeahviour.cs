using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBeahviour : MonoBehaviour
{
    private float TimeUntillSuicideGold = 90f;
    private float TimeUntillSuicideBlue = 60f;
    private float CountdownUntillYouJump = 0.0f;
    public GameObject ThisObject;
    private int SelfInflictedDeath = 0;
    public KeySystem keysystem;

    // Update is called once per frame
    void Update()
    {
        if (ThisObject.name == "TreasureGoldTemp(Clone)")
        {
            if (Time.time > CountdownUntillYouJump)
            {
                //ive tried to understand this but i dont =D
                CountdownUntillYouJump = Time.time + TimeUntillSuicideGold;

                if (SelfInflictedDeath == 1)
                {
                    //kills itself
                    Object.Destroy(ThisObject);
                }
                else if (SelfInflictedDeath == 0)
                {
                    //Causes object to want to kill itself
                    SelfInflictedDeath = 1;
                }
                
            }
        }

        if (ThisObject.name == "TreasureBlueTemp(Clone)")
        {
            if (Time.time > CountdownUntillYouJump)
            {
                CountdownUntillYouJump = Time.time + TimeUntillSuicideBlue;

                if (SelfInflictedDeath == 1)
                {
                    //kills itself
                    Object.Destroy(ThisObject);
                }
                else if (SelfInflictedDeath == 0)
                {
                    //Causes object to want to kill itself
                    SelfInflictedDeath = 1;
                }
                
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (ThisObject.name == "TreasureGoldTemp(Clone)" && collision.gameObject.name == "Player")
        {
            keysystem.AddTreasureGold(1);
            Object.Destroy(ThisObject);
        }
        if (ThisObject.name == "TreasureBlueTemp(Clone)" && collision.gameObject.name == "Player")
        {
            keysystem.AddTreasureBlue(1);
            Object.Destroy(ThisObject);
        }
    }

    public void LevelReset()
    {
        Object.Destroy(gameObject);
    }
}
