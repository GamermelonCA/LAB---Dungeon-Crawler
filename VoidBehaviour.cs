using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidBehaviour : MonoBehaviour
{
    public GameObject StartPad;
    public GameObject Player;
    public KeySystem keySystem;
    public float DelayTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {//temporary measure ---> replace with level system
            PlayerMovement3D playerMovement3D = collider.GetComponent<PlayerMovement3D>();
        
            Debug.Log("RESET COLIDER POSITION");
            playerMovement3D.AllowMovement = false;
            collider.transform.position = StartPad.transform.position + new Vector3(0, 1, 0);
            keySystem.TriggerGameover();
        }
        else
        {
            Destroy(collider.gameObject);
        }
    }

    public void ForceResetManuel()
    {
        //temporary measure ---> replace with level system
        Debug.Log("Test");
        PlayerMovement3D playerMovement3D = Player.GetComponent<PlayerMovement3D>();

        playerMovement3D.AllowMovement = false;
        Player.transform.position = StartPad.transform.position + new Vector3(0, 1, 0);

    }
}
