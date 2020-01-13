using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemButton : MonoBehaviour
{
    Vector3 screenPos;

    void Update ()
    {
        if(User.house>0&&!User.day&& GameObject.Find("House/House" + User.house + "/Closet")!=null)
        {
            this.transform.GetComponent<Image>().enabled = true;
            this.transform.GetChild(0).GetComponent<Image>().enabled = true;
            this.gameObject.SetActive(true);
            screenPos = Camera.main.WorldToScreenPoint(GameObject.Find("House/House" + User.house + "/Closet").transform.position);
            screenPos.y += Screen.height * 0.25f;
            this.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
        }
        else
        {
            this.transform.GetComponent<Image>().enabled = false;
            this.transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
    }
}
