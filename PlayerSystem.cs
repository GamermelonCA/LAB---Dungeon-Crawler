using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class PlayerSystem : MonoBehaviour
{
    //Creates An Array
    public string[] CharecterNames;
    public Sprite[] CharecterSprites;
    public bool[] ActiveCharecters;

    public int CurrentSelectedCharecter = 0;
    public GameObject CharecterImage;

    private int CharInt = 1;

    public TMPro.TMP_Text CharecterName;

    public GameObject PlayerModelDefult;
    public GameObject PlayerModelWizzard;
    public GameObject PlayerModelAngel;

    void Start()
    {
        //Avoids Not having No Charecters
        bool Char1fix = ActiveCharecters[0];

        if (Char1fix != true)
        {
            ActiveCharecters[0] = true;
        }

        //sets the image and text of charecter 1 at start
        NextChar();

        //unloads playermodels
        PlayerModelDefult.SetActive(false);
        PlayerModelWizzard.SetActive(false);
        PlayerModelAngel.SetActive(false);
    }

    void Update()
    {

    }

    public void NextChar()
    {
        
        //gets the length of array so selected charecter dosen got oob
        int lengthOfArray = ActiveCharecters.Length;

        //add one to currenct charecter
        CurrentSelectedCharecter = CurrentSelectedCharecter + 1;

        //Charecter increaser and looper
        if (CurrentSelectedCharecter > (lengthOfArray - 1))
        {
            CurrentSelectedCharecter = 0;
        }

        if (ActiveCharecters[CurrentSelectedCharecter] != true)
        {
            CurrentSelectedCharecter = 0;
        }

        TriggerUpdate();

        

    }

    public void PrevChar() 
    {
        //gets the length of array so selected charecter dosen got oob
        int lengthOfArray = ActiveCharecters.Length;

        //add one to currenct charecter
        CurrentSelectedCharecter = CurrentSelectedCharecter - 1;

        if (CurrentSelectedCharecter == -1)
        {
            CurrentSelectedCharecter = (lengthOfArray - 1);
        }

        if (ActiveCharecters[CurrentSelectedCharecter] != true)
        {
            CurrentSelectedCharecter = 0;
        }

        TriggerUpdate();

    }

    public void TriggerUpdate()
    {
        //changes text to charecter name
        CharecterName.text = CharecterNames[CurrentSelectedCharecter];

        //changes sprite to coke beacause coke is better
        CharecterImage.GetComponent<Image>().sprite = CharecterSprites[CurrentSelectedCharecter];

    }

    public void AddNextChar()
    {

        if (CharInt < ActiveCharecters.Length)
        {
            if (ActiveCharecters[CharInt] == false)
            {
                ActiveCharecters[CharInt] = true;
            }
            else
            {
                CharInt = CharInt + 1;
                AddNextChar();
            }

            if (ActiveCharecters[2] == true)
            {

            }
        }

    }

    public void LoadPlayer()
    {
        //resets the loaded player model
        PlayerModelDefult.SetActive(false);
        PlayerModelWizzard.SetActive(false);
        PlayerModelAngel.SetActive(false);

        //loads the correct player model
        if(CurrentSelectedCharecter == 0)
        {
            PlayerModelDefult.SetActive(true);
        }   
        else if(CurrentSelectedCharecter == 1)
        {
            PlayerModelWizzard.SetActive(true);
        }   
        else if(CurrentSelectedCharecter == 2)
        {
            PlayerModelAngel.SetActive(true);
        } 
    }

}
