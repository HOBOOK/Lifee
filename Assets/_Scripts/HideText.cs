using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideText : MonoBehaviour
{
    private float showtime;
    void Start()
    {
        showtime = 0;
    }

    void Update()
    {
        showtime += 2*Time.deltaTime;

        if(showtime>1)
        {
            Destroy(gameObject);
            showtime = 0;
        }

    }
}
