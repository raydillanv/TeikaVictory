using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public Sprite[] UISprites;
    public int[] queue;
    private SpriteRenderer[] childRenderers;
    
    public int maxQueueId;
    
    /// <summary>
    /// Using a Queue. Showing the next 4 balls that will drop. 
    /// 0 (the next ball), 2 (the second to next ball), 3 (etc.), 4
    /// Every time you drop one, you move things up. 
    /// </summary>
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        queue = new int[4];
        for (int i = 0; i < 4; i++) {
            queue[i] = Random.Range(0, 4);
        }
        
        childRenderers = new SpriteRenderer[4];
        for (int i = 0; i < transform.childCount; i++)
        {
            childRenderers[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            childRenderers[i].sprite = UISprites[queue[i]];
        }
    }

    public int updateQueue()
    {
        
        Debug.Log("Updating Queue");
        int currentType = queue[0];

        for (int i = 1; i < 4; i++)
        {
            queue [ i - 1 ] =  queue[i];
        }
        
        queue[3] = Random.Range(0, maxQueueId);

        return currentType;
    }
    
}
