using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayBonusManager : MonoBehaviour
{
	void Start ()
    {
        float result = (User.house * User.house) + 10;
        this.transform.GetChild(2).GetComponent<Text>().text = result.ToString("N0") + "개";
        this.transform.GetChild(3).GetChild(0).name = result.ToString("N0");
        this.transform.GetChild(4).GetChild(0).name = (result*2).ToString("N0");
    }

}
