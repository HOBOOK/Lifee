using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using GooglePlayGames.Android;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public class CloudButton : MonoBehaviour
{
    //클라우드 로드
    public void LoadGold()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        GoogleManager.Instance.LoadFromCloud((string dateToLoad) =>
        {
            if (dateToLoad == null)
                return;
            string data = dateToLoad;
            char[] split = { '|' };
            string[] mddata = data.Split(split);
            string userdata = mddata[0];
            string storydata = mddata[1];
            string stuffdata = mddata[2];

            User dat = JsonUtility.FromJson<User>(userdata);
            Story sty = JsonUtility.FromJson<Story>(storydata);
            MyFurnitrue mf = JsonUtility.FromJson<MyFurnitrue>(stuffdata);

            User.myname = dat.s_myname;
            User.money = dat.s_money;
            User.fatigue = dat.s_fatigue;
            User.knowledge = dat.s_knowledge;
            User.health = dat.s_health;
            User.charm = dat.s_charm;
            User.moral = dat.s_moral;
            User.lucky = dat.s_lucky;
            User.sleep = dat.s_sleep;
            User.particle = dat.s_particle;
            User.frustrate = dat.s_frustrate;
            User.house = dat.s_house;
            User.day = dat.s_day;
            User.night = dat.s_night;
            User.equip = dat.s_equip;
            User.storysequence = dat.s_storysequence;
            User.map = dat.s_map;
            User.heart = dat.s_heart;
            User.member = dat.s_member;
            User.memberlevel = dat.s_memberlevel;
            User.meter = dat.s_meter;
            User.skillLevel = dat.s_skillLevel;
            User.passiveLevel = dat.s_passiveLevel;
            User.healingLevel = dat.s_healingLevel;
            User.isReport = dat.s_isReport;
            User.message = dat.s_message;
            User.isDead = dat.s_isDead;
            User.bonusDate = dat.s_bonusDate;
            User.jobLevel = dat.s_jobLevel;
            User.tourLevel = dat.s_tourLevel;
            User.jobExp = dat.s_jobExp;
            User.tourExp = dat.s_tourExp;
            User.isEffectSound = dat.s_isEffectSound;
            User.isBackgroundSound = dat.s_isBackgroundSound;
            User.isCloud = dat.s_isCloud;
            User.isAds = dat.s_isAds;
            User.rewardSec = dat.s_rewardSec;
            User.jobenemylevel = dat.s_jobenemylevel;
            User.itemLevel = dat.s_itemLevel;

            Story.storyseqeuence = sty.s_storysequence;
            Story.tutorialseqeuence = sty.s_tutorialsequence;

            MyFurnitrue.stuffLv = mf.s_stuffLv;
            MyFurnitrue.stuffExp = mf.s_stuffExp;
            MyFurnitrue.stuffIsLevel = mf.s_isLevelUp;

            User.SaveDate();
            Story.SaveDate();
            MyFurnitrue.SaveDate();
            User.isCloudSave = true;
            SceneManager.LoadSceneAsync(3);
            
        });
    }

    public void LoadTextGold()
    {
        User.LoadData();
        Story.LoadData();
        MyFurnitrue.LoadData();
        string data = User.jsonData + "|" + Story.jsonStoryData + "|" + MyFurnitrue.jsonStuffData;
        char[] split = { '|' };
        string[] mddata = data.Split(split);
        string userdata = mddata[0];
        string storydata = mddata[1];
        string stuffdata = mddata[2];


        User dat = JsonUtility.FromJson<User>(userdata);
        Story sty = JsonUtility.FromJson<Story>(storydata);
        MyFurnitrue mf = JsonUtility.FromJson<MyFurnitrue>(stuffdata);

        User.myname = dat.s_myname;
        User.money = dat.s_money;
        User.fatigue = dat.s_fatigue;
        User.knowledge = dat.s_knowledge;
        User.health = dat.s_health;
        User.charm = dat.s_charm;
        User.moral = dat.s_moral;
        User.lucky = dat.s_lucky;
        User.sleep = dat.s_sleep;
        User.particle = dat.s_particle;
        User.frustrate = dat.s_frustrate;
        User.house = dat.s_house;
        User.day = dat.s_day;
        User.night = dat.s_night;
        User.equip = dat.s_equip;
        User.storysequence = dat.s_storysequence;
        User.map = dat.s_map;
        User.heart = dat.s_heart;
        User.member = dat.s_member;
        User.memberlevel = dat.s_memberlevel;
        User.meter = dat.s_meter;
        User.skillLevel = dat.s_skillLevel;
        User.passiveLevel = dat.s_passiveLevel;
        User.healingLevel = dat.s_healingLevel;
        User.isReport = dat.s_isReport;
        User.message = dat.s_message;
        User.isDead = dat.s_isDead;
        User.bonusDate = dat.s_bonusDate;
        User.jobLevel = dat.s_jobLevel;
        User.tourLevel = dat.s_tourLevel;
        User.jobExp = dat.s_jobExp;
        User.tourExp = dat.s_tourExp;
        User.isEffectSound = dat.s_isEffectSound;
        User.isBackgroundSound = dat.s_isBackgroundSound;
        User.isCloud = dat.s_isCloud;
        User.isAds = dat.s_isAds;
        User.rewardSec = dat.s_rewardSec;
        User.jobenemylevel = dat.s_jobenemylevel;
        User.itemLevel = dat.s_itemLevel;

        Story.storyseqeuence = sty.s_storysequence;
        Story.tutorialseqeuence = sty.s_tutorialsequence;

        MyFurnitrue.stuffLv = mf.s_stuffLv;
        MyFurnitrue.stuffExp = mf.s_stuffExp;
        MyFurnitrue.stuffIsLevel = mf.s_isLevelUp;

        User.SaveDate();
        Story.SaveDate();
        MyFurnitrue.SaveDate();
        User.isCloudSave = true;
        SceneManager.LoadSceneAsync(3);
    }

    //클라우드 세이브
    public void SaveGold()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        User.SaveDate();
        Story.SaveDate();
        MyFurnitrue.SaveDate();
        string test = "";
        for(int i = 0; i <User.jsonData.Length; i++)
        {
            test += User.jsonData[i];
        }
        string data = User.jsonData + "|" + Story.jsonStoryData + "|" + MyFurnitrue.jsonStuffData;
        Debug.Log(data);
        GoogleManager.Instance.SaveToCloud(data);
        

        
    }
}
