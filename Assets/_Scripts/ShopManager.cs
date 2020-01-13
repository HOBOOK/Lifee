using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
	void Start ()
    {
        if (User.isAds)
        {
            this.transform.GetChild(1).GetChild(3).GetComponent<Button>().enabled = false;
        }
        else
        {
            this.transform.GetChild(1).GetChild(3).GetComponent<Button>().enabled = true;
        }
    }
	void Update ()
    {
		if(User.isAds)
        {
            this.transform.GetChild(1).GetChild(3).GetComponent<Button>().enabled = false;
            this.transform.GetChild(1).GetChild(3).GetChild(0).GetComponent<Text>().text = "이용중";
        }
        else
        {
            this.transform.GetChild(1).GetChild(3).GetComponent<Button>().enabled = true;
        }
	}
}
