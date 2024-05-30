using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemieBehaviour : MonoBehaviour
{
    public GameObject Player;
    public bool AI = false;
    private float CurrentTime;
    private float EndTime;
    private float NextTime = 1f;
    public int Health;
    private int ResetHealth;
    private bool DamageCooldown = false;
    private float DamageCooldownEndTime;
    private float DamageCooldownNextTime = 0.6f;
    private float DamageCooldownVisualTime = 0.45f; //minus offset from above
    private bool WillTakeDamage = true;
    private bool AttackCooldown = false;
    private float AttackCooldownEndTime;
    private float AttackCooldownNextTime = 1.5f;
    public bool CanTakeDamage = false;

    public float distanceTOplayer;
    public AudioSource DamageSound;

    public Material NormalMat;
    public Material DamageMat;
    //private bool WillAttack;

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth = Health;
    }

    // Update is called once per frame
    void Update()
    {
        distanceTOplayer = Vector3.Distance(transform.position, Player.transform.position);

        //velocity.y = -9.8f * 3;
        //controller.Move(velocity * Time.deltaTime);

        //looping timer to play cards during a level
        CurrentTime = Time.time;

        //loot timer
        if (AI == true)
        {
            if (CurrentTime > EndTime)
            {
                Debug.Log("Send Move & Attack");
                Move();
                Attack();
                EndTime = CurrentTime + NextTime;
            }

            Look();
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
                GameObject child = gameObject.transform.GetChild(0).gameObject;
                child.GetComponent<MeshRenderer>().material = NormalMat;
            }

        }

        if (AttackCooldown == true)
        {
            if (CurrentTime > AttackCooldownEndTime)
            {
                AttackCooldownEndTime = CurrentTime + AttackCooldownNextTime;
                //WillAttack = true;
                AttackCooldown = false;

            }
        }

    }

    public void Move()
    {
        float distance = Vector3.Distance (transform.position, Player.transform.position);

        if (distance < 10 && distance > 1.5f)
        {
            transform.position += transform.forward * 0.5f;
            Debug.Log("Move");
        }
    }

    public void Attack()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance < 2 && AttackCooldown == false)
        {
            Debug.Log("Attack");
            var playerMovement3D = Player.GetComponent<PlayerMovement3D>();
            AttackCooldownEndTime = CurrentTime + AttackCooldownNextTime;
            AttackCooldown = true;
            playerMovement3D.AtemptToDamagePlayer(5);
        }
    }

    public void Activate()
    {
        Debug.Log("Activate");
        AI = true;
        CanTakeDamage = true;
    }

    public void Deactivate()
    {
        Debug.Log("Deactivtate");
        AI = false;
        CanTakeDamage = false;
    }

    public void Look()
    {
        float distance = Vector3.Distance (transform.position, Player.transform.position);
        
        if (distance < 10)
        {
            Vector3 targetPostition = new Vector3( Player.transform.position.x, transform.position.y, Player.transform.position.z );
            transform.LookAt( targetPostition ) ;
        }
    }

    public void OnTriggerEnter(Collider CollisionObject)
    {
        Debug.Log("Trigger Enter");
        //OnTriggerStay(CollisionObject);
    }
    
    public void OnTriggerStay(Collider CollisionObject)
    {
        if (CollisionObject.gameObject.name == "AttackTrigger")
        {
            var playerMovement3D = Player.GetComponent<PlayerMovement3D>();
            
            if (playerMovement3D.State == "Attack")
            {
                if (WillTakeDamage == true && CanTakeDamage == true)
                {
                    DamageCooldownEndTime = CurrentTime + DamageCooldownNextTime;
                    
                    WillTakeDamage = false;
                    DamageCooldown = true;

                    Health = Health - playerMovement3D.ActiveAttackDamage;
                    DamageSound.Play(0);

                    GameObject child = gameObject.transform.GetChild(0).gameObject;
                    child.GetComponent<MeshRenderer>().material = DamageMat;

                    if (Health <= 0)
                    {
                        if (gameObject.name == "Enemie")
                        {
                            //gameObject.SetActive(false);
                            Destroy(gameObject);
                        }
                        else
                        {
                            Debug.Log("Death");
                            Destroy(gameObject);
                        }
                        
                    }
                }
            }
        }

        if (CollisionObject.gameObject.name == "Arrow Projectile(Clone)")
        {
            var playerMovement3D = Player.GetComponent<PlayerMovement3D>();

            if (WillTakeDamage == true && CanTakeDamage == true)
            {
                DamageCooldownEndTime = CurrentTime + DamageCooldownNextTime;
                    
                WillTakeDamage = false;
                DamageCooldown = true;

                Health = Health - playerMovement3D.ActiveAttackDamage;
                DamageSound.Play(0);

                GameObject child = gameObject.transform.GetChild(0).gameObject;
                child.GetComponent<MeshRenderer>().material = DamageMat;

                if (Health <= 0)
                {
                    if (gameObject.name == "Enemie")
                    {
                        //gameObject.SetActive(false);
                        Destroy(gameObject);
                    }
                    else
                    {
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
        GameObject child = gameObject.transform.GetChild(0).gameObject;
        child.GetComponent<MeshRenderer>().material = NormalMat;
    }

}
