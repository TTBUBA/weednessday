using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    [Header("Weather Settings")]
    [SerializeField] private float ProbabilitySpawnCloud = 0.1f;
    [SerializeField] private GameObject[] Cloud;
    [SerializeField] private Transform[] PointSpawnCloud;
    [SerializeField] private GameObject CurrentCloudSpawn;
    [SerializeField] private bool ActiveMoveCloud;
    [SerializeField] private Vector3 MaxDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        SpawnCloud();
    }

    public void SpawnCloud()
    {
        if (Random.value < ProbabilitySpawnCloud)
        {
            CurrentCloudSpawn.SetActive(true);
            CurrentCloudSpawn.transform.position = PointSpawnCloud[Random.Range(0, PointSpawnCloud.Length)].position;
        }
        else
        {
            CurrentCloudSpawn.SetActive(false);
        }
    }
}
