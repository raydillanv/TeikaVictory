using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.PlayerLoop;

// Merging logic
//bowling pin -> tennis ball -> baseball - > 8 ball -> hockey puck -> 
//basket ball - > soccer ball -> ball star -> bowling ball, football, Toxic waste?

//Logic needed 
//A list of each of the prefabs and variants
//instantiate and drop a random one in the list... or array
//Check if they are colliding with another prefab
//if they are another of the same type then merge them
//instantiate new one destroy old one

//Difficulty scaling? 
//You would need to track what the last prefab instantiated was
//if you are in late game make it a small percentage to drop one of the same type?
//Increase speed
//Drop automatically within a certain time , time goes lower while playing

public class PlayerBehavior : MonoBehaviour
{
    //Determine hoe fast the player moves
    public float speed;
    //Object in player's hands
    //public GameObject HeldObject;
    //Current prefab of the object
    private GameObject CurrentHeldObject;
    
    private QueueManager Queue;

    public float OffY = -0.6f;
    public float OffX = 0.5f;

    public float min;
    public float max;
    
    //Custom logic for changing the sprite of the scientist while dropping
    public SpriteRenderer spriteRenderer;
    //Sprite of scientist showing arm dropping item
    public Sprite droppingSprite;
    //Base scientist sprite of the scientist not doing anything
    public Sprite BaseSprite;

    public GameObject[] heldObjects;
    
    public bool isGameOver = false; //Bool that is set by TopBorderBehavior through a reference to the player.

    //public int[] numbers;
    
    public AudioSource dropSource;
    
    public int[] points;
    public int total;
    public TMP_Text textField;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Gets sprite renderer from component, sets base sprite as the original sprite on the sprite renderer
        spriteRenderer =  GetComponent<SpriteRenderer>();
        BaseSprite = spriteRenderer.sprite;
        
        
        //Use case of array
        //for (int i = 0; i < numbers.Length; i++)
        //{
        //    print(numbers[i]);
        //}
        
        total = 0;
        dropSource = gameObject.GetComponents<AudioSource>()[1];

        Queue = GameObject.FindGameObjectWithTag("Queue").GetComponent<QueueManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver) // If the game is over then we do no checking for player movement etc. 
        {
                   float currentTime = Time.time;
        //print(currentTime);
        
        // Custom logic for setting sprite of scientist to droppingSprite
        if (Keyboard.current.spaceKey.isPressed)
        {
            if (droppingSprite != null)
            {
                spriteRenderer.sprite = droppingSprite;
            }

        }
        if (!Keyboard.current.spaceKey.isPressed)
        {
            if (BaseSprite != null)
            {
                spriteRenderer.sprite = BaseSprite; 
            }

        }
        
        //If we are holding something put in player's hand
        if(CurrentHeldObject != null){
            //current player position
            //Only need transform.position
            Vector3 playerPos = transform.position;
            Vector3 ObjectOffset = new Vector3(OffX, OffY, 0.0f);
            CurrentHeldObject.transform.position  = playerPos + ObjectOffset;
        }
        else
        {
            
            //The player is asking the queue manager what the next number is...
            int choice = Queue.updateQueue();
            //int choice = Random.Range(0, heldObjects.Length);
            CurrentHeldObject = Instantiate(heldObjects[choice], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        }
        //Drop item
        if (Keyboard.current.spaceKey.wasPressedThisFrame){
            //if holding something drop it...
            if (CurrentHeldObject != null)
            {
                Rigidbody2D body = CurrentHeldObject.GetComponent<Rigidbody2D>();

                body.gravityScale = 1.0f;

                Collider2D collider = CurrentHeldObject.GetComponent<Collider2D>();
            
                collider.enabled = true;

                CurrentHeldObject = null;
                
                //PLay our drop sound which is attached to the second audio source attached to hte player at Index [1]
                dropSource.Play();
            }

        }
        
        //Keyboard movement of player
        float offset = 0.0f;
        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed){
            //Debug.Log("Left arrow  OR A key was pressed.");
            offset -= speed;
            //Debug.Log(offset);
            

        } else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed){
            //Debug.Log("Right arrow OR D Key was pressed.");
            offset = speed;
            //Debug.Log(offset);
        } 
        
        //Better way of doing this is with a collider and an on trigger collider instead of hardcoded min and max values
        
        Vector3 newPos = transform.position;
        newPos.x = newPos.x + offset;

        //Checks if player is trying to go past max X
        if (newPos.x > max)
        {
            newPos.x = max;
        }
        //Checks if player is trying to go past min X
        if (newPos.x < min)
        {
            newPos.x = min;
        }
        
        transform.position = newPos;
         
        }

    }
    
    public void updateScore(int index) {
        total = total + points[index];
        textField.SetText("Score: " + total);
    }
    
}
