using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeManager : MonoBehaviour
{
	void Start ()
    {
        isfade = true;
	}
    bool isfade;
    float fadetime;
	void Update ()
    {
        if(isfade)
        {
            fadetime += Time.deltaTime;
            if (fadetime > 3.0)
            {
                fadetime = 0;
                isfade = false;
                this.gameObject.SetActive(false);
            }
            else
            {
                this.GetComponent<Image>().color = new Color(30 / 255, 30 / 255, 30 / 255, 0.9f - (fadetime * 0.3f));
            }
        }
        
		
	}
}
