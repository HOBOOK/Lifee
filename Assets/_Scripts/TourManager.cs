using UnityEngine;
using UnityEngine.UI;

public class TourManager : MonoBehaviour
{
    //힐링스킬

    private float popuptime;

    public float v;
    public float h;

    public float timeSpan;
    private float speed;
    private Vector3 lookDirection;
    private int varRan;


    public GameObject target;
    private bool isPursuit; //타겟따라다니기
    private bool isMoveEnd;

    private Animator animator;
    public float distance;

    void Start()
    {
        if(this.name.Contains("GirlFriend"))
        {
            if (User.memberlevel[1] < 1)
                this.gameObject.SetActive(false);
        }
        else if(this.name.Contains("Dog"))
        {
            if (User.memberlevel[0] < 1)
                this.gameObject.SetActive(false);
        }
            
        animator = GetComponent<Animator>();
        speed = 5;
    }

    void Pursuit()
    {
        if(this.name.Contains("Dog"))
        {
            target = GameObject.Find("User");
        }
        distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance > 7&&distance<50)
        {
            isPursuit = true;
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
                animator.SetInteger("dogani", 0);
            }
        }
    }

    void Update()
    {
        Pursuit();
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
