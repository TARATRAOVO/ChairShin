using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairsCollection : MonoBehaviour
{
    public GameObject Towers;

    // Start is called before the first frame update
    void Start()
    {
        Towers = GameObject.Find("Towers");

        foreach (Transform every in this.transform)
        {
            print(every.gameObject.name);
            every.parent = Towers.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
