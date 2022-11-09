using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Actions : MonoBehaviour
{
    public GameObject[] TargetList;
    public GameObject[] EnemiesList;
    public PlayerInput Inputs;
    public float PickDistance = 1.0f;
    public GameObject DishPicking; //现在拿着的盘子
    public bool isParalyzed = false; //是否处于被攻击的麻痹状态
    public float ParalyzedRemain = 0;

    // Start is called before the first frame update
    void Start()
    {
        Inputs = GetComponent<PlayerInput>();
        SitTheChair();
    }

    // Update is called once per frame
    void Update()
    {
        CarryTheDish();
        ParalyzedChecker();
    }
    private void FixedUpdate()
    {

    }
    public void Pick()//捡盘子
    {

        if (DishPicking)
        {
            DropTheDish();
        }

        else
        {
            TargetList = GameObject.FindGameObjectsWithTag("Target");//所有在场的盘子

            foreach (var Target in TargetList)//观察所有盘子的距离，在按下捡盘子键时捡起附近的盘子
            {
                float Distance = Vector3.Distance(this.transform.position, Target.transform.position);
                if (Distance <= 1)
                {
                    DishPicking = Target;
                    Target.GetComponent<Target>().IsOnTable = false;
                }
            }
        }
    }

    public void DropTheDish()
    {
        DishPicking.GetComponent<Target>().IsPickedUp = false;
        DishPicking = null;
    }

    public void ParalyzedChecker()//决定角色是否处于麻痹状态
    {
        if (isParalyzed)
        {
            if (DishPicking)
            {
                DropTheDish();
            }

            Inputs.enabled = false;
            ParalyzedRemain -= Time.deltaTime;

            if (ParalyzedRemain <= 0)
            {
                isParalyzed = false;
                Inputs.enabled = true;
            }
        }

        else
        {
            EnemiesList = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var Enemy in EnemiesList)
            {
                float Distance1 = Vector3.Distance(this.transform.position, Enemy.transform.position);
                if (Distance1 <= 1)
                {
                    isParalyzed = true;
                    ParalyzedRemain = Enemy.GetComponent<EnemyController>().ParalyzingPower;
                }
            }
        }
    }



    public void CarryTheDish() //带着盘子到处走
    {
        if (DishPicking)
        {
            DishPicking.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z);
            DishPicking.GetComponent<Target>().WhoTag = this.tag;
            DishPicking.GetComponent<Target>().IsPickedUp = true;
        }
    }

    public void SitTheChair()
    {
        gameObject.GetComponentInChildren<Duplicate>().BeSitted = true;
    }
}
