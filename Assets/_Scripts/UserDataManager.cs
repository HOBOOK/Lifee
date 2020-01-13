using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class User
{
    //저장 데이터
    public static string myname;
    public static float money;
    public static float fatigue;
    public static float knowledge;
    public static float health;
    public static float charm;
    public static float moral;
    public static float lucky;
    public static float sleep;
    public static float particle;
    public static float frustrate; //소포상태
    public static int status; //0:AI상태 1:잠자기 2:외출 3:공부 4:자소서쓰기 5:조이스틱 컨트롤 6:인터뷰
    public static int house;
    public static bool day; //true:저녁 false:낮
    public static int night; //몇번째 밤
    public static string equip;
    public static int storysequence; //0:인트로 1:에피소드 2:에피소드2
    public static int map; //3:집 4:도시 5:산 6:바다 6:면접실
    public static float heart;
    public static int[] member;
    public static int[] memberlevel;
    public static float meter;
    public static int[] skillLevel = new int[9];//직장스킬
    public static int[] passiveLevel;//인생스킬
    public static int[] healingLevel;//여행스킬
    public static bool isReport;
    public static List<string> message;
    public static bool isDead;
    public static string bonusDate;
    public static int jobLevel;
    public static int tourLevel;
    public static float jobExp;
    public static float tourExp;
    public static DateTime rewardSec;
    public static int jobenemylevel;
    public static int[] itemLevel = new int[5];

    //환경설정
    public static bool isEffectSound;
    public static bool isBackgroundSound;
    public static bool isCloud;
    public static bool isAds;

    //현장 데이터
    public static float Speed;
    public static bool isPlay;
    public static bool isTry;
    public static bool isDamage;
    public static int adsCount;
    public static bool[] tourSkill = new bool[3];
    public static bool isJobHard;
    public static bool isTourHard;

    public static bool isLovePower;//하트2배확인

    
    public static int buff;
    public static bool isPause;
    public static bool[,] selectedSkill = new bool[3,3];
    public static float tourSpeed;
    public static float skillSpeed;

    public static float bonusStat1, bonusStat2, bonusStat3, bonusStat4, bonusStat5;
    public static float TOUR_POWER, JOB_POWER;

    public static long TOUR_POINT;
    public static long JOB_POINT;

    public static float fireRun;
    public static float heartguage;

    public static bool isCloudSave;

    //돈 단위변환
    public static string ChangeUnit(float haveGold)
    {
        if (haveGold > 100000000000000)
            return string.Format("{0:#.###}C", (float)haveGold / 100000000000000);
        else if (haveGold > 10000000000000)
            return string.Format("{0:#.###}B", (float)haveGold / 10000000000000);
        else if (haveGold > 1000000000000)
            return string.Format("{0:#.###}A", (float)haveGold / 1000000000000);
        else if (haveGold > 100000000000)
            return string.Format("{0:#.###}i", (float)haveGold / 100000000000);
        else if (haveGold > 10000000000)
            return string.Format("{0:#.###}h", (float)haveGold / 10000000000);
        else if (haveGold > 1000000000)
            return string.Format("{0:#.###}g", (float)haveGold / 1000000000);
        else if (haveGold > 100000000)
            return string.Format("{0:#.###}f", (float)haveGold / 100000000);
        else if (haveGold > 10000000)
            return string.Format("{0:#.###}e", (float)haveGold / 10000000);
        else if (haveGold > 1000000)
            return string.Format("{0:#.###}d", (float)haveGold / 1000000);
        else if (haveGold > 100000)
            return string.Format("{0:#.###}c", (float)haveGold / 100000);
        else if (haveGold > 10000)
            return string.Format("{0:#.###}b", (float)haveGold / 10000);
        else if (haveGold > 1000)
            return string.Format("{0:#.###}a", haveGold / 1000);
        else
            return haveGold.ToString("N0");
    }

    public static string ChangeMoney(float haveGold)
    {
        if (haveGold > 1000000000000000)
            return string.Format("{0:#.###}천조", (float)haveGold / 1000000000000000);
        else if (haveGold > 100000000000)
            return string.Format("{0:#.###천억}", (float)haveGold / 100000000000);
        else if (haveGold > 10000000)
            return string.Format("{0:#.###}천만", haveGold / 10000000);
        else
            return haveGold.ToString("N0");
    }

    //카메라
    public static int cam;

    //시나리오
    public static bool[] isContact = new bool[3];

    //직장데이터
    public static float[] Job_Member_Hp = new float[3];
    public static int jobTempLevel;
    public static bool isAuto;

    //케릭터생성
    public static bool isSelectGender;

    

    //투데이 가계부 데이터
    public static float cost_manage;
    public static float cost_life;
    public static float cost_dog;
    public static float cost_car;
    public static float cost_bonus;
    //투데이 행복
    public static float today_good;
    public static float today_meet;
    public static float today_overcost;

    //실제 데이터 변수
    public string s_myname;
    public float s_money;
    public float s_fatigue;
    public float s_knowledge;
    public float s_health;
    public float s_charm;
    public float s_moral;
    public float s_lucky;
    public float s_sleep;
    public float s_particle;
    public float s_frustrate;
    public int s_house;
    public bool s_day;
    public int s_night;
    public string s_equip;
    public int s_storysequence;
    public int s_map;
    public float s_heart;
    public int[] s_member = new int[4];
    public int[] s_memberlevel= new int[4];
    public float s_meter;
    public int[] s_skillLevel = new int[9];
    public int[] s_passiveLevel = new int[3];
    public int[] s_healingLevel = new int[3];
    public bool s_isReport;
    public List<string> s_message = new List<string>();
    public bool s_isDead;
    public string s_bonusDate;
    public int s_jobLevel;
    public int s_tourLevel;
    public float s_jobExp;
    public float s_tourExp;
    public DateTime s_rewardSec;
    public int s_jobenemylevel;
    public int[] s_itemLevel = new int[5];

    //환경설정
    public bool s_isEffectSound;
    public bool s_isBackgroundSound;
    public bool s_isCloud;
    public bool s_isAds;

    public static string jsonData;

    public static void SaveDate()
    {
        User data = new User();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        data.s_myname = User.myname;
        data.s_money = User.money;
        data.s_fatigue = User.fatigue;
        data.s_knowledge = User.knowledge;
        data.s_health = User.health;
        data.s_charm = User.charm;
        data.s_moral = User.moral;
        data.s_lucky = User.lucky;
        data.s_sleep = User.sleep;
        data.s_particle = User.particle;
        data.s_frustrate = User.frustrate;
        data.s_house = User.house;
        data.s_day = User.day;
        data.s_night = User.night;
        data.s_equip = User.equip;
        data.s_storysequence = User.storysequence;
        data.s_map = User.map;
        data.s_heart = User.heart;
        data.s_member = User.member;
        data.s_memberlevel = User.memberlevel;
        data.s_meter = User.meter;
        data.s_skillLevel = User.skillLevel;
        data.s_passiveLevel = User.passiveLevel;
        data.s_healingLevel = User.healingLevel;
        data.s_isReport = User.isReport;
        data.s_message = User.message;
        data.s_isDead = User.isDead;
        data.s_bonusDate = User.bonusDate;
        data.s_jobLevel = User.jobLevel;
        data.s_tourLevel = User.tourLevel;
        data.s_jobExp = User.jobExp;
        data.s_tourExp = User.tourExp;
        data.s_isEffectSound = User.isEffectSound;
        data.s_isBackgroundSound = User.isBackgroundSound;
        data.s_isCloud = User.isCloud;
        data.s_isAds = User.isAds;
        data.s_rewardSec = User.rewardSec;
        data.s_jobenemylevel = User.jobenemylevel;
        data.s_itemLevel = User.itemLevel;
    
       
        bf.Serialize(file, data);
        jsonData = JsonUtility.ToJson(data);
        file.Close();
        Debug.Log("저장되었습니다.");
    }

    public static void LoadData()
    {
        try
        {
            User data = new User();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            if (file != null && file.Length > 0)
            {
                data = (User)bf.Deserialize(file);

                User.myname = data.s_myname;
                User.money = data.s_money;
                User.fatigue = data.s_fatigue;
                User.knowledge = data.s_knowledge;
                User.health = data.s_health;
                User.charm = data.s_charm;
                User.moral = data.s_moral;
                User.lucky = data.s_lucky;
                User.sleep = data.s_sleep;
                User.particle = data.s_particle;
                User.frustrate = data.s_frustrate;
                User.house = data.s_house;
                User.day = data.s_day;
                User.night = data.s_night;
                User.equip = data.s_equip;
                User.storysequence = data.s_storysequence;
                User.map = data.s_map;
                User.heart = data.s_heart;
                User.member = data.s_member;
                User.memberlevel = data.s_memberlevel;
                User.meter = data.s_meter;
                User.skillLevel = data.s_skillLevel;
                User.passiveLevel = data.s_passiveLevel;
                User.healingLevel = data.s_healingLevel;
                User.isReport = data.s_isReport;
                User.message = data.s_message;
                User.isDead = data.s_isDead;
                User.bonusDate = data.s_bonusDate;
                User.jobLevel = data.s_jobLevel;
                User.tourLevel = data.s_tourLevel;
                User.jobExp = data.s_jobExp;
                User.tourExp = data.s_tourExp;
                User.isEffectSound = data.s_isEffectSound;
                User.isBackgroundSound = data.s_isBackgroundSound;
                User.isCloud = data.s_isCloud;
                User.isAds = data.s_isAds;
                User.rewardSec = data.s_rewardSec;
                User.jobenemylevel = data.s_jobenemylevel;
                if(data.s_itemLevel==null)
                {
                    for(int i = 0; i < 5; i++)
                    {
                        User.itemLevel[i] = 0;
                    }
                }
                else
                 User.itemLevel = data.s_itemLevel;
            }
            file.Close();
            Debug.Log("로드되었습니다.");            
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }
}
[System.Serializable]
public class Story
{
    public static int storyseqeuence;
    public static int tutorialseqeuence;
    public static bool isScriptEnd;

    public int s_storysequence;
    public int s_tutorialsequence;

    public static string jsonStoryData;

    public static void SaveDate()
    {
        Story data = new Story();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/StoryInfo.dat");
        data.s_storysequence = storyseqeuence;
        data.s_tutorialsequence = tutorialseqeuence;
        bf.Serialize(file, data);
        file.Close();
        jsonStoryData = JsonUtility.ToJson(data);
        Debug.Log("스토리정보 저장");
    }

    public static void LoadData()
    {
        try
        {
            Story data = new Story();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/StoryInfo.dat", FileMode.Open);
            if (file != null && file.Length > 0)
            {
                data = (Story)bf.Deserialize(file);

                Story.storyseqeuence = data.s_storysequence;
                Story.tutorialseqeuence = data.s_tutorialsequence;
            }
            file.Close();
            
            Debug.Log("스토리정보 로드");
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }
}

[System.Serializable] //유저 소유 가구정보
public class MyFurnitrue
{
    public static int[] stuffLv = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public static float[] stuffExp = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public static bool[] stuffIsLevel= { false, false, false,false, false, false, false,false,false,false,false,false };
    public static int[] ExpTable = { 1,10,20,30,50,80,100,120,150,300,500};
    public static bool isLevelUp;

    public int[] s_stuffLv = new int[12];
    public float[] s_stuffExp = new float[11];
    public bool[] s_isLevelUp = new bool[12];

    public static string jsonStuffData;

    public static void LevelUp()
    {
        List<Furnitrue> fList = MyItems.LoadFurnitrueXML();
        for (int i = 1; i < stuffLv.Length; i++)
        {
            int level = stuffLv[i];
            if (stuffExp[i-1]>=ExpTable[level]&&!stuffIsLevel[i])
            {
                stuffIsLevel[i] = true;
                GameObject.Find("Canvas/Panels").transform.GetChild(8).gameObject.SetActive(true);
                Debug.Log("Items/item_furniture_" + fList[i].id.ToLower());
                GameObject.Find("Canvas/Panels").transform.GetChild(8).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/item_furniture_" + fList[i].id.ToLower());
                GameObject.Find("Canvas/Panels").transform.GetChild(8).GetChild(1).GetChild(0).GetComponent<Text>().text = "LEVEL " + stuffLv[i] + " → " + (stuffLv[i] + 1);
                GameObject.Find("Canvas/Panels").transform.GetChild(8).GetChild(3).GetChild(0).GetComponent<Text>().text = "- " + (10000*stuffLv[i]*fList[i].requireUp) + "원";
                GameObject.Find("Canvas/Panels").transform.GetChild(8).GetChild(4).GetChild(1).name = i.ToString();
                GameObject.Find("Canvas/Panels").transform.GetChild(8).GetChild(5).name = i.ToString();
            }
        }
    }

    public static void SaveDate()
    {
        MyFurnitrue data = new MyFurnitrue();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/FurnitrueInfo.dat");
        data.s_stuffLv = stuffLv;
        data.s_stuffExp = stuffExp;
        data.s_isLevelUp = stuffIsLevel;
        bf.Serialize(file, data);
        file.Close();
        jsonStuffData = JsonUtility.ToJson(data);
        Debug.Log("가구정보 저장");
    }

    public static void LoadData()
    {
        try
        {
            MyFurnitrue data = new MyFurnitrue();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/FurnitrueInfo.dat", FileMode.Open);
            if (file != null && file.Length > 0)
            {
                data = (MyFurnitrue)bf.Deserialize(file);

                MyFurnitrue.stuffLv = data.s_stuffLv;
                MyFurnitrue.stuffExp = data.s_stuffExp;
                MyFurnitrue.stuffIsLevel = data.s_isLevelUp;
            }
            file.Close();
            User.bonusStat1 = User.knowledge*(MyFurnitrue.stuffLv[2]+MyFurnitrue.stuffLv[6])*0.1f+User.knowledge*(MyFurnitrue.stuffLv[11]*0.05f) + User.knowledge*(User.memberlevel[2]*0.03f+User.memberlevel[3]*0.05f);
            User.bonusStat2 = User.health*(MyFurnitrue.stuffLv[1]+MyFurnitrue.stuffLv[7])*0.1f + User.health* (MyFurnitrue.stuffLv[11] * 0.05f) + User.health * (User.memberlevel[2] * 0.03f + User.memberlevel[3] * 0.05f);
            User.bonusStat3 = User.charm*(MyFurnitrue.stuffLv[4]+MyFurnitrue.stuffLv[10])*0.1f + User.charm * (MyFurnitrue.stuffLv[11] * 0.05f) + User.charm * (User.memberlevel[2] * 0.03f + User.memberlevel[3] * 0.05f);
            User.bonusStat4 = User.moral*(MyFurnitrue.stuffLv[3]+MyFurnitrue.stuffLv[8])*0.1f + User.moral * (MyFurnitrue.stuffLv[11] * 0.05f) + User.moral * (User.memberlevel[2] * 0.03f + User.memberlevel[3] * 0.05f);
            User.bonusStat5 = User.lucky*(MyFurnitrue.stuffLv[5]+MyFurnitrue.stuffLv[9])*0.1f + User.lucky * (MyFurnitrue.stuffLv[11] * 0.05f) + User.lucky * (User.memberlevel[2] * 0.03f + User.memberlevel[3] * 0.05f);

            float tempjob = ((User.knowledge + User.bonusStat1) * 0.3f + (User.health + User.bonusStat2) * 0.2f + (User.lucky + User.bonusStat5) * 0.5f) * User.house;
            float temptour = ((User.knowledge + User.bonusStat1) * 0.2f + (User.health + User.bonusStat2) * 0.3f + (User.moral + User.bonusStat4) * 0.5f) * User.house; ;
            User.JOB_POWER = tempjob + (tempjob*0.005f*(User.charm+User.bonusStat3));
            User.TOUR_POWER = temptour + (temptour * 0.01f * (User.charm + User.bonusStat3));
            Debug.Log("가구정보 로드");
            
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }

    public static void StuffManager()
    {
        List<Furnitrue> myfurnitrue = MyItems.LoadFurnitrueXML();
        for (int i = 1; i < myfurnitrue.Count; i++)
        {
            if (MyFurnitrue.stuffLv[i] == 0)
            {
                GameObject.Find("Canvas/Panels").transform.GetChild(6).GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(5).gameObject.SetActive(true);
                if (GameObject.Find("Furniture/" + myfurnitrue[i].id) != null)
                    GameObject.Find("Furniture/" + myfurnitrue[i].id).gameObject.SetActive(false);
            }
            else
            {
                if (GameObject.Find("Furniture/" + myfurnitrue[i].id) != null)
                    GameObject.Find("Furniture/" + myfurnitrue[i].id).gameObject.SetActive(true);
                GameObject.Find("Canvas/Panels").transform.GetChild(6).GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(5).gameObject.SetActive(false);
            }
        }
    }
}

public class UserDataManager : MonoBehaviour
{
	void Awake ()
    {
        User.LoadData();
        MyFurnitrue.LoadData();
	}
}
