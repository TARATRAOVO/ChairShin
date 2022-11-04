using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public Transform[] ExitPoints;
    public Transform Dish;
    public float EndDistance = 0.5f;
    public bool IfGameEnd = false;
    public GameObject TheEvents;
    // Start is called before the first frame update
    void Start()
    {
        ExitPoints = GetComponentsInChildren<Transform>();
        Dish = GameObject.FindGameObjectWithTag("Target").transform;
        TheEvents = GameObject.Find("TheEvents");
    }

    // Update is called once per frame
    void Update()
    {
        CheckEndGame();
    }

    public void CheckEndGame()
    {
        foreach (var ExitPoint in ExitPoints)
        {
            if (Vector3.Distance(new Vector3(Dish.position.x, 0, Dish.position.z), ExitPoint.position) < EndDistance)
            {
                TheEvents.SendMessage("Lose");
                Destroy(Dish.gameObject);//如果没有destroy的话会发送多次Lose，使得游戏重开之后还是时间暂停
            }
        }
    }
}
