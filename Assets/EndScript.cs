using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndScript : MonoBehaviour
{
    public GameObject TheEvents;
    // Start is called before the first frame update
    void Start()
    {
        TheEvents = GameObject.FindGameObjectWithTag("TheEventsController");
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickRestart()
    {
        TheEvents.SendMessage("RestartGame");
        this.gameObject.SetActive(false);
    }
}
