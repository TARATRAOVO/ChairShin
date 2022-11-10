using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEvents : MonoBehaviour
{
    public string SceneToStart;
    public bool IsEnd = false;
    public GameObject EndInterface;
    public float MaxChair = 20;
    public float CurrentUsingChair;
    public int Score = 0;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckUsingChairs();
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

}

