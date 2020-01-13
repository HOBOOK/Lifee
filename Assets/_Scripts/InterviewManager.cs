using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.EventSystems;

public class InterviewManager : MonoBehaviour
{
    public GameObject result;
    public GameObject clickedobj;
    public GameObject popupImage;
    public GameObject popupMessage;

    private bool isOver;
    private bool isStart;
    private int startTime;
    public GameObject effect;
    public GameObject effect2;
    public GameObject touchMoney;
    private float resultMoney;
    private float resultParticle;
    private float anitime;
    private float point;
    private float tpoint;
    private float touchCount;

    private bool vibrate;
    private float vibrateTime;

    private float popupTime;

    //총타임
    private float alltime;
    //버프
    private float buff1, buff2, buff3;
    private bool isbuff1, isbuff2, isbuff3;

    //스킬
    private float statbonus; //경제력 보너스
    private float touchBonus; //터치보너스
    private bool isFatigue;
    private float fireTime;
    private float frameTime;
    private float frameTime2;

    //스킬변수
    private float bonusCriticalDamage; //3-2스킬, 크리티컬
    private bool isCritical;
    private float buff3delay;
    private float skill31time;
    //탭포인트변수
    private float bonuspoint;
    private float autoTime;
    //스테이지
    private float[] stage =
    {
       1000,5000,15000,50000,100000,300000,700000,1200000,2300000,5000000,18000000,50000000
    };
    private float totalExp;
    private List<Exp> tblExp;

    //적관리
    private bool isTime;
    private float temptime;
    private string[] enemysName =
    {
        "정리안된서류", "고장난프린터","설탕과다커피","가득찬쓰레기통","고장난컴퓨터",
        "끼인파쇄기", "끊어진인터넷", "고객의불만", "야근" ,"해고" ,"팀장<color='red'>(boss)</color>","구조조정"
    };


    //멤버관리
    private float workmantime;

    void Start()
    {
        User.LoadData();
        MyFurnitrue.LoadData();
        User.buff = 0;
        User.isAuto = false;
        User.status = 6;
        
        if(User.isJobHard)
        {
            for (int i = 0; i < stage.Length; i++)
            {
                stage[i] = (stage[i] + 100000) * User.jobLevel;
            }
            for (int i = 0; i < enemysName.Length; i++)
            {
                enemysName[i] = "<color='yellow'>" + enemysName[i] + " ★"+User.jobLevel+"</color>";
            }
        }

        //SetMemberHp();
        anitime = 0;
        tblExp = MyItems.LoadIExpXML();
        isOver = false;
        resultMoney = 0;
        vibrate = false;
        bonuspoint = 1;
        autoTime = 0;
        InitialEnemy();


        buff1 = 0;
        buff2 = 0;
        buff3 = 0;
        setSkill();
        SelectSkill();


        User.isPause = true;
    }


    public float GetPoint(float bonus)
    {
        
        if ((User.lucky+User.charm)*0.001f  > Random.Range(0.0f, 1.1f))
        {
            isCritical = true;
        }
        else
            isCritical = false;
        float r = Random.Range(0.7f, 1.2f);
        point =  (User.JOB_POWER+ statbonus)  * bonus * r;
        if (User.buff == 2)//광고버프
            point *= 1.5f;
        if (isCritical)//크리티컬
            point *= (2.0f*(1+bonusCriticalDamage));
        point += touchBonus;//터치보너스
        return point;
    }

    public void BuffStack()
    {
        if (buff1 > 100)
            buff1 = 100;
        else if(!isbuff1)
        {
            for(int i = 0; i< 3; i++)
            {
                if (User.selectedSkill[0,i])
                    buff1 += 2.5f;
            }
        }
            
        if (buff2 > 100)
            buff2 = 100;
        else if(!isbuff2)
        {
            for (int i = 3; i < 6; i++)
            {
                if (User.selectedSkill[1,i-3])
                    buff2 += 1.85f;
            }
        }
            
        if (buff3 > 100)
            buff3 = 100;
        else if(!isbuff3)
        {
            for (int i = 6; i < 9; i++)
            {
                if (User.selectedSkill[2,i-6])
                    buff3 += 3.2f;
            }
        }
            
    }

