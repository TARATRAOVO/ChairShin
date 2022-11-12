using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public GameObject[] Targets;
    public float OnTableDistance;
    public GameObject CurrentChair;
    public GameObject Towers;
    public bool IsOn;
    // Start is called before the first frame update
    void Start()
    {
        OnTableDistance = 2.0f;
        Towers = GameObject.Find("Towers");
    }

    // Update is called once per frame
    void Update()
    {
        Targets = GameObject.FindGameObjectsWithTag("Target");
        CheckOnTable();
    }

    public void CheckOnTable()
    {
        foreach (GameObject Target in Targets)
        {
            float TheDistance = Vector3.Distance(Target.transform.position, transform.position);
            if (TheDistance < OnTableDistance)
            {
                if (Target.GetComponent<Target>().IsPickedUp == false && !IsOn)
                {
                    OnTable(Target);
                }
                if (Target.GetComponent<Target>().IsOnTable)
                {
                    return;
                }
            }
        }
        IsOn = false;

    }
    public void OnTable(GameObject Target)
    {
        Target.transform.position = this.transform.position;
        Target.GetComponent<Target>().IsOnTable = true;
        IsOn = true;
    }

}
