using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEvents : MonoBehaviour
{
    public string SceneToStart;
    public GameObject EndInterface;
    // Start is called before the first frame update
    void Start()
    {
        EndInterface = GameObject.Find("EndInterface");
    }

    // Update is called once per frame
    void Update()
    {
        print(Time.timeScale);
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

