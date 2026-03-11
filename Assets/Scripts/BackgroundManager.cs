using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject bckPrefab;
    public GameObject[] bcks;
    public float pivotPoint;
    public float speed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bcks = new GameObject[3];
        for (int i = 0 ; i < 3; i++)
        {
            float xPos = pivotPoint * (pivotPoint/2 *i);
            float yPos = pivotPoint * (pivotPoint/2 *i);
            Vector3 pos = new Vector3(xPos, yPos, 0.0f);
            bcks[i] = Instantiate(bckPrefab, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
