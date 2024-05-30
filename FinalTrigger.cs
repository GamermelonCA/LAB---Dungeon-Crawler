using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTrigger : MonoBehaviour
{
    public GameObject Door;
    public KeySystem keySystem;
    public bool Used = false;
    public AudioSource ExplosionSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && Used == false)
        {
            if (keySystem.CraftedExplosives == true)
            {
                Door.SendMessage("Open");
                keySystem.SpawnFinalTreasure();
                Used = true;
                ExplosionSound.Play(0);
            }
        }
    }

    public void LevelReset()
    {
        Used = false;
        Door.SetActive(true);
        Door.SendMessage("LReset");
    }
}
