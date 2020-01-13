using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour {

    public GameObject Building;

    public void BuildingSetActive()
    {
        if(Building.activeSelf)
        {
            if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
            {
                GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
            }
            Building.SetActive(false);
            User.isPlay = false;
        }
        else
        {
            if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
            {
                GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
            }
            Building.SetActive(true);
            User.isPlay = true;
        }

    }
}
