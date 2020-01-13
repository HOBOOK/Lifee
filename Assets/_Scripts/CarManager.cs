using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarManager : MonoBehaviour
{
    private float driveTime;
    private float bonustime;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        if(User.status!=1)
        {
            bonustime += Time.deltaTime;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            screenPos.y += Screen.height * 0.1f;
            screenPos.x += Screen.width * 0.1f;
            GameObject.Find("Guage").transform.GetChild(2).GetComponent<RectTransform>().position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
            GameObject.Find("Guage").transform.GetChild(2).GetComponent<Slider>().value = bonustime / (145 - (User.memberlevel[2] * 25));

        }

        if (User.status != 1 && bonustime > 145 - (User.memberlevel[2] * 25))
        {
            User.particle += 1;
            bonustime = 0;
        }
    }

}
