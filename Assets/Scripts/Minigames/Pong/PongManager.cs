using UnityEngine;
using UnityEngine.InputSystem;

public class PongManager : MonoBehaviour
{
    [SerializeField] GameObject PongTab; //Reference to the pong tab

    private Camera mainCam;

    [Header("Paddles")]

    [SerializeField] GameObject playerPaddle; //Paddle that the player wil control
    [SerializeField] Vector3 PlayerPaddlePos; //Starting position of the player paddle

    [SerializeField] GameObject AIPaddle; //Paddle that the ai will control
    [SerializeField] Vector3 AIPaddlePos; //Starting position of the AI Paddle


    [Header("Ball")]

    [SerializeField] GameObject Ball; //Reference to the ball

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get the local positions to set for the paddle vectors
        PlayerPaddlePos = playerPaddle.transform.position;

        AIPaddlePos = AIPaddle.transform.position;

        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (PongTab.transform.localScale == Vector3.zero) return; //Don't run if not on this tab

        #region Player

        Vector2 mousepos = Mouse.current.position.ReadValue(); //Get the mouse position

        Vector3 screenMouse = mainCam.ScreenToWorldPoint(mousepos); //Turn it into a vector 3 transform

        //Clamp y between 2.20, -3.45, positive being the upper bounds and negative being the lower bounds
        float y = Mathf.Clamp(screenMouse.y, -3.45f, 2.20f);

        playerPaddle.transform.position = new Vector3(PlayerPaddlePos.x, y, 0); //Set the player paddle position

        #endregion


        #region AI

        float yClamp = Mathf.Clamp(Ball.transform.position.y, -3.45f, 2.20f); //Clamp the paddles movement

        AIPaddle.transform.position = new Vector3(AIPaddlePos.x, yClamp, 0); //Move the AI paddle where the ball goes

        #endregion
    }
}
