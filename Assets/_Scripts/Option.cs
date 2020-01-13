using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
	void Start ()
    {
		if(this.name.Contains("Option"))
        {
            if (User.isBackgroundSound)
            {
                this.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = "상태 : OFF";
                this.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
            }
            else
            {
                this.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = "상태 : ON";
                this.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            if (User.isEffectSound)
            {
                this.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>().text = "상태 : OFF";
                this.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
            }
            else
            {
                this.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>().text = "상태 : ON";
                this.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }
	}

	void Update ()
    {
		if(User.isBackgroundSound)
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().mute = true;
        }
        else
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().mute = false;

        }
    }
}
