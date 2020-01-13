using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MyStuff : MonoBehaviour
{
    //0:집 1:침대 2:책상 3:책장 4:거울 5:선인장
    private List<Furnitrue> myfurnitrue;

    string bonusrate;
    
    private void Awake()
    {
        myfurnitrue = MyItems.LoadFurnitrueXML();
    }

    int[] needNight =
    {
        0,20,50,100,180,300
    };
    void Start ()
    {
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetComponent<Text>().text = "경제력과 여행력이 <color='yellow'>" + User.house * 100 + "%</color> 상승";
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(4).GetChild(2).GetComponent<Text>().text = User.night+"일/<color='yellow'>"+needNight[User.house]+"일</color>";
        for (int i=0; i<myfurnitrue.Count; i++)
        {
            //가구이름,레벨
            
            this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>().text = myfurnitrue[i].name + " Lv"+ MyFurnitrue.stuffLv[i];

            if (i>0)
            {
                if(MyFurnitrue.stuffLv[i]>0)
                {
                    this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(5).gameObject.SetActive(false);
                }
                this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(5).GetChild(2).name = myfurnitrue[i].name;

                //경험치
                this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetComponent<Slider>().value = (MyFurnitrue.stuffExp[i - 1] / MyFurnitrue.ExpTable[MyFurnitrue.stuffLv[i]]);
                this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(3).GetComponent<Text>().text = "EXP " + MyFurnitrue.stuffExp[i - 1].ToString("N0") + " / " + MyFurnitrue.ExpTable[MyFurnitrue.stuffLv[i]];

                //가격
                this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(5).GetChild(1).GetChild(1).name = myfurnitrue[i].price.ToString();
                this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(5).GetChild(1).GetChild(1).GetComponent<Text>().text = myfurnitrue[i].price.ToString() + "원";

                //하트
                this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(3).GetChild(0).GetComponent<Text>().text = "x" + (MyFurnitrue.stuffLv[i]).ToString() + "\r\n/sec";
            }
        }
        if (User.house == 0)
            return;
        //조건
        for (int i = 1; i <=GameObject.Find("House" + User.house + "/Furniture").transform.childCount; i++)
        {
            this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).gameObject.SetActive(true);
        }
    }


    void Update ()
    {
        if (User.house == 0)
            this.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(GameObject.Find("Panels").transform.GetChild(6).GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta.x, 300);
        else
        {
            for (int i = 1; i <= GameObject.Find("House" + User.house + "/Furniture").transform.childCount; i++)
            {
                this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).gameObject.SetActive(true);
            }
            this.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(GameObject.Find("Panels").transform.GetChild(6).GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta.x, 150+(GameObject.Find("House" + User.house + "/Furniture").transform.childCount) * 120);

        }

        this.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = User.ChangeUnit(TotalHeart()) +bonusrate;

        if (GameObject.Find("slotHouse") != null)
        {
            int[] needMoney =
            {
                100, 150000, 800000, 2500000, 5000000, 8000000, 13000000, 25000000, 800, 900, 1000
            };
            if (User.house >= 6)
                GameObject.Find("slotHouse").transform.GetChild(4).GetChild(1).GetComponent<Text>().text = "MAX";
            else
                GameObject.Find("slotHouse").transform.GetChild(4).GetChild(1).GetComponent<Text>().text = User.ChangeMoney(needMoney[User.house]);
        }

        for (int i = 0; i < myfurnitrue.Count; i++)
        {
           
            this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text = myfurnitrue[i].description;
            if (i==0&&User.house!=0)
            {
                
                this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/item_house_" + User.house);
                this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>().text = myfurnitrue[i].name + " Lv" + User.house;
                this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(2).GetComponent<Slider>().value = User.heartguage/100;
                this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(2).GetChild(3).GetComponent<Text>().text = (User.house*10).ToString("N0");
            }

            if (i > 0)
            {
                this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>().text = myfurnitrue[i].name + " Lv" + MyFurnitrue.stuffLv[i];

                //경험치
                this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetComponent<Slider>().value = (MyFurnitrue.stuffExp[i - 1] / MyFurnitrue.ExpTable[MyFurnitrue.stuffLv[i]]);
                this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(3).GetComponent<Text>().text = "EXP "+MyFurnitrue.stuffExp[i - 1].ToString("N0") + " / "+MyFurnitrue.ExpTable[MyFurnitrue.stuffLv[i]];

                this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(3).GetChild(2).GetChild(0).GetComponent<Text>().text = User.ChangeUnit(MyFurnitrue.stuffLv[i] * MyFurnitrue.stuffLv[i] * MyFurnitrue.stuffLv[i] *  MyFurnitrue.ExpTable[MyFurnitrue.stuffLv[i]]);


                //하트
                this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(3).GetChild(0).GetComponent<Text>().text = "+ " + (MyFurnitrue.stuffLv[i]).ToString();

                if(MyFurnitrue.stuffExp[i - 1] >= MyFurnitrue.ExpTable[MyFurnitrue.stuffLv[i]])
                    GameObject.Find("Canvas/Panels").transform.GetChild(6).GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(6).gameObject.SetActive(true);
                else
                    GameObject.Find("Canvas/Panels").transform.GetChild(6).GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(6).gameObject.SetActive(false);
            }

        }

        //지식
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetChild(2).GetComponent<Text>().text = "지식을 "+ MyFurnitrue.stuffLv[2]*10+"% 향상시킵니다.";
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(6).GetChild(2).GetComponent<Text>().text = "지식을 " + MyFurnitrue.stuffLv[6] * 10 + "% 향상시킵니다.";
        //체력
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetChild(2).GetComponent<Text>().text = "체력을 "+ MyFurnitrue.stuffLv[1]*10+"% 향상시킵니다.";
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(7).GetChild(2).GetComponent<Text>().text = "체력을 " + MyFurnitrue.stuffLv[7] * 10 + "% 향상시킵니다.";
        //매력
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(4).GetChild(2).GetComponent<Text>().text = "매력을 " + MyFurnitrue.stuffLv[4] * 10 + "% 향상시킵니다.";
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(10).GetChild(2).GetComponent<Text>().text = "매력을 " + MyFurnitrue.stuffLv[10] * 10 + "% 향상시킵니다.";
        //도덕
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(3).GetChild(2).GetComponent<Text>().text = "도덕을 "+ MyFurnitrue.stuffLv[3]*10+"% 향상시킵니다.";
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(8).GetChild(2).GetComponent<Text>().text = "도덕을 " + MyFurnitrue.stuffLv[8] * 10 + "% 향상시킵니다.";
        //행운
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(5).GetChild(2).GetComponent<Text>().text = "행운을 "+ MyFurnitrue.stuffLv[5]*10+"% 향상시킵니다.";
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(9).GetChild(2).GetComponent<Text>().text = "행운을 " + MyFurnitrue.stuffLv[9] * 10 + "% 향상시킵니다.";
        //올스탯
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(11).GetChild(2).GetComponent<Text>().text = "모든능력을 " + MyFurnitrue.stuffLv[11] * 5 + "% 향상시킵니다.";
    }

    //하트계산
    private float TotalHeart()
    {
        float res = 0.0f;
        for (int i = 1; i < MyFurnitrue.stuffLv.Length; i++)
        {
            res += MyFurnitrue.stuffLv[i];
        }
        res += User.house * 10;
        bonusrate = "<color='white'>(" + res.ToString("N0")+"+<color='yellow'>"+(res * User.itemLevel[0] * 0.01f).ToString("N0")+"</color>)</color>";
        res += res * User.itemLevel[0] * 0.01f;
        
        return res;
    }



}
