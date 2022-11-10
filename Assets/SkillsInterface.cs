using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsInterface : MonoBehaviour
{
    public int[] ChosenSkillNumbers;
    public GameObject[] SkillBtns;
    public GameObject[] PosList;
    // Start is called before the first frame update
    void Start()
    {
        SkillBtns = GameObject.FindGameObjectsWithTag("SkillBtn");
        PosList = GameObject.FindGameObjectsWithTag("PosList");
        foreach (GameObject SkillBtn in SkillBtns)
        {
            SkillBtn.SetActive(false);
        }
        ShowRandomSkills();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowRandomSkills()
    {
        Time.timeScale = 0;
        ChosenSkillNumbers = GetRandomSkillNumbers();
        int Index = 0;
        foreach (int Skill in ChosenSkillNumbers)
        {

            SkillBtns[Skill].SetActive(true);
            SkillBtns[Skill].transform.position = PosList[Index].transform.position;
            Index += 1;
        }

    }

    public int[] GetRandomSkillNumbers()//输出四个随机数
    {
        int[] SkillNumers = new int[] { 1000, 1000, 1000, 1000 };
        int[] Indexes = new int[] { 0, 1, 2, 3 };
        foreach (int Index in Indexes)
        {
            while (true)
            {
                bool ifContinueRandom = false;
                int RandomSkillNumer = Random.Range(0, 4);
                foreach (int SkillNumber in SkillNumers)
                {
                    if (SkillNumber.Equals(RandomSkillNumer))
                    {
                        ifContinueRandom = true;
                        break;
                    }
                }
                if(ifContinueRandom)
                {
                    continue;
                }
                SkillNumers[Index] = RandomSkillNumer;
                break;
            }
        }
        return SkillNumers;
    }

    public void AddChairsLimit()
    {
        DoWhileEndChoosing();
    }
    public void MakeFasterPizza()
    {
        DoWhileEndChoosing();
    }
    public void ChairAttractive()
    {
        DoWhileEndChoosing();
    }
    public void SuperDish()
    {
        DoWhileEndChoosing();
    }
    public void BadBehaviour()
    {
        DoWhileEndChoosing();
    }
    public void ChairIntelligence()
    {
        DoWhileEndChoosing();
    }

    public void DoWhileEndChoosing()
    {
        Time.timeScale = 1.0f;
        foreach (GameObject SkillBtn in SkillBtns)
        {
            SkillBtn.SetActive(false);
        }
    }

}
