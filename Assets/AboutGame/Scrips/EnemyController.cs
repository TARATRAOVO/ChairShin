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
    public bool Alive = true;
    public GameObject ScoreBoard;
    public int ScoreValue = 10;
    public float ParalyzingPower = 2.0f;
    public float SitDistance = 0.5f;
    public float PickDistance = 1.0f;
    public Animator Anim;

    public GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        CurrentHealth = MaxHealth;
        Targets = GameObject.FindGameObjectsWithTag("Target");
        Exit = GameObject.FindGameObjectWithTag("Exit");
        ScoreBoard = GameObject.FindGameObjectWithTag("ScoreBoard");
        Anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (DishPicking)
        {
            Destination = ChoseAnExit();
        }
        else if(OnSit)
        {
            Destination = Player.transform;
        }
        else
        {
            Destination = ChooseATarget();
        }


        Vector3 ToForward = Vector3.Normalize(Destination.position - transform.position);
        transform.forward = new Vector3(ToForward.x, transform.forward.y, ToForward.z);

        if (Alive == true)
        {
            PickTheDish();
            CarryTheDish();
        }
        CheckDeath();
        DetectChair();
    }

    public Transform ChoseAnExit()
    {
        Transform TheExit = ChooseTheNearestTransform(Exit.GetComponent<Exit>().ExitPoints);
        return TheExit;
    }

    public Transform ChooseATarget()
    {
        GameObject TheTarget = ChooseTheNearestGameObject(GameObject.FindGameObjectsWithTag("Target"));
        return TheTarget.transform;
    }

    public Transform ChooseTheNearestTransform(Transform[] TheOnes) //输入一组transform，输出距离本物体最近的物体
    {
        Transform TheOne = TheOnes[0];

        float TheDistance = Vector3.Distance(TheOne.position, transform.position);
        foreach(Transform One in TheOnes)
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
        foreach(GameObject One in TheOnes)
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
            ScoreBoard.GetComponent<ScoreBoard>().Score += ScoreValue;
            Destroy(gameObject);
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

    public void Leave()
    {

    }

    public void PickTheDish()
    {
        float TheDistance;
        Transform Target = ChooseATarget();
        TheDistance = Vector3.Distance(Target.position, this.transform.position);
        if (TheDistance < PickDistance)
        {
            DishPicking = Target.gameObject;
            Target.GetComponent<Target>().IsOnTable = false;
        }
    }

    public void CarryTheDish()
    {
        if (DishPicking)
        {
            DishPicking.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1.6f, this.transform.position.z);
            DishPicking.GetComponent<Target>().WhoTag = this.tag;
            DishPicking.GetComponent<Target>().IsPickedUp = true;
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
        Anim.applyRootMotion = false;
        ChairToSit.position = transform.position;
        ChairToSit.rotation = transform.rotation;
        Anim.SetTrigger("Sit");
        ChairToSit.GetComponent<Duplicate>().BeSitted = true;

        ChairToSit.parent = this.transform;
    }

}
