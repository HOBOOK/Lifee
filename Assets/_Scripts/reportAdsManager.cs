using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reportAdsManager : MonoBehaviour
{
	void Start ()
    {
        if (User.adsCount == 0)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
	}
}
