using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    public KeySystem keySystem;
    public string TypeOfChest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            keySystem.OpenChest(TypeOfChest);
        }
        
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
