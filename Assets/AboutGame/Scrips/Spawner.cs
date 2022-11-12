using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public float SpawnGap;
    public TheEvents TheEvents;
    private float StartTime;
    private float LastSpawnTime;
    public Transform[] SpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        TheEvents = GameObject.Find("TheEvents").GetComponent<TheEvents>();
        StartTime = Time.time;
        SpawnPoints = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnGap = TheEvents.SpawnGap;
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
