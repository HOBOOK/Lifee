using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

public class MyStatus : MonoBehaviour
{
    #region 변수
    public string myname;
    public float money;
    public  float fatigue;
    public float sleep;

    //자동하트
    public GameObject heartUI;
    float plusheart;

    //시간
    public float timeSpan;
    

    //UI
    Text moneyui;
    Text particleui;

    Text dayui;
    #endregion
    private void Awake()
    {
        User.LoadData();
        MyFurnitrue.LoadData();
        Story.LoadData();
    }
    void Start ()
    {
        moneyui = GameObject.Find("PanelMoney/txtMoney").GetComponent<Text>();
        particleui = GameObject.Find("PanelParticle/txtMoney").GetComponent<Text>();

        dayui = GameObject.Find("DayUI").GetComponent<Text>();

        if(User.isCloudSave)
        {
            GameObject.Find("Report").transform.GetChild(4).gameObject.SetActive(true);
            User.isCloudSave = false;
        }
        
        House();
        ChangeDay();
        MyFurnitrue.StuffManager();
        //출첵
        DayBonus();

        MessageInit();
        
    }
    void Update ()
    {
        if (User.isPause)
        {
            Time.timeScale = 0;
            return;
        }
        else
            Time.timeScale = 1;
        TestKey();
        PlusHearts();
        DesireManage();
        ViewUpdate();
        House();
        MemberManage();
        AlertPop();

    }
    //테스트용치트키
    void TestKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GameObject.Find("Report").transform.GetChild(5).gameObject.SetActive(true);
        if(Input.GetKeyDown(KeyCode.F1))
        {
            User.charm = 0;
        }
    }
    
    //욕구관리
    void DesireManage()
    {
        timeSpan += Time.deltaTime;

        //욕구증가
        if (User.status == 1) //잘 때
            User.sleep -= (5 + MyFurnitrue.stuffLv[1]) * Time.deltaTime;



        if (User.sleep > 100)
            User.sleep = 100;
        else if (User.sleep < 0)
            User.sleep = 0;


        if (timeSpan > 10.0)
        {
            User.SaveDate();
            MyFurnitrue.SaveDate();
            Story.SaveDate();
            PlayerPrefs.SetString("restdate", DateTime.Now.ToString());
            PlayerPrefs.Save();
            timeSpan = 0;
        }
    }
    //집관리
    void House()
    {
        if (SceneManager.GetActiveScene().buildIndex== 3)
        {
            if(User.house==0)
            {
                for (int i = 0; i < GameObject.Find("House").transform.childCount; i++)
                {
                    GameObject.Find("House").transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            else
            {
                for (int i = 0; i < GameObject.Find("House").transform.childCount; i++)
                {
                    if (GameObject.Find("House").transform.childCount <= i)
                        return;
                    if (User.house == i + 1)
                    {
                        GameObject.Find("House").transform.GetChild(i).gameObject.SetActive(true);
                    }
                    else
                    {
                        GameObject.Find("House").transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
        }
    }
    //UI업데이트
    void ViewUpdate()
    {
        money = User.money;
        sleep = User.sleep;


        if (moneyui!=null)
            moneyui.text = User.ChangeMoney(User.money);
        if(particleui!=null)
            particleui.text = User.particle.ToString("N0");

        if (User.day)
            dayui.text = User.night + "번째 저녁";
        else
            dayui.text = User.night + "번째 아침";

        GameObject.Find("HEART").transform.GetChild(1).GetComponent<Text>().text = User.ChangeUnit(User.heart);
        GameObject.Find("HEART").transform.GetChild(0).GetComponent<Image>().fillAmount = User.heartguage / 100;
        if (User.isLovePower)
            GameObject.Find("HEART").transform.GetChild(2).gameObject.SetActive(true);
        else
            GameObject.Find("HEART").transform.GetChild(2).gameObject.SetActive(false);
        DesirePopup();
    }
    //멤버관리
    private void MemberManage()
    {
        if (SceneManager.GetActiveScene().buildIndex != 3)
            return;
        //데이터
        // 1.강아지
        if (User.house >= 2 && User.member[0]==0 && (User.knowledge + User.health + User.charm + User.moral + User.lucky) >= 150 && User.tourLevel >= 5)
        {
            User.member[0] = 1;
            User.memberlevel[0] = 1;
            
            GameObject.Find("CanvasOverlay/Report").transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("CanvasOverlay/Report").transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "<color='yellow'>'귀여운 웰시코기 모찌'</color>\r\n이(가) 가족이 되었습니다!";
            GameObject.Find("CanvasOverlay/Report").transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Member/cogy");
            User.SaveDate();
        }
        // 2.여자친구
        if(User.house>=3 && User.member[1]==0 && (User.knowledge+User.health+User.charm+User.moral+User.lucky)>=300 &&User.jobLevel>=5)
        {
            User.member[1] = 1;
            User.memberlevel[1] = 1;
            
            GameObject.Find("CanvasOverlay/Report").transform.GetChild(1).gameObject.SetActive(true);
            if(User.isDead)
            {
                GameObject.Find("CanvasOverlay/Report").transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "<color='yellow'>'사랑스러운 여자친구 신애'</color>\r\n이(가) 가족이 되었습니다!";
                GameObject.Find("CanvasOverlay/Report").transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Member/love");

            }
            else
            {
                GameObject.Find("CanvasOverlay/Report").transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "<color='yellow'>'멋진 남자친구 경호'</color>\r\n이(가) 가족이 되었습니다!";
                GameObject.Find("CanvasOverlay/Report").transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Member/boy");

            }
            User.SaveDate();
        }
        // 3.자동차
        if (User.house >= 4 && User.member[2] == 0 && (User.knowledge + User.health + User.charm + User.moral + User.lucky) >= 1000 && User.jobLevel >= 10)
        {
            User.member[2] = 1;
            User.memberlevel[2] = 1;
            
            GameObject.Find("CanvasOverlay/Report").transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("CanvasOverlay/Report").transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "<color='yellow'>'젠틀한 자동차 붕붕이'</color>\r\n이(가) 가족이 되었습니다!";
            GameObject.Find("CanvasOverlay/Report").transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Member/car");
            User.SaveDate();
        }
        // 4.딸
        if (User.house >= 5 && User.member[3] == 0 && (User.knowledge + User.health + User.charm + User.moral + User.lucky) >= 2000 && User.tourLevel >= 15&&User.jobLevel>=15)
        {
            User.member[3] = 1;
            User.memberlevel[3] = 1;
            
            GameObject.Find("CanvasOverlay/Report").transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("CanvasOverlay/Report").transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "<color='yellow'>'사랑스러운 딸 뚜니'</color>\r\n이(가) 가족이 되었습니다!";
            GameObject.Find("CanvasOverlay/Report").transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Member/daughter");
            User.SaveDate();
        }

        

        //오브젝트
        for (int i = 0; i < User.member.Length; i++)
        {
            if (User.member[i] == 0)
            {
                if(GameObject.Find("Member")!=null)
                    GameObject.Find("Member").transform.GetChild(i).gameObject.SetActive(false);

                    GameObject.Find("CanvasOverlay/Pop/Guage").transform.GetChild(i).gameObject.SetActive(false);//게이지

            }
            else
            {
                if (GameObject.Find("Member") != null)
                    GameObject.Find("Member").transform.GetChild(i).gameObject.SetActive(true);
                if (SceneManager.GetActiveScene().buildIndex == 3|| SceneManager.GetActiveScene().buildIndex == 4)
                {
                    if(User.isPlay)
                        GameObject.Find("CanvasOverlay/Pop/Guage").transform.GetChild(i).gameObject.SetActive(false);//게이지
                    else
                        GameObject.Find("CanvasOverlay/Pop/Guage").transform.GetChild(i).gameObject.SetActive(true);//게이지
                }
                    
            } 
        }
    }
    //오브젝트클릭
    private GameObject GetClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 포인트 근처 좌표를 만든다. 

        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))   //마우스 근처에 오브젝트가 있는지 확인
        {
            //있으면 오브젝트를 저장한다.
            target = hit.collider.gameObject;
        }
        return target;
    }
    //종료시 발생
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            User.SaveDate();
            MyFurnitrue.SaveDate();
            Story.SaveDate();
        }

    }
    //욕구 정보
    void DesirePopup()
    {
        GameObject.Find("CanvasOverlay/PanelDesire/desireSleep").GetComponent<Image>().fillAmount = User.sleep/100;
    }
    //옷 정보
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
                if(User.isDead)
                    this.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = sleepTexturegirl;
                else
                    this.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = sleepTexture;

            }
            else
            {
                if(User.isDead)
                    this.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = clothTexturegirl;
                else
                    this.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = clothTexture;

            }
        }
    }
    
    //밤낮변경
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
    //가구관리
    
    //출석보상
    private void DayBonus()
    {
        if (SceneManager.GetActiveScene().buildIndex != 3)
            return;
 
        int timeDayDifference = (DateTime.Now - Convert.ToDateTime(User.bonusDate)).Days;
        if(timeDayDifference >= 1)
        {
            GameObject.Find("CanvasOverlay/Pop").transform.GetChild(4).gameObject.SetActive(true);
            Debug.Log("출첵");
        }
    }
    //알림
    private void AlertPop()
    {
        if (GameObject.Find("House/House" + User.house + "/Furniture/Desk") == null && GameObject.Find("House/House" + User.house + "/Furniture/Bed")==null)
            return;

        if(User.day&&!User.isPlay)
        {
            GameObject.Find("CanvasOverlay/Pop").transform.GetChild(5).gameObject.SetActive(true);
            if (!User.isReport)
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(GameObject.Find("House/House" + User.house + "/Furniture/Desk").transform.position);
                screenPos.y += Screen.height*0.25f;
                GameObject.Find("CanvasOverlay/Pop").transform.GetChild(5).name = "deskAlert";
                GameObject.Find("CanvasOverlay/Pop").transform.GetChild(5).position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
                GameObject.Find("CanvasOverlay/Pop").transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("report");
            }
            else
            {
                GameObject.Find("CanvasOverlay/Pop").transform.GetChild(5).name = "bedAlert";
                Vector3 screenPos = Camera.main.WorldToScreenPoint(GameObject.Find("House/House" + User.house + "/Furniture/Bed").transform.position);
                screenPos.y += Screen.height * 0.25f;
                GameObject.Find("CanvasOverlay/Pop").transform.GetChild(5).position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
                GameObject.Find("CanvasOverlay/Pop").transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("changeday");
            }
            
        }
        else
        {
            GameObject.Find("CanvasOverlay/Pop").transform.GetChild(5).gameObject.SetActive(false);
        }
    }


    private float lovetime;
    void PlusHearts()
    {
        if(User.isLovePower)
        {
            lovetime += Time.deltaTime;
            if(lovetime>60)
            {
                User.isLovePower = false;
                lovetime = 0;
            }
        }
        if(!User.isLovePower)
            User.heartguage += ((User.house*1.5f)+5) * Time.deltaTime;
        else
            User.heartguage += ((User.house * 1.5f) + 5) * 2 *Time.deltaTime;
        if (User.heartguage > 100)
        {
            for (int i = 1; i < MyFurnitrue.stuffLv.Length; i++)
            {
                plusheart += MyFurnitrue.stuffLv[i];
            }
            plusheart += User.house * 10;
            plusheart += plusheart * User.itemLevel[0] * 0.01f;
            HeartManager(plusheart);
            plusheart = 0;
            User.heartguage = 0;
        }
    }
    public void HeartManager(float heart)
    {
        if (!GameObject.Find("SoundOfPop").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
            GameObject.Find("SoundOfPop").GetComponent<AudioSource>().Play();

        if(SceneManager.GetActiveScene().buildIndex==3|| SceneManager.GetActiveScene().buildIndex == 8)
        {
            GameObject heartui = Instantiate(heartUI) as GameObject;
            heartui.transform.GetChild(1).GetComponent<Text>().text = User.ChangeUnit(heart);
            heartui.transform.SetParent(GameObject.Find("CanvasOverlay").transform);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            screenPos.y += Screen.height * 0.25f;
            heartui.transform.position = screenPos;
            heartui.transform.localScale = new Vector3(1, 1, 1);
            User.heart += heart;
        }
        
    }

    void MessageInit()
    {
        User.LoadData();
        User.message.Clear();

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

        if (User.isDead)
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

        for (int i = 0; i < User.memberlevel.Length; i++)
        {

        }
        if (User.memberlevel[0] > 0)
        {
            User.message[1] = "우리 귀염둥이 모찌 이리와~";
        }
        else if (User.memberlevel[1] > 0)
        {
            User.message[2] = "자기와 함께여서 행복해!";
            User.message[8] = "자기! 오늘 치맥먹으면서 영화어때?";
        }
        else if (User.memberlevel[2] > 0)
        {
            User.message[3] = "붕붕이는 정말 멋있는 차야";
        }
        else if (User.memberlevel[3] > 0)
        {
            User.message[4] = "착한 우리딸 뚜니 귀여워 ^^!!!";
        }

        User.SaveDate();
    }


}
