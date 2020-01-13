using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DogManager : MonoBehaviour
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
    private bool isMoveEnd;

    private Animator animator;
    public float distance;
    void Start()
    {
        animator = GetComponent<Animator>();
        checkTime = Random.Range(3.0f, 6.0f);
        speed = 5;
    }

    void Pursuit()
    {
        if(User.status == 1)
        {
            animator.SetInteger("dogani", 3);
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
                    checkTime = Random.Range(3.0f, 6.0f);
                    varRan = Random.Range(0, 3);
                    timeSpan = 0;
                }
                if (v == 0 && h == 0)
                {
                    return;
                }
                if (varRan == 1)
                {

                    animator.SetInteger("dogani", 2);
                    lookDirection = v * Vector3.forward + h * Vector3.right;
                    this.transform.rotation = Quaternion.LookRotation(lookDirection);
                    this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
                else
                {
                    if (varRan == 2)
                        animator.SetInteger("dogani", 1);
                    else
                        animator.SetInteger("dogani", 0);
                    lookDirection = v * Vector3.forward + h * Vector3.right;
                    this.transform.rotation = Quaternion.LookRotation(lookDirection);
                }
            }
        }
    }

    void Update()
    {
        
        if (User.status!=1)
        {
            popupTime += Time.deltaTime;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            screenPos.y += Screen.height * 0.1f;
            GameObject.Find("Guage").transform.GetChild(0).GetComponent<RectTransform>().position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
            GameObject.Find("Guage").transform.GetChild(0).GetComponent<Slider>().value = popupTime / (65.0f - (User.memberlevel[0] * 5));
        }
        if(popupTime>65.0f-(User.memberlevel[0]*5))
        {
            GameObject.Find("CanvasOverlay/Pop").transform.GetChild(3).gameObject.SetActive(true);

            popupTime = 0;
            HeartManager(User.charm);
            animator.SetInteger("dogani", 0);
            if (!GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
            {
                GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Bgm/puppy1");
                GetComponent<AudioSource>().Play();
            }
        }

        Pursuit();
    }

    public GameObject heartUI;
    public void HeartManager(float heart)
    {
        if (!GameObject.Find("SoundOfPop").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
            GameObject.Find("SoundOfPop").GetComponent<AudioSource>().Play();

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            GameObject heartui = Instantiate(heartUI) as GameObject;
            heartui.transform.GetChild(1).GetComponent<Text>().text = User.ChangeUnit(heart + User.memberlevel[3]);
            heartui.transform.SetParent(GameObject.Find("CanvasOverlay").transform);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            screenPos.y += Screen.height * 0.17f;
            GameObject.Find("CanvasTutorial/Pop").transform.GetChild(0).GetComponent<RectTransform>().position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
            heartui.transform.position = screenPos;
            heartui.transform.localScale = new Vector3(1, 1, 1);
        }
        User.heart += heart;
    }

}
