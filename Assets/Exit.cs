using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public Transform[] ExitPoints;
    // Start is called before the first frame update
    void Start()
    {
        ExitPoints = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
