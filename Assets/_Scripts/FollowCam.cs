using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FollowCam : MonoBehaviour
{

    //추적할 대상
    public Transform target;
    private float initTouchDist;
    public GameObject clickedobj;

    //카메라와의 거리   
    public float dist = 27f;

    //카메라 회전 속도
    public float xSpeed = 220.0f;


    //카메라 초기 위치
    private float x = 0.0f;
    private float y = 0.0f;

    //y값 제한
    public float yMinLimit;
    public float yMaxLimit;

    //앵글의 최소,최대 제한
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    void Start()
    {
        //커서 숨기기"//"를 지우세요
        //Cursor.lockState = CursorLockMode.Locked;
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void TargetChange()
    {
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            target = GameObject.Find("User").transform;
            return;
        }
           
        switch(User.cam)
        {
            case 0:
                target = null;
                break;
            case 1:
                target = GameObject.Find("User").transform;
                break;
            case 2:
                target = GameObject.Find("Dog").transform;
                break;
            case 3:
                target = GameObject.Find("GirlFriend").transform;
                break;
            case 4:
                target = GameObject.Find("Daughter").transform;
                break;
        }
    }
    void Update()
    {
        TouchZoom();
        TargetChange();

        if (dist < 15)
            dist = 15;
        if (dist >= 40)
            dist = 40;
        if (target)
        {
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0, 2.5f, -dist) + target.position + new Vector3(0.0f, 5.0f, 0.0f);
            if (User.status == 4||User.status==7||User.status==8)
                position.x = 0;
            transform.rotation = rotation;
            transform.position = position;
        }
        else
        {
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0, 2, -dist) + new Vector3(0.0f, 5f,0);
            transform.rotation = rotation;
            transform.position = position;
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


    Vector2 PrevPoint;
    void TouchZoom()
    {
        if (SceneManager.GetActiveScene().buildIndex != 3)
            return;

        if(Input.touchCount==1)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    y -= Input.GetTouch(0).deltaPosition.y *2  * Time.deltaTime;
                    y = ClampAngle(y, yMinLimit, yMaxLimit);

                    x -= Input.GetTouch(0).deltaPosition.x * -2 * Time.deltaTime;
                    x = ClampAngle(x, -30, 30);
                }
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            if (!EventSystem.current.IsPointerOverGameObject(touchZero.fingerId) && !EventSystem.current.IsPointerOverGameObject(touchOne.fingerId))
            {
                
                if (touchZero.phase == TouchPhase.Moved&& touchOne.phase == TouchPhase.Moved)
                {
                    Vector2 touchZeroPrevDist = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrevDist = touchOne.position - touchOne.deltaPosition;

                    float prevTouchDeltaMag = (touchZeroPrevDist - touchOnePrevDist).magnitude;
                    float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                    float deltaMagnitudediff = prevTouchDeltaMag - touchDeltaMag;


                    dist -= deltaMagnitudediff * 10 * Time.deltaTime;
                }
            }
        }
        else if (Input.GetMouseButton(1))
        {
            x -= Input.GetAxis("Mouse X") * -5 * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") *5 *Time.deltaTime;

            x = ClampAngle(x, -30 ,30);
            y = ClampAngle(y, yMinLimit, yMaxLimit);
        }
        else
        {
            dist -= Input.mouseScrollDelta.y * 10 *  Time.deltaTime;
        }

    }
}