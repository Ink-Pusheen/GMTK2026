using UnityEngine;

public class PongBall : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] float BallSpeedHorizontal; //Speed of the ball horizontally
    [SerializeField] float BallSpeedVertical; //Speed of the ball vertically

    [SerializeField] Vector3 startPos; //Start position of the ball

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position; //Get the initial position

        rb = GetComponent<Rigidbody2D>(); //Grab the rigidbody component

        UpdateVelocity(); //Update the velocity
    }

    //Functions to switch the velocity
    public void SwitchHorizontalVel()
    {
        BallSpeedHorizontal = -BallSpeedHorizontal;

        UpdateVelocity(); //Update the velocity
    }

    public void SwitchVerticalVel()
    {
        BallSpeedVertical = -BallSpeedVertical;

        UpdateVelocity(); //Update the velocity
    }


    //Update the velocity
    void UpdateVelocity()
    {
        rb.linearVelocity = new Vector2(BallSpeedHorizontal, BallSpeedVertical);
    }


    //Function to reset the ball
    public void ResetBall()
    {
        transform.position = startPos;
    }


}
