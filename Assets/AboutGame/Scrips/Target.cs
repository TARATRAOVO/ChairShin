using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool IsPickedUp = false;
    public string WhoTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TickUnpick();
    }

    public void PickORPlace()
    {
        
    }

    public void TickUnpick()
    {
        if(IsPickedUp == false)
        {
            WhoTag = null;
        }
    }
}
