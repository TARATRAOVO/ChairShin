using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    public float CookingCost = 1.5f;
    public float OnChefDistance = 1.5f;
    public GameObject[] Targets;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Targets = GameObject.FindGameObjectsWithTag("Target");
        CheckOnChef();
    }

    public void CheckOnChef()//寻找附近的盘子，并使其onchef
    {
        foreach (GameObject Target in Targets)
        {
            float TheDistance = Vector3.Distance(Target.transform.position, transform.position);
            if (TheDistance < OnChefDistance)
            {
                OnChef(Target);
            }
        }
    }
    public void OnChef(GameObject Target)
    {
        if (!Target.GetComponent<Target>().IsPickedUp)//如果不被人拿着
        {
            if (!Target.GetComponent<Target>().IsOnChef)//使其onchef
            {
                Target.transform.position = this.transform.position;
                Target.GetComponent<Target>().IsOnChef = true;
            }
            else
            {
                if (Target.GetComponent<Target>().PizzaLeft < 4.0f)
                {
                    Target.GetComponent<Target>().PizzaLeft += (Time.deltaTime / CookingCost);
                }

            }
        }
    }





}
