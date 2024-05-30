using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieSpawnerScript : MonoBehaviour
{
    public GameObject RoomZone;
    //public GameObject EnemieToCopy;
    public GameObject EnemiePref;
    public GameObject SpawnObject;
    public bool SpawnEnemies;
    public int Health = 50;
    private int ResetHealth;
    private float CurrentTime;
    private float NextTime = 15f;
    private float EndTime;
    public GameObject player;
    private bool WillTakeDamage = true;
    public bool CanTakeDamage = true;
    private bool DamageCooldown = false;
    private float DamageCooldownEndTime;
    private float DamageCooldownNextTime = 0.6f;
    private float DamageCooldownVisualTime = 0.5f; //minus offset from above
    public GameObject InsideSphere;
    public Material NormalMat;
    public Material DamageMat;
    //public RoomBehaviour roomBehvaiour;
    public int WaveSpawnAmount;
    public int AttackSpawnAmount;
    public AudioSource DamageSound;

    
    // Start is called before the first frame update
    void Start()
    {
        ResetHealth = Health;
        Spawn(WaveSpawnAmount);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime = Time.time;

        if (SpawnEnemies == true)
        {
            if (CurrentTime > EndTime)
            {
                EndTime = CurrentTime + NextTime;
                Spawn(WaveSpawnAmount);
            }
        }

        if (DamageCooldown == true)
        {
            if (CurrentTime > DamageCooldownEndTime)
            {
                DamageCooldownEndTime = CurrentTime + DamageCooldownNextTime;
                WillTakeDamage = true;
                DamageCooldown = false; 
            }

            if (CurrentTime > DamageCooldownEndTime - DamageCooldownVisualTime)
            {
                InsideSphere.GetComponent<MeshRenderer>().material = NormalMat;
            }

        }
    }

    public void Spawn(int amount)
    {
        if (SpawnObject.transform.childCount < 10)
        {
        
        for (int i = 0; i < amount; i++)
        {
            var RandX = Random.Range((RoomZone.transform.position.x) - (RoomZone.transform.localScale.x / 2), RoomZone.transform.position.x + RoomZone.transform.localScale.x / 2);
            var RandZ = Random.Range((RoomZone.transform.position.z) - (RoomZone.transform.localScale.z / 2), RoomZone.transform.position.z + RoomZone.transform.localScale.z / 2);
        
            var EnemieToSpawn = Instantiate(EnemiePref, new Vector3(RandX, RoomZone.transform.position.y, RandZ), Quaternion.Euler(0,0,0), SpawnObject.transform);       
            var EnemieBehaviour = EnemieToSpawn.GetComponent<EnemieBehaviour>();
            EnemieBehaviour.Player = player;
            EnemieBehaviour.Activate();

            //EnemieBehaviour.AI = true;
            //EnemieBehaviour.CanTakeDamage = true;

            //EnemieToSpawn.SendMessage("Enable");
        }

        }
    }

    public void UniversalActivate()
    {
        EndTime = CurrentTime + NextTime;
        SpawnEnemies = true;
    }

    public void UniversalDeactivate()
    {

    }

    public void OnTriggerEnter()
    {
        Debug.Log("test");
    }


    public void OnTriggerStay(Collider CollisionObject)
    {
        Debug.Log("Collison with object");
        if (CollisionObject.gameObject.name == "AttackTrigger")
        {
            Debug.Log("AttackTiggerEnter");
            var playerMovement3D = player.GetComponent<PlayerMovement3D>();

            if (playerMovement3D.State == "Attack")
            {
                Debug.Log("PlayerInAttackMode >:D");
                
                if (WillTakeDamage == true && CanTakeDamage == true)
                {
                    Debug.Log("Will Take Damage, Can Take Damage");
                    DamageCooldownEndTime = CurrentTime + DamageCooldownNextTime;
                    
                    WillTakeDamage = false;
                    DamageCooldown = true;

                    Health = Health - playerMovement3D.ActiveAttackDamage;
                    DamageSound.Play(0);

                    InsideSphere.GetComponent<MeshRenderer>().material = DamageMat;

                    Spawn(AttackSpawnAmount);

                    if (Health <= 0)
                    {

                        //Debug.Log("Activate Extrnaly");
                        //roomBehvaiour.TriggerCompleation();
                        //roomBehvaiour.SpawnTreasureGold(8);
                        //roomBehvaiour.SpawnTreasureBlue(5);

                        Debug.Log("Death");
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    public void LevelReset()
    {
        Health = ResetHealth;
    }
}
