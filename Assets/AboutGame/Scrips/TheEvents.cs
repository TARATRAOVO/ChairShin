using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEvents : MonoBehaviour
{
    public string SceneToStart = "Playground";
    public bool IsEnd = false;
    public GameObject EndInterface;
    public float MaxChair = 20;
    public float MaxPizza = 4.0f;
    public float SpawnGap = 4.0f;
    public float TimeToEat = 10.0f;
    public float StartTime;
    public float CookingCost = 10.0f;
    public float GameLength = 250.0f;
    public float CurrentUsingChair;
    public int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
        

    }

    // Update is called once per frame
    void Update()
    {
        CheckUsingChairs();
        SpawnSpeedUp();
    }

    public void CheckUsingChairs()
    {
        CurrentUsingChair = GameObject.FindGameObjectsWithTag("Chairs").Length;
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneToStart);
    }

    public void Lose()
    {
        Time.timeScale = 0;
        EndInterface.SetActive(true);
    }
    public void SpawnSpeedUp()
    {
        SpawnGap -= 3.5f*(Time.deltaTime/GameLength);
        
    }

    


}

