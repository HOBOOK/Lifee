using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobEnemyManager : MonoBehaviour
{
    float anitime;
    float retime;

    void Update()
    {
        if (this.name.Equals("Destroy"))
        {
            GetComponent<Animator>().enabled = false;
            anitime += Time.deltaTime * 1f;
            if (anitime >= 1)
                anitime = 1;

            this.transform.GetComponent<Image>().color = new Color(1-anitime, 1-anitime, 1-anitime, 1-anitime);
            if (this.transform.GetComponent<Image>().color.a==0)
            {
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            if(this.GetComponent<Image>().color.r<1)
            {
                retime += Time.deltaTime*1.5f;
                if (retime >= 1)
                    retime = 1;
                this.GetComponent<Image>().color = new Color(retime, retime, retime, 1);

                if (this.GetComponent<Image>().color.r==1)
                {
                    GameObject.Find("CanvasInterview/enemys").transform.GetChild(User.jobTempLevel).GetComponent<Image>().sprite = Resources.Load<Sprite>("JobEnemy/jobenemy"+ User.jobTempLevel);
                    retime = 0;
                }
            }
        }
    }
}
