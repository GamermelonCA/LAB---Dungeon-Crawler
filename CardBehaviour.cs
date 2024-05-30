using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CardBehaviour : MonoBehaviour
{
    public CardSystem CardSystem;
    public GameObject ThisCard;

    private int DoubleClickTimer = 100;
    private int CardClickCount = 0;

    private Sprite dash;
    private Sprite longdash;
    private Sprite quickdash;
    private Sprite phasedash;
    public Button ThisButton;

    private string LockCategory;

    // Start is called before the first frame update
    void Start()
    {
        ThisButton = ThisCard.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {

        //checks if a card is clicked again withing a certan time
        if (CardClickCount == 1)
        {
            if (DoubleClickTimer != 0)
            {
                DoubleClickTimer = DoubleClickTimer - 1;

            }
            else if (DoubleClickTimer == 0)
            {
                CardClickCount = 0;
            }

        }
        else if (CardClickCount >= 2)
        {

            CardClickCount = 0;
            DoubleClickTimer = 0;

            Sprite LoadCardSprite = ThisCard.GetComponent<Image>().sprite;

            if ((ThisCard.transform.parent.name) == "InventoryContent")
            {
                CardSystem.MoveToDeck(LoadCardSprite);
            }
            else if ((ThisCard.transform.parent.name) != "InventoryContent")
            {
                CardSystem.MoveToInventory(LoadCardSprite);
            }
        }

    }

    public void CardClick()
    {

        CardClickCount = CardClickCount + 1;
        if (DoubleClickTimer == 0)
        {
            DoubleClickTimer = 50;
        }

    }

    public void CardLock(bool AllowMovement)
    {
        dash = Resources.Load<Sprite>("Cards/Dash") as Sprite;
        longdash = Resources.Load<Sprite>("Cards/Long Dash") as Sprite;
        quickdash = Resources.Load<Sprite>("Cards/Quick Dash") as Sprite;
        phasedash = Resources.Load<Sprite>("Cards/Phase Dash") as Sprite;
        bool iscarddash = ThisCard.GetComponent<Image>().sprite == dash;
        bool iscardlongdash = ThisCard.GetComponent<Image>().sprite == longdash;
        bool iscardquickdash = ThisCard.GetComponent<Image>().sprite == quickdash;
        bool iscardphasedash = ThisCard.GetComponent<Image>().sprite == phasedash;

        if (LockCategory == "Dash")
        {
            if ((AllowMovement == false) && ((iscarddash == true) || (iscardlongdash == true) || (iscardquickdash == true) || (iscardphasedash == true)))
            {
                Debug.Log("Setting card to false");
                ThisButton.interactable = false;
            }
            else if ((AllowMovement == true) && ((iscarddash == true) || (iscardlongdash == true) || (iscardquickdash == true) || (iscardphasedash == true)))
            {
                Debug.Log("Setting card to true");
                ThisButton.interactable = true;
            }
            else if (ThisCard.GetComponent<Image>().sprite == Resources.Load<Sprite>("Cards/Debug Card") as Sprite)
            {
                Debug.Log("This card is debug card");
            }
            else
            {
                Debug.Log("Else");
            }
        }
        else if (LockCategory == "")
        {
            ThisButton.interactable = true;
        }

    }
    public void SetName(string CardCategroy)
    {
        LockCategory = CardCategroy;
    }

}
