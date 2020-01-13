using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GirlManager : MonoBehaviour
{
    //팝업메시지
    private float popuptime;
    private int ranpop;

    //보너스타임
    private float bonustime;
    private float realSpeed;
    Vector3 lookDirection;
    private Animator animator;
    public float v;
    public float h;

    public float timeSpan;
    public float checkTime;
    private float activityTime;
    private int varRan;

    Texture sleepTexture;
    Texture clothTexture;
    Texture sleepTexturegirl;
    Texture clothTexturegirl;
    private string[] girlScript =
    {
        " "+User.myname + " 은근히 \r\n 귀여운면이 있어요.",
        "모찌 밥좀 챙겨줘~~",
        "가끔씩 카페에서 책읽으면서 \r\n 커피를마셔요",
        "야식메뉴로는 치킨이 최고죠~!!",
        "주말에는 집순이 모드랍니다 ~",
        "흐아아암 =3"
    };
    private string[] boyScript =
    {
        " "+User.myname + " 씨 \r\n 사랑합니다!",
        "모찌야 밥먹자~~",
        "게임하고 싶은데...",
        "맥주랑 치킨이 생각나는군..",
        "주말에는 나가서 노는게 좋아",
        "크아아암 =3=3"
    };
    void Start ()
    {
        sleepTexture = Resources.Load("pj_01_cha_sleep_cloth") as Texture;
        clothTexture = Resources.Load("item_equip_defaultcloth") as Texture;
        sleepTexturegirl = Resources.Load("girlSleep") as Texture;
        clothTexturegirl = Resources.Load("girlDefault") as Texture;
        checkTime = 3;
        animator = GetComponent<Animator>();
        if (User.memberlevel[3]>0)
        {
            girlScript[3] = "뚜니야 밥먹자~";
        }
    }
    public void ChangeLoveCloth()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (User.day)
            {
                if (User.isDead)
                    this.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = sleepTexture;
                else
                    this.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = sleepTexturegirl;

            }
            else
            {
                if (User.isDead)
                    this.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = clothTexture;
                else
                    this.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = clothTexturegirl;

            }
        }
    }

    void Update ()
    {
        ChangeLoveCloth();
        if (User.status == 1)
        {
            this.GetComponent<BoxCollider>().enabled = false;
            animator.SetInteger("playerState", 3);
            Vector3 pos = GameObject.Find("House/House" + User.house + "/Furniture/Bed").transform.position;
            pos.y = -2;
            pos.x += 2;
            this.transform.position = pos;

            Quaternion rot = GameObject.Find("House/House" + User.house + "/Furniture/Bed").transform.rotation;
            this.transform.rotation = rot;
            this.transform.rotation = new Quaternion(0, -transform.rotation.y, 0, 0);
            if (User.sleep <1)
            {
                this.GetComponent<BoxCollider>().enabled = true;
                this.transform.position = new Vector3(-5f, 0.7f, 0);
            }
        }
        else
        {
            popuptime += Time.deltaTime;
            
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            screenPos.y += Screen.height * 0.25f;
            GameObject.Find("Guage").transform.GetChild(1).GetComponent<RectTransform>().position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
            GameObject.Find("Guage").transform.GetChild(1).GetComponent<Slider>().value = bonustime / (120 - (User.memberlevel[1] *15));
            if (User.status != 1&&bonustime>120-(User.memberlevel[1]*15))
            {
                User.isLovePower = true;
                ScriptOnGUI("오늘 하루도 힘내라 힘!\r\n<color='red'>(하트속도2배)</color>");
                popuptime = 0;
                bonustime = 0;
            }
            if(!User.isLovePower)
            {
                bonustime += Time.deltaTime;
            }
            if (popuptime > 12.0f)
            {
                ranpop = Random.Range(0, girlScript.Length);
                if (User.isDead)
                    ScriptOnGUI(boyScript[ranpop]);
                else
                    ScriptOnGUI(girlScript[ranpop]);
                popuptime = 0;
            }

            timeSpan += 1 * Time.deltaTime;  // 경과 시간을 계속 등록
            if (timeSpan > checkTime)  // 경과 시간이 특정 시간이 보다 커졋을 경우
            {
                v = Random.Range(-1f, 1f);
                h = Random.Range(-1f, 1f);
                
                varRan = Random.Range(0, 6);
                timeSpan = 0;
            }
            if (v == 0 && h == 0)
            {
                return;
            }
            if (varRan <= 1)
            {
                checkTime = 2;
                animator.SetInteger("playerState", 1);
                lookDirection = v * Vector3.forward + h * Vector3.right;
                this.transform.rotation = Quaternion.LookRotation(lookDirection);
                Vector3 temppos1 = this.transform.position;
                this.transform.Translate(Vector3.forward * User.Speed * Time.deltaTime);
                if (Mathf.Abs(temppos1.x - this.transform.position.x) <= 0.01 || Mathf.Abs(temppos1.z - this.transform.position.z) <= 0.01)
                {
                    varRan = 3;
                }
            }
            else
            {
                if (varRan >= 5)
                {
                    checkTime = 5;
                    animator.SetInteger("playerState", 2);
                }
                else
                {
                    checkTime = 4;
                    animator.SetInteger("playerState", 0);
                }

                lookDirection = v * Vector3.forward + h * Vector3.right;
                this.transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }
    }

    public void ScriptOnGUI(string script)
    {
        if (User.house == 0)
            return;
        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(2).gameObject.SetActive(true);
        if (GameObject.Find("CanvasOverlay/Pop").transform.GetChild(2).gameObject != null)
        {
            GameObject.Find("CanvasOverlay/Pop").transform.GetChild(2).GetChild(0).GetComponent<Text>().text = script;
        }
    }
}
