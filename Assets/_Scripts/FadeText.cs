using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    private float fadeTime;
	void Update ()
    {
        fadeTime += Time.deltaTime;

        if(fadeTime>5.0)
        {
            fadeTime = 0;
            this.gameObject.SetActive(false);
            
        }
	}
}
