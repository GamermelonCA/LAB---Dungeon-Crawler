using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehaviour : MonoBehaviour
{
    public GameObject[] Door;
    public GameObject player;
    public GameObject SwitchOrb;
    public GameObject SwitchTrigger;
    public Material InactiveOrbMat;
    public Material ActiveOrbMat;
    public bool SingleUse;
    public bool Active = false;
    public bool SwapState = true;

    private float CurrentTime;
    private float NextTime = 0.5f;
    private float EndTime;

    public AudioSource InteractSound;

    public PlayerMovement3D playerMovement3D;
    //EXPANDABLE THINGY TO CALL FUNCTIONS FROM ANOTHER GAMEOBJECT
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime = Time.time;
        
        if (SwapState == false)
        {
            if (CurrentTime > EndTime)
            {
                SwapState = true;
            }
        }
    }

    public void SwitchActiveState()
    {
        if (SingleUse == false)
        {
            if (Active == false)
            {
            Active = true;
            //set the material of the orb to refelcect what state it is
            SwitchOrb.GetComponent<MeshRenderer> ().material = ActiveOrbMat;

            for (int i = 0; i < Door.Length; i++)
            {
                DoorBehaviour doorBehaviour = Door[i].GetComponent<DoorBehaviour>();
                
                doorBehaviour.Open();
                //Door[i].SetActive(false);
                InteractSound.Play(0);
            }

            }
            else if(Active == true)
            {
            Active = false;
            //set the material of the orb to refelcect what state it is
            SwitchOrb.GetComponent<MeshRenderer> ().material = InactiveOrbMat;

            for (int i = 0; i < Door.Length; i++)
            {
                DoorBehaviour doorBehaviour = Door[i].GetComponent<DoorBehaviour>();
                
                Door[i].SetActive(true);
                doorBehaviour.Close();
                InteractSound.Play(0);
            }

            }
        }
        else if(SingleUse == true)
        {
            if (Active == false)
            {
            Active = true;
            //set the material of the orb to refelcect what state it is
            SwitchOrb.GetComponent<MeshRenderer> ().material = ActiveOrbMat;

            for (int i = 0; i < Door.Length; i++)
            {
                DoorBehaviour doorBehaviour = Door[i].GetComponent<DoorBehaviour>();
                
                doorBehaviour.Open();
                //Door[i].SetActive(false);
                InteractSound.Play(0);
            }

            }
            else if(Active == true)
            {
                //end
            }
        }
        
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Trigger Was Entered");

        if (SwapState == true)
        {
            if (playerMovement3D.State == "Attack" || collider.gameObject.name == "Arrow Projectile(Clone)")
            {
                SwitchActiveState();
                EndTime = CurrentTime + NextTime;
                SwapState = false;
            }
        }

    }

    void OnTriggerStay(Collider collider)
    {
        Debug.Log("Inside Trigger");

        if (SwapState == true)
        {
            if (playerMovement3D.State == "Attack" || collider.gameObject.name == "Arrow Projectile(Clone)")
            {
                SwitchActiveState();
                EndTime = CurrentTime + NextTime;
                SwapState = false;
            }
        }
    }

    void OnTriggerExit()
    {
        //SwapState = true;
    }

    public void LevelReset()
    {
        if (Active == true)
        {
            Active = false;
            SwitchOrb.GetComponent<MeshRenderer> ().material = InactiveOrbMat;

            for (int i = 0; i < Door.Length; i++)
            {
                Door[i].SetActive(true);
                Door[i].SendMessage("LReset", "e");
            }

        }
    }
}
