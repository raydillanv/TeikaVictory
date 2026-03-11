using System;
using UnityEngine;

public class SportBehavior : MonoBehaviour
{
    public float timeout;
    public float timeStart;
    public float timeThusFar;
    public GameObject gameOver;
    public bool GameOver = false;

    public GameObject[] Balls;

    public int BallType;
    
    private AudioSource mergeSource;
    //
    // // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Start()
     {
         timeStart = Time.time;
         Balls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>().heldObjects; 
         mergeSource = GameObject.FindGameObjectWithTag("Player").
             GetComponents<AudioSource>()[0];
     }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //     
    // }
    //
   
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ball")) {
            int otherType = other.gameObject.GetComponent<SportBehavior>().BallType;
            if (otherType == BallType && BallType < Balls.Length-1) {
                if (gameObject.transform.position.x < other.transform.position.x
                    || (gameObject.transform.position.x == other.transform.position.x 
                        && gameObject.transform.position.y >= other.transform.position.y)) {
                    
                    
                    // Create the merged one
                    int choice = BallType + 1;
                    GameObject currentBall = Instantiate(Balls[choice], 
                        Vector3.Lerp(gameObject.transform.position,other.gameObject.transform.position, 0.5f), 
                        Quaternion.identity);
                    currentBall.GetComponent<Collider2D>().enabled = true;
                    currentBall.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
                    
                    mergeSource.Play();
                    
                    GameObject.FindGameObjectWithTag("Player").
                        GetComponent<PlayerBehavior>().updateScore(BallType);
                    
                    // Destroy both things (Balls)
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}
