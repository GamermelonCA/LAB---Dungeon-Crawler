using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private float EndTime;
    private float NextTime = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Arrow!");
        EndTime = NextTime + Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * 17;

        if (Time.time > EndTime)
        {
            Object.Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.layer != 7)
        {
            Object.Destroy(gameObject);
            Debug.Log("Arrow hit :" + collider.gameObject.name);
        }
    }
}
