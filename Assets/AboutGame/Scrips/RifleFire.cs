using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleFire : MonoBehaviour
{
    public GameObject RifleBullet;
    public float FireLag = 0.2f;
    public float FireLagD = 3.0f;
    public bool IfAutoFire = false;
    private float LastFireTime;
    private StarterAssets.StarterAssetsInputs Inputs;
    private GameObject Player;


    public float BulletSpeed = 10.0f;
    public float DisappearAfter = 1.0f;
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
        if(transform.tag != "Player")
        {
            FireLag = FireLagD;
        }
        IfAutoFire = GetComponentInParent<Duplicate>().BeSitted;
        if(IfAutoFire)
        {
            AutoFire();
        }
        else
        {
            Fire();
        }

    }

    private void Fire()
    {
        if (Inputs.fire)
        {
            if (gameObject.GetComponentInParent<Duplicate>().BeSitted == true)
            {
                if (Time.time - LastFireTime > FireLag)
                {
                    GameObject TheBullet;
                    TheBullet = Instantiate(RifleBullet, transform.position, transform.rotation);
                    TheBullet.GetComponent<Rigidbody>().velocity = BulletSpeed * transform.forward;
                    LastFireTime = Time.time;
                    Destroy(TheBullet, DisappearAfter);
                }
            }
        }
    }

    public void AutoFire()
    {
        if (gameObject.GetComponentInParent<Duplicate>().BeSitted == true)
        {
            if (Time.time - LastFireTime > FireLag)
            {
                GameObject TheBullet;
                TheBullet = Instantiate(RifleBullet, transform.position, transform.rotation);
                TheBullet.GetComponent<Rigidbody>().velocity = BulletSpeed * transform.forward;
                LastFireTime = Time.time;
                Destroy(TheBullet, DisappearAfter);
            }
        }

    }




}
