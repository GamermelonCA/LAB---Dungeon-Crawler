using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EletricalGoop : MonoBehaviour
{
    public Vector3 ResetPostion;
    public int AmountToDamage;
    
    private bool CurrentlyEletricified = false;
    public float StartingOffset;
    private float CurrentTime;
    private float EndTime;
    private float ActiveTime = 0.65f;
    private float InactiveTime = 1.1f;
    public GameObject Player;

    public Material NormalMat;
    public Material EletricMat;

    public AudioSource EletricitySound;
    // Start is called before the first frame update
    void Start()
    {
        EndTime = CurrentTime + StartingOffset;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime = Time.time;

        if (CurrentlyEletricified == false)
        {
            if (CurrentTime > EndTime)
            {
                EndTime = CurrentTime + ActiveTime;
                CurrentlyEletricified = true;
                gameObject.GetComponent<MeshRenderer>().material = EletricMat;

                var DistanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
                if (DistanceToPlayer < 10)
                {
                    EletricitySound.Play(0);
                }    
            }
        }
        if (CurrentlyEletricified == true)
        {
            if (CurrentTime > EndTime)
            {
                EndTime = CurrentTime + InactiveTime;
                CurrentlyEletricified = false;
                gameObject.GetComponent<MeshRenderer>().material = NormalMat;
            }
        }
    }

    public void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.name == "Player" && CurrentlyEletricified == true)
        {
            Debug.Log("TellPlayerToHurt >:D");
            var PlayerMovement3D = collision.gameObject.GetComponent<PlayerMovement3D>();
            PlayerMovement3D.StopMovement();
            collision.gameObject.transform.position = ResetPostion;
            PlayerMovement3D.AtemptToDamagePlayer(AmountToDamage);

            Debug.Log("TellThePlayerToPause");
            

            Debug.Log("TellThePlayerToMove");
            

        }
        else if ((collision.gameObject.name == "Enemie Prefab(Clone)" || collision.gameObject.name == "Hard Enemie Varient(Clone)") && CurrentlyEletricified == true)
        {
            Object.Destroy(collision.gameObject);
        }
        else 
        {
            //Debug.Log(collision.gameObject.name);
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            var PlayerMovement3D = collision.gameObject.GetComponent<PlayerMovement3D>();
            PlayerMovement3D.StartMovement();
        }
    }
}
