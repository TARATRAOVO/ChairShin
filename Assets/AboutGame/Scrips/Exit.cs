using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public Transform[] ExitPoints;
    public GameObject[] Dishes;
    public float EndDistance = 0.5f;
    public int liferemain = 2;
    public bool IfGameEnd = false;
    public GameObject TheEvents;
    // Start is called before the first frame update
    void Start()
    {
        ExitPoints = GetComponentsInChildren<Transform>();
        ExitPoints[0] = ExitPoints[1];
        Dishes = GameObject.FindGameObjectsWithTag("Target");
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
            foreach (GameObject Dish in Dishes)
            {
                float DishExitDistance = Vector3.Distance(new Vector3(Dish.transform.position.x, 0, Dish.transform.position.z), ExitPoint.position);
                if (DishExitDistance < EndDistance)
                {
                    liferemain -= 1;
                    {
                        if (liferemain <= 0)
                        {
                            TheEvents.SendMessage("Lose");
                            this.gameObject.SetActive(false);//停止发送“Lose”信号
                        }
                    }

                }
            }

        }
    }
}
