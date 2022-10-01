using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public GameObject Target;
    public bool IsPicking = false;
    private Vector3 Destination;
    public float MaxHealth = 10;
    public float CurrentHealth;
    public GameObject DishPicking;
    public GameObject Exit;
    public bool Alive = true;
    public GameObject ScoreBoard;
    public int ScoreValue = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        Target = GameObject.FindGameObjectWithTag("Target");
        Exit = GameObject.FindGameObjectWithTag("Exit");
        ScoreBoard = GameObject.FindGameObjectWithTag("ScoreBoard");
    }

    // Update is called once per frame
    void Update()
    {
        if (DishPicking)
        {
            Destination = Exit.GetComponent<Exit>().ExitPoints[0].position;
        }
        else
        {
            Destination = Target.transform.position;
        }

        Vector3 ToForward = Vector3.Normalize(Destination - transform.position);
        transform.forward = new Vector3(ToForward.x, transform.forward.y, ToForward.z);

        CheckDeath();
        if (Alive == true)
        {
            PickTheDish();
            CarryTheDish();
        }
    }


    public void CheckDeath()
    {
        if (CurrentHealth <= 0)
        {
            if (DishPicking)
            {
                DishPicking.transform.position = new Vector3(this.transform.position.x, 0.3333f, this.transform.position.z);
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
        TheDistance = Vector3.Distance(Target.transform.position, this.transform.position);

        if (TheDistance <= 0.5)
        {
            DishPicking = Target;
        }
    }

    public void CarryTheDish()
    {
        if (DishPicking)
        {
            DishPicking.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1.6f, this.transform.position.z);
        }
    }

}
