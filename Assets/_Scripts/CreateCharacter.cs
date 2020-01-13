using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using System.Xml;

public class CreateCharacter : MonoBehaviour 
{
    public GameObject alert;
    private void Update()
    {
        if(User.isSelectGender)
        {
            if(User.isDead)
            {
                GameObject.Find("btnMan").transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("btnGirl").transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("btnGirl").transform.GetChild(2).gameObject.SetActive(true);
                GameObject.Find("btnMan").transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                GameObject.Find("btnMan").transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("btnGirl").transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("btnGirl").transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("btnMan").transform.GetChild(2).gameObject.SetActive(true);
            }
        }
    }
    public void Create()
    {
        Text userID = GameObject.Find("Canvas/PanelCreateCharacter/InputField/Text").GetComponent<Text>();
        if (userID.text == null || userID.text == "")
        {
            alert.gameObject.SetActive(true);
            alert.transform.GetChild(0).GetComponent<Text>().text = "이름을 확인해주세요.";
            return;
        }
        else if(!User.isSelectGender)
        {
            alert.gameObject.SetActive(true);
            alert.transform.GetChild(0).GetComponent<Text>().text = "케릭터를 선택하세요.";
            return;
        }
        User.myname = userID.text;
        User.money = 50000;
        User.fatigue = 5;
        User.knowledge = 5;
        User.health = 5;
        User.charm = 5;
        User.moral = 5;
        User.lucky = 5;
        User.sleep = 0;
        User.frustrate = 0;
        User.particle = 30;
        User.house = 0;
        User.day = false;
        User.night = 1;
        User.equip = "";
        User.storysequence = 1;
        User.map = 3;
        User.heart = 10;
        User.member = new int[4];
        User.memberlevel = new int[4];
        User.skillLevel = new int[9];
        User.passiveLevel = new int[3];
        User.healingLevel = new int[3];
        User.isReport = false;
        User.message = new List<string>();
        
        User.bonusDate = DateTime.Now.ToString();
        User.jobLevel = 1;
        User.tourLevel = 1;
        User.jobExp = 0;
        User.tourExp = 0;
        User.rewardSec = DateTime.Now;
        User.jobenemylevel = 0;
        User.itemLevel = new int[5];

        //환경설정
        User.isEffectSound = false;
        User.isBackgroundSound = false;
        User.isCloud = false;
        User.isAds = false;
        MessageInit();//대사초기화

        User.SaveDate();

        MyFurnitrue.stuffLv = new int[12];
        MyFurnitrue.stuffExp = new float[11];
        MyFurnitrue.stuffIsLevel = new bool[12];
        MyFurnitrue.stuffLv[0] = 1;

        MyFurnitrue.SaveDate();

        Story.storyseqeuence = 0;
        Story.tutorialseqeuence = 0;

        Story.SaveDate();

        SceneManager.LoadScene("Intro");
    }


    void XMLTest()
    {
        XmlDocument doc = new XmlDocument();
        XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
        doc.AppendChild(xmlDeclaration);
        XmlElement Root = doc.CreateElement("ItemDB");
        doc.AppendChild(Root);


        XmlElement Item = (XmlElement)Root.AppendChild(doc.CreateElement("Item"));

        Item.SetAttribute("id", "1");
        Item.SetAttribute("name", "아이템1");

        XmlElement Item2 = (XmlElement)Root.AppendChild(doc.CreateElement("Item"));

        Item2.SetAttribute("id", "2");
        Item2.SetAttribute("name", "아이템2");

        //파일쓰기
        File.WriteAllText(Application.dataPath + "/Resources/XML/ItemDB.xml", doc.OuterXml, System.Text.Encoding.UTF8);
        Debug.Log(doc.OuterXml);

        LoadXML("ItemDB");
    }

    void LoadXML(string _filename)
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/" + _filename);
        XmlDocument xmlDoc = new XmlDocument();
        Debug.Log(textAsset.text);
        xmlDoc.LoadXml(textAsset.text);

        /*
        XmlNodeList id_Table = xmlDoc.GetElementsByTagName("Field");
        foreach(XmlNode id in id_Table)
        {
            Debug.Log("[one by one] id : " + id.InnerText);
        }
        */

        XmlElement itemElement = xmlDoc["Item"];
        foreach (XmlElement elem in itemElement.ChildNodes)
        {
            Debug.Log("아이디 : " + elem.GetAttribute("id"));
        }

    }

    void MessageInit()
    {
        string[] msg =
        {
            "직장생활은 아직도 적응이 안돼...\r\n 나만 뒤쳐지면 어떡하지..",
            "귀여운 웰시코기 키우고 싶다!",
            "사랑하는 사람과 함께 산다면\r\n 얼마나 좋을까?",
            "차가 있으면 더 편할텐데..",
            "부모님께 연락 드려야 하는데....",
            "요즘 왜 이렇게 피곤한지 모르겠어..",
            "여행을 가는건 늘 새롭고 즐거워!",
            "오늘은 맛있는걸 사먹을테다!",
            "아이고.. 관리비가 너무 많이나오네..",
            "그만두고 아무것도 하기싫을때는\r\n 어떻게 해야할까?",
            "가끔은 밤하늘을 올려보면서 천천히 생각하자."
        };
        string[] msg2 =
        {
            "직장생활은 아직도 적응이 안돼...\r\n 나만 뒤쳐지면 어떡하지..",
            "귀여운 웰시코기 키우고 싶어!",
            "사랑하는 사람과 함께 산다면\r\n 얼마나 좋을까!",
            "차가 있으면 더 편할텐데..",
            "부모님께 연락 드려야 하는데....",
            "흐아아암 잠이 온다 =3",
            "여유롭게 카페에서 브런치를 즐기고 싶은 하루야",
            "오늘은 맛있는걸 사먹을테다!",
            "아이고.. 관리비가 너무 많이나오네..",
            "그만두고 아무것도 하기싫을때는\r\n 어떻게 해야할까?",
            "가끔은 밤하늘을 올려보면서 천천히 생각하자."
        };

        if(User.isDead)
        {
            for (int i = 0; i < msg.Length; i++)
            {
                User.message.Add(msg2[i]);
            }
        }
        else
        {
            for (int i = 0; i < msg.Length; i++)
            {
                User.message.Add(msg[i]);
                
            }
        }
    }
}
