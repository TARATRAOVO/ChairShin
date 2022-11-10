using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndScript : MonoBehaviour
{
    public GameObject TheEvents;
    public GameObject EndInterface;
    // Start is called before the first frame update
    void Start()
    {
        TheEvents = GameObject.FindGameObjectWithTag("TheEventsController");
        EndInterface.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickRestart()
    {
        TheEvents.SendMessage("RestartGame");
    }
}
