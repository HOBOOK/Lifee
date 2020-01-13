using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TodayReport : MonoBehaviour
{
	void Start ()
    {
        CostOfManage();
        CostOfLife();
        CostOfDog();
        CostOfCar();
        CostOfBonus();

        float TotalCost = (User.cost_manage + User.cost_life + User.cost_dog + User.cost_car + User.cost_bonus + AdsBonus(User.cost_manage + User.cost_life + User.cost_dog + User.cost_car + User.cost_bonus));
        this.transform.GetChild(2).GetComponent<Text>().text =
            "관리비 " + User.cost_manage + "\r\n" +
            "생활비 " + User.cost_life + "\r\n" +
            "애완견양육비 " + User.cost_dog + "\r\n" +
            "차량유지비 " + User.cost_car + "\r\n" +
            "<color='red'>스킬보너스 +" + (-TotalCost * User.passiveLevel[1] * 0.1f).ToString("N0") + "</color>\r\n" +
            "<color='red'>광고보너스 +" + AdsBonus(User.cost_manage + User.cost_life + User.cost_dog + User.cost_car + User.cost_bonus).ToString("N0") + "</color>\r\n" +
            "-------------\r\n";
        this.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<Text>().text = (TotalCost +(-TotalCost * User.passiveLevel[1] * 0.1f)).ToString("N0") + "원";

        this.transform.GetChild(3).GetComponent<Text>().text =
            "일의 만족 " + TodayGood().ToString("N0") + "\r\n" +
            "새가주는 힐링 " + TodayMeet().ToString("N0") + "\r\n" +
            "과소비 " + TodayCost().ToString("N0") + "\r\n" +
            "가구보너스 " + TodayStuff().ToString("N0") + "\r\n" +
            "<color='red'>광고보너스 +" + AdsBonus(TodayGood() + TodayMeet() + TodayCost() + TodayStuff()).ToString("N0") + "</color>\r\n" +
            "\r\n" +
            "-------------\r\n";
            this.transform.GetChild(5).GetChild(1).GetChild(0).GetComponent<Text>().text = (TodayGood() + TodayMeet() + TodayCost() + TodayStuff()+ AdsBonus(TodayGood() + TodayMeet() + TodayCost() + TodayStuff())).ToString("N0") + "하트";
    }


    void CostOfManage()
    {
        float cost = 0.0f;

        cost += (User.house * User.house) * 700;

        int furnitureNum = 1;
        for(int i = 0; i < MyFurnitrue.stuffLv.Length; i++)
        {
            furnitureNum += MyFurnitrue.stuffLv[i];
        }

        cost += furnitureNum*100;
        User.cost_manage = -cost;
    }

    void CostOfLife()
    {
        float cost = 0.0f;
        cost += (User.night*10);
        int memberCost = 0;
        for(int i = 0; i < User.member.Length; i++)
        {
            memberCost += User.memberlevel[i];
        }
        cost += cost*memberCost*User.house;
        cost += User.night;

        User.cost_life = -cost;
    }

    void CostOfDog()
    {
        float cost = 0.0f;
        cost += User.memberlevel[0] * 500;
        User.cost_dog = -cost;
    }

    void CostOfCar()
    {
        float cost = 0.0f;
        cost += User.memberlevel[2] * 3000;
        User.cost_car = -cost;
    }

    void CostOfBonus()
    {
        float bonus = 0;

        User.cost_bonus = bonus;
    }

    float TodayGood()
    {
        float good = 0;
        if (User.today_good > 3)
            good = User.jobLevel;
        else if (User.today_good > 2)
            good = User.heart / 100;
        else if (User.today_good > 1)
            good = User.heart / 200;
        else if (User.today_good > 0)
            good = User.heart / 300;
        else
            good = 0;

        good *= User.house;
        return good;
    }
    
    float TodayMeet()
    { 
        float meet = 0;
        if (User.today_meet > 0)
            meet = User.today_meet * 10;
        else
            meet = 0;

        meet *= User.house;
        return meet;
    }

    float TodayCost()
    {
        float cost = 0;
        if((User.cost_manage + User.cost_life + User.cost_dog + User.cost_car + User.cost_bonus)>(User.money/10))
        {
            cost = (User.cost_manage + User.cost_life + User.cost_dog + User.cost_car + User.cost_bonus) / 100;
        }
        return -cost;
    }

    float TodayStuff()
    {
        float stuff = 0.0f;
        for (int i = 1; i < MyFurnitrue.stuffLv.Length; i++)
        {
            stuff += MyFurnitrue.stuffLv[i];
        }
        stuff *= User.house;
        return stuff;
    }

    float AdsBonus(float amount)
    {
        float bonus = 0.0f;

        if (User.adsCount>0)
        {
            if (amount >= 0)
                bonus = amount * 0.3f;
            else
                bonus = (-amount) * 0.3f;
        }
        else
            bonus = 0;
        

        return bonus;
    }

}
