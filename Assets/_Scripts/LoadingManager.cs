using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
	void Start ()
    {
        User.isPause = false;
        GameObject.Find("Dog").GetComponent<Animator>().SetInteger("dogani", 2);
        if (User.sleep>=100)
        {
            GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = new Color(30 / 255f, 30 / 255f, 30 / 255f, 1);
        }
        else
        {
            GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = new Color(30/255f, 100 / 255f, 200/255f, 1);
        }
        switch (User.map)
        {
            case 3://집
                GameObject.Find("loadText").GetComponent<Text>().text = "\"집에 가즈아!\"";
                GameObject.Find("Dog").transform.localRotation = Quaternion.Euler(new Vector3(0, 220, 0));
                break;
            case 4://직장
                GameObject.Find("loadText").GetComponent<Text>().text = "\"출근하기는 싫지만... 월급은 좋아!\"";
                GameObject.Find("Dog").transform.localRotation = Quaternion.Euler(new Vector3(0, 120, 0));

                break;
            case 5://여행

                GameObject.Find("loadText").GetComponent<Text>().text = "\"가끔은 여행을 통해 힐링을 해요~\"";
                GameObject.Find("Dog").transform.localRotation = Quaternion.Euler(new Vector3(0, 120, 0));
                break;
            case 6://여행hard

                GameObject.Find("loadText").GetComponent<Text>().text = "\"가끔은 여행을 통해 힐링을 해요~\"";
                GameObject.Find("Dog").transform.localRotation = Quaternion.Euler(new Vector3(0, 120, 0));

                break;
            default:
                GameObject.Find("loadText").GetComponent<Text>().text = "\"로딩하면서 마음의 여유를!\"";
                break;
        }
	}
}
