using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleBGM : MonoBehaviour
{
    private void Start()
    {
        if (this.name.Contains("1"))
        {
            if (!User.isBackgroundSound)
            {
                this.transform.GetChild(1).GetComponent<Toggle>().isOn = true;
            }
            else 
            {
                this.transform.GetChild(2).GetComponent<Toggle>().isOn = true;
            }
        }
        else if (this.name.Contains("2"))
        {
            if (!User.isEffectSound)
            {
                this.transform.GetChild(1).GetComponent<Toggle>().isOn = true;
            }
            else
            {
                this.transform.GetChild(2).GetComponent<Toggle>().isOn = true;
            }
        }
        else if (this.name.Contains("3"))
        {
            if (!User.isCloud)
            {
                this.transform.GetChild(1).GetComponent<Toggle>().isOn = true;
            }
            else
            {
                this.transform.GetChild(2).GetComponent<Toggle>().isOn = true;
            }
        }

    }
    private void Update()
    {
        if (this.transform.GetChild(1).GetComponent<Toggle>().isOn)
        {
            if (this.name.Contains("1"))
                User.isBackgroundSound = false;
            else if (this.name.Contains("2"))
                User.isEffectSound = false;
            else if (this.name.Contains("3"))
                User.isCloud = false;
        }
        else
        {
            if (this.name.Contains("1"))
                User.isBackgroundSound = true;
            else if (this.name.Contains("2"))
                User.isEffectSound = true;
            else if (this.name.Contains("3"))
                User.isCloud = true;
        }
    }
}
