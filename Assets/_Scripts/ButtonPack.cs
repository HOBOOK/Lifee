using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonPack : MonoBehaviour
{
    public void CameraOnOff()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        if (!GameObject.Find("Main Camera").GetComponent<FollowCam>().isActiveAndEnabled)
            GameObject.Find("Main Camera").GetComponent<FollowCam>().enabled = true;
        else
        {
            int cnt = 0;
            for(int i =0; i<User.memberlevel.Length;i++)
            {
                if (User.memberlevel[i] > 0)
                    cnt++;
            }
            if(User.cam>= cnt+1)
            {
                GameObject.Find("Main Camera").transform.position = new Vector3(0.0f, 24f, -22f);
                GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(40, 0, 0);
                User.cam = 0;
            }
            else
                User.cam += 1;
        }
    }       
    public void GoToMountain()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        if (User.day)
        {
            ScriptOnGUI("밤이 됐으니 잠을 자야해..\r\n 여행은 내일 가자");
            AlertNoGUI("저녁에는 여행할 수 없습니다.");
            return;
        }
        if (User.money <= 0)
        {
            ScriptOnGUI("지금 놀러갈 때가 아니야..");
            AlertNoGUI("돈이 부족합니다.. ");
            return;
        }

        User.map = 5;
        User.status = 2;
        User.SaveDate();
        SceneManager.LoadScene("Loading");
    }
    public void GoToJobHard()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        if (User.jobenemylevel < 10)
        {
            ScriptOnGUI("아직 내가 할 일은 아닌거 같아..");
            AlertNoGUI("직장(Hard) 조건이 부족합니다.\r\n<Color='red'>여행(normal) 클리어</color>");
            return;
        }
        if (User.jobLevel < 15)
        {
            ScriptOnGUI("아직 아니야...");
            AlertNoGUI("직장(Hard) 가기에 조건이 부족합니다.\r\n<Color='red'>직장레벨 15이상</color>");
            return;
        }
        if (User.day)
        {
            ScriptOnGUI("밤이 됐으니 잠을 자야해..\r\n 출근은 내일하자고");
            AlertNoGUI("저녁에는 출근할 수 없습니다.");
            return;
        }
        if(User.heart-1000<1)
        {
            ScriptOnGUI("왜 일을 해야하는걸까?");
            AlertNoGUI("행복하트가 부족합니다.");
            return;
        }

        User.isJobHard = true;
        User.map = 4;
        User.heart -= 1000;
        User.status = 6;
        User.SaveDate();
        SceneManager.LoadScene("Loading");
    }
    public void RestHome()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        if (User.day)
        {
            ScriptOnGUI("피곤해... 이제 자야할 시간이야");
            AlertNoGUI("아침에만 할 수 있습니다.");
            return;
        }
        if (User.particle-30<0)
        {
            ScriptOnGUI("지금 쉴 때가 아니야!");
            AlertNoGUI("파편이 부족하여 집에서 쉴 수 없습니다.");
            return;
        }
        User.particle -= 30;

        float plusheart = 0.0f;
        float sec = 100 / (((User.house * 1.5f) + 5)); //하트차는소요시간
        for (int i = 1; i < MyFurnitrue.stuffLv.Length; i++)
        {
            plusheart += MyFurnitrue.stuffLv[i];
        }
        plusheart += User.house * 10;
        plusheart += plusheart * User.itemLevel[0] * 0.01f;
        User.heart += (plusheart / sec) * 3600;

        User.day = true;
        User.isReport = false;
        User.map = 3;
        User.status = 0;
        User.sleep = 100;
        User.isPlay = false;
        User.SaveDate();
        Story.SaveDate();
        MyFurnitrue.SaveDate();
        SceneManager.LoadSceneAsync(3);
    }
    public void GoToDesert()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        if (User.tourLevel < 15)
        {
            ScriptOnGUI("거기는 아직 무리야!");
            AlertNoGUI("사막으로 가기에 조건이 부족합니다.\r\n<Color='red'>여행레벨 15이상</color>");
            return;
        }
        if (User.money <= 0)
        {
            ScriptOnGUI("지금 놀러갈 때가 아니야..");
            AlertNoGUI("돈이 부족합니다..");
            return;
        }
        if (User.day)
        {
            ScriptOnGUI("밤이 됐으니 잠을 자야해..\r\n 여행은 내일 가자");
            AlertNoGUI("저녁에는 여행할 수 없습니다.");
            return;
        }

        User.isTourHard=true;
        User.map = 6;
        User.status = 2;
        User.SaveDate();
        SceneManager.LoadScene("Loading");
    }
    public void BackHome()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        
        User.isPause = false;
        User.map = 3;
        User.status = 0;
        User.SaveDate();
        SceneManager.LoadScene("Loading");
    }
    public void GoToHome()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();

        if (!User.isPlay)
            return;

        User.day = true;
        User.isReport = false;
        User.map = 3;
        User.status = 0;
        User.sleep = 100;
        User.isPlay = false;
        User.SaveDate();
        
        SceneManager.LoadScene("Loading");
    }
    public void GoToInterview()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        if (User.day)
        {
            ScriptOnGUI("밤이 됐으니 잠을 자야해..\r\n 출근은 내일하자고");
            AlertNoGUI("저녁에는 출근할 수 없습니다.");
            return;
        }
        if (User.heart - 100 < 1)
        {
            ScriptOnGUI("왜 일을 해야하는걸까?");
            AlertNoGUI("행복하트가 부족합니다.");
            return;
        }

        User.isJobHard = false;
        User.map = 4;
        User.heart -= 100;
        User.status = 6;
        User.SaveDate();
        SceneManager.LoadScene("Loading");
    }
    public void LogDay()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        GameObject.Find("Canvas/Panels").transform.GetChild(0).gameObject.SetActive(true);
    }
    public void ActiveSleepParticle()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        if(User.particle-10<0)
        {
            ScriptOnGUI("<color='red'>파편이 부족합니다.</color>");
            return;
        }
        User.particle -= 10;
        GameObject.Find("Canvas/popup").transform.GetChild(0).gameObject.SetActive(true);
        User.isReport = false;
        User.isPlay = false;
        User.sleep = 0;
        this.transform.position = new Vector3(0, 0.7f, 0);
        User.night += 1;
        User.day = false;
        User.status = 0;
        User.frustrate = User.night;
        for (int i = 0; i < MyFurnitrue.stuffExp.Length; i++)
        {
            if (MyFurnitrue.stuffLv[i + 1] != 0 && MyFurnitrue.stuffExp[i] < MyFurnitrue.ExpTable[MyFurnitrue.stuffLv[i + 1]])
                MyFurnitrue.stuffExp[i] += User.house*2;
            if (MyFurnitrue.stuffExp[i] >= MyFurnitrue.ExpTable[MyFurnitrue.stuffLv[i+1]])
            {
                MyFurnitrue.stuffExp[i] = MyFurnitrue.ExpTable[MyFurnitrue.stuffLv[i + 1]];
            }
        }

        ChangeDay();
    }
    public void ActiveSleep()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        if (User.status == 0)
            User.status = 1;
        GameObject.Find("Canvas/Panels/PanelBedActivity").SetActive(false);
    }
    public void ActiveStudy()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        if (User.status ==0)
            User.status = 3;
        GameObject.Find("Canvas/Panels/PanelDeskActivity").SetActive(false);
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
        if ((User.cost_manage + User.cost_life + User.cost_dog + User.cost_car + User.cost_bonus) > (User.money / 10))
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
    public void CompleteReport()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        User.isReport = true;
        User.status = 0;
        User.isPlay = false;
        GameObject.Find("User").transform.position = new Vector3(0, 0.7f, 0);

        float TotalCost = (User.cost_manage + User.cost_life + User.cost_dog + User.cost_car + User.cost_bonus + AdsBonus(User.cost_manage + User.cost_life + User.cost_dog + User.cost_car + User.cost_bonus));

        User.money += (TotalCost + (TotalCost * User.passiveLevel[1] * -0.1f));
        User.heart += (TodayGood() + TodayMeet() + TodayCost() + TodayStuff() + AdsBonus(TodayGood() + TodayMeet() + TodayCost() + TodayStuff()));

        
        //초기화
        User.cost_manage = 0;
        User.cost_life = 0;
        User.cost_dog = 0;
        User.cost_car = 0;
        User.cost_bonus = 0;

        User.today_good = 0;
        User.today_meet = 0;
        User.today_overcost = 0;
    }
    public void ActivePhone()
    {
        User.status = 6;
        GameObject.Find("Canvas/Panels/PanelBedActivity").SetActive(false);
    }

    public void BuyInfoActive()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        GameObject.Find("Canvas/Panels").transform.GetChild(7).gameObject.SetActive(true);
        GameObject.Find("Canvas/Panels").transform.GetChild(7).GetChild(2).name = this.transform.GetChild(2).name;
        GameObject.Find("Canvas/Panels").transform.GetChild(7).GetChild(1).name = this.gameObject.name;
        GameObject.Find("Canvas/Panels").transform.GetChild(7).GetChild(2).GetComponent<Text>().text = "<color='red'>'" + this.transform.GetChild(2).name+ "'</color> 구매하시겠습니까?";
        GameObject.Find("Canvas/Panels").transform.GetChild(7).GetChild(1).GetComponent<Image>().sprite= Resources.Load<Sprite>("Items/item_furniture_"+this.gameObject.name.ToLower());
        GameObject.Find("Canvas/Panels").transform.GetChild(7).GetChild(3).GetChild(0).GetComponent<Text>().text = "- " + this.transform.GetChild(1).GetChild(1).name + "원";
    }

    public void StuffLevelUp()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        int i = System.Convert.ToInt32(this.transform.GetChild(1).name);
        if (User.money - ((10000 * MyFurnitrue.stuffLv[i] * MyFurnitrue.stuffLv[i] * i)) < 0)
        {
            ScriptOnGUI("돈이 부족한걸...");
            return;
        }

        if (MyFurnitrue.stuffLv[i] >= User.house*2)
        {
            ScriptOnGUI("더욱 성장해야 올릴 수 있어!");
            return;
        }
        if (MyFurnitrue.stuffLv[i] >= 10)
        {
            ScriptOnGUI("더 이상 올릴 수 없어.");
            return;
        }
        if (MyFurnitrue.stuffLv[i]>=MyFurnitrue.ExpTable.Length)
        {
            ScriptOnGUI("이미 끝까지 올렸어.");
            return;  
        }

        User.money -= ((10000 * MyFurnitrue.stuffLv[i] * MyFurnitrue.stuffLv[i] * i));
        MyFurnitrue.stuffLv[i] += 1;
        MyFurnitrue.stuffExp[i - 1] = 0;
        MyFurnitrue.stuffIsLevel[i] = false;
        MyFurnitrue.StuffManager();
        MyFurnitrue.SaveDate();
    }

    public void StuffLevelRetain()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        int i = System.Convert.ToInt32(this.name);
        MyFurnitrue.stuffIsLevel[i] = true;
        MyFurnitrue.SaveDate();
    }

    public void LevelUpActive()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        int i = System.Convert.ToInt32(this.name);
        List<Furnitrue> fList = MyItems.LoadFurnitrueXML();
        MyFurnitrue.stuffIsLevel[i] = true;
        GameObject.Find("Canvas/Panels").transform.GetChild(8).gameObject.SetActive(true);
        GameObject.Find("Canvas/Panels").transform.GetChild(6).GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(6).gameObject.SetActive(true);
        Debug.Log("Items/item_furniture_" + fList[i].id.ToLower());
        GameObject.Find("Canvas/Panels").transform.GetChild(8).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/item_furniture_" + fList[i].id.ToLower());
        GameObject.Find("Canvas/Panels").transform.GetChild(8).GetChild(1).GetChild(0).GetComponent<Text>().text = "LEVEL " + MyFurnitrue.stuffLv[i] + " → " + (MyFurnitrue.stuffLv[i] + 1);
        GameObject.Find("Canvas/Panels").transform.GetChild(8).GetChild(3).GetChild(0).GetComponent<Text>().text = "- " + (10000 * MyFurnitrue.stuffLv[i] * MyFurnitrue.stuffLv[i] * i).ToString("N0") + "원";
        GameObject.Find("Canvas/Panels").transform.GetChild(8).GetChild(4).GetChild(1).name = i.ToString();
        GameObject.Find("Canvas/Panels").transform.GetChild(8).GetChild(5).name = i.ToString();
    }

    public void GetPause()
    {
        if (User.isPause)
            User.isPause = false;
        else
            User.isPause = true;
    }
    public void GameStart()
    {
        GameObject.Find("PanelStart").gameObject.SetActive(false);
        User.isPause = false;
    }

    public void SelectSkill()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        int i = System.Convert.ToInt32(gameObject.name);

        if (i>5)
        {
            for(int k =0;k<3; k++)
            {
                User.selectedSkill[2, k] = false;
            }
            User.selectedSkill[2, i-6] = true;
        }
        else if (i > 2)
        {
            for (int k = 0; k < 3; k++)
            {
                User.selectedSkill[1, k] = false;
            }
            User.selectedSkill[1, i-3] = true;
        }
        else
        {
            for (int k = 0; k < 3; k++)
            {
                User.selectedSkill[0, k] = false;
            }
            User.selectedSkill[0, i] = true;
        }
        
    }

    public void ScriptOnGUI(string script)
    {
        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(0).gameObject.SetActive(true);
        if (GameObject.Find("CanvasOverlay/Pop").transform.GetChild(0).gameObject != null)
        {
            GameObject.Find("CanvasOverlay/Pop").transform.GetChild(0).GetChild(0).GetComponent<Text>().text = script;
        }
    }

    public void AlertNoGUI(string alert)
    {
        GameObject.Find("PanelSelectMap").transform.GetChild(GameObject.Find("PanelSelectMap").transform.childCount - 1).gameObject.SetActive(true);
        GameObject.Find("PanelSelectMap").transform.GetChild(GameObject.Find("PanelSelectMap").transform.childCount - 1).GetChild(0).GetComponent<Text>().text = alert;
    }

    public void BoxBonus()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        float i = System.Convert.ToSingle(this.transform.GetChild(0).GetComponent<Text>().name);
        if(i>1000)
        {
            User.money += i;
        }
        else
        {
            User.particle += i;
        }
        
        User.frustrate = 0;
        User.SaveDate();
    }

    public void DayBonus()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        float i = System.Convert.ToSingle(this.transform.GetChild(0).GetComponent<Text>().name);
        User.particle += i;
        User.bonusDate = DateTime.Now.ToString();
        User.SaveDate();
    }

    public void LottoBonus()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        float i = System.Convert.ToSingle(this.name);
        User.money += i;
        User.SaveDate();
    }

    public void MemberLevelUp()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        int i = System.Convert.ToInt32(this.name);
        float cost = System.Convert.ToSingle(this.transform.GetChild(0).GetComponent<Text>().name);
        
        if(User.particle-cost<0)
        {
            ScriptOnGUI("파편이 부족해..");
            return;
        }
        if(User.memberlevel[i]+1>User.house)
        {
            ScriptOnGUI("아직은 더 올릴 수 없어.");
            return;
        }
        else
        {
            User.memberlevel[i] += 1;
            User.particle -= cost;
            MyFurnitrue.LoadData();
        }
        
    }

    public void BgmOnOff()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        if (User.isBackgroundSound)
        {
            User.isBackgroundSound = false;
            this.transform.GetChild(0).GetComponent<Text>().text = "상태 : ON";
            this.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        else
        {
            User.isBackgroundSound = true;
            this.transform.GetChild(0).GetComponent<Text>().text = "상태 : OFF";
            this.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
        }
    }
    public void EffectOnOff()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        if (User.isEffectSound)
        {
            User.isEffectSound = false;
            this.transform.GetChild(0).GetComponent<Text>().text = "상태 : ON";
            this.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            User.isEffectSound = true;
            this.transform.GetChild(0).GetComponent<Text>().text = "상태 : OFF";
            this.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
        }
    }

    public void RetryTour()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        int i = System.Convert.ToInt32(this.name);

        if (User.particle - i <= 0)
            return;
        else
            User.particle -= i;
        User.sleep = 0;
        User.isTry = true;
        User.isPlay = false;
        GameObject.Find("PanelTry").gameObject.SetActive(false);
    }
    public void GiveUpTour()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        User.isPause = false;
        User.sleep = 100;
        User.isTry = true;
        User.isPlay = false;
    }

    public void ChangeDay()
    {
        //낮과 밤
        if (User.day)//밤
        {
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 3:
                    GameObject.Find("Suns").transform.GetChild(2).gameObject.SetActive(true);
                    GameObject.Find("Suns").transform.GetChild(0).gameObject.SetActive(false);
                    if (UnityEngine.Random.Range(0, 12) < 3)
                        GameObject.Find("Suns").transform.GetChild(1).gameObject.SetActive(true);
                    else
                        GameObject.Find("Suns").transform.GetChild(1).gameObject.SetActive(false);

                    RenderSettings.fog = true;
                    int ran = UnityEngine.Random.Range(0, 3);
                    if (ran == 0)
                    {
                        GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Bgm/roombgmnight");
                        RenderSettings.skybox = Resources.Load<Material>("Skybox/Materials/SkyMidnight");
                        GameObject.Find("DayUI").transform.GetChild(0).GetComponent<Text>().text = "♪ 떨어지는 단풍잎(tido Kang)";
                    }
                    else if (ran == 1)
                    {
                        GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Bgm/roombgmnight2");
                        RenderSettings.skybox = Resources.Load<Material>("Skybox/Materials/SkyNight");
                        GameObject.Find("DayUI").transform.GetChild(0).GetComponent<Text>().text = "♪ 내 마음속의 달(tido Kang)";
                    }
                    else if (ran == 2)
                    {
                        GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Bgm/orgol");
                        RenderSettings.skybox = Resources.Load<Material>("Skybox/Materials/SkyNight");
                        GameObject.Find("DayUI").transform.GetChild(0).GetComponent<Text>().text = "♪ 오르골(오주희)";
                    }
                    if (!GameObject.Find("Main Camera").GetComponent<AudioSource>().isPlaying)
                        GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
                    if (GameObject.Find("House" + User.house + "/Furniture/Desk") != null)
                        GameObject.Find("House" + User.house + "/Furniture/Desk").transform.GetChild(0).gameObject.SetActive(true);
                    break;
                case 5:
                    GameObject.Find("DayUI").transform.GetChild(0).GetComponent<Text>().text = "";
                    break;
                case 6:
                    GameObject.Find("Sun/Directional Light").GetComponent<Light>().color = new Color(30 / 255f, 30 / 255f, 30 / 255f);
                    GameObject.Find("DayUI").transform.GetChild(0).GetComponent<Text>().text = "";
                    break;

            }
            GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = new Color(30 / 255f, 30 / 255f, 30 / 255f);
        }
        else//낮
        {
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 3:
                    RenderSettings.fog = false;
                    GameObject.Find("Suns").transform.GetChild(2).gameObject.SetActive(false);
                    GameObject.Find("Suns").transform.GetChild(0).gameObject.SetActive(true);
                    if (UnityEngine.Random.Range(0, 12) < 3)
                        GameObject.Find("Suns").transform.GetChild(1).gameObject.SetActive(true);
                    else
                        GameObject.Find("Suns").transform.GetChild(1).gameObject.SetActive(false);
                    if (UnityEngine.Random.Range(0, 2) < 1)
                    {
                        GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Bgm/roombgm1");
                        RenderSettings.skybox = Resources.Load<Material>("Skybox/Materials/SkyAfterNoon");
                        GameObject.Find("DayUI").transform.GetChild(0).GetComponent<Text>().text = "♪ 한 여름 날의 소풍(tido Kang)";
                    }
                    else
                    {
                        GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Bgm/roombgm2");
                        RenderSettings.skybox = Resources.Load<Material>("Skybox/Materials/SkyBrightMorning");
                        GameObject.Find("DayUI").transform.GetChild(0).GetComponent<Text>().text = "♪ 숲 속의 왈츠(tido Kang)";

                    }
                    if (!GameObject.Find("Main Camera").GetComponent<AudioSource>().isPlaying)
                        GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
                    if (GameObject.Find("House" + User.house + "/Furniture/Desk") != null)
                        GameObject.Find("House" + User.house + "/Furniture/Desk").transform.GetChild(0).gameObject.SetActive(false);
                    GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = new Color(227 / 255f, 245 / 255f, 200 / 255f);
                    break;
                case 5:
                    GameObject.Find("DayUI").transform.GetChild(0).GetComponent<Text>().text = "";
                    break;
                case 6:
                    GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = new Color(50 / 255f, 130 / 255f, 250 / 255f);
                    GameObject.Find("Sun/Directional Light").GetComponent<Light>().color = new Color(255 / 255f, 240 / 255f, 200 / 255f);
                    break;
            }
        }
        ChangeCloth();
    }

    public void ChangeCloth()
    {
        Texture sleepTexture = Resources.Load("pj_01_cha_sleep_cloth") as Texture;
        Texture clothTexture = Resources.Load("item_equip_defaultcloth") as Texture;
        Texture sleepTexturegirl = Resources.Load("girlSleep") as Texture;
        Texture clothTexturegirl = Resources.Load("girlDefault") as Texture;
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (User.day)
            {
                if (User.isDead)
                    GameObject.Find("User").GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = sleepTexturegirl;
                else
                    this.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = sleepTexture;

            }
            else
            {
                if (User.isDead)
                    GameObject.Find("User").GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = clothTexturegirl;
                else
                    GameObject.Find("User").GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = clothTexture;

            }
        }
        else
        {
            return;
        }
    }
    float AdsBonus(float amount)
    {
        float bonus = 0.0f;

        if (User.adsCount > 0)
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

    public void OnCredit()
    {
        User.money += 1;
    }

    public void OnReview()
    {
        Application.OpenURL("market://details?id=com.jegh.getajob");
    }

    public void ReceiveMoney()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        
        User.money += 1;


    }

    public void ReceiveParticle()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        User.particle += 1;
    }

    public void ExitGame()
    {
        User.SaveDate();
        MyFurnitrue.SaveDate();
        Story.SaveDate();
        Application.Quit();
    }


    public void AutoClickOn()
    {

        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        if (!User.isAuto)
        {
            if (User.particle - 5 < 0)
                return;
            User.particle -= 5;
            User.isAuto = true;
            this.transform.GetChild(1).gameObject.SetActive(false);
            this.transform.GetChild(2).gameObject.SetActive(false);
            this.transform.GetChild(3).gameObject.SetActive(true);
            Debug.Log("오토온");
        }
        else
            return;
    }

    public void buyDeskBed()
    {
        MyFurnitrue.stuffLv[1] = 1;
        MyFurnitrue.stuffLv[2] = 1;
        User.money -= 30000;
        Story.tutorialseqeuence = 6;
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
        GameObject.Find("House" + User.house + "/Furniture/Bed").gameObject.SetActive(true);
        GameObject.Find("House" + User.house + "/Furniture/Desk").gameObject.SetActive(true);
    }

    public void ChoiceMan()
    {
        User.isDead = false;
        User.isSelectGender = true;
        Debug.Log("남자선택");
    }
    public void ChoiceWoman()
    {
        User.isDead = true;
        User.isSelectGender = true;
        Debug.Log("여자선택");
    }

    public void AlertActivity()
    {
        if (User.status == 1)
            return;

        if(this.name.Equals("deskAlert"))
        {
            if (!User.day)
            {
                ScriptOnGUI("오늘 계획된 일을 다하고 하자.");
                User.status = 0;
                return;
            }
            if (User.isReport)
            {
                ScriptOnGUI("오늘은 이만 자야겠어...");
                User.status = 0;
                return;
            }
            if (GameObject.Find("Panels").transform.GetChild(2).gameObject.activeSelf)
                GameObject.Find("Panels").transform.GetChild(2).gameObject.SetActive(false);
            GameObject.Find("Panels").transform.GetChild(1).gameObject.SetActive(true);
        }
        else if(this.name.Equals("bedAlert"))
        {
            if (!User.day)
            {
                ScriptOnGUI("아직 피곤하지 않아..");
                User.status = 0;
                return;
            }
            else if (!User.isReport)
            {
                ScriptOnGUI("오늘 하루를 되돌아보고 자자!");
                User.status = 0;
                return;
            }
            if (GameObject.Find("Panels").transform.GetChild(1).gameObject.activeSelf)
                GameObject.Find("Panels").transform.GetChild(1).gameObject.SetActive(false);
            GameObject.Find("Panels").transform.GetChild(2).gameObject.SetActive(true);
        }       
    }

    public void GetRestBonus()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        float i = System.Convert.ToSingle(this.transform.GetChild(0).name);
        User.heart += i;
        User.SaveDate();
    }

    public void StuffExpUp()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        int i = System.Convert.ToInt32(this.transform.name);

        float cost = MyFurnitrue.stuffLv[i+1] * MyFurnitrue.stuffLv[i+1] * MyFurnitrue.stuffLv[i+1] * MyFurnitrue.ExpTable[MyFurnitrue.stuffLv[i+1]];
        if(User.heart-cost <0)
        {
            ScriptOnGUI("하트가 부족해!");
            return;
        }
        User.heart -= cost;
        MyFurnitrue.stuffExp[i] += 1;
    }

    public void GetParticle(float particle)
    {
        PlayerPrefs.SetInt("sorry", 1);
        User.particle += particle;
    }
}
