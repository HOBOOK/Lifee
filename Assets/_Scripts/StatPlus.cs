using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPlus : MonoBehaviour
{
    private void Update()
    {
        string stat = gameObject.name;
        int cost = 0;
        switch (stat)
        {
            case "knowledge":
                cost = GetCost(User.knowledge);
                if (ChkMoney(cost))
                    this.transform.GetChild(1).gameObject.SetActive(false);
                else
                    this.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case "health":
                cost = GetCost(User.health);
                if (ChkMoney(cost))
                    this.transform.GetChild(1).gameObject.SetActive(false);
                else
                    this.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case "charm":
                cost = GetCost(User.charm);
                if (ChkMoney(cost))
                    this.transform.GetChild(1).gameObject.SetActive(false);
                else
                    this.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case "moral":
                cost = GetCost(User.moral);
                if (ChkMoney(cost))
                    this.transform.GetChild(1).gameObject.SetActive(false);
                else
                    this.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case "lucky":
                cost = GetCost(User.lucky);
                if (ChkMoney(cost))
                    this.transform.GetChild(1).gameObject.SetActive(false);
                else
                    this.transform.GetChild(1).gameObject.SetActive(true);
                break;
        }
    }
    public void OnClick()
    {
        
        string stat = gameObject.name;
        int cost = 0;
        switch (stat)
        {
            case "knowledge":
                cost = GetCost(User.knowledge);
                if (User.knowledge >= 999)
                    return;
                if (ChkMoney(cost))
                {
                    User.knowledge += 1;
                    User.heart-= cost;
                }
                break;
            case "health":
                cost = GetCost(User.health);
                if (User.health >= 999)
                    return;
                if (ChkMoney(cost))
                {
                    User.health += 1;
                    User.heart -= cost;
                }
                break;
            case "charm":
                cost = GetCost(User.charm);
                if (User.charm >= 999)
                    return;
                if (ChkMoney(cost))
                {
                    User.charm += 1;
                    User.heart -= cost;
                }
                break;
            case "moral":
                cost = GetCost(User.moral);
                if (User.moral >= 999)
                    return;
                if (ChkMoney(cost))
                {
                    User.moral += 1;
                    User.heart -= cost;
                }
                break;
            case "lucky":
                cost = GetCost(User.lucky);
                if (User.lucky >= 999)
                    return;
                if (ChkMoney(cost))
                {
                    User.lucky += 1;
                    User.heart -= cost;
                }
                break;
        }
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        MyFurnitrue.LoadData();
    }

    public bool ChkMoney(float cost)
    {
        if (User.heart-cost < 0)
            return false;
        else
            return true;
    }

    public int GetCost(float stat)
    {
        stat = stat + 1;
        float cost = 0.0f;
        if (stat <= 100)
            cost = stat;
        else if (stat <= 200)
            cost = stat * 2;
        else if (stat <= 300)
            cost = stat * 4;
        else if (stat <= 400)
            cost = stat * 8;
        else if (stat <= 500)
            cost = stat * 16;
        else if (stat <= 600)
            cost = stat * 32;
        else if (stat <= 700)
            cost = stat * 64;
        else if (stat <= 800)
            cost = stat * 128;
        else if (stat <= 900)
            cost = stat * 256;
        else if (stat <= 1000)
            cost = stat * 512;
        else 
            cost = stat * stat;

        cost -= cost * (User.passiveLevel[2] * 0.05f);
        return System.Convert.ToInt32(cost);
    }
}
