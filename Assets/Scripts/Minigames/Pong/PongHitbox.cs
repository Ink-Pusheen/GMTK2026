using UnityEngine;

public class PongHitbox : MonoBehaviour
{

    [Header("FlipState")]

    [SerializeField] bool bHorizontalChange; //Should this effect the balls horizontal velocity?
    [SerializeField] bool bVerticalChange; //Should this effect the balls vertical velocity?

    [SerializeField] bool bResetBall; //Is this a reset ball trigger?

    [SerializeField] bool bRestoreAwakeMeter; //Is this trigger meant to wake the player up?
    [SerializeField] float sleepReturned; //How much time does the player get back on their timer?

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PongBall ball = collision.gameObject.GetComponent<PongBall>(); //Try to see if the hit object has this script

        if (ball is null) return; //Don't do anything if not ball

        if (bHorizontalChange) ball.SwitchHorizontalVel(); //Switch the balls horizontal velocity

        if(bVerticalChange) ball.SwitchVerticalVel(); //Switch the balls vertical velocity

        if (bRestoreAwakeMeter) PlayerScript.instance.RemoveSleepTime(sleepReturned);

        if (bResetBall)
        {
            ball.ResetBall();

            ball.SwitchHorizontalVel();

            // 50/50 chance that the ball changes vertical tragectory
            int vertChance = Random.Range(0, 2);

            if (vertChance is 0)
            {
                //Do not flip the vertical
            }

            else if (vertChance is 1) ball.SwitchVerticalVel();
        }
    }
}
