using System;
using UnityEngine;

public class TopBorderBehavior : MonoBehaviour
{
    public float timeout;
    private float timeStart;
    private float timeThusFar;
    public GameObject gameOver;
    //public bool isGameOver = false;
    
    public PlayerBehavior player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeStart = Time.time;
        if (player == null) // Check to auto set the reference because we only have one player.
            player = FindObjectOfType<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        string tag =  collision.gameObject.tag;
        //Debug.Log("You Entered the trigger ofL " + collision.gameObject.tag);
        if (tag.Equals("Ball"))
        {
            //Debug.Log("Game Over Timer Started at: " + timeStart);
            timeStart = Time.time;
        }
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!player.isGameOver)
        {
            string tag =  collision.gameObject.tag;
            //Debug.Log("Trigger Stay on: " + collision.gameObject.tag);
            if (tag.Equals("Ball"))
            {
                timeThusFar = Time.time - timeStart;
                Debug.Log("Game over Timer Updated: " + timeThusFar);
                if (timeThusFar >= timeout)
                {
                    Debug.Log("Game Over");
                    player.isGameOver = true;
                    gameOver.SetActive(true);
                } 
            }
        }


    }
}
