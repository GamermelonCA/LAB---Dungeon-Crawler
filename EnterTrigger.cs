using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnterTrigger : MonoBehaviour
{
    //public GameObject[] Executables;
    public GameObject DoorToClose;
    public GameObject ObjectToActivate;
    public bool HasBeenActivated = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && HasBeenActivated == false)
        {
            DoorToClose.SetActive(true);
            DoorToClose.SendMessage("Close");

            ObjectToActivate.SendMessage("UniversalActivate");

            HasBeenActivated = true;
        }
  
    }

    public void LevelReset()
    {
        HasBeenActivated = false;
    }
}
