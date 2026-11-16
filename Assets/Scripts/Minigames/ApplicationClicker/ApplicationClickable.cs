using UnityEngine;
using UnityEngine.UI;

public class ApplicationClickable : MonoBehaviour, iInteraction
{
    [SerializeField] float timeToReturn;

    [SerializeField] float rotationRandomness; //Amount of randomness per click

    private Image render;
    [SerializeField] Sprite[] randomSprites;

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


    void Start()
    {
        render = GetComponent<Image>(); //Get the SpriteRenderer component
    }

    //Create a random rotation when the paper is clicked
    void RandomRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-rotationRandomness, rotationRandomness)); //Set to a random rotation

        int RandomSprite = Random.Range(0, randomSprites.Length); //Get a random sprite

        //Set the sprite in accordance
        render.sprite = randomSprites[RandomSprite];
    }
}
