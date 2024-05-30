using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRoomBehaviour : MonoBehaviour
{
    //public GameObject RoomParent; /* This Object */
    public int StartingSpawnAmount;
    public GameObject EnemieOBJ;

    public bool ContainEnemieSpawner;
    public GameObject EnemieSpawnerOBJ;
    public Vector3 EnemieSpawnerVec3;
    public int SpawnerHealth;

    //public float SpawnTime;
    public int WaveSpawnAmount;
    public int AttackSpawnAmount;

    public GameObject SpawningZone;
    public GameObject AdaptiveSpawningZone;
    public GameObject ActivateTrigger;


    public GameObject[] ExecutablesToActivate;
    public bool ActivateOnClear;
    public bool ActivateOnObject;
    public GameObject ActivationObject;

    public int RewardTreasureBlue;
    public int RewardTreasureGold;

    //stuff to upload to enemie
    public GameObject player;


    //private stuff
    private bool InLevel = false;
    private bool RunOnce = false;
    public KeySystem keySystem;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InLevel == true && gameObject.transform.childCount == 0 && RunOnce == false && ActivateOnClear == true)
        {
            for (int i = 0; i < ExecutablesToActivate.Length; i++)
            {
                ExecutablesToActivate[i].SendMessage("Open");
                SpawnTreasureBlue(RewardTreasureBlue);
                SpawnTreasureGold(RewardTreasureGold);
            }
            RunOnce = true;
        }

        if (InLevel == true && ActivationObject == null && RunOnce == false && ActivateOnObject == true)
        {
            for (int i = 0; i < ExecutablesToActivate.Length; i++)
            {
                ExecutablesToActivate[i].SendMessage("Open");
                SpawnTreasureBlue(0);
                SpawnTreasureGold(0);
            }
            RunOnce = true;
        }
    }

    public void LevelStart()
    {
        for (int i = 0; i < StartingSpawnAmount; i++)
        {
            var RandX = Random.Range((SpawningZone.transform.position.x) - (SpawningZone.transform.localScale.x / 2), SpawningZone.transform.position.x + SpawningZone.transform.localScale.x / 2);
            var RandZ = Random.Range((SpawningZone.transform.position.z) - (SpawningZone.transform.localScale.z / 2), SpawningZone.transform.position.z + SpawningZone.transform.localScale.z / 2);

            var Roty = Random.Range(0, 360);
            
            var SpawnedEnemie = Instantiate(EnemieOBJ, new Vector3(RandX, SpawningZone.transform.position.y, RandZ), Quaternion.Euler(0,Roty,0), gameObject.transform);
            var EnemieBehaviour = SpawnedEnemie.GetComponent<EnemieBehaviour>();
            EnemieBehaviour.Player = player;
            EnemieBehaviour.Activate();
        }

        InLevel = true;

        if (ContainEnemieSpawner == true)
        {
            var EnemieSpawner = Instantiate(EnemieSpawnerOBJ, EnemieSpawnerVec3, Quaternion.Euler(0,0,0), gameObject.transform);
            EnemieSpawner.transform.localPosition = EnemieSpawnerVec3;
            ActivationObject = EnemieSpawner;

            var SpawnerBehaviour = EnemieSpawner.GetComponent<EnemieSpawnerScript>();
            SpawnerBehaviour.Health = SpawnerHealth;
            SpawnerBehaviour.WaveSpawnAmount = WaveSpawnAmount;
            SpawnerBehaviour.AttackSpawnAmount = AttackSpawnAmount;
            SpawnerBehaviour.EnemiePref = EnemieOBJ;
            SpawnerBehaviour.SpawnObject = AdaptiveSpawningZone;
            SpawnerBehaviour.RoomZone = SpawningZone;
            SpawnerBehaviour.player = player;

            var SetUpTriggerScript = ActivateTrigger.GetComponent<EnterTrigger>();
            SetUpTriggerScript.ObjectToActivate = ActivationObject;
        }
    }

    public void LevelReset()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.name != "AdaptiveSpawningZone")
            {
                Object.Destroy(gameObject.transform.GetChild(i).gameObject);
            }
            
        }

        InLevel = false;
        RunOnce = false;

        for (int i = 0; i < ExecutablesToActivate.Length; i++)
        {
            if (ExecutablesToActivate[i].activeSelf == false)
            {
                ExecutablesToActivate[i].SetActive(true);
            } 
            ExecutablesToActivate[i].SendMessage("LReset");

        }

        if (AdaptiveSpawningZone != null)
        {
            for (int i = 0; i < AdaptiveSpawningZone.transform.childCount; i++)
            {
                Object.Destroy(AdaptiveSpawningZone.transform.GetChild(i).gameObject);
            }
        }
        if (ActivationObject != null && ActivationObject.gameObject.name == "lab capsule spawner Variant(Clone)")
        {
            Object.Destroy(ActivationObject.gameObject);
        }
    }

    public void SpawnTreasureGold(int amount)
    {
        //spawns gold treasure
        keySystem.GetDataSpawnData(2);
        keySystem.GetDataSpawnType(2);
        keySystem.GetDataSpawnAmount(RewardTreasureGold + amount);
        keySystem.GetDataSpawnRegion(3);
    }
    public void SpawnTreasureBlue(int amount)
    {
        //spawns blue treasure
        keySystem.GetDataSpawnData(2);
        keySystem.GetDataSpawnType(1);
        keySystem.GetDataSpawnAmount(RewardTreasureBlue + amount);
        keySystem.GetDataSpawnRegion(3);
    }
}
