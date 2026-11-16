using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;

    [Header("Countdown")]

    [SerializeField] float sleepTimer; //How much time the player has before they crash
    public float maxSleepTimer; //The amount time it takes until the player falls asleep

    public bool bRunSleepTimer = false; //Boolean deciding if the timer should be running

    [SerializeField] Image sleepPanel; //Image display how sleepy the player is


    private void Awake()
    {
        instance = this; //Set this instance
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set the inital sleep timer to none
        sleepTimer = 0;

        bRunSleepTimer = true; //temp
    }

    // Update is called once per frame
    void Update()
    {
        //Run the timer if told to
        if (!bRunSleepTimer) return;

        sleepTimer += Time.deltaTime; //Time awake equates to real time

        float sleepvalue = sleepTimer / maxSleepTimer;
        sleepPanel.color = new Color(0, 0, 0, sleepvalue); //Darken the panel accordingly

        //If ever hitting 0, the game is over since you fell asleep
        if (sleepTimer >= maxSleepTimer)
        {
            Debug.Log("Womp womp, you've fallen asleep.");
            bRunSleepTimer = false;

            sleepPanel.color = new Color(0, 0, 0, 1); //Darken the panel accordingly

            SceneManager.LoadScene("GameLose");
        }
    }


    //Function to add time to the sleep timer [Bad]
    public void AddSleepTime(float inAwakeTime)
    {
        sleepTimer += inAwakeTime; //Else add the time respectfully
    }


    //Function to remove time from the sleep timer [Good]
    public void RemoveSleepTime(float inRemovedTime)
    {
        if (sleepTimer - inRemovedTime < 0) sleepTimer = 0; //Cap the time

        else sleepTimer -= inRemovedTime; //Remove the specified amount of time
    }


    //Function to set the run timer state
    public void SetTimerState(bool bIsRunning)
    {
        if (bIsRunning) bRunSleepTimer = true;

        else bRunSleepTimer = false;
    }
}
