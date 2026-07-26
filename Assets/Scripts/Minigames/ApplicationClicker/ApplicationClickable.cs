using UnityEngine;

public class ApplicationClickable : MonoBehaviour, iInteraction
{
    [SerializeField] float timeToReturn;

    [SerializeField] float rotationRandomness; //Amount of randomness per click

    #region Interface

    public void LeftClickInteraction()
    {
        PlayerScript.instance.RemoveSleepTime(timeToReturn); // Return .25 seconds to the players timer

        RandomRotation(); //Set a random rotation
    }

    public void RightClickInteraction()
    {
        // Do nothing
    }

    #endregion

    //Create a random rotation when the paper is clicked
    void RandomRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-rotationRandomness, rotationRandomness));
    }
}
