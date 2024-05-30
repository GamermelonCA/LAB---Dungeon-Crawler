 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeySystem : MonoBehaviour
{
    public Transform[] SpawnZones;
    public GameObject[] Interactables;
    public GameObject FinalZone1;
    public GameObject FinalZone2;

    public GameObject BlueTreasure;
    public GameObject GoldTreasure;

    private int varGetDataSpawnRegion;
    private int varGetDataSpawnData;
    private int varGetDataSpawnType;
    private int varGetDataSpawnAmount;

    private int Loop = 0;
    
    public TMPro.TMP_Text BlueTreasureNumber;
    public TMPro.TMP_Text GoldTreasureNumber;
    private int BlueTreasureAmount = 0;
    private int PossibleBlueTreasureAmount = 0;
    private int GoldTreasureAmount = 0;
    private int PossibleGoldTreasureAmount = 0;

    public GameObject player;

    public GameObject DiscalmerUI;
    public GameObject StartScreenUI;
    public GameObject DeckUIMenu;
    public GameObject DeckScreenUI;
    public GameObject MapUI;
    public GameObject ShopUI;
    public GameObject ShopUIMenu;
    public GameObject MainScreenUI;
    public GameObject PauseUI;
    public GameObject SecretChestUI;
    public GameObject EndChestUI;
    public GameObject EndScreenUI;
    public GameObject GameOverUI;
    public GameObject unused;
    public GameObject CRTUI;

    public GameObject DiscalmerUIButton;
    public bool DisclamerDelay = false;
    public float DiscEndTime;

    public GameObject StartPad;

    //loot stuff
    public float CurrentTime;
    public float EndTime;
    public bool GiveExtraLoot = false;
    public GameObject TreasureMaster;
    //public float AmountTime;


    //map vars
    public GameObject MapUIImage;

    public CardSystem CardSystem;
    public PlayerSystem PlayerSystem;
    public PlayerMovement3D PlayerMovement3D;
    public EnemieMainControll enemieMainControll;
    public GameObject LevelOneMain;

    public int ActiveLevel;
    public int LevelToSet;
    public Collider ColImp;
    public Collider PlayerZone;

    public bool GotExplosives;
    public bool GotCasing;
    public bool CraftedExplosives;
    public GameObject ExplosivesIcon;
    public GameObject CasingIcon;
    public GameObject CraftedExplosivesIcon;
    public GameObject CasingIconGray;
    public GameObject ExplosivesIconGray;

    //end UI stuff
    public TMPro.TMP_Text EndUIBlueTreasure;
    public TMPro.TMP_Text EndUIGoldTreasure;
    public GameObject CardCheatButton;
    public GameObject CardCheatInput;

    public GameObject Glassshard;
    public GameObject Glasswall;

    public GameObject SecretChest;
    public GameObject EndChest;

    public bool OpenedSecretChest = false;
    public bool GottenSecretChest = false;

    //sounds

    public AudioSource CraftingSound;
    public AudioSource ItemPickupSound;
    public AudioSource BlueLootPickup;
    public AudioSource GoldLootPickup;

    public string CurrentPlayingSound;
    public AudioSource Ambience1;
    public AudioSource Ambience2;
    public AudioSource Ambience3;
    public bool CanPlayAmbience = false;
    public float AmbienceCooldownTime = 10f;
    public float AmbienceCooldownEndTime;
    public bool hasplayedsound = false;

    // Start is called before the first frame update
    void Start()
    {
        DiscalmerUIButton.SetActive(false);
        DiscEndTime = CurrentTime + 5f;
        DisclamerDelay = true;
        
        UIsoftReset();
        OpenHomeMenu();
        //DiscalmerUI.SetActive(true);

        CardCheatButton.SetActive(false);
        CardCheatInput.SetActive(false);

        CurrentPlayingSound = "Nothing";

        //var INPUTSOUND = CurrentPlayingSound.GetComponent<AudioClip>();
        //INPUTSOUND = Ambience1;
        //CurrentPlayingSound.Play();

        //CurrentPlayingSound = Ambience1;
    }

    // Update is called once per frame
    void Update()
    { 

        if (Input.GetKeyDown(KeyCode.Escape) && (PauseUI.activeSelf == false) && (MainScreenUI.activeSelf == true) && (CRTUI.activeSelf == false)) //get input esc key && PauseUI.SetActive(false)
        {
            PauseUI.SetActive(true);
            PlayerMovement3D.StopMovement();
            PlayerMovement3D.StopDamage();
            enemieMainControll.BroadcastStopAI();

            

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && (PauseUI.activeSelf == true)) //get input esc key && PauseUI.SetActive(true)
        {
            ContinueButton();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && (CRTUI.activeSelf == true))
        {
            Debug.Log("Close the CRT UI");  
            OpenCRT(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && (DeckUIMenu.activeSelf == false) && (DeckScreenUI.activeSelf == true))
        {
            DeckUIMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && (DeckUIMenu.activeSelf == true)) //get input esc key && PauseUI.SetActive(true)
        {
            DeckUIMenu.SetActive(false);
        }

        
        CurrentTime = Time.time;

        //loot timer
        if (GiveExtraLoot == true)
        {
            if (CurrentTime > EndTime)
            {
                StopExtraLoot();
            }
        }

        //delay before the 
        if (DisclamerDelay == true)
        {
            if (CurrentTime > DiscEndTime)
            {
                DiscalmerUIButton.SetActive(true);
                DisclamerDelay = false;
            }
        }


        if (CanPlayAmbience == true)
        {
            if (hasplayedsound == true && CurrentPlayingSound == "Ambience1")
            {
                if (Ambience1.isPlaying == false)
                {
                    AmbienceCooldownEndTime = CurrentTime + AmbienceCooldownTime;
                    hasplayedsound = false;
                    CurrentPlayingSound = "Nothing";
                }
            }
            else if (hasplayedsound == true && CurrentPlayingSound == "Ambience2")
            {
                if (Ambience2.isPlaying == false)
                {
                    AmbienceCooldownEndTime = CurrentTime + AmbienceCooldownTime;
                    hasplayedsound = false;
                    CurrentPlayingSound = "Nothing";
                }
            }
            else if (hasplayedsound == true && CurrentPlayingSound == "Ambience3")
            {
                if (Ambience3.isPlaying == false)
                {
                    AmbienceCooldownEndTime = CurrentTime + AmbienceCooldownTime;
                    hasplayedsound = false;
                    CurrentPlayingSound = "Nothing";
                }
            }
            else if (CurrentTime > AmbienceCooldownEndTime && hasplayedsound == false && CurrentPlayingSound == "Nothing") 
            {
                var RANDOMAMBIENCE = Random.Range(1,4);
                Debug.Log("Random Ambience::::::::::::::::::::::::::::" + RANDOMAMBIENCE);
                Debug.Log("Random Number Picked");

                if (RANDOMAMBIENCE == 1)
                {
                    CurrentPlayingSound = "Ambience1";
                    Ambience1.Play();
                    Debug.Log("Set Sound the ambience 1");
                }
                else if (RANDOMAMBIENCE == 2)
                {
                    CurrentPlayingSound = "Ambience2";
                    Ambience2.Play();
                    Debug.Log("Set sound to ambience 2");
                }
                else if (RANDOMAMBIENCE == 3)
                {
                    CurrentPlayingSound = "Ambience3";
                    Ambience3.Play();
                    Debug.Log("Set sound to ambience 3");
                }

                hasplayedsound = true;
            }
            
        }
        
        if (CanPlayAmbience == false && Ambience1.isPlaying == true)
        {
           Ambience1.Stop();
        }
        if (CanPlayAmbience == false && Ambience2.isPlaying == true)
        {
            Ambience2.Stop();
        }
        if (CanPlayAmbience == false && Ambience3.isPlaying == true)
        {
            Ambience3.Stop();
        }

    }

    public void NewGame()
    {
        //save button --> opens file explorer
    }

    public void LoadGame()
    {
        //file import
    }

    public void OpenHomeMenu()
    {
        UIsoftReset();
        StartScreenUI.SetActive(true);
    }

    public void OpenDeckMenu()
    {
        //PLAY BUTTON IS TEMPORARY --> SAVE/LOAD GAME

        UIsoftReset();
        DeckScreenUI.SetActive(true);
        CardSystem.PreLoadCards();
    }

    public void Save()
    {

    }

    public void Load()
    {

    }

    public void UIsoftReset()
    {
        DiscalmerUI.SetActive(false);
        StartScreenUI.SetActive(false);
        DeckScreenUI.SetActive(false);
        MapUI.SetActive(false);
        MainScreenUI.SetActive(false);
        PauseUI.SetActive(false);
        EndScreenUI.SetActive(false);
        GameOverUI.SetActive(false);
        unused.SetActive(false);
        DeckUIMenu.SetActive(false);
        SecretChestUI.SetActive(false);
        EndChestUI.SetActive(false);
        ShopUI.SetActive(false);
        ShopUIMenu.SetActive(false);
        CRTUI.SetActive(false);
    }
    
    public void StartGame()
    {
        player.SetActive(true);
        player.transform.position = StartPad.transform.position + new Vector3(0, 1, 0);
        
        PlayerMovement3D.StartGame();
        PlayerSystem.LoadPlayer();
        CardSystem.InitialiseCards();
        //enemieMainControll.BroadcastStartAI();
        
        PauseUI.SetActive(false);
        MapUI.SetActive(false);
        MainScreenUI.SetActive(true);
        PlayerMovement3D.StartMovement();

        CardSystem.EnablePlayingCards();

        ExplosivesIcon.SetActive(false);
        CasingIcon.SetActive(false);
        CraftedExplosivesIcon.SetActive(false);
        CasingIconGray.SetActive(false);
        ExplosivesIconGray.SetActive(false);

        LevelOneMain.BroadcastMessage("LevelStart");

        CanPlayAmbience = true;
    }


    //this part sucks

    public void GetDataSpawnRegion(int Data)
    {
        Debug.Log("GetDataSpawnRegion");
        //one to three
        //1 - Here
        //2 - Near
        //3 - uni

        varGetDataSpawnRegion = Data;

        if (Data == 1)
        {
            SpawnTreasureHere();
        }
        else if (Data == 2)
        {
            SpawnTreasureNear();
        }
        else if (Data == 3)
        {
            SpawnTreasureUniversal();
        }
    }

    public void GetDataSpawnData(int Data)
    {
        Debug.Log("GetDataSpawnData");
        // zero to two
        //0 - null
        //1 - (uni) Type 1
        //2 - (uni) Type 2

        varGetDataSpawnData = Data;
    }

    public void GetDataSpawnType(int Data)
    {
        Debug.Log("GetDataSpawnType");
        //one to two
        //1 - blue
        //2 - gold

        varGetDataSpawnType = Data;
    }

    public void GetDataSpawnAmount(int Data)
    {
        Debug.Log("GetDataSpawnAmount");
        //nothing else needed

        varGetDataSpawnAmount = Data;
    }

    //end



    public void SpawnTreasureHere()
    {
        Debug.Log("SpawnTreasreHere");

        for (var d = 0; d < SpawnZones.Length; d++)
        {
            float dist = Vector3.Distance(player.transform.position, SpawnZones[d].position);
            print(dist);
        }
        //spawns treasure in the box that the player is in
        //if it is not in a box then triggers SpawnTreasureNear
    }

    public void SpawnTreasureNear()
    {
        Debug.Log("SpawnTreasureNear");
        //Finds the closest treasure box to the player
        //spawns treasure there
        //dose not include boxes the player is in
    }
    
    public void SpawnTreasureUniversal()
    {
        Debug.Log("SpawnTreasureUNI");
        Loop = 0;
        //either spawns the same amount of treasure in every box (Type 1)
        //or
        //a set amount of treasure evenly throughout all boxes (Type 2)

        if (varGetDataSpawnData == 1)
        {
            //same in all box
            Debug.Log("UNI Spawn Amount Everywhere");
            for (var i = 0; i < SpawnZones.Length; i++)
            {
                Debug.Log("Spawned At Spawn Zone");
                //SpawnZones[loop].position.x

                //creates a new object at the position
                if (varGetDataSpawnType == 1)
                {
                    Debug.Log("Spawn Blue");
                    for (var o = 0; o < varGetDataSpawnAmount; o++)
                    {
                        float Posx = Random.Range((SpawnZones[Loop].position.x - (SpawnZones[Loop].localScale.x / 2)) + (BlueTreasure.transform.localScale.x / 2), (SpawnZones[Loop].position.x + (SpawnZones[Loop].localScale.x / 2)) - (BlueTreasure.transform.localScale.x / 2));
                        float Posz = Random.Range((SpawnZones[Loop].position.z - (SpawnZones[Loop].localScale.z / 2)) + (BlueTreasure.transform.localScale.z / 2), (SpawnZones[Loop].position.z + (SpawnZones[Loop].localScale.z / 2)) - (BlueTreasure.transform.localScale.z / 2));

                        Instantiate(BlueTreasure, new Vector3(Posx, SpawnZones[Loop].position.y, Posz), Quaternion.Euler(0, 0, 0), TreasureMaster.transform);
                        Debug.Log("Spawned Blue");
                    }
                }
                if (varGetDataSpawnType == 2)
                {
                    Debug.Log("Spawn Gold");
                    for (var o = 0; o < varGetDataSpawnAmount; o++)
                    {
                        float Posx = Random.Range((SpawnZones[Loop].position.x - (SpawnZones[Loop].localScale.x / 2)) + (GoldTreasure.transform.localScale.x / 2), (SpawnZones[Loop].position.x + (SpawnZones[Loop].localScale.x / 2)) - (GoldTreasure.transform.localScale.x / 2));
                        float Posz = Random.Range((SpawnZones[Loop].position.z - (SpawnZones[Loop].localScale.z / 2)) + (GoldTreasure.transform.localScale.z / 2), (SpawnZones[Loop].position.z + (SpawnZones[Loop].localScale.z / 2)) - (GoldTreasure.transform.localScale.z / 2));

                        Instantiate(GoldTreasure, new Vector3(Posx, SpawnZones[Loop].position.y, Posz), Quaternion.Euler(0, 0, 0), TreasureMaster.transform);
                        Debug.Log("Spawned Gold");
                    }
                }

                Loop = Loop + 1;
            }
        }
        else if (varGetDataSpawnData == 2)
        {
            //set amount spread randomly
            Debug.Log("UNI Spawn Amount Evenly");

            for (var i = 0; i < varGetDataSpawnAmount; i++)
            {
                int SpawnOption = Random.Range(0, SpawnZones.Length);

                if (varGetDataSpawnType == 1)
                {
                    float Posx = Random.Range((SpawnZones[SpawnOption].position.x - (SpawnZones[SpawnOption].localScale.x / 2)) + (BlueTreasure.transform.localScale.x / 2), (SpawnZones[SpawnOption].position.x + (SpawnZones[SpawnOption].localScale.x / 2)) - (BlueTreasure.transform.localScale.x / 2));
                    float Posz = Random.Range((SpawnZones[SpawnOption].position.z - (SpawnZones[SpawnOption].localScale.z / 2)) + (BlueTreasure.transform.localScale.z / 2), (SpawnZones[SpawnOption].position.z + (SpawnZones[SpawnOption].localScale.z / 2)) - (BlueTreasure.transform.localScale.z / 2));

                    Instantiate(BlueTreasure, new Vector3(Posx, SpawnZones[SpawnOption].position.y, Posz), Quaternion.Euler(0, 0, 0), TreasureMaster.transform);
                    Debug.Log("Spawned Blue");
                }
                if (varGetDataSpawnType == 2)
                {
                    float Posx = Random.Range((SpawnZones[SpawnOption].position.x - (SpawnZones[SpawnOption].localScale.x / 2)) + (GoldTreasure.transform.localScale.x / 2), (SpawnZones[SpawnOption].position.x + (SpawnZones[SpawnOption].localScale.x / 2)) - (GoldTreasure.transform.localScale.x / 2));
                    float Posz = Random.Range((SpawnZones[SpawnOption].position.z - (SpawnZones[SpawnOption].localScale.z / 2)) + (GoldTreasure.transform.localScale.z / 2), (SpawnZones[SpawnOption].position.z + (SpawnZones[SpawnOption].localScale.z / 2)) - (GoldTreasure.transform.localScale.z / 2));

                    Instantiate(GoldTreasure, new Vector3(Posx, SpawnZones[SpawnOption].position.y, Posz), Quaternion.Euler(0, 0, 0), TreasureMaster.transform);
                    Debug.Log("Spawned Gold");
                }
            }

        }
    }

    public void AddTreasureGold(int Amount)
    {
        GoldLootPickup.Play(0);
        
        //game end ---> PossibleGoldTreasureAmount = GoldTreasureAmount
        Debug.Log("Add Treasure Gold");
        PossibleGoldTreasureAmount = PossibleGoldTreasureAmount + Amount;

        if (GiveExtraLoot == true)
        {
            PossibleGoldTreasureAmount = PossibleGoldTreasureAmount + Amount;
        }

        UpdateGoldTreasure();
    }

    public void AddTreasureBlue(int Amount)
    {
        BlueLootPickup.Play(0);
        
        //game end ---> PossibleBlueTreasureAmount = BlueTreasureAmount
        Debug.Log("Add Treasure Blue");
        PossibleBlueTreasureAmount = PossibleBlueTreasureAmount + Amount;

        if (GiveExtraLoot == true)
        {
            PossibleBlueTreasureAmount = PossibleBlueTreasureAmount + Amount;
        }

        UpdateBlueTreasure();
    }

    public void UpdateBlueTreasure()
    {
        var BlueTreaUpd = BlueTreasureAmount + PossibleBlueTreasureAmount;
        BlueTreasureNumber.text = BlueTreaUpd.ToString();
    }
    public void UpdateGoldTreasure()
    {
        var GoldTreaUpd = GoldTreasureAmount + PossibleGoldTreasureAmount;
        GoldTreasureNumber.text = GoldTreaUpd.ToString();
    }

    public void StartExtraLoot(float ExtraLootTime)
    {
        EndTime = CurrentTime + ExtraLootTime;
        GiveExtraLoot = true;
        Debug.Log("Start " + ExtraLootTime.ToString());
    }

    public void StopExtraLoot()
    {
        Debug.Log("Stop");
        GiveExtraLoot = false;
        CardSystem.ClearPlayedCard();
    }

    public void SpawnFinalTreasure()
    {
        //clears the CraftedExplosvies
        //CraftedExplosives = false;
        CraftedExplosivesIcon.SetActive(false);
        
        //spawn zone 1 gold
        Debug.Log("Spawn Gold Zone 1");
        for (var o = 0; o < 5; o++)
        {
            float Posx = Random.Range((FinalZone1.transform.position.x - (FinalZone1.transform.localScale.x / 2)) + (GoldTreasure.transform.localScale.x / 2), (FinalZone1.transform.position.x + (FinalZone1.transform.localScale.x / 2)) - (GoldTreasure.transform.localScale.x / 2));
            float Posz = Random.Range((FinalZone1.transform.position.z - (FinalZone1.transform.localScale.z / 2)) + (GoldTreasure.transform.localScale.z / 2), (FinalZone1.transform.position.z + (FinalZone1.transform.localScale.z / 2)) - (GoldTreasure.transform.localScale.z / 2));

            Instantiate(GoldTreasure, new Vector3(Posx, FinalZone1.transform.position.y, Posz), Quaternion.Euler(0, 0, 0));
            Debug.Log("Spawned Gold");
        }

        //spawn zone 1 blue
        Debug.Log("Spawn Blue Zone 1");
        for (var o = 0; o < 2; o++)
        {
            float Posx = Random.Range((FinalZone1.transform.position.x - (FinalZone1.transform.localScale.x / 2)) + (BlueTreasure.transform.localScale.x / 2), (FinalZone1.transform.position.x + (FinalZone1.transform.localScale.x / 2)) - (BlueTreasure.transform.localScale.x / 2));
            float Posz = Random.Range((FinalZone1.transform.position.z - (FinalZone1.transform.localScale.z / 2)) + (BlueTreasure.transform.localScale.z / 2), (FinalZone1.transform.position.z + (FinalZone1.transform.localScale.z / 2)) - (BlueTreasure.transform.localScale.z / 2));

            Instantiate(BlueTreasure, new Vector3(Posx, FinalZone1.transform.position.y, Posz), Quaternion.Euler(0, 0, 0));
            Debug.Log("Spawned Gold");
        }

        //spawn zone 2 gold
        Debug.Log("Spawn Gold Zone 2");
        for (var o = 0; o < 5; o++)
        {
            float Posx = Random.Range((FinalZone2.transform.position.x - (FinalZone2.transform.localScale.x / 2)) + (GoldTreasure.transform.localScale.x / 2), (FinalZone2.transform.position.x + (FinalZone2.transform.localScale.x / 2)) - (GoldTreasure.transform.localScale.x / 2));
            float Posz = Random.Range((FinalZone2.transform.position.z - (FinalZone2.transform.localScale.z / 2)) + (GoldTreasure.transform.localScale.z / 2), (FinalZone2.transform.position.z + (FinalZone2.transform.localScale.z / 2)) - (GoldTreasure.transform.localScale.z / 2));

            Instantiate(GoldTreasure, new Vector3(Posx, FinalZone2.transform.position.y, Posz), Quaternion.Euler(0, 0, 0));
            Debug.Log("Spawned Gold"); 
        }

        //spawn zone 2 blue
        Debug.Log("Spawn Blue Zone 2");
        for (var o = 0; o < 3; o++)
        {
            float Posx = Random.Range((FinalZone2.transform.position.x - (FinalZone2.transform.localScale.x / 2)) + (BlueTreasure.transform.localScale.x / 2), (FinalZone2.transform.position.x + (FinalZone2.transform.localScale.x / 2)) - (BlueTreasure.transform.localScale.x / 2));
            float Posz = Random.Range((FinalZone2.transform.position.z - (FinalZone2.transform.localScale.z / 2)) + (BlueTreasure.transform.localScale.z / 2), (FinalZone2.transform.position.z + (FinalZone2.transform.localScale.z / 2)) - (BlueTreasure.transform.localScale.z / 2));

            Instantiate(BlueTreasure, new Vector3(Posx, FinalZone2.transform.position.y, Posz), Quaternion.Euler(0, 0, 0));
            Debug.Log("Spawned Gold"); 
        }
    }


    public void SetLevel(int SwapLevel)
    {
        LevelToSet = SwapLevel;
        LevelSystem();
    }

    public void LevelSystem()
    {
        Debug.Log("Level system");
        //0 is Main Ui's
        //100 Is tutorial
        //105 Would Be Hub
        //1+ Are the Levels

        ActiveLevel = 100;

        if (LevelToSet != ActiveLevel)
        {
            //initalises whatever is needed
            //would change the scene at somepoint
        }
        
        if (ActiveLevel == 100)
        {
            //for now i am hoping this solves all of my problems
            PlayerMovement3D.StartMovement();
            Debug.Log("Send Message");
        }

    }

    public void TriggerGameover()
    {
        CardSystem.DissablePlayingCards();
        
        PlayerMovement3D.StopMovement();
        player.transform.position = StartPad.transform.position + new Vector3(0, 1, 0);

        player.SetActive(false);
        
        GiveExtraLoot = false;
        PossibleBlueTreasureAmount = 0;
        UpdateBlueTreasure();
        PossibleGoldTreasureAmount = 0;
        UpdateGoldTreasure();
        
        PauseUI.SetActive(false);
        MainScreenUI.SetActive(false);
        GameOverUI.SetActive(true);

        if (GottenSecretChest == false && OpenedSecretChest == true)
        {
            SecretChest.SetActive(true);
            OpenedSecretChest = false;
        }

        EndChest.SetActive(true);
        CardSystem.LevelReset();

        Glassshard.SetActive(false);
        Glasswall.SetActive(true);
    }

    public void GameOverReset()
    {
        player.SetActive(true);
        PlayerMovement3D.StartMovement();

        //temporary soloution --> intergrate with level system
        //MainScreenUI.SetActive(true);
        GameOverUI.SetActive(false);

        LevelOneMain.BroadcastMessage("LevelReset");
        StartGame();

        for (var i = 0; i < Interactables.Length; i++)
        {
            Interactables[i].SetActive(true);
        }

        CraftedExplosives = false;
        GotCasing = false;
        GotExplosives = false;
        CraftedExplosivesIcon.SetActive(false);
        ExplosivesIcon.SetActive(false);
        ExplosivesIconGray.SetActive(false);
        CasingIcon.SetActive(false);
        CasingIconGray.SetActive(false);

        Glassshard.SetActive(false);
        Glasswall.SetActive(true);

        if (GottenSecretChest == false && OpenedSecretChest == true)
        {
            SecretChest.SetActive(true);
            OpenedSecretChest = false;
        }

        EndChest.SetActive(true);
        CardSystem.LevelReset();
    }

    public void GameOverToMenu()
    {
        CardSystem.DissablePlayingCards();
        enemieMainControll.BroadcastStopAI();

        PlayerMovement3D.StopMovement();
        player.transform.position = StartPad.transform.position + new Vector3(0, 1, 0);
        
        GiveExtraLoot = false;
        PossibleBlueTreasureAmount = 0;
        UpdateBlueTreasure();
        PossibleGoldTreasureAmount = 0;
        UpdateGoldTreasure();
        
        OpenDeckMenu();

        LevelOneMain.BroadcastMessage("LevelReset");

        for (var i = 0; i < Interactables.Length; i++)
        {
            Interactables[i].SetActive(true);
        }

        CraftedExplosives = false;
        GotCasing = false;
        GotExplosives = false;
        CraftedExplosivesIcon.SetActive(false);
        ExplosivesIcon.SetActive(false);
        ExplosivesIconGray.SetActive(false);
        CasingIcon.SetActive(false);
        CasingIconGray.SetActive(false);

        Glassshard.SetActive(false);
        Glasswall.SetActive(true);

        if (GottenSecretChest == false && OpenedSecretChest == true)
        {
            SecretChest.SetActive(true);
            OpenedSecretChest = false;
        }

        EndChest.SetActive(true);
        CardSystem.LevelReset();

        CanPlayAmbience = false;
    }

    public void ContinueButton()
    {
        PauseUI.SetActive(false);
        PlayerMovement3D.StartMovement();
        PlayerMovement3D.StartDamage();
        enemieMainControll.BroadcastStartAI();
    }

    public void PlayerZoneSet(Collider collider)
    {
        //FOR LOOT SYSTEM THAT WILL NOT BE ADDED UNLESS SPARE TIME
        ColImp = collider;

        for (var i = 0; i < SpawnZones.Length; i++)
        {
            if (ColImp.transform == SpawnZones[1])
            {
                PlayerZone = ColImp;
            }
            else if (ColImp == null)
            {
                PlayerZone = null;
            }
        }

    }
    
    public void OpenMap()
    {
        //a fancy animation of the map opening would go here but NO unity just gives me a "stack overflow error"
        DeckScreenUI.SetActive(false);
        MapUI.SetActive(true);
    }

    public void CloseMap()
    {
        DeckScreenUI.SetActive(true);
        MapUI.SetActive(false);
    }

    public void TriggerEndChest(string Type)
    {
        Debug.Log("EndChestTrigger");
        
        if (Type == "Open")
        {
            Debug.Log("Open End");
            UIsoftReset();
            MainScreenUI.SetActive(true);
            EndChestUI.SetActive(true);

            PlayerMovement3D.StopMovement();
            PlayerMovement3D.StopDamage();
            enemieMainControll.BroadcastStopAI();

            CardSystem.LoadEndChest();
        }
        else if (Type == "Close")
        {
            Debug.Log("Close end");
            UIsoftReset();
            MainScreenUI.SetActive(true);

            PlayerMovement3D.StartMovement();
            PlayerMovement3D.StartDamage();
            enemieMainControll.BroadcastStartAI();
        }
    }

    public void TriggerSecretChest(string Type)
    {
        Debug.Log("SecretChestTrigger");
        OpenedSecretChest = true;
        
        if (Type == "Open")
        {
            Debug.Log("Open End");
            UIsoftReset();
            MainScreenUI.SetActive(true);
            SecretChestUI.SetActive(true);

            PlayerMovement3D.StopMovement();
            PlayerMovement3D.StopDamage();
            enemieMainControll.BroadcastStopAI();

            CardSystem.LoadSecretChest();
        }
        else if (Type == "Close")
        {
            Debug.Log("Close end");
            UIsoftReset();
            MainScreenUI.SetActive(true);

            PlayerMovement3D.StartMovement();
            PlayerMovement3D.StartDamage();
            enemieMainControll.BroadcastStartAI();
        }
    }

    public void OpenChest(string TypeOfChest)
    {
        if (TypeOfChest == "Secret")
        {
            TriggerSecretChest("Open");
        }
        else if (TypeOfChest == "End")
        {
            TriggerEndChest("Open");
        }
    }

    public void OpenCRT(int Type)
    {
        if(Type == 0)
        {
            if (CRTUI.activeSelf == true)
            {
                Debug.Log("Closing the CrtUI");
                UIsoftReset();
                MainScreenUI.SetActive(true);
                PlayerMovement3D.StartMovement();
            }
        }
        if (Type == 1)
        {
            if (CRTUI.activeSelf == false)
            {
                Debug.Log("Opening The Crt UI");
                UIsoftReset();
                MainScreenUI.SetActive(true);
                CRTUI.SetActive(true);
                PlayerMovement3D.StopMovement();
            }
        }
        if (Type == 2)
        {

        }
    }

    public void PickUpObject(string Object)
    {
        if (Object == "Explosives")
        {
            ExplosivesIcon.SetActive(true);
            GotExplosives = true;
            ItemPickupSound.Play(0);

            if (ExplosivesIconGray.activeSelf == true)
            {
                ExplosivesIconGray.SetActive(false);
            }
        }
        if (Object == "Casing")
        {
            CasingIcon.SetActive(true);
            GotCasing = true;
            ItemPickupSound.Play(0);

            if (CasingIconGray.activeSelf == true)
            {
                CasingIconGray.SetActive(false);
            }
        }
        if (Object == "Blueprint")
        {
            if (GotExplosives == false && CraftedExplosives == false)
            {
                ExplosivesIconGray.SetActive(true);
            }
            if (GotCasing == false && CraftedExplosives == false)
            {
                CasingIconGray.SetActive(true);
            }
            if (GotExplosives == true && GotCasing == true)
            {
                PickUpObject("CraftedExplosives");
                CraftingSound.Play(0);
            } 
                
        }
        if (Object == "CraftedExplosives")
        {
            ExplosivesIcon.SetActive(false);
            GotExplosives = false;
            CasingIcon.SetActive(false);
            GotCasing = false;
            CraftedExplosivesIcon.SetActive(true);
            CraftedExplosives = true;

        }
    }

    
    public void GameEnd()
    {
        Debug.Log("End Game");
        UIsoftReset();
        EndScreenUI.SetActive(true);

        PlayerMovement3D.StopMovement();
        PlayerMovement3D.StopDamage();
        enemieMainControll.BroadcastStopAI();

        EndUIBlueTreasure.text = PossibleBlueTreasureAmount.ToString();
        EndUIGoldTreasure.text = PossibleGoldTreasureAmount.ToString();
        
        BlueTreasureAmount = BlueTreasureAmount + PossibleBlueTreasureAmount;
        PossibleBlueTreasureAmount = 0;
        UpdateBlueTreasure();
        GoldTreasureAmount = GoldTreasureAmount + PossibleGoldTreasureAmount;
        PossibleGoldTreasureAmount = 0;
        UpdateGoldTreasure();

        CardCheatButton.SetActive(true);
        CardCheatInput.SetActive(true);

        PlayerSystem.AddNextChar();
        PlayerSystem.AddNextChar();

        player.transform.position = StartPad.transform.position + new Vector3(0, 1, 0);

        

        CardSystem.DissablePlayingCards();
        enemieMainControll.BroadcastStopAI();

        GiveExtraLoot = false;
        CraftedExplosives = false;
        GotCasing = false;
        GotExplosives = false;
        CraftedExplosivesIcon.SetActive(false);
        ExplosivesIcon.SetActive(false);
        ExplosivesIconGray.SetActive(false);
        CasingIcon.SetActive(false);
        CasingIconGray.SetActive(false);

        LevelOneMain.BroadcastMessage("LevelReset");

        for (var i = 0; i < Interactables.Length; i++)
        {
            Interactables[i].SetActive(true);
        }

        Glassshard.SetActive(false);
        Glasswall.SetActive(true);

        EndChest.SetActive(true);

        if (OpenedSecretChest == true)
        {
            GottenSecretChest = true;
        }

        CardSystem.LevelEnd();

        CanPlayAmbience = false;
        //reset the level but not reset loot and stuff




        //check functions for adding blue and gold treasure

        //chest at the end lets you chose one of three cards for free
        // 1 2 3
        // 2 will allways be a bronse or silver card you dont have (or one of the ones you have the least amount of)
        // 1 & 3 have a 10% (each) of being silver

        // shop???
        // will refresh daily giving cards

        //not permanant option but the other charecters will unlock when you compleate the level
        //charecters will be either bought or gotten at the end of bossfights
    }

    public void GameToHome()
    {
        OpenDeckMenu();
    }

}
