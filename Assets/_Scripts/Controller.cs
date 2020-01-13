using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    public Transform Stick;
    Vector3 axis;

    float radius;
    Vector3 defaultCenter;
    Touch myTouch;

    void Start()
    {
        radius = GetComponent<RectTransform>().sizeDelta.y / 4;
        defaultCenter = Stick.position;
    }

    private void Update()
    {
        
    }

    public float GetHorizontalValue()
    {
        return axis.x;
    }

    public float GetVerticalValue()
    {
        return axis.y;
    }

    public void Move()
    {
        User.status = 5;
        Vector3 touchPos = Input.mousePosition;
        axis = (touchPos - defaultCenter).normalized;

        float Distance = Vector3.Distance(touchPos, defaultCenter);
        if (Distance > radius)
            Stick.position = defaultCenter + axis * radius;
        else
            Stick.position = defaultCenter + axis * Distance;
    }
    public void End()
    {
        axis = Vector3.zero;
        Stick.position = defaultCenter;
        User.status = 0;
    }
}
