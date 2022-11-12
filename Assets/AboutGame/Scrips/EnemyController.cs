using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public GameObject[] Targets;
    public bool IsPicking = false;
    public bool OnSit = false;
    public Transform Destination;
    public float MaxHealth = 1;
    public float CurrentHealth;
    public GameObject DishPicking;
    public GameObject Exit;
    public GameObject[] Chairs;
    public bool Alive = true;
    public TheEvents TheEvents;
    public int ScoreValue = 10;
    public float ParalyzingPower = 0.5f;
    public float SitDistance = 1.0f;
    public float PickDistance = 2.0f;
    public float EatDistance = 4.0f;
    public float TimeToEat;
    public float ChairArmor = 1.0f;
    public float PizzaEaten;
    public float StealDelay = 5.0f;
    public float StealRemain;
    public float CrazySpeed = 3.0f;
    public bool IsFull = false;
    public Animator Anim;
    public GameObject Player;
    public Rigidbody _rigidbody;
    public float ChangeDirectionDelay = 3.0f;
    public float ChangeDirectionRemain;
    public Vector3 AfterDinnerDestination;


    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        TheEvents = GameObject.Find("TheEvents").GetComponent<TheEvents>();
        CurrentHealth = MaxHealth;
        TimeToEat = TheEvents.TimeToEat;
        Targets = GameObject.FindGameObjectsWithTag("Target");
        Exit = GameObject.FindGameObjectWithTag("Exit");
        Anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Destination = null;
        Targets = GameObject.FindGameObjectsWithTag("Target");//随时更新目标
        Chairs = GameObject.FindGameObjectsWithTag("Chairs");

        foreach (GameObject Chair in Chairs)
        {
            if (Chair.GetComponent<Duplicate>().BeSitted == false)
            {
                Destination = Chair.transform;
            }
        }

        if (!Destination)
        {
            Destination = ChooseATarget();
        }

        if (IsPicking)
        {
            Destination = ChooseAnExit();
        }

        if (OnSit)
        {
            GameObject Empty = GameObject.Find("Empty");
            Empty.transform.position = GetRandomLocation();
            Destination = Empty.transform;
        }


        Vector3 ToForward = Vector3.Normalize(Destination.position - transform.position);
        transform.forward = new Vector3(ToForward.x, transform.forward.y, ToForward.z);

        if (Alive == true)
        {
            MoveAfterDinner();
            PickTheDish();
            CarryTheDish();
            FindAndEatPizza();
        }
        CheckDeath();
        DetectChair();
    }

    public Transform ChooseAnExit()
    {
        Transform TheExit = ChooseTheNearestTransform(Exit.GetComponent<Exit>().ExitPoints);
        return TheExit;
    }

    public Transform ChooseATarget()
    {
        GameObject TheTarget = ChooseTheNearestGameObject(Targets);
        return TheTarget.transform;
    }

    public Transform ChooseAnEnemy()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject Enemy in Enemies)
        {
            if (!OnSit)
            {
                return Enemy.transform;
            }
        }
        return Enemies[0].transform;
    }

    public Transform ChooseTheNearestTransform(Transform[] TheOnes) //输入一组transform，输出距离本物体最近的物体
    {
        Transform TheOne = TheOnes[0];

        float TheDistance = Vector3.Distance(TheOne.position, transform.position);
        foreach (Transform One in TheOnes)
        {
            if (Vector3.Distance(One.position, transform.position) < TheDistance)
            {
                TheOne = One;

                TheDistance = Vector3.Distance(One.position, transform.position);
            }
        }
        return TheOne;
    }

    public GameObject ChooseTheNearestGameObject(GameObject[] TheOnes) //输入一组gameobject，输出距离本物体最近的物体
    {
        GameObject TheOne = TheOnes[0];

        float TheDistance = Vector3.Distance(TheOne.transform.position, transform.position);
        foreach (GameObject One in TheOnes)
        {
            if (Vector3.Distance(One.transform.position, transform.position) < TheDistance)
            {
                TheOne = One;

                TheDistance = Vector3.Distance(One.transform.position, transform.position);
            }
        }
        return TheOne;
    }

    public void CheckDeath()
    {
        if (CurrentHealth <= 0)
        {
            if (DishPicking)
            {
                DishPicking.transform.position = new Vector3(this.transform.position.x, 0.3333f, this.transform.position.z);
                DishPicking.GetComponent<Target>().WhoTag = "Enemy";
                DishPicking.GetComponent<Target>().IsPickedUp = false;
                DishPicking = null;
            }
            Alive = false;
            Destroy(gameObject);
        }
    }

    public void PickTheDish()//捡起附近的盘子
    {
        if (IsPicking == false && OnSit == false)
        {
            float TheDistance;
            Transform Target = ChooseATarget();
            TheDistance = Vector3.Distance(Target.position, this.transform.position);
            if (TheDistance < PickDistance)
            {
                DishPicking = Target.gameObject;
                Target.GetComponent<Target>().IsOnTable = false;
                Target.GetComponent<Target>().IsOnChef = false;
                DishPicking.GetComponent<Target>().WhoTag = this.tag;
                DishPicking.GetComponent<Target>().IsPickedUp = true;
            }
        }

    }
    public void CarryTheDish()//带着盘子移动
    {
        if (DishPicking)
        {
            DishPicking.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1.6f, this.transform.position.z);
            IsPicking = true;
        }
    }
    public void DetectChair()
    {
        foreach (Transform TowerChair in GameObject.Find("Towers").transform)
        {
            if (Vector3.Distance(transform.position, TowerChair.position) < SitDistance)
            {
                if (OnSit == false && IsPicking == false && TowerChair.GetComponent<Duplicate>().BeSitted == false)
                {
                    OnSit = true;
                    SitTheChair(TowerChair);
                }
            }
        }
    }

    public void SitTheChair(Transform ChairToSit)
    {
        CurrentHealth += ChairArmor;
        Anim.applyRootMotion = false;
        transform.position = ChairToSit.position;
        transform.rotation = ChairToSit.rotation;
        Anim.SetTrigger("Sit");
        ChairToSit.GetComponent<Duplicate>().BeSitted = true;
        ChairToSit.parent = this.transform;
    }

    public void FindAndEatPizza()//吃掉就近的pizza，然后吃饱，减少pizza，减少急，增加分数
    {
        if (IsFull == false)
        {
            if (OnSit)
            {
                foreach (GameObject Target in Targets)
                {
                    if (Target.GetComponent<Target>().IsOnTable == true)
                    {
                        if (Vector3.Distance(this.transform.position, Target.transform.position) < EatDistance)
                        {
                            if (Target.GetComponent<Target>().PizzaLeft >= 0)
                            {
                                PizzaEaten += Time.deltaTime / TimeToEat;
                                Target.GetComponent<Target>().PizzaLeft -= Time.deltaTime / TimeToEat;
                            }
                            if (PizzaEaten >= 1)
                            {
                                TheEvents.GetComponent<TheEvents>().Score += ScoreValue;
                                IsFull = true;
                            }
                        }
                    }

                }
            }
        }

    }

    public void MoveAfterDinner()
    {
        if (IsFull)
        {
            ChangeDirectionRemain -= Time.deltaTime;

            if (ChangeDirectionRemain <= 0)
            {
                ChangeDirectionRemain = ChangeDirectionDelay;
                AfterDinnerDestination = GetRandomLocation();
                Destination = ChooseAnEnemy();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, AfterDinnerDestination, CrazySpeed * Time.deltaTime);
            }

        }
    }


    public Vector3 GetRandomLocation()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
        int t = Random.Range(0, navMeshData.indices.Length - 3);
        Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
        point = Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);
        return point;
    }

}
