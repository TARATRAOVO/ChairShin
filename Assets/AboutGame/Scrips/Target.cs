using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool IsPickedUp = false;
    public bool IsCooking = false;
    public bool IsOnTable = false;
    public bool IsOnChef = false;
    public string WhoTag;
    public float PizzaLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TickUnpick();
        KeepOnGround();
    }

    public void PickORPlace()
    {
        
    }

    public void TickUnpick()
    {
        if(IsPickedUp == false)
        {
            WhoTag = null;
        }
    }

    public void KeepOnGround()
    {
        if(!IsPickedUp)
        {
            if(!IsOnTable)
            {
                if(!IsCooking)
                {
                    transform.position = new Vector3(transform.position.x, 0.33333f, transform.position.z);
                }
            }
        }
    }
}
