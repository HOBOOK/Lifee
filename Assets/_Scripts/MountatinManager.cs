using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.EventSystems;

public class MountatinManager : MonoBehaviour
{
    float endTime;
    float anitime;
    float moneytime;

    public GameObject time;
    GameObject score;
    public GameObject clickedobj;
    public GameObject fatigueUI;
    public GameObject heartUI;
    public GameObject CanvasMountain;
    public GameObject statUI;
    public GameObject moneyUI;

    private bool isOver;
    float totalParticle;
    float totalExp;
    float totalHeart;

    //하트 오브젝트
    public GameObject heart;
    public GameObject redheart;

    //장애물오브젝트
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    //장애물오브젝트
    public GameObject animal1;
    public GameObject animal2;
    public GameObject animal3;

    //새
    public GameObject bird1;
    public GameObject bird2;

    //캠프파이어
    public GameObject campfire;

    //장애물 배치 변수
    private int start;
    private bool check;
    private float gap;
    private bool isWater;
    private float waterTime;

    //재시도 변수
    public GameObject retry;
    //결과
    public GameObject result;
    List<Exp> tblExp;

    //여행스킬
    private float skillspeed;
    private float bonusheart;
    private bool isUlti;

    float skilltime, skilltime2, skilltime3;

    float viewdistance;
    float tempSpeed;


