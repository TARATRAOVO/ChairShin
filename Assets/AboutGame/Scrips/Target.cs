using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool IsPickedUp = false;
    public bool IsCooking = false;
    public bool IsOnTable = false;
    public bool IsOnChef = false;
    public GameObject[] Pizzas;
    public string WhoTag;
    public float PizzaLeft;
    public float MaxPizza = 4.0f;
    public float PizzaPercent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TickUnpick();
        KeepOnGround();
        PizzaVision();
    }

    public void PickORPlace()
    {

    }

    public void TickUnpick()
    {
        if (IsPickedUp == false)
        {
            WhoTag = null;
        }
    }

    public void KeepOnGround()
    {
        if (!IsPickedUp)
        {
            if (!IsOnTable)
            {
                if (!IsCooking)
                {
                    transform.position = new Vector3(transform.position.x, 0.33333f, transform.position.z);
                }
            }
        }
    }

    public void PizzaVision()
    {
        PizzaPercent = PizzaLeft / MaxPizza;
        
        foreach(GameObject Pizza in Pizzas)
        {
            PizzaPercent -=  1.0f/Pizzas.Length;
            if(PizzaPercent >= 0)
            {
                Pizza.SetActive(true);
            }
            else
            {
                Pizza.SetActive(false);
            }
        }

    }

}
