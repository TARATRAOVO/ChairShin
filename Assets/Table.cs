using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public GameObject[] Targets;
    public float OnTableDistance = 1.0f;
    public GameObject CurrentChair;
    public GameObject Towers;
    // Start is called before the first frame update
    void Start()
    {
        Targets = GameObject.FindGameObjectsWithTag("Target");
        Towers = GameObject.Find("Towers");
        DoDuplicate();

    }

    // Update is called once per frame
    void Update()
    {
        CheckOnTable();
        
    }

    public void CheckOnTable()
    {
        foreach (GameObject Target in Targets)
        {
            float TheDistance = Vector3.Distance(Target.transform.position, transform.position);
            if (TheDistance < OnTableDistance)
            {
                OnTable(Target);
            }
        }
    }

    public void OnTable(GameObject Target)
    {
        if (!Target.GetComponent<Target>().IsPickedUp)
        {
            if (!Target.GetComponent<Target>().IsOnTable)
            {
                Target.transform.position = this.transform.position;
                Target.GetComponent<Target>().IsOnTable = true;
            }

        }

    }
    public void DoDuplicate()
    {
        Vector3 FollowChairPosition = new Vector3(this.transform.position.x + 1.0f, this.transform.position.y, this.transform.position.z);
        Instantiate(CurrentChair, FollowChairPosition, this.transform.rotation, Towers.transform);
    }
}
