using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionSystem : MonoBehaviour
{
    public int DishesInEnemy = 0;
    public int DishesInPlayer = 0;
    public GameObject[] Targets;
    public GameObject[] Enemies;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Targets = GameObject.FindGameObjectsWithTag("Target");
        CalculateDishCount();
        EnemyEmotion();
    }

    public void CalculateDishCount()
    {
        int InEnemy = 0;
        int InPlayer = 0;
        foreach(var Target in Targets)
        {
            if(Target.GetComponent<Target>().WhoTag == "Player")
            {
                InPlayer += 1;
            }
            if(Target.GetComponent<Target>().WhoTag == "Enemy")
            {
                InEnemy += 1;
            }
        }

        DishesInEnemy = InEnemy;
        DishesInPlayer = InPlayer;

    }

    public void EnemyEmotion()
    {
        if(DishesInPlayer >= 1)
        {
            foreach(var Enemy in Enemies)
            {
                Enemy.GetComponent<Animator>().speed = 1.5f;
            }
        }
        else
        {
            foreach(var Enemy in Enemies)
            {
                Enemy.GetComponent<Animator>().speed = 1.5f;
            }
        }
        
    }

    public void PlayerEmotion()
    {

    }
}
