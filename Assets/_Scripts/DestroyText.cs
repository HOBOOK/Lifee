using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyText : MonoBehaviour
{
    private float showtime;
    void Start()
    {
        showtime = 0;
    }

    void Update()
    {
        showtime += Time.deltaTime;

        if(this.name.Contains("touchMoney"))
        {
            if(showtime>0.8f)
            {
                Destroy(gameObject);
                showtime = 0;
            }
            else if (showtime > 0.5f)
            {
                this.transform.localScale = new Vector3(1.3f - showtime, 1.3f - showtime, 1.3f - showtime);
            }
            else if(showtime <0.3f)
            {
                this.transform.localScale = new Vector3(1f + showtime, 1f + showtime, 1f + showtime);
            }

            if(showtime>0.3f)
            {
                Vector2 pos = new Vector2(this.transform.position.x, this.transform.position.y);
                pos.y += Time.deltaTime * 200;
                this.transform.position = pos;

            }
        }
        else if(this.name.Contains("balloon"))
        {
            if(showtime>5.0f)
            {
                Destroy(gameObject);
                showtime = 0;
            }
        }
        else if(showtime>=0.9f)
        {
            Destroy(gameObject);
            showtime = 0;
        }

    }
}