    private int walkLevel;
    void Start()
    {

        tblExp = MyItems.LoadIExpXML();
        User.buff = 0;
        User.map = 5;
        User.status = 4;
        User.meter = 0;
        User.isDamage = false;
        User.isPlay = false;
        User.isTry = false;
        for(int i =0; i<3;i++)
        {
            User.tourSkill[i] = false;
            User.isContact[i] = false;
        }
        
        User.skillSpeed = 0;
        User.tourSpeed = User.memberlevel[0];
        User.SaveDate();

        totalHeart = 0;
        isOver = false;
        score = GameObject.Find("CanvasMountain/Score");
        result.transform.GetChild(5).gameObject.SetActive(false);
        anitime = 0;
        walkLevel = 0;
        totalParticle = 0;
        start = 0;
        gap = 30;

        int memcount = 1;
        for(int i = 0; i < User.memberlevel.Length; i++)
        {
            if (User.memberlevel[i] > 0)
                memcount += 1;
        }
        if(User.isTourHard)
            minusMoney = (1000*memcount) - User.itemLevel[3];
        else
            minusMoney = (100*memcount)-User.itemLevel[3];
        if (minusMoney < 0)
            minusMoney = 0;

        if(User.memberlevel[3]>0)
        {
            GameObject.Find("Daughter").gameObject.SetActive(true);
        }
        else
            GameObject.Find("Daughter").gameObject.SetActive(false);


        ResponeBird();
        ResponeFire();
        ResponeEnemy();

        if (UnityEngine.Random.Range(0, 2) < 1)
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Bgm/mountain2");
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
            GameObject.Find("DayUI").transform.GetChild(0).GetComponent<Text>().text = "♪ With you(tido Kang)";
        }
        else
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Bgm/tourbgm");
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
            GameObject.Find("DayUI").transform.GetChild(0).GetComponent<Text>().text = "♪ 숲 속의 고요(오주희)";

        }
        
        User.isPause = true;
    }

    float minusMoney = 0;
    void Update()
    {
        if (User.isPause)
        {
            Time.timeScale = 0;
            return;
        }
        else
        {
            Time.timeScale = 1;
        }
        if (User.status == 7)
            return;
        TimeChange();
        TryManager();
        
        GameOver();
        if (isOver)
            return;

        LevelManager();
        TourSkill();
        CheckBird();
        MtUiView();
        MoneyManager(minusMoney);
        ShiningSpeak();

        //지속적인 생성
        if(this.transform.position.z > (start+10)* gap && !check)
        {
            check = true;
        }
        if(check)
        {
            start += 15;
            ResponeEnemy();
            check=false;
        }
    }

    void MtUiView()
    {
        score.transform.GetChild(0).GetComponent<Slider>().value = this.transform.position.z / 10050;
        GameObject.Find("birdCount").transform.GetChild(1).GetComponent<Text>().text = User.today_meet.ToString("N0");
    }


    void MoneyManager(float money)
    {
        if (User.isPlay)
            return;

        moneytime += Time.deltaTime;
        if(moneytime>1.0)
        {
            GameObject moneyui = Instantiate(moneyUI) as GameObject;
            moneyui.transform.SetParent(CanvasMountain.transform);
            moneyui.transform.GetChild(1).GetComponent<Text>().text = "-" + money.ToString("N0");
            moneyui.transform.localScale = new Vector3(1, 1, 1);
            moneytime = 0;
            User.money -= money;
            
        }
        
    }

    void FatigueManager(float hp)
    {
        GameObject fatigueui = Instantiate(fatigueUI) as GameObject;
        fatigueui.transform.GetChild(1).GetComponent<Text>().text = (-hp).ToString("N0");
        fatigueui.transform.SetParent(CanvasMountain.transform);
        fatigueui.transform.localPosition = new Vector3(0, 50, 0);
        fatigueui.transform.localScale = new Vector3(1, 1, 1);
        User.fatigue += hp;
    }
    

    void ParticleManager(float particle)
    {
        GameObject statui = Instantiate(statUI) as GameObject;
        statui.transform.SetParent(CanvasMountain.transform);
        statui.transform.localScale = new Vector3(1, 1, 1);
        statui.transform.GetChild(1).GetComponent<Text>().text = "+ " + particle.ToString("N0");
        totalParticle += particle;
        User.particle += particle;
    }

    void CheckBird()
    {
        for(int i = 0; i < GameObject.Find("Objects").transform.childCount; i++)
        {
            if(Vector3.Distance(GameObject.Find("Objects").transform.GetChild(i).transform.position,GameObject.Find("User").transform.position)<20)
            {
                if (!GameObject.Find("SoundOfBird").transform.GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
                    GameObject.Find("SoundOfBird").transform.GetComponent<AudioSource>().Play();
                if (GameObject.Find("Objects").transform.GetChild(i).name!="false")
                {
                    if (!GameObject.Find("Main Camera/effects").transform.GetChild(0).GetComponent<ParticleSystem>().isPlaying)
                        GameObject.Find("Main Camera/effects").transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                    ParticleManager(1+User.healingLevel[2]);
                    User.today_meet += 1;
                    isSpeak = true;
                    GameObject.Find("Objects").transform.GetChild(i).name = "false";
                }
                break;
            }
        }


    }

   

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
    
    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "campfire"&&col.name.Contains("fire"))
        {
            User.status = 7;
            User.isPlay = true;
            col.name = "DestroyFire";
            if (!col.GetComponent<AudioSource>().isPlaying&&!User.isEffectSound)
                col.GetComponent<AudioSource>().Play();
            Vector3 pos = col.transform.position;
            pos.z -= 10;
            this.transform.position = pos;
        }
        
        if (col.transform.tag == "heart")
        {
            if (!GameObject.Find("SoundOfPop").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
                GameObject.Find("SoundOfPop").GetComponent<AudioSource>().Play();
            col.name = "Destroy";
            HeartManager(User.TOUR_POWER*((walkLevel+1)*0.12f));
        }
        if (col.transform.tag == "redheart")
        {
            if (!GameObject.Find("SoundOfHeart").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
                GameObject.Find("SoundOfHeart").GetComponent<AudioSource>().Play();

            col.name = "Destroy";
            User.sleep -= 15;
            User.tourSkill[0] = true;
            User.tourSkill[1] = true;
            User.sleep -= (User.TOUR_POWER + 500) * 0.01f;
        }

        //적과 충돌
        if (col.transform.tag == "enemy")
        {
            if (!GameObject.Find("SoundOfDamage").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
            {
                GameObject.Find("SoundOfDamage").GetComponent<AudioSource>().Play();
            }
            if (!User.isPlay&&!isUlti)
            {
                User.isDamage = true;
                if (!User.isTourHard)
                    User.sleep += 20 + walkLevel;
                else
                    User.sleep += 50 + walkLevel;
            }

            col.name = "Destroy";
            
        }

        if(col.transform.tag=="mountain")
        {
            //User.sleep = 100;
        }
    }

    int rowCount;
    int heartcount;
    public void ResponeEnemy()
    {
        float l, c, c2,r;
        GameObject[] enemys = { enemy1, enemy2, enemy3};
        GameObject[] animals = { animal1, animal2, animal3};

        for (int i = start; i < start+15; i++)//열
        {
            if (start * gap > 9800)
                break;
            rowCount = 0;
            heartcount = 0;
            for (int j = 0; j < 4; j++)//횡
            {
                l = Random.Range(-15f, -23f);
                c = Random.Range(-10f, -3f);
                c2  = Random.Range(3f, 10f);
                r = Random.Range(15f, 23f);
                float[] randomX = { l, c, c2,r };
                
                if (Random.Range(0, 2) < 1&&heartcount<3)
                {
                    GameObject heartTemp = Instantiate(heart) as GameObject;
                    heartTemp.transform.position = new Vector3(randomX[j], 0, ((i + 1) * (gap*2))+15);
                    heartcount++;
                }
                else if (Random.Range(0, 20) < 1 && heartcount < 3)
                {
                    GameObject heartTemp = Instantiate(redheart) as GameObject;
                    heartTemp.transform.position = new Vector3(randomX[j], 0, ((i + 1) * (gap * 2)) + 15);
                    heartcount++;
                }
                if (start*gap>2500)
                {
                    if(!User.isTourHard)
                    {
                        GameObject.Find("effects").transform.GetChild(1).gameObject.SetActive(false);
                        GameObject.Find("effects").transform.GetChild(2).gameObject.SetActive(true);
                    }
                    
                    //오브젝트배치
                    if (Random.Range(0, 7) < 3&&rowCount<4)
                    {
  
                        GameObject animal = Instantiate(animals[Random.Range(0, animals.Length)]) as GameObject;
                        animal.transform.position = new Vector3(randomX[j], 0, (i + 1) * gap);
                        if(animal.name.Contains("tree"))
                            animal.transform.position = new Vector3(randomX[j], -2, (i + 1) * gap);
                        rowCount++;
                    }
                }
                else
                {
                    //오브젝트배치
                    if (Random.Range(0f, 2f) < 0.7f && rowCount < 3)
                    {

                        GameObject enemy = Instantiate(enemys[Random.Range(0, enemys.Length)]) as GameObject;
                        enemy.transform.position = new Vector3(randomX[j], 0, (i + 1) * gap);
                        rowCount++;
                    }
                }
            }
        }
    }

    public void ResponeBird()
    {
        int[] randomX = { -25, 25 };//x좌표
        GameObject[] birds = { bird1, bird2};

        for (int i = 0; i < 20; i++)//열
        {
            int ranBird = Random.Range(0, birds.Length);
            GameObject bird = Instantiate(birds[ranBird]) as GameObject;
            bird.transform.SetParent(GameObject.Find("Objects").transform);
            bird.transform.position = new Vector3(randomX[Random.Range(0, 2)], 3, (i + 1) * 400);
        }
    }

    public void ResponeFire()
    {
        for (int i = 0; i < 7; i++)//열
        {
            GameObject fire = Instantiate(campfire) as GameObject;
            fire.transform.position = new Vector3(0, -2, ((i + 1) * 1500)+15);
        }
    }

    float sleepamount = (User.TOUR_POWER + 500) * 0.0005f;
    void GameOver()
    {
        if (sleepamount > 1.4f)
            sleepamount = 1.4f;
        User.sleep += (1.5f - sleepamount + (walkLevel * 0.1f)) * Time.deltaTime;
        if(this.transform.position.z>10050&&User.sleep<100)
        {
            User.isTry = true;
            User.sleep = 100;
            ParticleManager(walkLevel);
        }
        if (User.money<0)
        {
            User.isTry = true;
            User.sleep = 100;
            User.money = 0;
        }

        if (User.sleep>=100&User.isTry)
        {
            if (anitime == -1)
                return;
            anitime += Time.deltaTime;
            float stuffbonus = 0;
            for (int i = 1; i < MyFurnitrue.stuffLv.Length; i++)
            {
                stuffbonus += MyFurnitrue.stuffLv[i];
            }
            if (anitime > 2.0)
            {
                User.isPlay = true;
                User.skillSpeed = 0;
                User.tourExp += totalExp;
                User.isTourHard = false;
                User.SaveDate();
                TourLevelUp();
                result.transform.GetChild(1).GetComponent<Text>().text = User.ChangeUnit(totalHeart);
                result.transform.GetChild(6).GetComponent<Slider>().value = User.tourExp / tblExp[User.tourLevel - 1].needExp;
                result.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = User.tourLevel.ToString(); //레벨
                result.transform.GetChild(5).gameObject.SetActive(true);
                anitime = -1;
                return;
            }
            else
            {
                User.tourSpeed = 0;
                User.skillSpeed = 0;
                User.status = 0;
                if (!User.isTourHard)
                    totalExp = (User.meter * User.today_meet) + ((User.meter * User.today_meet) * User.passiveLevel[0] * 0.1f);
                else
                    totalExp = (User.meter * User.today_meet *0.1f) + ((User.meter * User.today_meet * 0.1f) * User.passiveLevel[0] * 0.1f);

                User.sleep = 100;
                isOver = true;

                result.gameObject.SetActive(true);
                result.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = User.tourLevel.ToString(); //레벨

                result.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "+ " + totalParticle.ToString("N0");

                result.transform.GetChild(6).GetChild(2).GetComponent<Text>().text = "+ " + totalExp.ToString("N0");

                result.transform.GetChild(1).GetComponent<Text>().text = User.ChangeUnit((totalHeart * 0.5f*anitime));
                result.transform.GetChild(6).GetComponent<Slider>().value = (User.tourExp + (totalExp * 0.5f * anitime)) / tblExp[User.tourLevel - 1].needExp;
            }
            
            

        }
    }
    void TryManager()
    {
        if (User.sleep>=100&&!User.isTry)
        {
            User.isPlay = true;
            retry.gameObject.SetActive(true);
            retry.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = walkLevel.ToString("N0");
            retry.transform.GetChild(0).GetChild(2).name = walkLevel.ToString("N0");
        }
    }

    void TourLevelUp()
    {
        if(User.tourExp>=tblExp[User.tourLevel-1].needExp && User.tourLevel < 30)
        {
            User.tourExp = 0;
            User.tourLevel += 1;

            if (User.tourLevel == 5 && !PlayerPrefs.HasKey("tour5"))
            {
                GoogleManager.Instance.CompleteTour5();
            }
            else if (User.tourLevel == 10 && !PlayerPrefs.HasKey("tour10"))
            {
                GoogleManager.Instance.CompleteTour10();
            }
            else if (User.tourLevel == 15 && !PlayerPrefs.HasKey("tour15"))
            {
                GoogleManager.Instance.CompleteTour15();
            }
            else if (User.tourLevel == 20 && !PlayerPrefs.HasKey("tour20"))
            {
                GoogleManager.Instance.CompleteTour20();
            }
            else if (User.tourLevel == 25 && !PlayerPrefs.HasKey("tour25"))
            {
                GoogleManager.Instance.CompleteTour25();
            }
            else if (User.tourLevel == 30 && !PlayerPrefs.HasKey("tour30"))
            {
                GoogleManager.Instance.CompleteTour30();
            }
        }
    }

    string ChangeMeter(float meter)
    {
        if (meter > 1000)
            return string.Format("{0:#.##}KM", meter / 1000);
        else
            return string.Format("{0:#}M", meter);
    }

    Vector3 screenPos;
    void TourSkill()
    {
        //UI
        //
        if (User.tourSkill[0])
        {
            skilltime += Time.deltaTime;
            this.transform.GetChild(3).gameObject.SetActive(true);
            isUlti = true;
            User.skillSpeed = 15;
            
            if (skilltime >1.5f+(User.healingLevel[0]*0.5f))
            {
                viewdistance -= 0.5f;
            }
            else
            {
                viewdistance += 1;
            }
            if (viewdistance > 70)
                viewdistance = 70;
            else if (viewdistance < 60)
                viewdistance = 60;

            GameObject.Find("Main Camera").GetComponent<Camera>().fieldOfView = viewdistance;

            if (skilltime> 2.5f+(User.healingLevel[0] * 0.5f))
            {
                isUlti = false;
                User.tourSkill[0] = false;
                User.skillSpeed = 0;
                viewdistance = 60;
                GameObject.Find("Main Camera").GetComponent<Camera>().fieldOfView = viewdistance;
                this.transform.GetChild(3).gameObject.SetActive(false);
                skilltime = 0;
            }
        }
        if(User.tourSkill[1])
        {
            skilltime2 += Time.deltaTime;
            if(skilltime2>10.0f)
            {
                User.tourSkill[1] = false;
                skilltime2 = 0;
            }
        }
        if(User.tourSkill[2])
        {
            skilltime3 += Time.deltaTime;

            if(skilltime3>1.0)
            {
                User.tourSkill[2] = false;
                skilltime3 = 0;
            }
        }
        

    }

    public void TimeChange()
    {
        float t = 200 - (User.sleep * 1.5f);
        time.GetComponent<Camera>().backgroundColor = new Color(30 / 255f, 40 / 255f, t/255f);

        Vector3 sunpos = time.transform.GetChild(5).transform.position;
        sunpos.y = 25-(User.sleep*0.1f);
        time.transform.GetChild(5).transform.position = sunpos;

        GameObject.Find("Directional Light").GetComponent<Light>().color = new Color(t / 255f, t/ 255f, t / 255f, 1);
    }


    float[] level =
    {
        50,100,150,200,250,300,350,400,450,500,600,700,800,900,1000,1200,1400,1600,1800,2000
    };


    void LevelManager()
    {

        if (User.meter < level[walkLevel])
        {
            return;
        }
        else if (User.meter > level[walkLevel])
        {
            walkLevel += 1;
            if(!User.isTourHard)
                User.tourSpeed += 2.5f;
            else
                User.tourSpeed += 4f;
        }
    }

    private float speakTime, fadeTime;
    private bool isSpeak;
    private string[] speaks =
    {
        "나는 애정을 받을 엄청난 욕구와 그것을 베풀 엄청난 욕구를 타고났다. \r\n-오드리햅번",
        "우리는 우리가 어른이 되는 것에서 도망치고 있다고 생각했다. 허나 이제 우리가 어른이 되어버렸다. \r\n-마가릿 애트우드",
        "우리가 모든 아이들을 최고의 방법으로 교육시키고, 모든 도심 지역을 청소하기 전에는 할일이 결코 부족하지는 않을 것이다. \r\n-빌게이츠",
        "우리는 모두 행복한 삶을 살고 싶어 한다. 사는 모습은 달라도 행복해지기를 원하는 것은 누구나 마찬가지다. \r\n-안네프랑크(안네의일기)",
        "여행은 사람을 순수하게, 그러나 강하게 만든다.\r\n- 서양 격언 中 -",
        "진정 무엇인가를 발견하는 여행은 새로운 풍경을 바라보는 것이 아니라 새로운 눈을 가지는 데 있다.\r\n- Marcel Proust-",
        "여행할 목적지가 있다는 것은 좋은 일이다. 그러나, 중요한 것은 여행 그 자체다. \r\n- 어슐러 K.르귄 -",
        "인생이란 여행에서 주인공은 바로 당신이다. \r\n- Lifee 제작자",
    };
    int randomspeaknum;
    void ShiningSpeak()
    {
        if (isSpeak)
        {
            GameObject.Find("PanelSpeak").transform.GetChild(0).gameObject.SetActive(true);
            
            speakTime += Time.deltaTime;
            if(speakTime<0.1f)
            {
                randomspeaknum = Random.Range(0, speaks.Length);
                GameObject.Find("PanelSpeak").transform.GetChild(0).GetComponent<Text>().text = speaks[randomspeaknum];
            }
            if (speakTime < 1)
            {
                GameObject.Find("PanelSpeak").transform.GetChild(0).GetComponent<Text>().color = new Color(0, 0, 0, speakTime);
            }
            else if (speakTime > 9 && speakTime < 10)
            {
                fadeTime += Time.deltaTime;
                GameObject.Find("PanelSpeak").transform.GetChild(0).GetComponent<Text>().color = new Color(0, 0, 0, 1 - fadeTime);

            }
            else if (speakTime > 10)
            {
                isSpeak = false;
                speakTime = 0;
                fadeTime = 0;
                GameObject.Find("PanelSpeak").transform.GetChild(0).gameObject.SetActive(false);
                return;
            }
            else
            {
                GameObject.Find("PanelSpeak").transform.GetChild(0).GetComponent<Text>().color = new Color(0, 0, 0, 1);

            }
        }
        else
            return;
    }


    public void HeartManager(float heart)
    {
        if (!GameObject.Find("SoundOfPop").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
            GameObject.Find("SoundOfPop").GetComponent<AudioSource>().Play();

        if (User.isTourHard)
            heart *= 2;

        GameObject heartui = Instantiate(heartUI) as GameObject;
        heartui.transform.GetChild(1).GetComponent<Text>().text = User.ChangeUnit(heart + (heart*User.memberlevel[3]*0.1f));
        heartui.transform.SetParent(GameObject.Find("CanvasOverlay").transform);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        screenPos.y += Screen.height * 0.25f;
        heartui.transform.position = screenPos;
        heartui.transform.localScale = new Vector3(1, 1, 1);
        User.heart += heart + (heart * User.memberlevel[3] * 0.1f);
        totalHeart += heart + (heart * User.memberlevel[3] * 0.1f);
    }


}
