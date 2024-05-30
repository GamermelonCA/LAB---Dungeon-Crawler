using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DoorBehaviour : MonoBehaviour
{
    private bool DoOpen;
    private bool DoClose;
    private bool IsOpen;
    private float OpenPosition;
    private float ClosedPosition;

    public AudioSource DoorSound;
    //public GameObject ThisObject;
    // Start is called before the first frame update
    void Start()
    {
        ClosedPosition = transform.position.y;
        OpenPosition = transform.position.y - 2.7f;
    }

    // Update is called once per frame
    void Update()
    {
        if (DoOpen == true)
        {
            transform.position = transform.position + new Vector3(0, -(transform.localScale.y / 5), 0);

            if (transform.position.y < OpenPosition)
            {
                transform.position = new Vector3(transform.position.x, OpenPosition, transform.position.z);
            }

            if (DoorSound.isPlaying == false)
            {
                DoOpen = false;
                gameObject.SetActive(false);
            }

        }

        if (DoClose == true)
        {
            transform.position = transform.position + new Vector3(0, (transform.localScale.y / 10), 0);

            if (transform.position.y > ClosedPosition)
            {
                transform.position = new Vector3(transform.position.x, ClosedPosition, transform.position.z);
                DoClose = false;
            }
        }
        
    }

    public void Open()
    {
        Debug.Log("Open");
        //OpenPosition = transform.position.y - 2.7f;
        DoOpen = true;
        DoClose = false;
        //transform.position = transform.position + new Vector3(0, -(transform.localScale.y / 10), 0);
        DoorSound.Play(0);
    }

    public void Close()
    {
        Debug.Log("Door Inlevel Close: " + gameObject.name);
        Debug.Log("Close");
        //ClosedPosition = transform.position.y + 2.7f;
        DoClose = true;
        DoOpen = false;
        //transform.position = transform.position + new Vector3(0, transform.localScale.y / 10, 0);
        DoorSound.Play(0);
    }

    public void LReset()
    {
        Debug.Log("Door Level Reset");
        transform.position = new Vector3(transform.position.x, ClosedPosition, transform.position.z);
    }

}
