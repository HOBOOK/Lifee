using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LottoManager : MonoBehaviour
{
    TimeSpan timeDayDifference;
	
	void Update ()
    {
        timeDayDifference = DateTime.Now - User.rewardSec;
        if (timeDayDifference.TotalSeconds >= 600)
        {
            this.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
            this.transform.GetChild(3).GetComponent<Button>().enabled = true;
        }
        else
        {
            this.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
            this.transform.GetChild(3).GetChild(1).GetComponent<Text>().text = CalculateTime();
            this.transform.GetChild(3).GetComponent<Button>().enabled = false;
        }
	}

    string CalculateTime()
    {
        string cal;
        if (timeDayDifference.TotalSeconds >= 600)
        {
            return "";
        }
        else
        {
            cal = string.Format("{0:00}:{1:00} ",9-timeDayDifference.Minutes,59-timeDayDifference.Seconds);
        }
        return cal;
    }
}