    string[] ranceosay =
    {
        "아직 할 일이 많아요!", "먼저 퇴근하세요. 저는 할 일이 남아서..", "아직.. 인가요?", "이거 이렇게 하시면 안되는데..", User.myname+"씨 이것도 해줄 수 있죠?","자자 얼마 안남았어요!","이게 최선인거죠?"
    };
    void MemberManage()
    {
        if (!isStart||isOver)
            return;
        int lv = 0;
        if (User.isJobHard)
            lv = User.jobTempLevel + User.jobLevel+1;
        else
            lv = User.jobTempLevel+1;

        workmantime += (lv+10)*0.35f*Time.deltaTime;
       
        if (workmantime >= 100)
        {
            PopupMessage(GameObject.Find("Ceo"), ranceosay[Random.Range(0,ranceosay.Length)]);
            if (resultMoney - stage[User.jobTempLevel] * 0.05f< 0)
                resultMoney = 0;
            else
                resultMoney -= stage[User.jobTempLevel] * 0.05f;
            workmantime = 0;
        }
        GameObject.Find("CanvasInterview/MemberManager").transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = workmantime / 100;

        
        GameObject.Find("CanvasInterview/MemberManager").transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "레벨 "+lv+" 팀장님";
        Vector3 screenPos3 = Camera.main.WorldToScreenPoint(GameObject.Find("Ceo").transform.position);
        screenPos3.y += Screen.height * 0.25f;
        GameObject.Find("CanvasInterview/MemberManager").transform.GetChild(0).transform.GetComponent<RectTransform>().position = new Vector3(screenPos3.x, screenPos3.y, screenPos3.z);
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

    
    private void AutoClick()
    {
        if (!User.isAuto || isOver ||!isStart)
            return;

        autoTime += Time.deltaTime;
        if (autoTime > 0.15f)
        {
            Debug.Log("클릭중...");
            if (!vibrate)
                vibrate = true;
            User.sleep -= 0.5f;
            GameObject.Find("CanvasInterview/enemys").transform.GetChild(User.jobTempLevel).GetComponent<Image>().sprite = Resources.Load<Sprite>("JobEnemy/jobenemydam" + User.jobTempLevel);
            GameObject.Find("CanvasInterview/enemys").transform.GetChild(User.jobTempLevel).GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);

            touchCount++;

            if(!GameObject.Find("SoundOfHeart").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
                GameObject.Find("SoundOfHeart").GetComponent<AudioSource>().Play();
            float r = Random.Range(0, 1000);

            BuffStack();

            if (r < User.lucky * 0.02f + (User.jobTempLevel * 3))
            {
                GameObject CreateTouchMoney2 = Instantiate(touchMoney);
                CreateTouchMoney2.transform.SetParent(GameObject.Find("CanvasInterview").transform, false);
                CreateTouchMoney2.GetComponent<Text>().text = "+" + 1;
                CreateTouchMoney2.GetComponent<Text>().fontSize = 40;
                CreateTouchMoney2.GetComponent<Text>().color = new Color(0, 0.4f, 1, 1);
                CreateTouchMoney2.GetComponent<Outline>().effectColor = new Color(0, 0.4f, 1, 0.5f);
                CreateTouchMoney2.transform.GetChild(1).gameObject.SetActive(true);
                CreateTouchMoney2.transform.GetChild(0).gameObject.SetActive(false);

                CreateTouchMoney2.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
                User.particle += 1;
                resultParticle += 1;
            }
            tpoint = GetPoint(bonuspoint);

            GameObject CreateTouchMoney = Instantiate(touchMoney);
            CreateTouchMoney.transform.SetParent(GameObject.Find("CanvasInterview").transform, false);
            CreateTouchMoney.GetComponent<Text>().text = "+" + tpoint.ToString("N0");
            if (isCritical)
                CreateTouchMoney.transform.GetChild(0).gameObject.SetActive(true);
            else
                CreateTouchMoney.transform.GetChild(0).gameObject.SetActive(false);
            CreateTouchMoney.transform.GetChild(1).gameObject.SetActive(false);
            CreateTouchMoney.transform.position =  new Vector2(Screen.width/2, Screen.height/2);


            resultMoney += tpoint;
            autoTime = 0;
        }
        
    }

    bool isAwake;
    void Update()
    {
        if (User.isPause)
        {
            if(!isAwake)
                SelectSkill();
            Time.timeScale = 0;
            return;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (isOver)
            return;
        if(!isAwake)
            isAwake = true;
        PopupManager();
        ChangeGoal();
        StageManager();
        AutoClick();
        MemberManage();

        if(isTime)
        {
            temptime += Time.deltaTime;
            if (temptime > 3.0f)
            {
                isTime = false;
                GameObject.Find("CanvasInterview/enemys").transform.GetChild(User.jobTempLevel).gameObject.SetActive(true);
                temptime = 0;
            }
            return;
        }

        if(!isStart&& GameObject.Find("CanvasInterview/countdown").transform.childCount>0)
        {
            startTime += 1;
            if (startTime > 0)
                GameObject.Find("CanvasInterview/countdown").transform.GetChild(0).gameObject.SetActive(true);
            else if(startTime>10)
            {
                GameObject.Find("CanvasInterview/countdown").transform.GetChild(1).gameObject.SetActive(true);
            }
            else if(startTime>20)
            {
                GameObject.Find("CanvasInterview/countdown").transform.GetChild(2).gameObject.SetActive(true);
                
            }
            else if(startTime>30)
            {
                GameObject.Find("CanvasInterview/countdown").transform.GetChild(3).gameObject.SetActive(true);
            }
            return;
        }
        else
            isStart = true;
        alltime += Time.deltaTime*0.4f;
        if(!isFatigue)
            User.sleep += alltime * Time.deltaTime;
        
        buffManage();

        if (vibrate)
        {
            vibrateTime += 100*Time.deltaTime;
            
            if (vibrateTime > 30)
            {
                vibrateTime = 0;
                vibrate = false;
                GameObject.Find("enemys").transform.localPosition = new Vector3(0, 0, 0);
                return;
            }
            if (Input.mousePosition.x > (Screen.width / 2))
            {
                if (Input.mousePosition.y > (Screen.height / 2))
                {
                    GameObject.Find("enemys").transform.localPosition = new Vector3(-30f + vibrateTime,-30 + vibrateTime, 0);
                }
                else
                {
                    GameObject.Find("enemys").transform.localPosition = new Vector3(-30f + vibrateTime, 30-vibrateTime, 0);
                }
                
            }
            else
            {
                if (Input.mousePosition.y > (Screen.height / 2))
                {
                    GameObject.Find("enemys").transform.localPosition = new Vector3(30f - vibrateTime, -30 + vibrateTime, 0);
                }
                else
                {
                    GameObject.Find("enemys").transform.localPosition = new Vector3(30f - vibrateTime, 30 - vibrateTime, 0);
                }

            }
            
        }


        if (!isOver)
        {
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId))
                    {
                        if (Input.GetTouch(i).phase == TouchPhase.Began)
                        {
                            if (!vibrate)
                                vibrate = true;
                            User.sleep -= 0.5f;
                            GameObject.Find("CanvasInterview/enemys").transform.GetChild(User.jobTempLevel).GetComponent<Image>().sprite = Resources.Load<Sprite>("JobEnemy/jobenemydam" + User.jobTempLevel);
                            GameObject.Find("CanvasInterview/enemys").transform.GetChild(User.jobTempLevel).GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);

                            touchCount++;
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            float r = Random.Range(0, 1000);
                            BuffStack();
                            if (r < User.lucky*0.02f + (User.jobTempLevel*3))
                            {
                                Vector3 pos2 = ray.GetPoint(20);
                                GameObject CreateEffect2 = Instantiate(effect2);
                                CreateEffect2.transform.position = pos2;
                                CreateEffect2.GetComponent<ParticleSystem>().Play();

                                GameObject CreateTouchMoney2 = Instantiate(touchMoney);
                                CreateTouchMoney2.transform.SetParent(GameObject.Find("CanvasInterview").transform, false);
                                CreateTouchMoney2.GetComponent<Text>().text = "+" + 1;
                                CreateTouchMoney2.GetComponent<Text>().fontSize = 40;
                                CreateTouchMoney2.GetComponent<Text>().color = new Color(0, 0.4f, 1, 1);
                                CreateTouchMoney2.GetComponent<Outline>().effectColor = new Color(0, 0.4f, 1, 0.5f);
                                CreateTouchMoney2.transform.GetChild(1).gameObject.SetActive(true);
                                CreateTouchMoney2.transform.GetChild(0).gameObject.SetActive(false);
                                
                                CreateTouchMoney2.transform.position = Input.GetTouch(i).position + new Vector2(20, Screen.height * 0.2f+20);
                                User.particle += 1;
                                resultParticle += 1;
                            }
                            tpoint = GetPoint(bonuspoint);
                            Vector3 pos = ray.GetPoint(20);
                            GameObject CreateEffect = Instantiate(effect);
                            if (isCritical)
                            {
                                if (!User.isEffectSound)
                                {
                                    //CreateEffect.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Bgm/critical");
                                    CreateEffect.GetComponent<AudioSource>().Play();
                                }
                            }
                            else
                            {
                                if (!User.isEffectSound)
                                {
                                    CreateEffect.GetComponent<AudioSource>().Play();
                                }
                            }
                            CreateEffect.transform.position = pos;
                            CreateEffect.GetComponent<ParticleSystem>().Play();

                            GameObject CreateTouchMoney = Instantiate(touchMoney);
                            CreateTouchMoney.transform.SetParent(GameObject.Find("CanvasInterview").transform, false);
                            CreateTouchMoney.GetComponent<Text>().text = "+" + tpoint.ToString("N0");
                            if(isCritical)
                                CreateTouchMoney.transform.GetChild(0).gameObject.SetActive(true);
                            else
                                CreateTouchMoney.transform.GetChild(0).gameObject.SetActive(false);
                            CreateTouchMoney.transform.GetChild(1).gameObject.SetActive(false);
                            CreateTouchMoney.transform.position = Input.GetTouch(i).position + new Vector2(0, Screen.height * 0.2f);

                            resultMoney += tpoint;
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
                    if (!vibrate)
                        vibrate = true;
                    User.sleep -= 0.5f;
                    GameObject.Find("CanvasInterview/enemys").transform.GetChild(User.jobTempLevel).GetComponent<Image>().sprite = Resources.Load<Sprite>("JobEnemy/jobenemydam" + User.jobTempLevel);
                    GameObject.Find("CanvasInterview/enemys").transform.GetChild(User.jobTempLevel).GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    float r = Random.Range(0, 1000);

                    BuffStack();
                    touchCount++;
                    if (r < User.lucky * 0.02f + (User.jobTempLevel * 3))
                    {
                        Vector3 pos2 = ray.GetPoint(20);
                        GameObject CreateEffect2 = Instantiate(effect2);
                        CreateEffect2.transform.position = pos2;
                        CreateEffect2.GetComponent<ParticleSystem>().Play();

                        GameObject CreateTouchMoney2 = Instantiate(touchMoney);
                        CreateTouchMoney2.transform.SetParent(GameObject.Find("CanvasInterview").transform, false);
                        CreateTouchMoney2.GetComponent<Text>().text = "+" + 1;
                        CreateTouchMoney2.GetComponent<Text>().fontSize = 40;
                        CreateTouchMoney2.GetComponent<Text>().color = new Color(0, 0.4f, 1, 1);
                        CreateTouchMoney2.GetComponent<Outline>().effectColor = new Color(0, 0.4f, 1, 0.5f);
                        CreateTouchMoney2.transform.GetChild(1).gameObject.SetActive(true);
                        CreateTouchMoney2.transform.GetChild(0).gameObject.SetActive(false);
                        CreateTouchMoney2.transform.position = Input.mousePosition+new Vector3(20, Screen.height * 0.2f+20, 0);
                        User.particle += 1;
                        resultParticle += 1;
                    }
                    tpoint = GetPoint(bonuspoint);
                    Vector3 pos = ray.GetPoint(20);
                    GameObject CreateEffect = Instantiate(effect);
                    if (isCritical)
                    {
                        if (!User.isEffectSound)
                        {
                            //CreateEffect.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Bgm/critical");
                            CreateEffect.GetComponent<AudioSource>().Play();
                        }
                    }
                    else
                    {
                        if (!User.isEffectSound)
                        {
                            CreateEffect.GetComponent<AudioSource>().Play();
                        }
                    }
                    CreateEffect.transform.position = pos;
                    CreateEffect.GetComponent<ParticleSystem>().Play();

                    GameObject CreateTouchMoney = Instantiate(touchMoney);
                    CreateTouchMoney.transform.SetParent(GameObject.Find("CanvasInterview").transform, false);
                    CreateTouchMoney.GetComponent<Text>().text = "+" + tpoint.ToString("N0");

                    if (isCritical)
                        CreateTouchMoney.transform.GetChild(0).gameObject.SetActive(true);
                    else
                        CreateTouchMoney.transform.GetChild(0).gameObject.SetActive(false);
                    CreateTouchMoney.transform.GetChild(1).gameObject.SetActive(false);
                    CreateTouchMoney.transform.position = Input.mousePosition + new Vector3(0,Screen.height*0.2f,0);
                    resultMoney += tpoint;
                }
            }
        }
        //결과
        if (User.sleep >= 100)
        {
            if (resultMoney == 0)
                totalExp = 0;
            anitime += Time.deltaTime;
            if (anitime > 2.0)
            {
                User.jobExp += totalExp;
                if (User.isJobHard)
                {
                    User.money += User.memberlevel[1] * 5000 + ((User.jobTempLevel * User.jobTempLevel * 1000) * (((User.skillLevel[7] + 1) * 0.1f) + 1)) * User.jobLevel + (resultMoney * 0.05f) + (User.itemLevel[1] * 100);
                    result.transform.GetChild(1).GetComponent<Text>().text = "+ " + (User.memberlevel[1] * 5000 + ((User.jobTempLevel * User.jobTempLevel * 1000) * (((User.skillLevel[7] + 1) * 0.1f) + 1)) * User.jobLevel + (resultMoney * 0.05f) + (User.itemLevel[1] * 100)).ToString("N0") + "원";

                }
                else
                {
                    User.money += User.memberlevel[1] * 5000 + ((User.jobTempLevel + 1) * 2500 + resultMoney * 0.1f) + (User.itemLevel[1] * 100);
                    result.transform.GetChild(1).GetComponent<Text>().text = "+ " + (User.memberlevel[1] * 5000 + ((User.jobTempLevel + 1) * 2500 + resultMoney * 0.1f) + (User.itemLevel[1] * 100)).ToString("N0") + "원";
                }
                JobLevelUp();
                result.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = User.jobLevel.ToString(); //레벨
                result.transform.GetChild(6).GetComponent<Slider>().value = User.jobExp/ tblExp[User.jobLevel - 1].needExp;
                if (User.isJobHard)
                    result.transform.GetChild(7).GetComponent<Text>().text = "★ " + User.jobLevel;
                else
                    result.transform.GetChild(7).GetComponent<Text>().text = "";

                User.sleep = 100;
                User.day = true;
                User.isReport = false;
                User.buff = 0;
                User.SaveDate();
                User.isPlay = true;
                isOver = true;
                result.transform.GetChild(5).gameObject.SetActive(true);
                return;

            }
            else
            {
                User.isAuto = false;
                totalExp = ((User.jobTempLevel * User.jobTempLevel) + (resultMoney + 100000) * 0.0001f) + ((User.jobTempLevel * User.jobTempLevel) + (resultMoney + 100000) * 0.0001f) * User.passiveLevel[0] * 0.1f;

                result.transform.GetChild(6).GetChild(2).GetComponent<Text>().text = "+ " + totalExp.ToString("N0");
                result.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = User.jobLevel.ToString(); //레벨
                if (User.isJobHard)
                {
                    result.transform.GetChild(1).GetComponent<Text>().text = "+ " + ((User.memberlevel[1] * 5000 + ((User.jobTempLevel * User.jobTempLevel * 1000) * (((User.skillLevel[7] + 1) * 0.1f) + 1)) * User.jobLevel + (resultMoney * 0.05f) + (User.itemLevel[1] * 100))*0.5f*anitime).ToString("N0") + "원";

                }
                else
                {
                    result.transform.GetChild(1).GetComponent<Text>().text = "+ " + ((User.memberlevel[1] * 5000 + ((User.jobTempLevel + 1) * 2500 + resultMoney * 0.1f) + (User.itemLevel[1] * 100))*anitime*0.5f).ToString("N0") + "원";
                }
                result.transform.GetChild(6).GetComponent<Slider>().value = (User.jobExp + (totalExp * 0.5f * anitime)) / tblExp[User.jobLevel - 1].needExp;

            }
            

        }

