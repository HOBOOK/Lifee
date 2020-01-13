using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseManager : MonoBehaviour
{
    //
    private int[] needMoney =
    {
        100, 150000, 800000, 2500000, 5000000, 8000000, 13000000, 25000000, 800, 900, 1000
    };
    public static float buytime;
    public static bool isHouseLoad;
    public static bool isBuy;
    public GameObject fade;
	void Start ()
    {
        isHouseLoad = false;
        buytime = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isBuy&&fade!=null)
        {
            buytime += Time.deltaTime;
            fade.SetActive(true);

            //레벨업
            if(isHouseLoad&&buytime>2.0f)
            {
                User.house += 1;

                //튜토리얼
                

                MyFurnitrue.stuffLv[0] = User.house;
                User.SaveDate();
                
                MyFurnitrue.SaveDate();

                

                GameObject.Find("PanelMyStuff").SetActive(false);
                GameObject.Find("User").transform.position = new Vector3(0, 0.8f, 0);
                GameObject.Find("PanelHouseUp").SetActive(false);
                isHouseLoad = false;
            }
            if (buytime > 5.0f)
            {
                if (User.house==1&&!PlayerPrefs.HasKey("achivement1"))
                {
                    GoogleManager.Instance.CompleteHouse1();
                }
                else if (User.house == 2 && !PlayerPrefs.HasKey("achivement2"))
                {
                    GoogleManager.Instance.CompleteHouse1();
                    GameObject.Find("CanvasOverlay/Report").transform.GetChild(9).gameObject.SetActive(true);
                }
                else if (User.house == 3 && !PlayerPrefs.HasKey("achivement3"))
                {
                    GoogleManager.Instance.CompleteHouse1();
                }
                else if (User.house == 4 && !PlayerPrefs.HasKey("achivement4"))
                {
                    GoogleManager.Instance.CompleteHouse1();
                }
                else if (User.house == 5 && !PlayerPrefs.HasKey("achivement5"))
                {
                    GoogleManager.Instance.CompleteHouse1();
                }
                MyFurnitrue.StuffManager();
                fade.SetActive(false);
                isBuy = false;
                buytime = 0.0f;
                if (User.house == 1&&Story.tutorialseqeuence==2)
                {
                    Story.tutorialseqeuence = 3;
                }
                Story.SaveDate();

            }
        }
	}
    int[] needNight =
    {
        0,20,50,100,170,270,400,600,800
    };

    public void BuyHouse()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        if(User.night<needNight[User.house])
        {
            ScriptOnGUI("언제가는 이사갈 날이 오겠지!!?");
            return;
        }
        if (User.money<needMoney[User.house])
        {
            ScriptOnGUI("경제상황이 좋지않아..");
            return;
        }
        if(User.house>0)
        {
            for (int i = 1; i <= GameObject.Find("House" + User.house + "/Furniture").transform.childCount; i++)
            {
                if (MyFurnitrue.stuffLv[i] < User.house)
                {
                    ScriptOnGUI("<color='red'>가구레벨이 낮아..</color>");
                    return;
                }
            }
        }
        
        if (User.house >= 8)
            return;
        User.money -= needMoney[User.house];
        isBuy = true;
        isHouseLoad = true;
    }

    public void ActiveBuyHouse()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        if (User.house >= 7)
            return;
        GameObject.Find("Canvas/Panels").transform.GetChild(9).gameObject.SetActive(true);

        if(User.money >= needMoney[User.house])
        {
            GameObject.Find("Canvas/Panels").transform.GetChild(9).GetChild(3).GetChild(0).GetComponent<Text>().text = "<color='yellow'>" + User.ChangeMoney(needMoney[User.house]) + " 원</color>";

        }
        else
        {
            GameObject.Find("Canvas/Panels").transform.GetChild(9).GetChild(3).GetChild(0).GetComponent<Text>().text = User.ChangeMoney(needMoney[User.house]) + " 원";

        }
        if (User.night >= needNight[User.house])
        {
            GameObject.Find("Canvas/Panels").transform.GetChild(9).GetChild(4).GetChild(0).GetComponent<Text>().text = "<color='yellow'>" + needNight[User.house] + " 일</color>";

        }
        else
        {
            GameObject.Find("Canvas/Panels").transform.GetChild(9).GetChild(4).GetChild(0).GetComponent<Text>().text = needNight[User.house] + " 일";


        }

        GameObject.Find("Canvas/Panels").transform.GetChild(9).GetChild(1).GetChild(0).GetComponent<Text>().text = "LEVEL " + User.house + " → " + (User.house + 1);
        if (User.house == 2)
            return;
        GameObject.Find("Canvas/Panels").transform.GetChild(9).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/item_house_" + (User.house + 1));
    }

    public void ScriptOnGUI(string script)
    {
        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(0).gameObject.SetActive(true);
        if (GameObject.Find("CanvasOverlay/Pop").transform.GetChild(0).gameObject != null)
        {
            GameObject.Find("CanvasOverlay/Pop").transform.GetChild(0).GetChild(0).GetComponent<Text>().text = script;
        }
    }
}
