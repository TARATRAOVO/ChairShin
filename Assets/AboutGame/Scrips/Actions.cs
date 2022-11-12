using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Actions : MonoBehaviour
{
    public GameObject[] TargetList;
    public GameObject[] EnemiesList;
    public StarterAssets.StarterAssetsInputs SInput;
    public PlayerInput Inputs;
    public float PickDistance;
    public GameObject DishPicking; //现在拿着的盘子
    public GameObject TheEvents;
    public bool isParalyzed = false; //是否处于被攻击的麻痹状态
    public float ParalyzedRemain = 0;
    public float GoldenTimeRemain = 0;
    public float GoldenTime = 0.3f;
    public bool CanChairBePlaced = true;
    public float MinChairDistance = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        PickDistance = 2.0f;
        Inputs = GetComponent<PlayerInput>();
        SInput = GetComponent<StarterAssets.StarterAssetsInputs>();
        TheEvents = GameObject.Find("TheEvents");
        SitTheChair();
    }

    // Update is called once per frame
    void Update()
    {
        CarryTheDish();
        ParalyzedChecker();
        CheckIfChairCanBePlaced();
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
                if (Distance <= PickDistance)
                {
                    DishPicking = Target;
                    DishPicking.GetComponent<Target>().WhoTag = this.tag;
                    DishPicking.GetComponent<Target>().IsPickedUp = true;
                    Target.GetComponent<Target>().IsOnTable = false;
                    Target.GetComponent<Target>().IsOnChef = false;
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
            SInput.fire = false;
            Inputs.enabled = false;
            ParalyzedRemain -= Time.deltaTime;

            if (ParalyzedRemain <= 0)//恢复
            {
                isParalyzed = false;
                Inputs.enabled = true;
                GoldenTimeRemain = GoldenTime;
            }
        }
        else
        {
            GoldenTimeRemain -= Time.deltaTime;
            if (GoldenTimeRemain <= 0)
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
    }



    public void CarryTheDish() //带着盘子到处走
    {
        if (DishPicking)
        {
            DishPicking.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z);
        }
    }

    public void SitTheChair()
    {
        gameObject.GetComponentInChildren<Duplicate>().BeSitted = true;
    }

    public void CheckIfChairCanBePlaced()
    {
        foreach (Transform TowerChair in GameObject.Find("Towers").transform)
        {
            if (Vector3.Distance(transform.position, TowerChair.position) < MinChairDistance)
            {
                CanChairBePlaced = false;
                return;
            }
        }

        if(TheEvents.GetComponent<TheEvents>().CurrentUsingChair >= TheEvents.GetComponent<TheEvents>().MaxChair)
        {
            CanChairBePlaced = false;
            return;
        }
        
        CanChairBePlaced = true;
    }
}
