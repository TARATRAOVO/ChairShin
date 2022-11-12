using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    public float CookingCost;
    public float OnChefDistance;
    public bool IsOn;
    public TheEvents TheEvents;
    public GameObject[] Targets;
    // Start is called before the first frame update
    void Start()
    {
        OnChefDistance = 2.0f;
        TheEvents = GameObject.Find("TheEvents").GetComponent<TheEvents>();
    }

    // Update is called once per frame
    void Update()
    {
        CookingCost = TheEvents.CookingCost;
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
                if (Target.GetComponent<Target>().IsPickedUp == false && !IsOn)
                {
                    OnChef(Target);
                }
                if (Target.GetComponent<Target>().IsOnChef)
                {
                    OnChef(Target);
                    return;
                }
            }
        }
        IsOn = false;
    }
    public void OnChef(GameObject Target)
    {

        Target.transform.position = this.transform.position;
        Target.GetComponent<Target>().IsOnChef = true;
        IsOn = true;
        if (Target.GetComponent<Target>().PizzaLeft < Target.GetComponent<Target>().MaxPizza)
        {
            Target.GetComponent<Target>().PizzaLeft += (Time.deltaTime / CookingCost);
        }
    }





}
