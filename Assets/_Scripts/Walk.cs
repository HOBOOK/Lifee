using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

[System.Serializable]
public class Walk : MonoBehaviour
{
    private float realSpeed;
    Vector3 lookDirection;
    private Animator animator;
    public float v;
    public float h;

    public float timeSpan;
    public float checkTime;
    private float activityTime;
    private int varRan;
    private float fireTime;


    //여행
    public GameObject target;
    private bool isPursuit;
    private float damageTime;

    GameObject clickedobj;
    
    private int TempTexture;

    //터치
    private Touch tempTouch;

    //조이스틱
    public Controller controller;
    float hj; 
    float vj;

    //팝업메시지
    private float popuptime;
    private int ranpop;

    //물
    private bool isWater;
    private float waterTime;

    //여행시나리오
    float contacttime;


    void Start ()
    {
        User.isPlay = false;
        Input.multiTouchEnabled = false;
        User.tourSpeed = 0;
        GetSpeed(0,1);
        timeSpan = 0.0f;
        checkTime = Random.Range(2.0f, 5.0f);
        animator = GetComponent<Animator>();
    }
    
    void GetSpeed(float plus, float wall)
    {
        User.Speed = (5.0f + plus) * wall + User.tourSpeed + User.skillSpeed;
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


        if(Input.touchCount>0)
        {
            clickedobj = GetClickedObject();
            for (int i =0; i <Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId)==false)
                    {
                        if (clickedobj.Equals(GameObject.Find("Desk")))
                        {
                            ScriptOnGUI("책상에서 오늘 하루를 되돌아 볼 수있어.");
                        }
                        if (clickedobj.Equals(GameObject.Find("Bed")))
                        {
                            ScriptOnGUI("잠이안와도 잠을 자야 내일이 와!");
                        }
                        if (clickedobj.Equals(GameObject.Find("User")))
                        {
                            ranpop = Random.Range(0, User.message.Count);
                            ScriptOnGUI(User.message[ranpop]);
                        }
                        if (clickedobj.Equals(GameObject.Find("Guitar")))
                        {
                            ScriptOnGUI("FM7/A ~~ FM7/A ~~ Em ♪");
                            int ran = Random.Range(0, 10);
                            if (ran == 0)
                                User.heart += 1;
                            if (!GameObject.Find("SoundOfGuitar").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
                            {
                                GameObject.Find("SoundOfGuitar").GetComponent<AudioSource>().Play();
                            }
                            if (!GameObject.Find("Effects").transform.GetChild(0).GetComponent<ParticleSystem>().isPlaying)
                                GameObject.Find("Effects").transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                        }
                        if (clickedobj.Equals(GameObject.Find("Closet")))
                        {
                            ScriptOnGUI("옷들은 나의 능력을 더 빛나게 해주지!");
                        }
                        if (clickedobj.Equals(GameObject.Find("Bathtub")))
                        {
                            ScriptOnGUI("씻을 수는 없어...");
                        }
                        if (clickedobj.Equals(GameObject.Find("Mirror")))
                        {
                            ScriptOnGUI("이정도면 나름 괜찮은 얼굴이지");
                        }
                        if (clickedobj.Equals(GameObject.Find("Refrigerator")))
                        {
                            ScriptOnGUI("항상 배고프지만 이곳에선 고프지않지.");
                        }
                        if (clickedobj.Equals(GameObject.Find("Computer")))
                        {
                            ScriptOnGUI("얼른 랭크를 올려야 하는데..");
                        }
                        if (clickedobj.Equals(GameObject.Find("Sofa")))
                        {
                            ScriptOnGUI("푹!신!푹!신");
                        }
                        if (clickedobj.Equals(GameObject.Find("Cactus")))
                        {
                            ScriptOnGUI("선인장인거 같은데?");
                        }
                        if (clickedobj.Equals(GameObject.Find("Bookcase")))
                        {
                            ScriptOnGUI("책을 읽으면 마음이 안정이되요!");
                        }
                        if (clickedobj.Equals(GameObject.Find("Clock")))
                        {
                            ScriptOnGUI("시간은 뒤를 돌아보지않아.");
                        }
                        if (clickedobj.name.Contains("bird"))
                        {
                            if (!GameObject.Find("SoundOfBird").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
                            {
                                GameObject.Find("SoundOfBird").GetComponent<AudioSource>().Play();
                            }
                            if (!GameObject.Find("Effects").transform.GetChild(0).GetComponent<ParticleSystem>().isPlaying)
                                GameObject.Find("Effects").transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                        }
                    }

                }
                          
            }
        }
        else if(Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if(clickedobj==null)
                    return;
                if (clickedobj.Equals(GameObject.Find("Bed")))
                {
                    ScriptOnGUI("잠이안와도 잠을 자야 내일이 와!");
                }
                if (clickedobj.Equals(GameObject.Find("Guitar")))
                {
                    ScriptOnGUI("FM7/A ~~ FM7/A ~~ Em ♪");
                    int ran = Random.Range(0, 10);
                    if (ran == 0)
                        User.heart += 1;
                    if (!GameObject.Find("SoundOfGuitar").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
                    {
                        GameObject.Find("SoundOfGuitar").GetComponent<AudioSource>().Play();
                    }
                    if (!GameObject.Find("Effects").transform.GetChild(0).GetComponent<ParticleSystem>().isPlaying)
                        GameObject.Find("Effects").transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                }
                if (clickedobj.Equals(GameObject.Find("Desk")))
                {
                    ScriptOnGUI("책상에서 오늘 하루를 되돌아 볼 수있어.");
                }
                if (clickedobj.Equals(GameObject.Find("Closet")))
                {
                    ScriptOnGUI("옷들은 나의 능력을 더 빛나게 해주지!");
                }
                if (clickedobj.Equals(GameObject.Find("User")))
                {
                    ranpop = Random.Range(0, User.message.Count);
                    ScriptOnGUI(User.message[ranpop]);
                }
                if (clickedobj.Equals(GameObject.Find("Bathtub")))
                {
                    ScriptOnGUI("씻을 수는 없어...");
                }
                if (clickedobj.Equals(GameObject.Find("Mirror")))
                {
                    ScriptOnGUI("이정도면 나름 괜찮은 얼굴이지");
                }
                if (clickedobj.Equals(GameObject.Find("Refrigerator")))
                {
                    ScriptOnGUI("항상 배고프지만 이곳에선 고프지않지.");
                }
                if (clickedobj.Equals(GameObject.Find("Computer")))
                {
                    ScriptOnGUI("얼른 랭크를 올려야 하는데..");
                }
                if (clickedobj.Equals(GameObject.Find("Sofa")))
                {
                    ScriptOnGUI("푹!신!푹!신");
                }
                if (clickedobj.Equals(GameObject.Find("Cactus")))
                {
                    ScriptOnGUI("선인장인거 같은데?");
                }
                if (clickedobj.Equals(GameObject.Find("Bookcase")))
                {
                    ScriptOnGUI("책을 읽으면 마음이 안정이되요!");
                }
                if (clickedobj.Equals(GameObject.Find("Clock")))
                {
                    ScriptOnGUI("시간은 뒤를 돌아보지않아.");
                }
                if (clickedobj.name.Contains("bird"))
                {
                    if (!GameObject.Find("SoundOfBird").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
                    {
                        GameObject.Find("SoundOfBird").GetComponent<AudioSource>().Play();
                    }
                    if (!GameObject.Find("Effects").transform.GetChild(0).GetComponent<ParticleSystem>().isPlaying)
                        GameObject.Find("Effects").transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                }
            }
        }
        


        if (User.day)
        {
            animator.SetBool("tired", true);
            GetSpeed(-2, 1);
        }
        else
        {
            animator.SetBool("tired", false);
            GetSpeed(0, 1);
        }


        switch (User.status)
        {
            case 0: //ai
                popuptime += Time.deltaTime;
                
                if (popuptime > 10.0f&&Story.tutorialseqeuence>=4)
                {
                    ranpop = Random.Range(0, User.message.Count);
                    ScriptOnGUI(User.message[ranpop]);
                    popuptime = 0;
                }

                timeSpan += 1 * Time.deltaTime;  // 경과 시간을 계속 등록
                if (timeSpan > checkTime)  // 경과 시간이 특정 시간이 보다 커졋을 경우
                {
                    v = Random.Range(-1f, 1f);
                    h = Random.Range(-1f, 1f);
                    checkTime = Random.Range(2.0f, 4.0f);
                    varRan = Random.Range(0, 5);
                    timeSpan = 0;
                }
                if (v == 0 && h == 0)
                {
                    return;
                }
                if (varRan > 3)
                {
                    animator.SetInteger("playerState", 1);
                    lookDirection = v * Vector3.forward + h * Vector3.right;
                    this.transform.rotation = Quaternion.LookRotation(lookDirection);
                    Vector3 temppos1 = this.transform.position;
                    this.transform.Translate(Vector3.forward * User.Speed * Time.deltaTime);
                    if(Mathf.Abs(temppos1.x-this.transform.position.x)<=0.01|| Mathf.Abs(temppos1.z-this.transform.position.z)<=0.01)
                    {
                        varRan = 3;
                    }
                }
                else
                {
                    if (varRan < 1)
                    {
                        animator.SetInteger("playerState", 2);
                    }
                    else
                    {
                        animator.SetInteger("playerState", 0);
                    }

                    lookDirection = v * Vector3.forward + h * Vector3.right;
                    this.transform.rotation = Quaternion.LookRotation(lookDirection);
                }

                break;

            case 1: //잠
                if (User.day)
                {

                    this.GetComponent<BoxCollider>().enabled = false;
                    animator.SetInteger("playerState", 3);
                    Vector3 pos = GameObject.Find("House/House" + User.house + "/Furniture/Bed").transform.position;
                    pos.y = -2;
                    if (User.member[1] > 0)
                        pos.x -= 2;
                    if(!User.isPlay)
                        this.transform.position = pos;
                    User.isPlay = true;
                    Quaternion rot = GameObject.Find("House/House" + User.house + "/Furniture/Bed").transform.rotation;
                    this.transform.rotation = rot;
                    this.transform.rotation = new Quaternion(0, -transform.rotation.y, 0, 0);
                    if(User.sleep < 10&&User.sleep>8)
                        GameObject.Find("Canvas/popup").transform.GetChild(0).gameObject.SetActive(true);

                    if(!GameObject.Find("SoundOfSleep").GetComponent<AudioSource>().isPlaying&&!User.isEffectSound)
                    {
                        GameObject.Find("SoundOfSleep").GetComponent<AudioSource>().Play();
                    }
                    if (User.sleep == 0)
                    {
                        if (GameObject.Find("SoundOfSleep").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
                        {
                            GameObject.Find("SoundOfSleep").GetComponent<AudioSource>().Stop();
                        }
                        this.GetComponent<BoxCollider>().enabled = true;
                        User.isReport = false;
                        User.isPlay = false;
                        this.transform.position = new Vector3(0, 0.7f, 0);
                        User.night += 1;
                        User.day = false;
                        User.status = 0;
                        User.frustrate = User.night;
                        for(int i = 0; i < MyFurnitrue.stuffExp.Length; i++)
                        {
                            if(MyFurnitrue.stuffLv[i+1]!=0&&MyFurnitrue.stuffExp[i]<MyFurnitrue.ExpTable[MyFurnitrue.stuffLv[i + 1]])
                                MyFurnitrue.stuffExp[i] += User.house;
                            if(MyFurnitrue.stuffExp[i] >= MyFurnitrue.ExpTable[MyFurnitrue.stuffLv[i + 1]])
                            {
                                MyFurnitrue.stuffExp[i] = MyFurnitrue.ExpTable[MyFurnitrue.stuffLv[i + 1]];
                            }
                        }
                        ChangeDay();
                    }
                }
                break;

            case 2: //외출
                timeSpan += 1 * Time.deltaTime;  // 경과 시간을 계속 등록
                if (timeSpan > checkTime)  // 경과 시간이 특정 시간이 보다 커졋을 경우
                {
                    v = Random.Range(-1f, 1f);
                    h = Random.Range(-1f, 1f);
                    checkTime = Random.Range(2.0f, 5.0f);
                    varRan = Random.Range(0, 3);
                    timeSpan = 0;
                }
                if (varRan == 1)
                {
                    animator.SetInteger("playerState", 1);
                    lookDirection = v * Vector3.forward + h * Vector3.right;
                    this.transform.rotation = Quaternion.LookRotation(lookDirection);
                    this.transform.Translate(Vector3.forward * User.Speed * Time.deltaTime);

                }
                else
                {
                    if (varRan < 2)
                    {
                        animator.SetInteger("playerState", 2);
                    }
                    else
                    {
                        animator.SetInteger("playerState", 0);
                    }

                    lookDirection = v * Vector3.forward + h * Vector3.right;
                    this.transform.rotation = Quaternion.LookRotation(lookDirection);
                }
                break;

            case 3: //책상
                if (User.day)
                {
                    User.isPlay = true;
                    animator.SetInteger("playerState", 4);
                    Vector3 pos = GameObject.Find("House/House" + User.house + "/Furniture/Desk").transform.position;
                    this.transform.position = pos;
                    this.transform.position = new Vector3(pos.x-2, 0.2f, pos.z+1);
                    this.transform.LookAt(GameObject.Find("House/House" + User.house + "/Furniture/Desk").transform);

                    GameObject.Find("CanvasOverlay/Report").transform.GetChild(0).gameObject.SetActive(true);
                }
                break;
            case 4: //여행
                

                contacttime += Time.deltaTime;
                if (contacttime < 1)
                {
                    ScriptOnGUI("좋아 가는거야!");
                    animator.SetInteger("playerState", 2);
                    User.isPlay = true;
                }
                else if (contacttime > 5.0)
                {
                    User.isPlay = false;

                    contacttime = 2;
                }

                if (User.isPlay)
                    return;
                if (isWater)
                {
                    waterTime += Time.deltaTime;
                    if(waterTime>1.0)
                    {
                        GetSpeed(Random.Range(5,30), 1);
                        waterTime = 0;
                    }

                }
                else
                    GetSpeed(10, 1);
                timeSpan = 10;
                animator.SetInteger("playerState", 1);

                float distance = Vector3.Distance(target.transform.position, transform.position);
                if (distance > 5)
                {
                    isPursuit = true;
                }
                else if (distance < 3)
                {
                    isPursuit = false;
                    animator.SetInteger("playerState", 0);
                }

                if (target != null)
                {
                    if (isPursuit&&!User.isDamage)
                    {
                        animator.SetBool("damaged", false);
                        animator.SetInteger("playerState", 1);
                        lookDirection = target.transform.forward + target.transform.right;
                        if (vj != 0 || hj != 0)
                            this.transform.rotation = Quaternion.LookRotation(lookDirection);
                        this.transform.LookAt(target.transform);
                        this.transform.Translate(Vector3.forward * User.Speed * Time.deltaTime);
                        User.meter += (User.Speed *0.1f) * Time.deltaTime;
                    }
                    else if (User.isDamage)
                    {
                        
                        animator.SetBool("damaged", true);
                        damageTime += Time.deltaTime;
                        if (damageTime > 1.0f)
                        {
                            User.isDamage = false;
                            damageTime = 0;
                        }

                    }
                    else
                    {
                        animator.SetInteger("playerState", 0);
                    }
                }

                break;
            case 5: //조이스틱

                GameObject cam = GameObject.Find("Main Camera");
                hj = controller.GetHorizontalValue();
                vj = controller.GetVerticalValue();
                animator.SetInteger("playerState", 1);
                
                lookDirection = vj * cam.transform.forward + hj * cam.transform.right;
                lookDirection.y = 0;
                this.transform.rotation = Quaternion.LookRotation(lookDirection);
                //this.transform.eulerAngles = new Vector3(0, cam.transform.rotation.eulerAngles.y, 0);
                this.transform.Translate(Vector3.forward * User.Speed * Time.deltaTime);
                
                timeSpan = 10;
                User.meter += (User.Speed / 10) * Time.deltaTime;
                break;
            case 6: //폰
                if (User.isPlay)
                {
                    animator.SetInteger("playerState", 5);
                }
                else
                    User.status = 0;
                

                break;
            case 7: //캠프파이어
                fireTime += Time.deltaTime;
                if (fireTime<1)
                {
                    ScriptOnGUI("따뜻하고 좋아!");
                }
                if(fireTime>4)
                {
                    animator.SetInteger("playerState", 0);
                }
                else
                {
                    animator.SetInteger("playerState", 2);
                }
                
                User.sleep -= 10 * Time.deltaTime;
                
                if(fireTime>5.0)
                {
                    GameObject.Find("DestroyFire").name = "Destroy";
                    User.status = 4;
                    User.isPlay = false;
                    fireTime = 0;
                }
                break;
        }
        //사운드
        if (animator.GetInteger("playerState")==0) //안녕
        {
            //
        }
    }

    void ChangeAction()
    {
        v = Random.Range(-1f, 1f);
        h = Random.Range(-1f, 1f);
        checkTime = Random.Range(2.0f, 4.0f);
        varRan = Random.Range(0, 4);
        timeSpan = 0;
        Debug.Log("충돌");
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("water"))
        {
            isWater = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.transform.CompareTag("water"))
        {
            isWater = false;
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.transform.CompareTag("water"))
        {
            isWater = true;

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


    public void ScriptOnGUI(string script)
    {
        if (User.house == 0)
            return;
        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(0).gameObject.SetActive(true);
        if (GameObject.Find("CanvasOverlay/Pop").transform.GetChild(0).gameObject != null)
        {
            GameObject.Find("CanvasOverlay/Pop").transform.GetChild(0).GetChild(0).GetComponent<Text>().text = script;
        }
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
                    this.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = sleepTexturegirl;
                else
                    this.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = sleepTexture;

            }
            else
            {
                if (User.isDead)
                    this.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = clothTexturegirl;
                else
                    this.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = clothTexture;

            }
        }
        else
        {
            return;
        }
    }
}