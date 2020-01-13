using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TourPointer : MonoBehaviour
{
    private float turnSpeed;

    void Update ()
    {
        if(!User.isDamage&&!User.isPlay)
            this.transform.Translate(Vector3.forward * User.Speed * Time.deltaTime);
        turnSpeed = 5;

        Touch();
    }
    void Touch()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Stationary || Input.GetTouch(i).phase == TouchPhase.Moved)
                {
                    Vector3 pos = this.transform.position;
                    //좌
                    if (Input.mousePosition.x < (Screen.width / 2))
                    {
                        if (pos.x - User.Speed * Time.deltaTime < -21)
                            return;
                        else
                        {

                            pos.x -= User.Speed * Time.deltaTime;
                            if (pos.z > GameObject.Find("User").transform.position.z + turnSpeed)
                                pos.z = GameObject.Find("User").transform.position.z + turnSpeed;
                            this.transform.position = pos;
                        }

                    }
                    else //우
                    {
                        if (pos.x + User.Speed * Time.deltaTime > 21)
                            return;
                        else
                        {

                            pos.x += User.Speed * Time.deltaTime;
                            if (pos.z > GameObject.Find("User").transform.position.z + turnSpeed)
                                pos.z = GameObject.Find("User").transform.position.z + turnSpeed;
                            this.transform.position = pos;
                        }
                    }
                    break;
                }
            }
        }
        //pc디버깅용 마우스이벤트

        else if (Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 pos = this.transform.position;
                //좌
                if (Input.mousePosition.x < (Screen.width / 2))
                {

                    if (pos.x - User.Speed* Time.deltaTime < -21)
                        return;
                    else
                    {

                        pos.x -= User.Speed * Time.deltaTime;
                        
                        if (pos.z > GameObject.Find("User").transform.position.z + turnSpeed)
                            pos.z = GameObject.Find("User").transform.position.z + turnSpeed;
                        this.transform.position = pos;
                    }

                }
                else //우
                {
                    if (pos.x + User.Speed * Time.deltaTime > 21)
                        return;
                    else
                    {

                        pos.x += User.Speed * Time.deltaTime;
                        if (pos.z > GameObject.Find("User").transform.position.z + turnSpeed)
                            pos.z = GameObject.Find("User").transform.position.z + turnSpeed;
                        this.transform.position = pos;
                    }
                }
            }
        }
    }
}
