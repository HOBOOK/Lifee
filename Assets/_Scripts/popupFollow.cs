using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popupFollow : MonoBehaviour
{
    private GameObject target;
    private void Awake()
    {
        if (!this.name.Contains("Girl"))
            target = GameObject.Find("User");
        else
            target = GameObject.Find("GirlFriend");
        PopupButton();
    }
    void Update ()
    {
        PopupButton();
	}
    public void PopupButton()
    {
        if (GameObject.Find("CanvasTutorial") != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
            screenPos.y += Screen.height*0.25f;
            GameObject.Find("CanvasTutorial/Pop").transform.GetChild(0).GetComponent<RectTransform>().position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
        }
        if (GameObject.Find("CanvasOverlay/Pop").transform.GetChild(0) != null&&!target.name.Contains("Girl"))
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
            screenPos.y += Screen.height * 0.25f;
            GameObject.Find("CanvasOverlay/Pop").transform.GetChild(0).GetComponent<RectTransform>().position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
        }
        if (GameObject.Find("CanvasOverlay/Pop").transform.GetChild(2) != null&&target.name.Contains("Girl"))
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
            screenPos.y += Screen.height * 0.25f;
            GameObject.Find("CanvasOverlay/Pop").transform.GetChild(2).GetComponent<RectTransform>().position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
        }
    }

}
