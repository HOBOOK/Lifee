using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoxResultManager : MonoBehaviour
{
    //보상수치 = 하우스렙+직장렙+여행렙
    //보상수치2 = 보낸날짜 * 1000원
    private string[] parentScript =
    {
        "\""+User.myname+"야 잘 지내고 있지? 사랑한다!\"\r\nby 엄마",
        "\"사랑하는 "+User.myname+"! 생활비에 보태 쓰거라\"\r\nby 아빠"
    };
	void Start ()
    {
        int ran = Random.Range(0, 2);

        if(ran == 0)
        {
            int result = Random.Range(1, 10);
            this.transform.GetChild(0).GetComponent<Text>().text = parentScript[0];
            this.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("particle");
            this.transform.GetChild(2).GetComponent<Text>().text = "행복의 파편";
            this.transform.GetChild(3).GetComponent<Text>().text = "x " + (result * User.house).ToString("N0");
            this.transform.GetChild(4).GetChild(0).name = (result * User.house).ToString("N0");
        }
        else
        {
            int result = 1000;
            this.transform.GetChild(0).GetComponent<Text>().text = parentScript[1];
            this.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("money");
            this.transform.GetChild(2).GetComponent<Text>().text = "행복의 용돈";
            this.transform.GetChild(3).GetComponent<Text>().text = "+ " + (result*User.night*User.house).ToString("N0");
            this.transform.GetChild(4).GetChild(0).name = (result*User.night*User.house).ToString("N0");

        }

    }

}
