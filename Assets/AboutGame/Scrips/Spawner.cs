using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public int SpawnGap = 1;
    private float StartTime;
    private float LastSpawnTime;
    public Transform[] SpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
        SpawnPoints = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    void Spawn()
    {
        int SpawnInt = Random.Range(0,Enemies.Length);
        int SpawnPointInt = Random.Range(0, SpawnPoints.Length);

        if (Time.time - LastSpawnTime > SpawnGap)
        {
            int count = 0;
            while (count<= 0)
            {
                Instantiate(Enemies[SpawnInt],SpawnPoints[SpawnPointInt].position, SpawnPoints[SpawnPointInt].rotation);
                count += 1;
            }
            LastSpawnTime = Time.time;
        }
    }



}
