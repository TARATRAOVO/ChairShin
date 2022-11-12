using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChairsRemain : MonoBehaviour
{
    public Text ChairsText;
    public GameObject TheEvents;
    // Start is called before the first frame update
    void Start()
    {
        TheEvents = GameObject.Find("TheEvents");
        ChairsText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ChairsText.text = "椅子剩余：" + (TheEvents.GetComponent<TheEvents>().MaxChair - TheEvents.GetComponent<TheEvents>().CurrentUsingChair) + "(" + TheEvents.GetComponent<TheEvents>().MaxChair + ")";
    }
}
