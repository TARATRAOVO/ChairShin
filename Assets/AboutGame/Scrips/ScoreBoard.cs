using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public Text ScoreText;
    public int Score;
    public GameObject TheEvents;

    // Start is called before the first frame update
    void Start()
    {
        TheEvents = GameObject.Find("TheEvents");
        ScoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "总流水（单位/$）：" + TheEvents.GetComponent<TheEvents>().Score + "亿";
    }

}
