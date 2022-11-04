using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicate : MonoBehaviour
{
    public GameObject CurrentChair;
    public GameObject Towers;
    // Start is called before the first frame update
    void Start()
    {
        CurrentChair = this.gameObject;
        Towers = GameObject.Find("Towers");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DoDuplicate()
    {
        Instantiate(CurrentChair, this.transform.position, this.transform.rotation, Towers.transform);
    }
}
