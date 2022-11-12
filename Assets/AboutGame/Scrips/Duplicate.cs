using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicate : MonoBehaviour
{
    public GameObject CurrentChair;
    public GameObject Towers;
    public bool BeSitted = false;
    // Start is called before the first frame update
    void Start()
    {
        Towers = GameObject.Find("Towers");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DoDuplicate()
    {
        Vector3 ToDuPlace = new Vector3(this.transform.position.x, 0.01f, this.transform.position.z);
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Actions>().CanChairBePlaced == true)
        {
            Instantiate(CurrentChair, ToDuPlace, this.transform.rotation, Towers.transform);
        }
    }

}
