using TMPro;
using UnityEngine;

public class MathManiaManager : MonoBehaviour
{
    [Header("Variables")]

    [SerializeField] int Value1; //Value of number 1
    [SerializeField] int Value2; // Value of number 2

    [SerializeField] int operation; //0 - Addition, 1 - Subtractions


    [Header("Text Fields")]

    [SerializeField] TMP_Text equationDisplay; //Display text for the current equation

    [SerializeField] TMP_Text answerInput; //Text to display what numerics the player put in as an answer

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateEquation(); //Prelude create an equation
    }


    //Function to create a random math equation
    void CreateEquation()
    {
        //Random values from 1 - 15
        Value1 = Random.Range(1, 16);
        Value2 = Random.Range(1, 16);

        //Set a random operator
        operation = Random.Range(0, 2);

        if (operation is 0) //Display the addition equation
        {
            equationDisplay.text = $"{Value1} + {Value2}";
        }
        else if (operation is 1) //Display the subtraction equation
        {
            equationDisplay.text = $"{Value1} - {Value2}";
        }

        answerInput.text = ""; //Reset the player input
    }


    //Function to add to the players answer
    public void AddToAnswer(string inCharacterToAdd)
    {
        if (inCharacterToAdd == "-" && answerInput.text.Length != 0) return; //Don't add a minus if an answer was already added

        answerInput.text += inCharacterToAdd; //Add the character to the answer
    }


    //Function to check if the answer is correct
    public void CheckAnswer()
    {
        if (answerInput.text.Length is 0) return; //Nothing is inputted

        int playerInput = int.Parse(answerInput.text);

        if(operation is 0) //Check for addition
        {
            //Check if the input equals the two values added together
            if (playerInput == Value1 + Value2)
            {
                Debug.Log("Addition correct");

                PlayerScript.instance.RemoveSleepTime(6f); //Keep the player awake 3 more seconds
            }

            else Debug.Log("Addition incorrect");
        }
        else if (operation is 1) //Check for subtraction
        {
            //Check if the input equals the two values subtracted together
            if (playerInput == Value1 - Value2)
            {
                Debug.Log("Subtraction correct");

                PlayerScript.instance.RemoveSleepTime(6f); //Keep the player awake 3 more seconds
            }

            else Debug.Log("Subtraction incorrect");
        }

        CreateEquation(); //Create a new equation
    }
}
