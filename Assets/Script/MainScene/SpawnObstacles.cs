using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnObstacles : MonoBehaviour {
    public Transform obstacle;
    private float timeBetweenWaves = 1f;
    private float timeToSpawn = 2f;
    private float zSpawnLoc = 40f;
    private float ySpawnLoc = 0.5f;
    private int lowerLimitofThings;
    private float timeToChangeWaveSpeed = 5f;
    private float time;//time after player starts playing
    List<float> points = new List<float>();

    private void Start()
    {        
        lowerLimitofThings = 1;
        if (obstacle.name.Equals("lowpolytree"))
            ySpawnLoc = 0.29f;
    }


    void Update () {
        time += Time.deltaTime;
       
        if (time >= timeToSpawn)
        {
            //x number of things to be spawned
            SpawnThings( Random.Range(lowerLimitofThings, 4));
            timeToSpawn = time + timeBetweenWaves;
        }

        if (time >= timeToChangeWaveSpeed && time < (timeToChangeWaveSpeed + Time.deltaTime) && timeBetweenWaves >= 0.68f)
        {
            lowerLimitofThings = 2;
            ChangeWaveSpeed();
        }
    }

   private void ChangeWaveSpeed()//change speed between waves
    {
        if (time >= timeToChangeWaveSpeed)
        {
            timeBetweenWaves -= 0.2f;
            timeToChangeWaveSpeed = timeToChangeWaveSpeed * 2f;
        }
    }

    private Vector3 SpawnSpawnPoints()//points where obstacles will be spawned later
    {
        float spawnPointX = Random.Range(-1, 3);

        Vector3 spawnPosition = new Vector3(spawnPointX, ySpawnLoc, zSpawnLoc);
        return spawnPosition;

    }


    private void SpawnThings( int numofItems)//spawning the obstacles
    {
       
        for(int i = 0; i < numofItems; i++)
        {
            Vector3 sPoint = SpawnSpawnPoints(); // get a point to spawn at 

            if (!points.Contains(sPoint.x))
            {
                points.Add(sPoint.x);
            }
            
            else
            {
                while (points.Contains(sPoint.x))
                {
                    sPoint.x++;
                    if (sPoint.x >= 3)
                        sPoint.x = -1;
                    
                }

                points.Add(sPoint.x);
            }

            if (obstacle.name.Equals("lowpolytree"))
                Instantiate(obstacle, sPoint, Quaternion.Euler(0, Random.Range(0f, 180f), 0));
            else Instantiate(obstacle, sPoint, Quaternion.identity);
        }
        points.Clear();    
    }

    public float ReturnTimeBetweenWaveSpeed()
    {
        return timeBetweenWaves;
    }

    public float CurrentTime()
    {
        return time;
    }
}