        if (User.sleep >= 100)
        {
            User.status = 0;
            result.SetActive(true);
            if (touchCount>300)
            {
                result.transform.GetChild(3).GetComponent<Text>().text = "슈퍼~그뤠잇!! 드디어 성공하셨군요";
                User.today_good = 3;
            }
            else if (touchCount > 200)
            {
                result.transform.GetChild(3).GetComponent<Text>().text = "대단하세요~! 노력은 배신하지 않죠";
                User.today_good = 2;
            }
            else if (touchCount > 50)
            {
                result.transform.GetChild(3).GetComponent<Text>().text = "괜찮은 성과에요!!";
                User.today_good = 1;
            }
            else
            {
                result.transform.GetChild(3).GetComponent<Text>().text = "항상 좋을 수는 없어요! 쉬어가면서 하세요";
                User.today_good = 4;
            }

            
            result.transform.GetChild(4).GetChild(0).GetComponentInChildren<Text>().text = "+" + resultParticle.ToString("N0");
            
            //잠


            if (GameObject.Find("CanvasInterview/Balloon") != null)
                GameObject.Find("CanvasInterview/Balloon").gameObject.SetActive(false);
            
        }
    }

    public void buffManage()
    {
        GameObject.Find("CanvasInterview/PanelBuff/buff1").transform.GetChild(0).GetComponent<Image>().fillAmount = buff1 / 100;
        GameObject.Find("CanvasInterview/PanelBuff/buff2").transform.GetChild(0).GetComponent<Image>().fillAmount = buff2 / 100;
        GameObject.Find("CanvasInterview/PanelBuff/buff3").transform.GetChild(0).GetComponent<Image>().fillAmount = buff3 / 100;

        if (buff1 >= 100)
            isbuff1 = true;
        if (buff2 >= 100)
            isbuff2 = true;
        if (buff3 >= 100)
            isbuff3 = true;

        if (isbuff1)
        {
            bonuspoint = 1f;
            buff1 -= 20 * Time.deltaTime;
            int skillnum=0;
            for(int i=0; i<3; i++)
            {
                if (User.selectedSkill[0,i])
                {
                    //이펙트
                    if (GameObject.Find("BuffEffect").transform.GetChild(i) != null)
                        GameObject.Find("BuffEffect").transform.GetChild(i).gameObject.SetActive(true);
                    if (!GameObject.Find("BuffEffect").transform.GetChild(i).GetComponent<ParticleSystem>().IsAlive())
                        GameObject.Find("BuffEffect").transform.GetChild(i).GetComponent<ParticleSystem>().Play();
                    //사운드
                    if (!GameObject.Find("BuffEffect").transform.GetChild(i).GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
                        GameObject.Find("BuffEffect").transform.GetChild(i).GetComponent<AudioSource>().Play();

                    skillnum = i;
                }
                    
            }
            switch(skillnum)
            {
                case 0:
                    User.sleep -= User.skillLevel[0]*Time.deltaTime;
                    break;
                case 1:
                    fireTime += Time.deltaTime;
                    if(fireTime>=0.1f)
                    {
                        GameObject.Find("CanvasInterview/enemys").transform.GetChild(User.jobTempLevel).GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);

                        tpoint = GetPoint(bonuspoint) * 0.1f * User.skillLevel[1];
                        GameObject CreateTouchMoney = Instantiate(touchMoney);
                        CreateTouchMoney.transform.SetParent(GameObject.Find("CanvasInterview").transform, false);
                        CreateTouchMoney.GetComponent<Text>().text = "+" + tpoint.ToString("N0");
                        if (isCritical)
                            CreateTouchMoney.transform.GetChild(0).gameObject.SetActive(true);
                        else
                            CreateTouchMoney.transform.GetChild(0).gameObject.SetActive(false);
                        CreateTouchMoney.transform.GetChild(1).gameObject.SetActive(false);
                        CreateTouchMoney.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        CreateTouchMoney.GetComponent<RectTransform>().rotation = Quaternion.Euler(1, 1, 1);
                        Vector2 pos = new Vector3(0,0);
                        pos.x += Screen.width / 2;
                        pos.y += Screen.height / 2 + (Screen.width * 0.1f);
                        CreateTouchMoney.transform.position = pos;

                        resultMoney += tpoint;
                        fireTime = 0;
                    }
                    break;
                case 2:
                    touchBonus = User.skillLevel[2] * 100;
                    break;
            }

            if (buff1<=0)
            {
                buff1 = 0;
                //이펙트
                GameObject.Find("BuffEffect/buff0" + skillnum).SetActive(false);
                //GameObject.Find("BuffEffect/buff0" + skillnum).GetComponent<ParticleSystem>().Stop();
                switch (skillnum)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        touchBonus = 0;
                        break;
                }
                isbuff1 = false;
            }
        }
        if (isbuff2)
        {
            
            int skillnum = 0;
            for (int i = 0; i < 3; i++)
            {
                if (User.selectedSkill[1,i])
                {
                    //이펙트
                    if (GameObject.Find("BuffEffect").transform.GetChild(i+3) != null)
                        GameObject.Find("BuffEffect").transform.GetChild(i+3).gameObject.SetActive(true);
                    if (!GameObject.Find("BuffEffect").transform.GetChild(i+3).GetComponent<ParticleSystem>().IsAlive())
                        GameObject.Find("BuffEffect").transform.GetChild(i+3).GetComponent<ParticleSystem>().Play();
                    //사운드
                    if (!GameObject.Find("BuffEffect").transform.GetChild(i+3).GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
                        GameObject.Find("BuffEffect").transform.GetChild(i+3).GetComponent<AudioSource>().Play();

                    skillnum = i;
                }
                
            }
            switch (skillnum)
            {
                case 0:
                    buff2 -= (100/(4+User.skillLevel[3])) * Time.deltaTime;
                    isFatigue = true;
                    break;
                case 1:
                    buff2 -= 10 * Time.deltaTime;
                    statbonus = User.JOB_POWER * (1f + (User.skillLevel[4] * 0.1f));
                    break;
                case 2:
                    buff2 -= 20 * Time.deltaTime;
                    skill31time += Time.deltaTime;
                    if (skill31time > 1.0)
                    {
                        GameObject.Find("CanvasInterview/enemys").transform.GetChild(User.jobTempLevel).GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);

                        tpoint = stage[User.jobTempLevel] * User.skillLevel[5] * 0.01f;
                        if (tpoint >= 150000)
                            tpoint = 150000;
                        GameObject CreateTouchMoney = Instantiate(touchMoney);
                        CreateTouchMoney.transform.SetParent(GameObject.Find("CanvasInterview").transform, false);
                        CreateTouchMoney.GetComponent<Text>().text = "+" + tpoint.ToString("N0");
                        if (isCritical)
                            CreateTouchMoney.transform.GetChild(0).gameObject.SetActive(true);
                        else
                            CreateTouchMoney.transform.GetChild(0).gameObject.SetActive(false);
                        CreateTouchMoney.transform.GetChild(1).gameObject.SetActive(false);
                        CreateTouchMoney.GetComponent<Text>().color = new Color(0, 0.2f, 1, 1);
                        CreateTouchMoney.GetComponent<Outline>().effectColor = new Color(0, 0.2f, 1, 0.5f);
                        CreateTouchMoney.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        CreateTouchMoney.GetComponent<RectTransform>().rotation = Quaternion.Euler(1, 1, 1);
                        Vector2 pos = new Vector3(0, 0);
                        pos.x += Screen.width / 2;
                        pos.y += Screen.height / 2 - (Screen.width * 0.1f);
                        CreateTouchMoney.transform.position = pos;
                        resultMoney += tpoint;
                        skill31time = 0;
                    }
                    
                    break;
            }
            
            if (buff2 <= 0)
            {
                buff2 = 0;
                //이펙트
                GameObject.Find("BuffEffect/buff1" + skillnum).SetActive(false);
                //GameObject.Find("BuffEffect/buff1" + skillnum).GetComponent<ParticleSystem>().Stop();

                switch (skillnum)
                {
                    case 0:
                        isFatigue = false;
                        break;
                    case 1:
                        statbonus = 0;
                        break;
                    case 2:
                        break;
                }
                isbuff2 = false;
            }
        }
        if (isbuff3)
        {
            
            int skillnum = 0;
            for (int i = 0; i < 3; i++)
            {
                if (User.selectedSkill[2,i])
                {
                    //이펙트
                    if (GameObject.Find("BuffEffect").transform.GetChild(i+6) != null)
                        GameObject.Find("BuffEffect").transform.GetChild(i+6).gameObject.SetActive(true);
                    if (!GameObject.Find("BuffEffect").transform.GetChild(i+6).GetComponent<ParticleSystem>().IsAlive())
                        GameObject.Find("BuffEffect").transform.GetChild(i+6).GetComponent<ParticleSystem>().Play();
                    //사운드
                    if (!GameObject.Find("BuffEffect").transform.GetChild(i+6).GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
                        GameObject.Find("BuffEffect").transform.GetChild(i+6).GetComponent<AudioSource>().Play();
                    skillnum = i;
                }
            }
            switch (skillnum)
            {
                case 0:
                    frameTime += Time.deltaTime;
                    frameTime2 += Time.deltaTime;
                    if (frameTime >= 1f)
                    {
                        GameObject.Find("CanvasInterview/enemys").transform.GetChild(User.jobTempLevel).GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);

                        tpoint = GetPoint(bonuspoint) * User.skillLevel[6];
                        GameObject CreateTouchMoney = Instantiate(touchMoney);
                        CreateTouchMoney.transform.SetParent(GameObject.Find("CanvasInterview").transform, false);
                        CreateTouchMoney.GetComponent<Text>().text = "+" + tpoint.ToString("N0");
                        if (isCritical)
                            CreateTouchMoney.transform.GetChild(0).gameObject.SetActive(true);
                        else
                            CreateTouchMoney.transform.GetChild(0).gameObject.SetActive(false);
                        CreateTouchMoney.transform.GetChild(1).gameObject.SetActive(false);
                        CreateTouchMoney.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        CreateTouchMoney.GetComponent<RectTransform>().rotation = Quaternion.Euler(1, 1, 1);
                        Vector2 pos = new Vector3(0, 0);
                        pos.x += Screen.width / 1.5f;
                        pos.y += Screen.height / 2;
                        CreateTouchMoney.transform.position = pos;

                        resultMoney += tpoint;
                        frameTime = 0;
                    }
                    if (frameTime2 >= 1f)
                    {
                        GameObject.Find("CanvasInterview/enemys").transform.GetChild(User.jobTempLevel).GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);

                        tpoint = GetPoint(bonuspoint) * User.skillLevel[6];
                        GameObject CreateTouchMoney = Instantiate(touchMoney);
                        CreateTouchMoney.transform.SetParent(GameObject.Find("CanvasInterview").transform, false);
                        CreateTouchMoney.GetComponent<Text>().text = "+" + tpoint.ToString("N0");
                        if (isCritical)
                            CreateTouchMoney.transform.GetChild(0).gameObject.SetActive(true);
                        else
                            CreateTouchMoney.transform.GetChild(0).gameObject.SetActive(false);
                        CreateTouchMoney.transform.GetChild(1).gameObject.SetActive(false);
                        CreateTouchMoney.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        CreateTouchMoney.GetComponent<RectTransform>().rotation = Quaternion.Euler(1, 1, 1);
                        Vector2 pos = new Vector3(0, 0);
                        pos.x += Screen.width / 2.5f;
                        pos.y += Screen.height / 2;
                        CreateTouchMoney.transform.position = pos;

                        resultMoney += tpoint;
                        frameTime2 = 0;
                    }
                    break;
                case 1:
                    bonusCriticalDamage = User.skillLevel[7] * 0.2f;
                    User.money += User.skillLevel[7]*Time.deltaTime*10;
                    buff3 -= 10*Time.deltaTime;
                    break;
                case 2:
                    if(buff3delay==0)
                    {
                        if (!GameObject.Find("BuffEffect/buff22").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
                            GameObject.Find("BuffEffect/buff22").GetComponent<AudioSource>().Play();

                        GameObject.Find("CanvasInterview/enemys").transform.GetChild(User.jobTempLevel).GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);

                        tpoint = GetPoint(bonuspoint) * User.skillLevel[8] * 10;
                        GameObject CreateTouchMoney = Instantiate(touchMoney);
                        CreateTouchMoney.transform.SetParent(GameObject.Find("CanvasInterview").transform, false);
                        CreateTouchMoney.GetComponent<Text>().text = "+" + tpoint.ToString("N0");
                        if (isCritical)
                            CreateTouchMoney.transform.GetChild(0).gameObject.SetActive(true);
                        else
                            CreateTouchMoney.transform.GetChild(0).gameObject.SetActive(false);
                        CreateTouchMoney.transform.GetChild(1).gameObject.SetActive(false);
                        CreateTouchMoney.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        CreateTouchMoney.GetComponent<RectTransform>().rotation = Quaternion.Euler(1, 1, 1);
                        Vector2 pos = new Vector3(0, 0);
                        pos.x += Screen.width / 2;
                        pos.y += Screen.height / 2 + (Screen.width * 0.1f);
                        CreateTouchMoney.transform.position = pos;
                        resultMoney += tpoint;
                    }
                    buff3delay += Time.deltaTime;
                    if(buff3delay>1)
                    {
                        buff3 = 0;
                        buff3delay = 0;
                    }
                    
                    break;
            }

            if (buff3 <= 0)
            {
                buff3 = 0;
                //이펙트
                GameObject.Find("BuffEffect/buff2" + skillnum).SetActive(false);
                //GameObject.Find("BuffEffect/buff2" + skillnum).GetComponent<ParticleSystem>().Stop();

                switch (skillnum)
                {
                    case 0:

                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                }
                isbuff3 = false;
            }
        }
        if (!isbuff1&&!isbuff2&&!isbuff3)
        {
            for(int i = 3; i < GameObject.Find("BuffEffect").transform.childCount-1; i++)
            GameObject.Find("BuffEffect").transform.GetChild(i).GetComponent<ParticleSystem>().Stop();
            bonuspoint = 1;
        }
    }

    


    private string[] ListofJobSkillDesc = new string[9];

    //스킬초기화
    public void setSkill()
    {
        for (int i = 0; i < 3; i++)
        {
            for(int k = 0; k < 3; k++)
            {
                User.selectedSkill[i,k] = false;
            }
            
        }
        for (int i = 0; i < 3; i++)
        {
            for(int k = 0; k <3; k++)
            {
                if (User.skillLevel[i] > 0)
                {
                    User.selectedSkill[i,k] = true;
                    break;
                }
            }
        }
        
    }

    //스킬정보
    void SkillDescUpdate()
    {
        ListofJobSkillDesc[0] = "즉시 피곤함이 <color='yellow'>" + ((User.skillLevel[0] * 5) + 5) + "</color> 감소해요.";
        ListofJobSkillDesc[1] = "열정으로 5초간 초당 <color='yellow'>" + (User.JOB_POWER * User.skillLevel[1]).ToString("N0") + "pts</color> 의 성과를 얻어요.";
        ListofJobSkillDesc[2] = "5초간 탭당 <color='yellow'>" + (User.skillLevel[2] * 100) + "pts</color>를 추가 획득해요.";
        ListofJobSkillDesc[3] = "<color='yellow'>" + (4 + User.skillLevel[3]) + "초</color>간 시간이 정지되요.";
        ListofJobSkillDesc[4] = "10초간 경제력을 <color='yellow'>" + (User.skillLevel[4] * 10) + "%</color> 증가시켜요";
        ListofJobSkillDesc[5] = "5초간 초당 최대체력의 <color='yellow'>" + (User.skillLevel[5]) + "%</color>의 pts를 획득.";
        ListofJobSkillDesc[6] = "발동시 끝날때까지 초당 <color='yellow'>" + User.JOB_POWER * User.skillLevel[6] * 2 + "pts</color>  획득.";
        ListofJobSkillDesc[7] = "10초간 크리티컬 성과가 <color='yellow'>" + (User.skillLevel[7] * 20) + "%</color> 증가해요.";
        ListofJobSkillDesc[8] = "즉시 <color='yellow'>" + (User.JOB_POWER * User.skillLevel[8] * 10).ToString("N0") + "pts</color>를 획득해요.";
    }
    //스킬활성화
    public void SelectSkill()
    {
        int r = 0, c = 0;
        bool[] isExist = { false, false, false };
        for (int i = 0; i < 9; i++)
        {
            
            if (GameObject.Find("CanvasInterview/PanelStart/skill") != null)
            {
                if (User.skillLevel[i] == 0)
                {
                    GameObject.Find("CanvasInterview/PanelStart/skill").transform.GetChild(i).GetChild(2).gameObject.SetActive(true);
                }
                else
                {
                    GameObject.Find("CanvasInterview/PanelStart/skill").transform.GetChild(i).GetChild(2).gameObject.SetActive(false);
                }

                
                if(i>5)
                {
                    r = 2;
                    c = i - 6;
                }
                else if(i>2)
                {
                    r = 1;
                    c = i - 3;
                }
                else
                {
                    r = 0;
                    c = i;
                }

                if (User.selectedSkill[r,c])
                {
                    isExist[r] = true;
                    GameObject.Find("CanvasInterview/PanelBuff").transform.GetChild(r).gameObject.SetActive(true);
                    GameObject.Find("CanvasInterview/PanelStart/skill").transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
                    SkillDescUpdate();
                    GameObject.Find("CanvasInterview/PanelStart/skill").transform.GetChild(9+r).GetChild(0).GetComponent<Text>().text = ListofJobSkillDesc[i];
                    GameObject.Find("CanvasInterview/PanelBuff").transform.GetChild(r).GetChild(0).GetComponent<Image>().sprite = GameObject.Find("CanvasInterview/PanelStart/skill").transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite;
                }
                else
                {
                    GameObject.Find("CanvasInterview/PanelStart/skill").transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
                }
            }
            if (!isExist[r])
            {
                GameObject.Find("CanvasInterview/PanelBuff").transform.GetChild(r).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("key");
                GameObject.Find("CanvasInterview/PanelBuff").transform.GetChild(r).gameObject.SetActive(false);
            }
            
        }
    }

    bool isHeal;
    int healCount;
    public void PopupManager()
    {
        if (isOver||!isStart)
            return;

        
        if(isHeal)
        {
            popupTime += Time.deltaTime;
            if(popupTime>10.0f)
            {
                isHeal = false;
                popupTime = 0;
            }
        }
        if (!isHeal&&healCount<6)
        {
            if(resultMoney/stage[User.jobTempLevel]>0.7)
            {
                for(int i=0; i<Random.Range(1,5); i++)
                {
                    PopupImage(Resources.Load<Sprite>("coffee"));
                }
                healCount++;
                isHeal = true;    
            }
            else if(touchCount>150)
            {
                for (int i = 0; i < Random.Range(1, 7); i++)
                {
                    PopupImage(Resources.Load<Sprite>("coffee"));
                }
                healCount++;
                isHeal = true;
            }
        }
    }

    void PopupImage(Sprite img)
    {
        GameObject popup = Instantiate(popupImage) as GameObject;
        popup.transform.SetParent(GameObject.Find("CanvasInterview").transform);
        popup.transform.localScale = new Vector3(1, 1, 1);
        int x = Random.Range(0, Screen.width);
        int y = Random.Range(0, Screen.height);
        popup.transform.position = new Vector2(x, y);
        popup.GetComponent<Image>().sprite = img;
    }

    void PopupMessage(GameObject target, string msg)
    {
        GameObject popup = Instantiate(popupMessage) as GameObject;
        popup.transform.SetParent(GameObject.Find("CanvasInterview").transform);
        popup.transform.localScale = new Vector3(1, 1, 1);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
        popup.transform.position = new Vector3(screenPos.x, screenPos.y + Screen.height * 0.25f, screenPos.z);

        popup.transform.GetChild(0).GetComponent<Text>().text = msg;
    }

    public void StageManager()
    {
        GameObject.Find("Score").transform.GetChild(0).GetComponent<Slider>().value = resultMoney / stage[User.jobTempLevel];
        if(User.jobTempLevel % 10 == 0&&User.jobTempLevel > 9)
            GameObject.Find("Score").transform.GetChild(1).GetComponent<Text>().text = resultMoney.ToString("N0") + "/???";
        else
            GameObject.Find("Score").transform.GetChild(1).GetComponent<Text>().text = resultMoney.ToString("N0") + "/" + stage[User.jobTempLevel];
        GameObject.Find("Score").transform.GetChild(2).GetComponent<Text>().text = enemysName[User.jobTempLevel];
    }

    void ChangeGoal()
    {
        if (User.jobTempLevel >= 11)
            return;
        if (resultMoney >= stage[User.jobTempLevel] &&!isTime)
        {
            if (!GameObject.Find("SoundOfHit").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
                GameObject.Find("SoundOfHit").GetComponent<AudioSource>().Play();
            GameObject.Find("CanvasInterview/enemys").transform.GetChild(User.jobTempLevel).name = "Destroy";
            User.jobTempLevel += 1;
            if(!User.isJobHard)
                User.jobenemylevel = User.jobTempLevel;
            User.sleep = 0;
            if(alltime>User.jobTempLevel)
                alltime = User.jobTempLevel;
            resultMoney = 0;
            touchCount = 0;
            isTime = true;
            if(User.isJobHard)
                MoneyManager(User.JOB_POWER*User.jobTempLevel);
            else
                MoneyManager(User.JOB_POWER * (User.jobTempLevel+User.jobLevel));
            User.SaveDate();

        }
    }

    void InitialEnemy()
    {
        if(User.isJobHard)
        {
            User.jobTempLevel = 0;
        }
        else
            User.jobTempLevel = User.jobenemylevel;

        for (int i = 0; i <= User.jobTempLevel; i++)
        {
            GameObject.Find("CanvasInterview/enemys").transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i <= User.jobTempLevel; i++)
        {
            if(i< User.jobTempLevel)
            {
                GameObject.Find("CanvasInterview/enemys").transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                GameObject.Find("CanvasInterview/enemys").transform.GetChild(User.jobTempLevel).gameObject.SetActive(true);
                break;
            }

        }
    }

    void JobLevelUp()
    {
        if (User.jobExp >= tblExp[User.jobLevel - 1].needExp&&User.jobLevel<30)
        {
            User.jobExp = 0;
            User.jobLevel += 1;

            if (User.jobLevel== 5 && !PlayerPrefs.HasKey("job5"))
            {
                GoogleManager.Instance.CompleteJob5();
            }
            else if (User.jobLevel == 10 && !PlayerPrefs.HasKey("job10"))
            {
                GoogleManager.Instance.CompleteJob10();
            }
            else if (User.jobLevel == 15 && !PlayerPrefs.HasKey("job15"))
            {
                GoogleManager.Instance.CompleteJob15();
            }
            else if (User.jobLevel == 20 && !PlayerPrefs.HasKey("job20"))
            {
                GoogleManager.Instance.CompleteJob20();
            }
            else if (User.jobLevel == 25 && !PlayerPrefs.HasKey("job25"))
            {
                GoogleManager.Instance.CompleteJob25();
            }
            else if (User.jobLevel == 30 && !PlayerPrefs.HasKey("job30"))
            {
                GoogleManager.Instance.CompleteJob30();
            }
        }
    }

    float r;
    float g;
    float b;


    float moneytime;
    public GameObject moneyUI;
    void MoneyManager(float money)
    {
        if (User.status == 8 || User.status == 7)
            return;

        moneytime += Time.deltaTime;
        if (moneytime > 1.0)
        {
            GameObject moneyui = Instantiate(moneyUI) as GameObject;
            moneyui.transform.SetParent(GameObject.Find("CanvasInterview").transform);
            moneyui.transform.GetChild(1).GetComponent<Text>().text = "+" + money.ToString("N0");
            moneyui.transform.localScale = new Vector3(1, 1, 1);
            moneytime = 0;
            User.money += money;
        }
    }

}
