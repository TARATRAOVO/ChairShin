using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleFire : MonoBehaviour
{
    public GameObject RifleBullet;
    public float FireLag = 0.2f;

    private float LastFireTime;
    private StarterAssets.StarterAssetsInputs Inputs;
    private GameObject Player;

    public float BulletSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        LastFireTime = 0.0f;
        Player = GameObject.FindGameObjectWithTag("Player");
        Inputs = Player.GetComponent<StarterAssets.StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (Inputs.fire)
        {
            if (Time.time - LastFireTime > FireLag)
            {
                GameObject TheBullet;
                TheBullet = Instantiate(RifleBullet, transform.position, transform.rotation);
                TheBullet.GetComponent<Rigidbody>().velocity = BulletSpeed * transform.forward;
                LastFireTime = Time.time;
            }
        }
    }


}
