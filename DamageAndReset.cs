using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAndReset : MonoBehaviour
{
    public Vector3 ResetPostion;
    public int AmountToDamage;
    public bool ResetPlayer;
    public bool KillEnemie;
    
    
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
        if (collision.gameObject.name == "Player" && ResetPlayer == true)
        {
            Debug.Log("TellPlayerToHurt >:D");
            var PlayerMovement3D = collision.gameObject.GetComponent<PlayerMovement3D>();
            PlayerMovement3D.AtemptToDamagePlayer(AmountToDamage);

            Debug.Log("TellThePlayerToPause");
            PlayerMovement3D.StopMovement();

            Debug.Log("TellThePlayerToMove");
            collision.gameObject.transform.position = ResetPostion;

        }
        else if ((collision.gameObject.name == "Enemie Prefab(Clone)" || collision.gameObject.name == "Hard Enemie Varient(Clone)") && KillEnemie == true)
        {
            Object.Destroy(collision.gameObject);
        }
        else 
        {
            Debug.Log(collision.gameObject.name);
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Player" && ResetPlayer == true)
        {
            var PlayerMovement3D = collision.gameObject.GetComponent<PlayerMovement3D>();
            PlayerMovement3D.StartMovement();
        }
    }
}
