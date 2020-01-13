using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class HomeManager : MonoBehaviour
{
    
    public GameObject effect;
    public GameObject fade;
    private string restdate;

    private void Start()
    {
        RestBonus();
        fade.gameObject.SetActive(true);

    }

    void Update ()
    {

        HomeTouch();
	}

    void HomeTouch()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (!EventSystem.current.IsPointerOverGameObject(i))
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        Vector3 pos = ray.GetPoint(20);
                        GameObject CreateEffect = Instantiate(effect);
                        if (!User.isEffectSound)
                        {
                            CreateEffect.GetComponent<AudioSource>().Play();
                        }
                        CreateEffect.transform.position = pos;
                        CreateEffect.GetComponent<ParticleSystem>().Play();
                        User.heartguage += ((User.house * 2) * (1 + (User.itemLevel[4] * 0.1f)));
                        break;

                    }
                }
                
            }
        }
        //pc디버깅용 마우스이벤트

        else if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 pos = ray.GetPoint(20);
                GameObject CreateEffect = Instantiate(effect);
                if (!User.isEffectSound)
                {
                    CreateEffect.GetComponent<AudioSource>().Play();
                }
                CreateEffect.transform.position = pos;
                CreateEffect.GetComponent<ParticleSystem>().Play();
                User.heartguage += User.house*2;
            }
        }
    }

    void RestBonus()
    {
        if (PlayerPrefs.HasKey("restdate"))
        {
            restdate = PlayerPrefs.GetString("restdate");
        }
        else
        {
            PlayerPrefs.SetString("restdate", DateTime.Now.ToString());
            return;
        }

        TimeSpan timeSpan= DateTime.Now - Convert.ToDateTime(restdate);

        Debug.Log(timeSpan);
        Debug.Log(restdate);
        if (timeSpan.TotalSeconds > 30*60)
        {
            //하트계산식
            float plusheart = 0.0f;
            float sec = 100 / (((User.house * 1.5f) + 5)); //하트차는소요시간
            float resultheart = 0.0f;
            for (int i = 1; i < MyFurnitrue.stuffLv.Length; i++)
            {
                plusheart += MyFurnitrue.stuffLv[i];
            }
            plusheart += User.house * 10;
            plusheart += plusheart * User.itemLevel[0] * 0.01f;

            if(System.Convert.ToSingle(timeSpan.TotalSeconds)>24*60*60)
                resultheart = (plusheart / sec) * 24*60*6;
            else
                resultheart = (plusheart / sec)*System.Convert.ToSingle(timeSpan.TotalSeconds)*0.1f;

            Debug.Log(timeSpan.TotalSeconds);
            Debug.Log(plusheart / sec);

            GameObject.Find("Report").transform.GetChild(10).gameObject.SetActive(true);
            GameObject.Find("Report").transform.GetChild(10).GetChild(0).GetComponent<Text>().text = "+" + User.ChangeUnit(resultheart);
            GameObject.Find("Report").transform.GetChild(10).GetChild(1).GetChild(0).name = resultheart.ToString("N0");
            GameObject.Find("Report").transform.GetChild(10).GetChild(2).GetChild(0).name = resultheart.ToString("N0");
            GameObject.Find("Report").transform.GetChild(10).GetChild(3).GetChild(0).name = (resultheart*2).ToString("N0");

            PlayerPrefs.SetString("restdate", DateTime.Now.ToString());
            PlayerPrefs.Save();
            return;
        }
    }


    
}
