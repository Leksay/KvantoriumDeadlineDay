using UnityEngine;

public class NanoFluidSource : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject fluidPrefab;
    [SerializeField] private int fluidCount;
    [SerializeField] private float fluidSize;
    [SerializeField][Range(1,100)]private float spawnRate; 
    private float nextTimeSpawn;
    private float spawnTime;
    private int i;
    private GameObject[] fluids;
    private Transform t;
    public void Activate()
    {
        gameObject.SetActive(true);
        i = 0;
        nextTimeSpawn = Time.time + spawnTime;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        t = transform;
        spawnTime = 1.0f / spawnRate;
        fluids = new GameObject[fluidCount];
        gameObject.SetActive(false);
    }

    void Update()
    {
        if(i >= fluidCount)
        {
            Deactivate();
            return;
        }
        if(Time.time >= nextTimeSpawn)
        {
            fluids[i] = GameObject.Instantiate(fluidPrefab, t.position + Vector3.right * Random.Range(0.0f,0.05f), Quaternion.identity);
            nextTimeSpawn = Time.time + spawnTime;
            i++;
        }
    }

    private void CreateDrop()
    {
        
    }
}
