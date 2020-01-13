using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class Loading : MonoBehaviour
{
    public Slider slider;
    bool IsDone = false;
    float fTime = 0f;
    AsyncOperation async_operation;

    void Start()
    {
        switch(User.map)
        {
            case 3:
                StartCoroutine(StartLoad("MyHome"));
                break;
            case 4:
                StartCoroutine(StartLoad("Interview"));
                break;
            case 5:
                StartCoroutine(StartLoad("Mountain"));
                break;
            case 6:
                StartCoroutine(StartLoad("Desert"));
                break;
            default:
                break;
        }
        
    }

    void Update()
    {
        fTime += Time.deltaTime;
        slider.value = fTime;

        if (fTime >= 2)
        {
            async_operation.allowSceneActivation = true;
        }
    }

    public IEnumerator StartLoad(string strSceneName)
    {
        async_operation = SceneManager.LoadSceneAsync(strSceneName);
        async_operation.allowSceneActivation = false;

        if (IsDone == false)
        {
            IsDone = true;

            while (async_operation.progress < 0.8f)
            {
                slider.value = async_operation.progress;

                yield return true;
            }
        }
    }

}
