using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class cubescript : MonoBehaviour
{
    private float CurrentTime;
    private float NextTime = 0.2f;
    private float EndTime;
    private float SoundNextTime = 0.36f;
    private float SoundEndTime;
    private bool CanPlaySound = false;
    private bool CanMoveBox;
    private bool Fallen = false;

    public GameObject BridgeCollision;

    public AudioSource Sound;
    
    
    // Start is called before the first frame update
    void Start()
    {
        BridgeCollision.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x > 18.6f && Fallen == false)
        {
            transform.localPosition = new Vector3(19.54f, 14.35f, transform.localPosition.z);
            EnableCollision();
        }

        CurrentTime = Time.time;
        
        if (CanMoveBox == false)
        {
            if (CurrentTime > EndTime)
            {
                CanMoveBox = true;
            }
        }
        if (CanPlaySound == false)
        {
            if (CurrentTime > SoundEndTime)
            {
                CanPlaySound = true;
            }
        }

        //Debug.Log(transform.position.x);
        //Debug.Log(transform.position.x > 18.5f);
    }

    public void Move()
    {
        Debug.Log("Move Cubes");
        
    }

    public void EnableCollision()
    {
        BridgeCollision.SetActive(true);
        Fallen = true;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Collision Trigger" && transform.localPosition.x < 18.6f && CanMoveBox == true)
        {
            if (CanPlaySound == true)
            {
                Sound.Play(0);
                SoundEndTime = CurrentTime + SoundNextTime;
                CanPlaySound = false;
            }
            transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
            EndTime = CurrentTime + NextTime;
            CanMoveBox = false;
        }
    }

    public void LevelReset()
    {
        BridgeCollision.SetActive(false);
        CanMoveBox = true;
        Fallen = false;
        transform.localPosition = new Vector3(13.732f, 20.064f, 77.21f);
    }
}
