using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DocumentManager : MonoBehaviour
{
    [Header("Input")]

    private PlayerInput pInput;

    private InputAction type;

    [SerializeField] GameObject typingTab; //Reference to the typing tab to see if the player can accept typing input


    [Header("Text Fields")]

    [SerializeField] TMP_Text compareText; //Text that will be compared for completion
    [SerializeField] TMP_Text playerTypingInput; //A text field of the player input

    [SerializeField] bool bCapitalizeLetter = false; //Should the typed letter be capitalized?

    [Header("Required Texts")]

    [SerializeField] List<string> writings = new(); //List of strings that need to be written to complete the game
    private int chosenWriting; //Variable to track what line to remove at the respective time


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pInput = GetComponent<PlayerInput>(); //Grab the player input

        //Assign the input actions
        type = pInput.actions.FindAction("Key");

        SelectWriting(); //Default select a writing
    }

    // Update is called once per frame
    void Update()
    {
        //When any keyboard input is pressed, analyze what was pressed and adjust the text accordingly
        if (type.WasPressedThisFrame() && typingTab.transform.localScale == Vector3.one && PlayerScript.instance.bRunSleepTimer)
        {
            CollectInputData();
        }
    }


    //Function to grab the key data
    void CollectInputData()
    {
        string hitKey = ""; //Create a string of what the key is

        //Loop through all the keys with a variable expectation of all keyboard keys
        foreach(var keyControl in Keyboard.current.allKeys)
        {
            hitKey = keyControl.displayName; //This grabs the name of the key that is pressed down

            if (keyControl.wasPressedThisFrame) //Display only the pressed key this frame
            {
                Debug.Log(hitKey); //Display what key was pressed

                if(hitKey is "Caps Lock" || hitKey is "Shift")
                {
                    bCapitalizeLetter = !bCapitalizeLetter; //Flip the capitalize state

                    return; //Don't effect the text
                }

                //Check if the hit key is space, ensuring Space isn't added to the text
                if(hitKey is "Space")
                {
                    playerTypingInput.text += " "; //Add a space

                    return;
                }

                //Backspace the last letter
                if(hitKey is "Backspace")
                {
                    string input = playerTypingInput.text; //Get the currently typed text
                    input = input.Substring(0, input.Length - 1); //Remove the last character
                    playerTypingInput.text = input; //Set the new text

                    return;
                }

                if (hitKey.Length > 1) //Check if the input string is a character, return if not
                    return;

                if(bCapitalizeLetter) playerTypingInput.text += hitKey.ToUpper(); //Add the new text to the typing text Capitalized

                else playerTypingInput.text += hitKey.ToLower(); //Add the new text to the typing text Uncapitalized
            }

            
        }

        if (playerTypingInput.text == compareText.text && writings.Count > 1) ProgressLine(); //Debug.Log("This would mark the end of the game");

        else if(playerTypingInput.text == compareText.text && writings.Count is 1)
        {
            Debug.Log("Game win, swap scenes");

            SceneManager.LoadScene("GameWin");
        }

    }


    //Function to select a random line for the player to type
    public void SelectWriting()
    {
        int selectedWriting = Random.Range(0, writings.Count); //Select the index of a random line and set it for tracking
        chosenWriting = selectedWriting;

        //Apply the line to the comparison line
        compareText.text = writings[chosenWriting];

        //Reset the player input
        playerTypingInput.text = string.Empty;
    }

    //Function to select a guideline to type
    public void ProgressLine()
    {
        writings.RemoveAt(chosenWriting); //Remove the tracked writing

        if (writings.Count is 0) Debug.Log("Game Win"); //Display that the game is won, temporary

        else SelectWriting(); //Progress to another line to type
    }
}
