using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glasswall : MonoBehaviour
{
    public GameObject Glassshards;
    private float CurrentTime;
    private float NextTime = 0.2f;
    private float EndTime;
    private bool DoAnimation = false;
    private float ResetPosX = -4.95f;
    private float ResetPosy = 18.75f;
    private float ResetPosz = 56.3f;
    public PlayerMovement3D playerMovement3D;
    public AudioSource GlassSmashSound;
    
    // Start is called before the first frame update
    void Start()
    {
        Glassshards.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime = Time.time;

        if (DoAnimation == true)
        {
            if (CurrentTime > EndTime)
            {
                Animation();
                EndTime = CurrentTime + NextTime;
            }
        }
    }
    
    public void GameReset()
    {
        gameObject.transform.position = new Vector3(ResetPosX, ResetPosy, ResetPosz);
    }

    public void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.name == "Collision Trigger")
        {
            Debug.Log("Collision =D");
            var PlayerMovement3D = collider.gameObject.transform.parent.gameObject.GetComponent<PlayerMovement3D>();
            if (PlayerMovement3D.State == "Dash")
            {
                DoAnimation = true;
            }
        }
        else
        {
            Debug.Log("Collision D=");
            Debug.Log(collider.gameObject.name + " Hit Me OUCH");
        }
    }

    public void Animation()
    {
        //float downY = -0.1f;
        //gameObject.transform.localPosition = gameObject.transform.localPosition + new Vector3 (0, downY, 0);

        //gameObject.transform.Rotate(0, 5, 0);
        GlassSmashSound.Play(0);

        DoAnimation = false;
        Glassshards.SetActive(true);
        gameObject.SetActive(false);
        playerMovement3D.DamagePlayer(5);
    }

}
