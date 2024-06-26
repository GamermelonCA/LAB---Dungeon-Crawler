using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public KeySystem KeySystem;
    public string Object;
    
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
            KeySystem.PickUpObject(Object);
            gameObject.SetActive(false);
        }
    }
}
