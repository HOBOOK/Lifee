using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaughterManager : MonoBehaviour
{

    public float v;
    public float h;

    public float timeSpan;
    private float checkTime;
    private float speed;
    private Vector3 lookDirection;
    private float activityTime;
    private int varRan;

    private float popupTime;

    public GameObject target;
    private bool isPursuit; //타겟따라다니기

    private Animator animator;
    private bool isAnimated;
    public float distance;
    void Start()
    {
        animator = GetComponent<Animator>();
        checkTime = Random.Range(3.0f, 6.0f);
        speed = 5;
    }

    void Pursuit()
    {
        if (User.status == 1)
        {
            animator.SetInteger("dogani", 3);
            Vector3 pos = GameObject.Find("House/House" + User.house + "/BabyBed").transform.position;
            pos.y = -2;
            this.transform.position = pos;

            Quaternion rot = GameObject.Find("House/House" + User.house + "/BabyBed").transform.rotation;
            this.transform.rotation = rot;
            this.transform.rotation = new Quaternion(0, -transform.rotation.y, 0, 0);
            if (User.sleep == 0)
            {
                this.transform.position = new Vector3(10f, 0.7f, 0);
            }
            return;
        }
        distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance > 7)
        {
            isPursuit = true;
            if (User.status==0)
                isPursuit = false;
        }
        else if(distance<5)
        {
            isPursuit = false;
            animator.SetInteger("dogani", 0);
        }
            
        if (target!=null)
        {
            speed = User.Speed;
            if(isPursuit)
            {
                animator.SetInteger("dogani", 2);
                lookDirection = target.transform.forward + target.transform.right;
                this.transform.rotation = Quaternion.LookRotation(lookDirection);
                this.transform.LookAt(target.transform);
                this.transform.Translate(Vector3.forward* speed * Time.deltaTime);
            }
            else
            {
                timeSpan += Time.deltaTime;
                if (timeSpan > checkTime)  // 경과 시간이 특정 시간이 보다 커졋을 경우
                {
                    v = Random.Range(-1f, 1f);
                    h = Random.Range(-1f, 1f);
                    isAnimated = false;
                    varRan = Random.Range(0, 3);
                    timeSpan = 0;
                }
                if (v == 0 && h == 0)
                {
                    return;
                }
                if (varRan >0&&!isAnimated)
                {
                    checkTime = 2.5f;
                    animator.SetInteger("dogani", 2);
                    lookDirection = v * Vector3.forward + h * Vector3.right;
                    this.transform.rotation = Quaternion.LookRotation(lookDirection);
                    this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
                else if(!isAnimated)
                {
                    checkTime = 5;
                    animator.SetInteger("dogani", 0);
                    lookDirection = v * Vector3.forward + h * Vector3.right;
                    this.transform.rotation = Quaternion.LookRotation(lookDirection);
                }
            }
        }
    }

    void Update()
    {
        //PopupButton();
        Pursuit();
        if (User.status!=1)
        {
            popupTime += Time.deltaTime;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            screenPos.y += Screen.height * 0.2f;
            GameObject.Find("Guage").transform.GetChild(3).GetComponent<RectTransform>().position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
            GameObject.Find("Guage").transform.GetChild(3).GetComponent<Slider>().value = popupTime / 10;
        }
        if(popupTime>20)
        {
            popupTime = 0;
            User.particle += User.memberlevel[3];
            animator.SetInteger("dogani", 1);
            timeSpan = 0;
            checkTime = 2;
            isAnimated = true;
            if (!GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        
    }

    public void PopupButton()
    {
        if(GameObject.Find("CanvasOverlay/Pop")!=null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            screenPos.y += Screen.height * 0.25f;
            GameObject.Find("CanvasOverlay/Pop").transform.GetChild(3).GetComponent<RectTransform>().position = new Vector3(screenPos.x-20, screenPos.y , screenPos.z);
        }
    }

}
