using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class CardSystem : MonoBehaviour
{
    public string[] CardsNames;
    public int[] CardsAmount;
    public string[] CardsClass;
    public string[] CardsRarity;
    public Sprite[] CardsSprites;
    public bool[] CardsEnabled;
    public int[] CardsAmountEnabled;
    public Sprite[] ShufferlerArray;
    public int[] CardsShuffled;
    public int[] CardsToAdd;

    public Sprite CardInSlot1;
    public Sprite CardInSlot2;
    private int cardcheckloop = 0;
    private int cardloadloop = 0;
    public GameObject DebugCard;
    public GameObject DisplayDebugCard;
    public Transform InventoryContent;
    public Transform DeckContent;
    public Transform CharecterSlotOne;
    public Transform CharecterSlotTwo;
    public Transform OverlayGUICardSlot;
    private string CRDNAME;
    private string CRDCLASS;
    private Sprite CRDSPRITE;
    private string CRDRARE;
    private int movecardloop = 0;
    private string InvRareFilter;
    private string InvTypeFilter;
    private string DeckRareFilter;
    public GameObject InventorySearchBar;
    public GameObject DeckSearchBar;
    public CardDissabler CardDissabler;
    private bool ActivateCardLock;
    public GameObject TextCardId;
    private int CardAccuLoop = 0;
    private int CRDENBAMOUNT = 0;
    private int AmountSuffled = 0;
    private int X;
    public int LastPlayedCard = 0;
    public GameObject DeckUI;

    public float CurrentTime;
    public float EndTime;
    public bool PlayCards = false;
    public float NextTime = 60f;

    public GameObject EndCardBox1;
    private int EndCardBox1ID;
    public GameObject EndCardBox2;
    private int EndCardBox2ID;
    public GameObject EndCardBox3;
    private int EndCardBox3ID;

    public GameObject SecretCardBox;
    private int SecretCardBoxID;

    private Sprite dash;
    private Sprite longdash;
    private Sprite quickdash;
    private Sprite phasedash;
    public PlayerMovement3D PlayerMovement3D; 
    public KeySystem keySystem;

    public AudioSource CardDraw;

    // Start is called before the first frame update
    void Start()
    {
        InventorySearchBar.SetActive(false);
        DeckSearchBar.SetActive(false);

        InvRareFilter = "All";
        InvTypeFilter = "Both";
        DeckRareFilter = "All";

        dash = Resources.Load<Sprite>("Cards/Dash") as Sprite;
        longdash = Resources.Load<Sprite>("Cards/Long Dash") as Sprite;
        quickdash = Resources.Load<Sprite>("Cards/Quick Dash") as Sprite;
        phasedash = Resources.Load<Sprite>("Cards/Phase Dash") as Sprite;
    }

    void Update()
    {
        //looping timer to play cards during a level
        CurrentTime = Time.time;

        //loot timer
        if (PlayCards == true)
        {
            if (CurrentTime > EndTime)
            {
                PlayCard();
                EndTime = CurrentTime + NextTime;
            }
        }
    }

    public void AddCardMain()
    {
        //get array length
        int lengthOfArray = CardsNames.Length;
        //Debug.Log(lengthOfArray);
        //Debug.Log(cardcheckloop);

        if (cardcheckloop == lengthOfArray)
        {
            Debug.Log("Add new card to array");
            
            //Debug.Log("Add a new feild with data");
            //adds new feilds for card and sets data
            System.Array.Resize(ref CardsNames, CardsNames.Length + 1);
            CardsNames[cardcheckloop] = CRDNAME;
            System.Array.Resize(ref CardsAmount, CardsAmount.Length + 1);
            CardsAmount[cardcheckloop] = 1;
            System.Array.Resize(ref CardsClass, CardsClass.Length + 1);
            CardsClass[cardcheckloop] = CRDCLASS;
            System.Array.Resize(ref CardsSprites, CardsSprites.Length + 1);
            CardsSprites[cardcheckloop] = CRDSPRITE;
            System.Array.Resize(ref CardsEnabled, CardsEnabled.Length + 1);
            CardsEnabled[cardcheckloop] = false;
            System.Array.Resize(ref CardsAmountEnabled, CardsAmountEnabled.Length + 1);
            CardsAmountEnabled[cardcheckloop] = 0;
            System.Array.Resize(ref CardsRarity, CardsRarity.Length + 1);
            CardsRarity[cardcheckloop] = CRDRARE;

            cardcheckloop = 0;
        }
        else if (cardcheckloop != lengthOfArray)
        {
            if (CardsNames[cardcheckloop] == CRDNAME)
            {
                Debug.Log("Increases Card Amount");
                //increases card amount
                CardsAmount[cardcheckloop] = CardsAmount[cardcheckloop] + 1;
                //resets loop
                cardcheckloop = 0;
            }
            else
            {
                Debug.Log("Repeats loop");
                cardcheckloop = cardcheckloop + 1;
                AddCardMain();
            }
        }
    }
    //card referances (optimise)
    
    public void AddCard(int CardID)
    {
        string CRDADDID = TextCardId.GetComponent<Text>().text;
        //for if gate - mainly use for dev button

        if ((CRDADDID == "1") || (CardID == 1))
        {
            CRDNAME = "Dash";
            CRDCLASS = "Player";
            CRDRARE = "Bronse";
            CRDSPRITE = Resources.Load<Sprite>("Cards/Dash") as Sprite;
        }
        else if ((CRDADDID == "2") || (CardID == 2))
        {
            CRDNAME = "Long Dash";
            CRDCLASS = "Player";
            CRDRARE = "Silver";
            CRDSPRITE = Resources.Load<Sprite>("Cards/Long Dash") as Sprite;
        }
        else if ((CRDADDID == "3") || (CardID == 3))
        {
            CRDNAME = "Quick Dash";
            CRDCLASS = "Player";
            CRDRARE = "Gold";
            CRDSPRITE = Resources.Load<Sprite>("Cards/Quick Dash") as Sprite;
        }
        else if ((CRDADDID == "4") || (CardID == 4))
        {
            CRDNAME = "Phase Dash";
            CRDCLASS = "Player";
            CRDRARE = "Gold";
            CRDSPRITE = Resources.Load<Sprite>("Cards/Phase Dash") as Sprite;
        }
        else if ((CRDADDID == "5") || (CardID == 5))
        {
            CRDNAME = "Health Plus";
            CRDCLASS = "Player";
            CRDRARE = "Silver";
            CRDSPRITE = Resources.Load<Sprite>("Cards/Health Plus") as Sprite;
        }
        else if ((CRDADDID == "6") || (CardID == 6))
        {
            CRDNAME = "Health Plus Plus";
            CRDCLASS = "Player";
            CRDRARE = "Gold";
            CRDSPRITE = Resources.Load<Sprite>("Cards/Health Plus Plus") as Sprite;
        }
        else if ((CRDADDID == "7") || (CardID == 7))
        {
            CRDNAME = "Speed";
            CRDCLASS = "Deck";
            CRDRARE = "Bronse";
            CRDSPRITE = Resources.Load<Sprite>("Cards/Speed") as Sprite;
        }
        else if ((CRDADDID == "8") || (CardID == 8))
        {
            CRDNAME = "Super Speed";
            CRDCLASS = "Deck";
            CRDRARE = "Silver";
            CRDSPRITE = Resources.Load<Sprite>("Cards/Super Speed") as Sprite;
        }
        else if ((CRDADDID == "9") || (CardID == 9))
        {
            CRDNAME = "Super Speed Plus";
            CRDCLASS = "Deck";
            CRDRARE = "Gold";
            CRDSPRITE = Resources.Load<Sprite>("Cards/Super Speed Plus") as Sprite;
        }
        else if ((CRDADDID == "10") || (CardID == 10))
        {
            CRDNAME = "Damage";
            CRDCLASS = "Deck";
            CRDRARE = "Bronse";
            CRDSPRITE = Resources.Load<Sprite>("Cards/Damage") as Sprite;
        }
        else if ((CRDADDID == "11") || (CardID == 11))
        {
            CRDNAME = "Damage Plus";
            CRDCLASS = "Deck";
            CRDRARE = "Silver";
            CRDSPRITE = Resources.Load<Sprite>("Cards/Damage Plus") as Sprite;
        }
        else if ((CRDADDID == "12") || (CardID == 12))
        {
            CRDNAME = "Sheild";
            CRDCLASS = "Deck";
            CRDRARE = "Silver";
            CRDSPRITE = Resources.Load<Sprite>("Cards/Sheild") as Sprite;
        }
        else if ((CRDADDID == "13") || (CardID == 13))
        {
            CRDNAME = "Reinforced Sheild";
            CRDCLASS = "Deck";
            CRDRARE = "Gold";
            CRDSPRITE = Resources.Load<Sprite>("Cards/Reinforced Sheild") as Sprite;
        }
        else if ((CRDADDID == "14") || (CardID == 14))
        {
            CRDNAME = "Loot Hunter";
            CRDCLASS = "Deck";
            CRDRARE = "Silver";
            CRDSPRITE = Resources.Load<Sprite>("Cards/Loot Hunter") as Sprite;
        }
        else if ((CRDADDID == "15") || (CardID == 15))
        {
            CRDNAME = "Loot Scavenger";
            CRDCLASS = "Deck";
            CRDRARE = "Gold";
            CRDSPRITE = Resources.Load<Sprite>("Cards/Loot Scavenger") as Sprite;
        }
        else if ((CRDADDID == "16") || (CardID == 16))
        {
            CRDNAME = "Inner Strength";
            CRDCLASS = "Deck";
            CRDRARE = "Gold";
            CRDSPRITE = Resources.Load<Sprite>("Cards/Inner Strength") as Sprite;
        }
        else
        {
            Debug.Log("Card dose not exist");
        }

    }

    public void PreLoadCards()
    {
        //removes all exising cards from inventory (Refresh)
        
        //removes all loaded cards in a seperate function so it dosent loop
        for (var i = InventoryContent.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(InventoryContent.transform.GetChild(i).gameObject);
        }
        Debug.Log("Destoyes loaded cards - inventory");
        //removes all existing cards from deck (Refresh)

        for (var i = DeckContent.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(DeckContent.transform.GetChild(i).gameObject);
        }
        Debug.Log("Destoyes loaded cards - deck");

        //removes player cards (Refresh)
        for (var i = CharecterSlotOne.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(CharecterSlotOne.transform.GetChild(i).gameObject);
        }
        for (var i = CharecterSlotTwo.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(CharecterSlotTwo.transform.GetChild(i).gameObject);
        }
        Debug.Log("Destoyes loaded cards - player");

        LoadCards();
    }
    
    public void LoadCards()
    {
        Debug.Log("Load card start");
        //Loads Cards For Inventory
        
        //gets array length
        int lengthOfArray = CardsNames.Length;

        //if the array is the same as the loop then ends
        if (lengthOfArray == cardloadloop)
        {
            Debug.Log("Load card End");
            cardloadloop = 0;

            if (ActivateCardLock == true)
            {

                if ((CardInSlot1 == (dash) || (CardInSlot1 == (longdash)) || (CardInSlot1 == (quickdash)) || (CardInSlot1 == (phasedash))) || (CardInSlot2 == (dash) || (CardInSlot2 == (longdash)) || (CardInSlot2 == (quickdash)) || (CardInSlot2 == (phasedash))))
                {
                    CardDissabler.BroadcastCardLock(false, "Dash");
                }
            }
            else if (ActivateCardLock == false)
            {
                CardDissabler.BroadcastCardLock(true, "");
            }

        }
        else if(lengthOfArray != cardloadloop) // if their not the same
        {
            Debug.Log("Load Card Loop");
            //checks how many cards it need to add
            int AddCardLoop = CardsAmount[cardloadloop];

            //adds card to the player slots
            if (CardInSlot1 == CardsSprites[cardloadloop])
            {
                //stops the card from loading in the invetory - player slot 1
                AddCardLoop = AddCardLoop - 1;
                var AddCardTemp = Instantiate(DebugCard);
                AddCardTemp.transform.SetParent(CharecterSlotOne, false);
                AddCardTemp.GetComponent<Image>().sprite = CardsSprites[cardloadloop];
                Debug.Log("Loads card into slot 1");
            }
            if (CardInSlot2 == CardsSprites[cardloadloop])
            {
                //stops the card from loading in the invetory - player slot 2
                AddCardLoop = AddCardLoop - 1;
                var AddCardTemp = Instantiate(DebugCard);
                AddCardTemp.transform.SetParent(CharecterSlotTwo, false);
                AddCardTemp.GetComponent<Image>().sprite = CardsSprites[cardloadloop];
                Debug.Log("Loads card into slot 2");
            }


            //adds the cards into the inventory - takes away all cards in the deck
            for (int i = 0; i < (AddCardLoop - CardsAmountEnabled[cardloadloop]); i++)
            {
                
                if (((CardsRarity[cardloadloop] == InvRareFilter) || (InvRareFilter == "All")) && ((CardsClass[cardloadloop] == InvTypeFilter) || (InvTypeFilter == "Both")))
                {
                    var AddCardTemp = Instantiate(DebugCard);
                    AddCardTemp.transform.SetParent(InventoryContent, false);
                    AddCardTemp.GetComponent<Image>().sprite = CardsSprites[cardloadloop];
                    Debug.Log("Loads cards if they apply to the filter - Inventory");
                }

            }
            //loads cards into deck by amount that are enabled
            for (int i = 0; i < CardsAmountEnabled[cardloadloop]; i++)
            {
                if((CardsRarity[cardloadloop] == DeckRareFilter) || (DeckRareFilter == "All"))
                {
                    var AddCardTemp = Instantiate(DebugCard);
                    AddCardTemp.transform.SetParent(DeckContent, false);
                    AddCardTemp.GetComponent<Image>().sprite = CardsSprites[cardloadloop];
                    Debug.Log("Loads cards if they apply to the filter - Deck");
                }

            }
            Debug.Log("Loops Load Cards");
            //moves onto the next feild in the array
            cardloadloop = cardloadloop + 1;
            //loops
            LoadCards();

        }

    }

    public void MoveToDeck(Sprite LoadCardSprite)
    {
        //uses permiters to determine which card was doubled clicked
        if (CardsSprites[movecardloop] == LoadCardSprite)
        {
            if(CardsClass[movecardloop] == "Player")
            {
                Debug.Log("Card is of type player so move to player");
                MoveToPlayer(LoadCardSprite);
                movecardloop = 0;
            }
            else if (CardsEnabled[movecardloop] == false)
            {
                Debug.Log("Card is of type not player and has not yet been enabled");
                CardsEnabled[movecardloop] = true;
                CardsAmountEnabled[movecardloop] = 1;
                PreLoadCards() ;
                movecardloop = 0;
            }
            else if (CardsEnabled[movecardloop] == true)
            {
                Debug.Log("Card is of type not player and has been enabled");
                CardsAmountEnabled[movecardloop] = CardsAmountEnabled[movecardloop] + 1;
                PreLoadCards();
                movecardloop = 0;
            }

        }
        else if (CardsSprites[movecardloop] != LoadCardSprite) // if their not the same
        {
            Debug.Log("Move to deck loop");
            movecardloop = movecardloop + 1;
            MoveToDeck(LoadCardSprite);
        }
    }

    public void MoveToInventory(Sprite LoadCardSprite)
    {
        if (CardsSprites[movecardloop] == LoadCardSprite)
        {
            if (CardsEnabled[movecardloop] == true && CardsAmountEnabled[movecardloop] == 1)
            {
                Debug.Log("MoveToInventory from deck - last card");
                CardsEnabled[movecardloop] = false;
                CardsAmountEnabled[movecardloop] = 0;
                PreLoadCards();
                movecardloop = 0;
            }
            else if (CardsEnabled[movecardloop] == true && CardsAmountEnabled[movecardloop] > 1)
            {
                Debug.Log("MoveToInventory from deck - another of the same card");
                CardsAmountEnabled[movecardloop] = CardsAmountEnabled[movecardloop] - 1;
                PreLoadCards();
                movecardloop = 0;
            }
            else if (LoadCardSprite == CardInSlot1)
            {
                Debug.Log("MoveToInventory From Card Slot 1");
                ActivateCardLock = false;
                CardInSlot1 = null;
                PreLoadCards();
                movecardloop = 0;
            }
            else if (LoadCardSprite == CardInSlot2)
            {
                Debug.Log("MoveToInventory From Card Slot 2");
                ActivateCardLock = false;
                CardInSlot2 = null;
                PreLoadCards();
                movecardloop = 0;
            }

        }
        else if (CardsSprites[movecardloop] != LoadCardSprite) // if their not the same
        {
            Debug.Log("MoveToInventory Loop");
            movecardloop = movecardloop + 1;
            MoveToInventory(LoadCardSprite);
        }
    }

    public void MoveToPlayer(Sprite LoadCardSprite) 
    {
        if (CardInSlot1 == null)
        {
            CardInSlot1 = LoadCardSprite;
            Debug.Log("Move To Player Slot 1");
            ActivateCardLock = true;
            PreLoadCards();
        }
        else if ((CardInSlot1 != null) && (CardInSlot2 == null))
        {
            CardInSlot2 = LoadCardSprite;
            Debug.Log("Move To Player Slot 2");
            ActivateCardLock = true;
            PreLoadCards();

        }
        else
        {
            Debug.Log("Player Slots Full");
        }

    }

    public void InventoryRarity(int index)
    {
        switch (index)
        {
            case 0: InvRareFilter = "All"; break;
            case 1: InvRareFilter = "Bronse"; break;
            case 2: InvRareFilter = "Silver"; break;
            case 3: InvRareFilter = "Gold"; break;
            case 4: InvRareFilter = "Platinum"; break;
        }

        PreLoadCards();
    }

    public void InventoryType(int index)
    {
        switch (index)
        {
            case 0: InvTypeFilter = "Both"; break;
            case 1: InvTypeFilter = "Deck"; break;
            case 2: InvTypeFilter = "Player"; break;
        }

        PreLoadCards();
    }

        public void DeckRarity(int index)
    {
        switch (index)
        {
            case 0: DeckRareFilter = "All"; break;
            case 1: DeckRareFilter = "Bronse"; break;
            case 2: DeckRareFilter = "Silver"; break;
            case 3: DeckRareFilter = "Gold"; break;
            case 4: DeckRareFilter = "Platinum"; break;
        }

        PreLoadCards();
    }

    public void ClearShuffler ()
    {
        if (ShufferlerArray.Length > 0)
        {
            System.Array.Resize(ref ShufferlerArray, ShufferlerArray.Length - 1);
            Debug.Log("Shuffler Array -1");
            ClearShuffler();
        }
        else if (CardsShuffled.Length > 0)
        {
            System.Array.Resize(ref CardsShuffled, CardsShuffled.Length - 1);
            Debug.Log("cards shuffled array -1");
            ClearShuffler();
        }
        else if ((ShufferlerArray.Length == 0) && (CardsShuffled.Length == 0))
        {
            Debug.Log("Arrays clear move on");
            ShufflerAccuLoop();
        }
        else
        {
            Debug.Log("Failsafe");
            //ClearShuffler();
        }
    }
    public void ShufflerAccuLoop()
    {
        if (CardAccuLoop == CardsAmountEnabled.Length)
        {
            CardAccuLoop = 0;

            for (int i = 0; i < CRDENBAMOUNT; i++)
            {
                System.Array.Resize(ref ShufferlerArray, ShufferlerArray.Length + 1);
            }
            for (int i = 0; i < CardsAmountEnabled.Length; i++)
            {
                System.Array.Resize(ref CardsShuffled, CardsShuffled.Length + 1);
            }
            
            ShuffleCards();
        }
        else if (CardAccuLoop < CardsAmountEnabled.Length)
        {
            CRDENBAMOUNT = CRDENBAMOUNT + CardsAmountEnabled[CardAccuLoop];
            CardAccuLoop = CardAccuLoop + 1;
            ShufflerAccuLoop();
        }
    }
    public void ShuffleCards()
    {
        Debug.Log("#################################");
        Debug.Log("AmountSuffled");
        Debug.Log(AmountSuffled);

        if (AmountSuffled == CRDENBAMOUNT)
        {
            Debug.Log("End");
            CRDENBAMOUNT = 0; //TEMP MOVE TO END
            AmountSuffled = 0;
            //end
        }
        else
        {
            X = Random.Range(0, CardsAmountEnabled.Length);
            Debug.Log("Ranom number");
            Debug.Log(X);

            Debug.Log("CardsAmountEnabled[X]");
            Debug.Log(CardsAmountEnabled[X]);
            Debug.Log("CardsShuffled[X]");
            Debug.Log(CardsShuffled[X]);

            if ((CardsAmountEnabled[X] >= 1))
            {
                Debug.Log("Random Card has one or more enabled");
                if ((CardsShuffled[X] < CardsAmountEnabled[X]))
                {
                    Debug.Log("Amount Of card allready shuffled is lower than it is enabled");
                    Debug.Log("Can Shuffle Card");
                    ShufferlerArray[AmountSuffled] = CardsSprites[X];
                    CardsShuffled[X] = CardsShuffled[X] + 1;
                    AmountSuffled = AmountSuffled + 1;
                    ShuffleCards();
                }
                else
                {
                    ShuffleCards();
                }
            }
            else
            {
                Debug.Log("Cannot Shuffle Card");
                ShuffleCards();
            }
        }
    }

    public void InitialiseCards()
    {
        for (var i = OverlayGUICardSlot.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(OverlayGUICardSlot.transform.GetChild(i).gameObject);
        }

        for (var i = 0; i < CardsToAdd.Length; i++)
        {
            System.Array.Resize(ref CardsToAdd, CardsToAdd.Length - 1);
        }

        LastPlayedCard = 0;
        LoadPlayerCards();
        ClearShuffler();
    }

    public void LoadPlayerCards()
    {
        if (CardInSlot1 == Resources.Load<Sprite>("Cards/Dash") as Sprite)
        {
            //Dash Slot 1
            Debug.Log("Dash Slot 1");
            PlayerMovement3D.EnableEffect("Dash");
        }
        if (CardInSlot1 == Resources.Load<Sprite>("Cards/Long Dash") as Sprite)
        {
            //Long Dash Slot 1
            Debug.Log("Long Dash Slot 1");
            PlayerMovement3D.EnableEffect("Long Dash");
        }
        if (CardInSlot1 == Resources.Load<Sprite>("Cards/Quick Dash") as Sprite)
        {
            //Quick Dash Slot 1
            Debug.Log("Quick Dash Slot 1");
            PlayerMovement3D.EnableEffect("Quick Dash");
        }
        if (CardInSlot1 == Resources.Load<Sprite>("Cards/Phase Dash") as Sprite)
        {
            //Phase Dash Slot 1
            Debug.Log("Phase Dash Slot 1");
            PlayerMovement3D.EnableEffect("Phase Dash");
        }
        if (CardInSlot1 == Resources.Load<Sprite>("Cards/Health Plus") as Sprite)
        {
            //Health Plus Slot 1
            Debug.Log("Health Plus Slot 1");
            PlayerMovement3D.EnableStat("Health Plus");
        }
        if (CardInSlot1 == Resources.Load<Sprite>("Cards/Health Plus Plus") as Sprite)
        {
            //Health Plus Plus Slot 1
            Debug.Log("Health Plus Plus Slot 1");
            PlayerMovement3D.EnableStat("Health Plus Plus");
        }


        if (CardInSlot2 == Resources.Load<Sprite>("Cards/Dash") as Sprite)
        {
            //Dash Slot 2
            Debug.Log("Dash Slot 2");
            PlayerMovement3D.EnableEffect("Dash");
        }
        if (CardInSlot2 == Resources.Load<Sprite>("Cards/Long Dash") as Sprite)
        {
            //Long Dash Slot 2
            Debug.Log("Long Dash Slot 2");
            PlayerMovement3D.EnableEffect("Long Dash");
        }
        if (CardInSlot2 == Resources.Load<Sprite>("Cards/Quick Dash") as Sprite)
        {
            //Quick Dash Slot 2
            Debug.Log("Quick Dash Slot 2");
            PlayerMovement3D.EnableEffect("Quick Dash");
        }
        if (CardInSlot2 == Resources.Load<Sprite>("Cards/Phase Dash") as Sprite)
        {
            //Phase Dash Slot 2
            Debug.Log("Phase Dash Slot 2");
            PlayerMovement3D.EnableEffect("Phase Dash");
        }
        if (CardInSlot2 == Resources.Load<Sprite>("Cards/Health Plus") as Sprite)
        {
            //Health Plus Slot 2
            Debug.Log("Health Plus Slot 2");
            PlayerMovement3D.EnableStat("Health Plus");
        }
        if (CardInSlot2 == Resources.Load<Sprite>("Cards/Health Plus Plus") as Sprite)
        {
            //Health Plus Plus Slot 2
            Debug.Log("Health Plus Plus Slot 2");
            PlayerMovement3D.EnableStat("Health Plus Plus");
        }
    }

    public void EnablePlayingCards()
    {
        PlayCards = true;
        EndTime = CurrentTime + NextTime;
        PlayCard();
    }
    public void DissablePlayingCards()
    {
        PlayCards = false;
    }

    public void PlayCard()
    {
        if (LastPlayedCard == ShufferlerArray.Length)
        {
            for (var i = OverlayGUICardSlot.transform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(OverlayGUICardSlot.transform.GetChild(i).gameObject);
            }
        }
        else if (LastPlayedCard < ShufferlerArray.Length)
        {
            LastPlayedCard = LastPlayedCard + 1;
            CallCardEffect(ShufferlerArray[LastPlayedCard - 1]);
            CardDraw.Play(0);
        }
    }

    public void CallCardEffect(Sprite CardPlayed)
    {
        if (CardPlayed == Resources.Load<Sprite>("Cards/Damage") as Sprite)
        {
            Debug.Log("Damage");
            PlayerMovement3D.IncreaseDamage(15);
        }
        else if (CardPlayed == Resources.Load<Sprite>("Cards/Damage Plus") as Sprite)
        {
            Debug.Log("Damage +");
            PlayerMovement3D.IncreaseDamage(60);
        }
        else if (CardPlayed == Resources.Load<Sprite>("Cards/Inner Strength") as Sprite)
        {
            Debug.Log("Inner Strenghth");
            PlayerMovement3D.EnableStat("Inner Strength");
        }
        else if (CardPlayed == Resources.Load<Sprite>("Cards/Loot Hunter") as Sprite)
        {
            Debug.Log("Loot hunter");
            keySystem.StartExtraLoot(30);
        }
        else if (CardPlayed == Resources.Load<Sprite>("Cards/Loot Scavenger") as Sprite)
        {
            Debug.Log("Loo tscavenger");
            keySystem.StartExtraLoot(60);
        }
        else if (CardPlayed == Resources.Load<Sprite>("Cards/Reinforced Sheild") as Sprite)
        {
            Debug.Log("Ref Sheild");
            PlayerMovement3D.ActivateSheild(10);
        }
        else if (CardPlayed == Resources.Load<Sprite>("Cards/Sheild") as Sprite)
        {
            Debug.Log("Sheild");
            PlayerMovement3D.ActivateSheild(4);
        }
        else if (CardPlayed == Resources.Load<Sprite>("Cards/Speed") as Sprite)
        {
            Debug.Log("Sped");
            PlayerMovement3D.StartSpeedIncrease(15);
        }
        else if (CardPlayed == Resources.Load<Sprite>("Cards/Super Speed") as Sprite)
        {
            Debug.Log("Sup Speed");
            PlayerMovement3D.SuperSpeedIncrease();
            PlayerMovement3D.StartSpeedIncrease(15);
        }
        else if (CardPlayed == Resources.Load<Sprite>("Cards/Super Speed Plus") as Sprite)
        {
            Debug.Log("Sup SPeed Pluis");
            PlayerMovement3D.SuperSpeedIncrease();
            PlayerMovement3D.StartSpeedIncrease(60);
        }
        else
        {
            Debug.Log("Unkniown");
        }

        //this step display the card on the GUI
        //--
        //Removes the loaded card
        for (var i = OverlayGUICardSlot.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(OverlayGUICardSlot.transform.GetChild(i).gameObject);
        }

        //adds the new card
        var AddDispCardTemp = Instantiate(DisplayDebugCard);
        AddDispCardTemp.transform.SetParent(OverlayGUICardSlot, false);
        AddDispCardTemp.GetComponent<Image>().sprite = CardPlayed;
    }

    public void ClearPlayedCard()
    {
        
        //dumb shit bc i suck at coding
        if ((ShufferlerArray[LastPlayedCard - 1] != (Resources.Load<Sprite>("Cards/Super Speed Plus") as Sprite)) || (ShufferlerArray[LastPlayedCard - 1] != (Resources.Load<Sprite>("Cards/Damage Plus") as Sprite)))
        {    
            for (var i = OverlayGUICardSlot.transform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(OverlayGUICardSlot.transform.GetChild(i).gameObject);
            }
        }
    }

    public void LoadEndChest()
    {
        for (int i = 0; i < 4; i++)
        {
            Debug.Log("Add Card Slot Roll thingy start");
            Debug.Log(i);
            // i = slot
            if ((i == 1) || (i == 3))
            {
                Debug.Log("Slot 1 or 3 roll");
                float Rnd1 = Random.Range(1, 4);
                float Rnd2 = Random.Range(1, 4);

                var IDTEMP = 0;

                if (Rnd1 == Rnd2)
                {
                    Debug.Log("Roll silver");
                    //silver card
                    //slay the spire

                    int Crd2Add = Random.Range(1, 7);

                    if (Crd2Add == 1)
                    {
                        AddCard(2);
                        IDTEMP = 2;
                    }
                    else if (Crd2Add == 2)
                    {
                        AddCard(5);
                        IDTEMP = 5;
                    }
                    else if (Crd2Add == 3)
                    {
                        AddCard(8);
                        IDTEMP = 8;
                    }
                    else if (Crd2Add == 4)
                    {
                        AddCard(11);
                        IDTEMP = 11;
                    }
                    else if (Crd2Add == 5)
                    {
                        AddCard(12);
                        IDTEMP = 12;
                    }
                    else if (Crd2Add == 6)
                    {
                        AddCard(14);
                        IDTEMP = 14;
                    }
                }
                else
                {
                    Debug.Log("Roll Bronse");
                    //bronse card

                    int Crd2Add = Random.Range(1, 4);
                    Debug.Log(Crd2Add);

                    if (Crd2Add == 1)
                    {
                        AddCard(1);
                        IDTEMP = 1;
                    }
                    else if (Crd2Add == 2)
                    {
                        AddCard(7);
                        IDTEMP = 7;
                    }
                    else if (Crd2Add == 3)
                    {
                        AddCard(10);
                        IDTEMP = 10;
                    }
                }

                //temporary
                //AddCardMain();

                if (i == 1)
                {
                    EndCardBox1ID = IDTEMP;
                }
                else if (i == 3)
                {
                    EndCardBox3ID = IDTEMP;
                }
            }
            else if (i == 2)
            {
                Debug.Log("Slot 2");

                Debug.Log("Slot 1 or 3 roll");
                float Rnd1 = Random.Range(1, 6);
                float Rnd2 = Random.Range(1, 6);

                var IDTEMP = 0;

                if (Rnd1 == Rnd2)
                {
                    Debug.Log("Roll Gold");
                    //Gold card

                    int Crd2Add = Random.Range(1, 8);

                    if (Crd2Add == 1)
                    {
                        AddCard(3);
                        IDTEMP = 3;
                    }
                    else if (Crd2Add == 2)
                    {
                        AddCard(9);
                        IDTEMP = 9;
                    }
                    else if (Crd2Add == 3)
                    {
                        AddCard(6);
                        IDTEMP = 6;
                    }
                    else if (Crd2Add == 4)
                    {
                        AddCard(4);
                        IDTEMP = 4;
                    }
                    else if (Crd2Add == 5)
                    {
                        AddCard(13);
                        IDTEMP = 13;
                    }
                    else if (Crd2Add == 6)
                    {
                        AddCard(15);
                        IDTEMP = 15;
                    }
                    else if (Crd2Add == 7)
                    {
                        AddCard(16);
                        IDTEMP = 16;
                    }
                }
                else
                {
                    Debug.Log("Roll Silver");
                    //Gold card

                    int Crd2Add = Random.Range(1, 7);

                    if (Crd2Add == 1)
                    {
                        AddCard(2);
                        IDTEMP = 2;
                    }
                    else if (Crd2Add == 2)
                    {
                        AddCard(5);
                        IDTEMP = 5;
                    }
                    else if (Crd2Add == 3)
                    {
                        AddCard(8);
                        IDTEMP = 8;
                    }
                    else if (Crd2Add == 4)
                    {
                        AddCard(11);
                        IDTEMP = 11;
                    }
                    else if (Crd2Add == 5)
                    {
                        AddCard(12);
                        IDTEMP = 12;
                    }
                    else if (Crd2Add == 6)
                    {
                        AddCard(14);
                        IDTEMP = 14;
                    }
                }

                EndCardBox2ID = IDTEMP;
            }


            if (i == 1)
            {
                for (var o = EndCardBox1.transform.childCount - 1; o >= 0; o--)
                {
                    Object.Destroy(EndCardBox1.transform.GetChild(o).gameObject);
                }
                Debug.Log("Destoyes loaded cards - End Gui s1");

                var AddCardTemp = Instantiate(DisplayDebugCard);
                AddCardTemp.transform.SetParent(EndCardBox1.transform, false);
                AddCardTemp.GetComponent<Image>().sprite = CRDSPRITE;
                AddCardTemp.transform.localScale = new Vector3(0.9f, 1.35f, 1f);
                Debug.Log("Loads card into end card slot 1");
            }
            else if (i == 2)
            {
                for (var o = EndCardBox2.transform.childCount - 1; o >= 0; o--)
                {
                    Object.Destroy(EndCardBox2.transform.GetChild(o).gameObject);
                }
                Debug.Log("Destoyes loaded cards - End Gui s2");

                var AddCardTemp = Instantiate(DisplayDebugCard);
                AddCardTemp.transform.SetParent(EndCardBox2.transform, false);
                AddCardTemp.GetComponent<Image>().sprite = CRDSPRITE;
                AddCardTemp.transform.localScale = new Vector3(0.9f, 1.35f, 1f);
                Debug.Log("Loads card into end card slot 2");
            }
            else if (i == 3)
            {
                for (var o = EndCardBox3.transform.childCount - 1; o >= 0; o--)
                {
                    Object.Destroy(EndCardBox3.transform.GetChild(o).gameObject);
                }
                Debug.Log("Destoyes loaded cards - End Gui s3");

                var AddCardTemp = Instantiate(DisplayDebugCard);
                AddCardTemp.transform.SetParent(EndCardBox3.transform, false);
                AddCardTemp.GetComponent<Image>().sprite = CRDSPRITE;
                AddCardTemp.transform.localScale = new Vector3(0.9f, 1.35f, 1f);
                Debug.Log("Loads card into end card slot 3");
            }
        }

    }

    public void EndChestChooseCard(int PickedSlotNum)
    {
        if (PickedSlotNum == 1)
        {
            //AddCard(EndCardBox1ID);
            //AddCardMain();
            System.Array.Resize(ref CardsToAdd, CardsToAdd.Length + 1);
            int LENGTHTMP = CardsToAdd.Length;
            CardsToAdd[LENGTHTMP - 1] = EndCardBox1ID;
        }
        else if (PickedSlotNum == 2)
        {
            //AddCard(EndCardBox2ID);
            //AddCardMain();
            System.Array.Resize(ref CardsToAdd, CardsToAdd.Length + 1);
            int LENGTHTMP = CardsToAdd.Length;
            CardsToAdd[LENGTHTMP - 1] = EndCardBox2ID;
        }
        else if (PickedSlotNum == 3)
        {
            //AddCard(EndCardBox3ID);
            //AddCardMain();
            System.Array.Resize(ref CardsToAdd, CardsToAdd.Length + 1);
            int LENGTHTMP = CardsToAdd.Length;
            CardsToAdd[LENGTHTMP - 1] = EndCardBox3ID;
        }

        keySystem.TriggerEndChest("Close");
    }

    public void LoadSecretChest()
    {
        float Rnd1 = Random.Range(1,3);
        float Rnd2 = Random.Range(1,3);

        var IDTEMP = 0;

        if (Rnd1 == Rnd2)
        {
            //Gold Card
            float Rnd3 = Random.Range(1,4);
            if (Rnd3 == 1)
            {
                AddCard(6);
                IDTEMP = 6;
            }
            else if (Rnd3 == 2)
            {
                AddCard(16);
                IDTEMP = 16;
            }
            else if (Rnd3 == 3)
            {
                AddCard(9);
                IDTEMP = 9;
            }
        }
        else
        {
            //Silver Card
            float Rnd3 = Random.Range(1,4);
            if (Rnd3 == 1)
            {
                AddCard(12);
                IDTEMP = 12;
            }
            else if (Rnd3 == 2)
            {   
                AddCard(8);
                IDTEMP = 8;
            }
            else if (Rnd3 == 3)
            {
                AddCard(14);
                IDTEMP = 14;
            }
        }

        SecretCardBoxID = IDTEMP;

        for (var o = SecretCardBox.transform.childCount - 1; o >= 0; o--)
        {
            Object.Destroy(SecretCardBox.transform.GetChild(o).gameObject);
        }
        Debug.Log("Destoyes loaded cards - Secret Gui ");

        var AddCardTemp = Instantiate(DisplayDebugCard);
        AddCardTemp.transform.SetParent(SecretCardBox.transform, false);
        AddCardTemp.GetComponent<Image>().sprite = CRDSPRITE;
        AddCardTemp.transform.localScale = new Vector3(0.9f, 1.35f, 1f);
        Debug.Log("Loads card into secret card slot");

    }

    public void SecretChestChooseCard()
    {
        System.Array.Resize(ref CardsToAdd, CardsToAdd.Length + 1);
        int LENGTHTMP = CardsToAdd.Length;
        CardsToAdd[LENGTHTMP - 1] = SecretCardBoxID;

        //AddCard(SecretCardBoxID);
        //AddCardMain();

        keySystem.TriggerSecretChest("Close");

    }

    public void LevelEnd()
    {
        for (var i = 0; i < CardsToAdd.Length; i++)
        {
            AddCard(CardsToAdd[i]);
            AddCardMain();
        }

        for (var i = 0; i < CardsToAdd.Length; i++)
        {
            System.Array.Resize(ref CardsToAdd, CardsToAdd.Length - 1);
        }
    }

    public void LevelReset()
    {
        for (var i = 0; i < CardsToAdd.Length; i++)
        {
            System.Array.Resize(ref CardsToAdd, CardsToAdd.Length - 1);
        }
    }

}
