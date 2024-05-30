using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDissabler : MonoBehaviour
{
    public GameObject ThisObject;
    
    //Repeater Using Children Option
    public void BroadcastCardLock(bool AllowMovement, string CardCategory)
    {
        Debug.Log("Message Repeter");
        if(ThisObject.transform.childCount > 0)
        {
            BroadcastMessage("SetName", CardCategory);
            BroadcastMessage("CardLock", AllowMovement) ;
        }
    }
}
