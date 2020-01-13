using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxManager : MonoBehaviour
{
	void Update ()
    {
        if (User.night%7==0&&User.frustrate==User.night)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            if (this.transform.GetChild(1).gameObject.activeSelf)
            {
                if (User.isAds)
                {
                    this.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                    this.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
                }
                else
                {
                    this.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                    this.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
                }
            }

        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }

        
    }
}
