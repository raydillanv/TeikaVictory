using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject bckPrefab;
    public GameObject[] bcks;
    public float pivotPoint;
    public float speed;
    public float scale;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //pivotPoint = scale * 16 * -0.32f;
        bckPrefab.transform.localScale = new Vector3(scale, scale, 0.0f);
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
        for (int i = 0 ; i < 3; i++)
        {
            float xPos = bcks[i].transform.position.x + speed * Time.deltaTime;
            float yPos = bcks[i].transform.position.y + speed * Time.deltaTime;
            Vector3 pos = new Vector3(xPos, yPos, 0.0f);
            
            if (bcks[i].transform.position.x > -pivotPoint / 2)
            {
                pos = new Vector3(pivotPoint, pivotPoint, 0.0f);
            }
            bcks[i].transform.position = pos;
        }
    }
}
