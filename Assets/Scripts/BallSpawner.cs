using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallSpawner : MonoBehaviour {


    public GameObject ball;
    Timer timer;
    Vector2 LeftBottomPoint;
    Vector2 RightTopPoint;

    bool retrySpawn = false;


    public static Collider2D OverlapArea;
	// Use this for initialization
	void Start () {
        timer = this.gameObject.GetComponent<Timer>();
        timer.Duration = Random.Range(ConfigurationUtils.MinSpawnTime, ConfigurationUtils.MaxSpawnTime);
        timer.Run();
        GameObject tempBall = Instantiate(ball);
        BoxCollider2D collider2D = tempBall.GetComponent<BoxCollider2D>();
        LeftBottomPoint = new Vector2(tempBall.transform.position.x - collider2D.size.x / 2.0f, tempBall.transform.position.y - collider2D.size.y / 2.0f);
        RightTopPoint= new Vector2(tempBall.transform.position.x + collider2D.size.x / 2.0f, tempBall.transform.position.y + collider2D.size.y / 2.0f);
        Destroy(tempBall);

        EventManager.AddListener(EventName.BallDiedEvent, SpawnTheBall);
        EventManager.AddListener(EventName.BallReducedEvent, SpawnTheBall);

        SpawnTheBall();
    }
    void SpawnTheBall() {
        if (Physics2D.OverlapArea(LeftBottomPoint, RightTopPoint) == null&&SceneManager.GetActiveScene().name=="GamePlay")
        {
            retrySpawn = false;
            Instantiate(ball);
        }
        else
        {
            retrySpawn = true;
        }
    }
	// Update is called once per frame
	void Update () {
        RandomTimeSpawnBall();

        if (retrySpawn)
        {
            SpawnTheBall();
        }
	}
    void RandomTimeSpawnBall()
    {

        if (!timer.running)
        {
            SpawnTheBall();
            timer.Duration = Random.Range(ConfigurationUtils.MinSpawnTime, ConfigurationUtils.MaxSpawnTime);
            timer.Run();

        }

    }
}
