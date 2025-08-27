using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    public class CloudData
    {
        public Transform transform;
        public float speed;
        public Vector3 destinationPoint;
        public bool IsMoving;
    }

    [Header("Cloud Settings")]
    [SerializeField] private float probabilitySpawnCloud = 0.01f;
    [SerializeField] private GameObject[] cloudPrefabs;
    [SerializeField] private Transform[] pointSpawnCloud_Left;
    [SerializeField] private Transform[] pointSpawnCloud_Right;
    [SerializeField] private Transform[] pointArrivalLeft;
    [SerializeField] private Transform[] pointArrivalRight;
    [SerializeField] private bool activeSpawnCloud;

    [Header("Rain Settings")]
    [SerializeField] private float probabilityRain = 0.01f;
    [SerializeField] private ParticleSystem rainParticle;
    [SerializeField] private ParticleSystem RainRipple;
    [SerializeField] private float DurationRain;


    private List<CloudData> activeClouds = new List<CloudData>();
    public int lastDayChack = -1;


    public cycleDayNight cycleDayNight;
    private void Start()
    {
        rainParticle.Stop();
        RainRipple.Stop();
        StartCoroutine(ActiveWheater());
    }
    void Update()
    {
        SpawnCloud();
        MoveClouds();
    }

    //cloud System//

    //Move clouds in destination position
    private void MoveClouds()
    {
        List<CloudData> toRemove = new List<CloudData>();

        foreach (CloudData cloud in activeClouds)
        {
            if (!cloud.IsMoving) continue;

            cloud.transform.position = Vector3.MoveTowards(
                cloud.transform.position,
                cloud.destinationPoint,
                cloud.speed * Time.deltaTime
            );

            if (Vector3.Distance(cloud.transform.position, cloud.destinationPoint) < 0.1f)
            {
                cloud.IsMoving = false;
                cloud.transform.gameObject.SetActive(false);
                toRemove.Add(cloud);
            }
        }

        foreach (CloudData cloud in toRemove)
        {
            activeClouds.Remove(cloud);
        }
    }

    //spawm cloud in different position with probability and spawn in diffent position left or right
    private void SpawnCloud()
    {
        if (activeSpawnCloud)
        {
            for (int i = 0; i < cloudPrefabs.Length; i++)
            {
                if (!cloudPrefabs[i].activeInHierarchy)
                {
                    int spawnIndex = Random.Range(0, cloudPrefabs.Length);
                    int direction = Random.Range(0, 2);

                    cloudPrefabs[spawnIndex].SetActive(true);

                    Vector3 startPos, destPos;

                    if (direction == 0)
                    {
                        startPos = pointSpawnCloud_Left[Random.Range(0, pointSpawnCloud_Left.Length)].position;
                        destPos = pointArrivalLeft[Random.Range(0, pointArrivalLeft.Length)].position;
                    }
                    else 
                    {
                        startPos = pointSpawnCloud_Right[Random.Range(0, pointSpawnCloud_Right.Length)].position;
                        destPos = pointArrivalRight[Random.Range(0, pointArrivalRight.Length)].position;
                    }

                    cloudPrefabs[spawnIndex].transform.position = startPos;

                    CloudData cloudData = new CloudData
                    {
                        transform = cloudPrefabs[spawnIndex].transform,
                        speed = Random.Range(0.5f, 1f),
                        destinationPoint = destPos,
                        IsMoving = true
                    };

                    activeClouds.Add(cloudData);

                    Debug.Log("Cloud Spawned: " + cloudPrefabs[spawnIndex].name);
                    activeSpawnCloud = false;
                }
            }
        }
    }
    

    IEnumerator ActiveWheater()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            
            int currentDay = Mathf.FloorToInt(cycleDayNight.CurrentDay);

            if(currentDay > lastDayChack)
            {
                lastDayChack = currentDay;

                if (Random.value < probabilitySpawnCloud)
                {
                    activeSpawnCloud = true;
                }

                if (Random.value < probabilityRain)
                {
                    rainParticle.Play();
                    RainRipple.Play();
                    DurationRain = Random.Range(30, 80);
                    StartCoroutine(DisactiveRain());

                }
            }
        }
    }

    IEnumerator DisactiveRain()
    {
        yield return new WaitForSeconds(DurationRain);
        rainParticle.Stop();
        RainRipple.Stop();
        DurationRain = 0;
    }
}
