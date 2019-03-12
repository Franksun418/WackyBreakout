using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Paddle : MonoBehaviour {

    private Rigidbody2D rgbd2d;
    private Vector2 velocity;
    private BoxCollider2D boxCollider2D;
    public float halfLength;
    const float BounceAngleHalfRange = 60.0f * Mathf.Deg2Rad;

    public bool freezed;
    Timer timer;
    // Use this for initialization
    void Start () {
        rgbd2d = this.gameObject.GetComponent<Rigidbody2D>();
        velocity = new Vector2(ConfigurationUtils.PaddleMoveUnitsPerSecond, 0);
        boxCollider2D = this.GetComponent<BoxCollider2D>();
        halfLength = boxCollider2D.size.x/4.0f;
        timer = this.gameObject.AddComponent<Timer>();
        EventManager.AddFreezerEffectListener(freezerThePaddle);

        timer.AddTimerFinishedEventListener(HandleTimerFinishedEvent);

    }

    private void FixedUpdate()
    {
        CalculateClampedX();
        if(!freezed)
        rgbd2d.MovePosition(rgbd2d.position+ (velocity*Input.GetAxis("Horizontal")) * Time.deltaTime);
    }


    void CalculateClampedX() {
        if((this.gameObject.transform.position.x+halfLength)>ScreenUtils.ScreenRight)
            this.gameObject.transform.position = new Vector3 ((ScreenUtils.ScreenRight-halfLength),this.gameObject.transform.position.y,this.gameObject.transform.position.z);
        if ((this.gameObject.transform.position.x - halfLength) < ScreenUtils.ScreenLeft)
            this.gameObject.transform.position = new Vector3((ScreenUtils.ScreenLeft + halfLength), this.gameObject.transform.position.y, this.gameObject.transform.position.z);

    }
    /// <summary>
    /// Detects collision with a ball to aim the ball
    /// </summary>
    /// <param name="coll">collision info</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball")&&TopCollision(coll))
        {
            AudioManager.Play(AudioClipName.BallHitPaddle);
            // calculate new ball direction
            float ballOffsetFromPaddleCenter = transform.position.x -
                coll.transform.position.x;
            float normalizedBallOffset = ballOffsetFromPaddleCenter /
                halfLength;
            float angleOffset = normalizedBallOffset * BounceAngleHalfRange;
            float angle = Mathf.PI / 2 + angleOffset;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            // tell ball to set direction to new direction
            Ball ballScript = coll.gameObject.GetComponent<Ball>();
            ballScript.SetDirection(direction);
        }
    }
    bool TopCollision(Collision2D coll)
    {
        const float tolerance = 0.05f;

        // on top collisions, both contact points are at the same y location
        ContactPoint2D[] contacts = coll.contacts;
        return Mathf.Abs(contacts[0].point.y - contacts[1].point.y) < tolerance;
    }

    public void freezerThePaddle(float duration) {
        if (timer.running)
        {
            timer.Duration += duration;
        }
        else
        {
            timer.Duration = duration;
            timer.Run();
            freezed = true;
        }
    }

    public void HandleTimerFinishedEvent() {
        freezed = false;
    }
}
