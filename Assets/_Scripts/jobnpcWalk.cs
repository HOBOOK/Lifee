using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jobnpcWalk : MonoBehaviour
{
    float timeSpan;
    float checkTime;
    private Animator animator;
    private Vector3 lookDirection;
    public float v;
    public float h;
    private int varRan;
    void Start ()
    {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
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
        if (varRan > 1)
        {
            animator.SetInteger("dogani", 2);
            lookDirection = v * Vector3.forward + h * Vector3.right;
            this.transform.rotation = Quaternion.LookRotation(lookDirection);
            Vector3 temppos1 = this.transform.position;
            this.transform.Translate(Vector3.forward *5 * Time.deltaTime);
        }
        else
        {
            animator.SetInteger("dogani", 0);

            lookDirection = v * Vector3.forward + h * Vector3.right;
            this.transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}
