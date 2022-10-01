using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    public GameObject[] TargetList;
    public float PickDistance = 1.0f;
    public GameObject DishPicking;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CarryTheDish();
    }
    private void FixedUpdate() {
        
    }
    public void Pick()
    {

        if (DishPicking)
        {
            DishPicking.transform.position = new Vector3(this.transform.position.x, 0.3333f, this.transform.position.z);
            DishPicking = null;
        }

        else
        {
            TargetList = GameObject.FindGameObjectsWithTag("Target");

            foreach (var Target in TargetList)
            {
                float Distance = Vector3.Distance(this.transform.position, Target.transform.position);
                if (Distance <= 1)
                {
                    DishPicking = Target;
                }
            }
        }
    }

    public void CarryTheDish()
    {
        if (DishPicking)
        {
            DishPicking.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z);
        }
    }
}
