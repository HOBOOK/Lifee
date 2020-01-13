using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoToMainGame : MonoBehaviour
{
    public void OnClick()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        try
        {
            Debug.Log(Application.persistentDataPath + "/playerInfo.dat");
            if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
            {
                
                SceneManager.LoadScene("MyHome");
            }
            else
            {
                SceneManager.LoadScene("CreateCharacter");
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
