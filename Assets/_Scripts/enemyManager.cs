using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class enemyManager : MonoBehaviour
{
    float anitime;
    float check;
    void Update()
    {
        TourSkill();
        if (this.transform.localScale.x < 1)
        {
            //Destroy(gameObject);
            this.gameObject.SetActive(false);

        }
        if(this.transform.position.z<GameObject.Find("User").transform.position.z - 25)
        {
            //Destroy(gameObject);
            this.gameObject.SetActive(false);
        }
        if (this.name.Equals("Destroy"))
        {
            Vector3 size = this.transform.localScale;
            size -= size / 4;
            this.transform.localScale = size;
        }

    }

    public void TourSkill()
    {
        if(this.name.Contains("heart"))
        {
            if(User.tourSkill[1])
            {
                GameObject target = GameObject.Find("User");
                float distance = Vector3.Distance(target.transform.position, transform.position);

                if (distance > 15+(User.healingLevel[1]*2))
                    return;
                
                Vector3 lookDirection = target.transform.forward + target.transform.right;
                this.transform.rotation = Quaternion.LookRotation(lookDirection);
                this.transform.LookAt(target.transform);
                this.transform.Translate(Vector3.forward * 20 * Time.deltaTime);
            }
            else
            {
                return;
            }
        }

        if(this.tag =="enemy")
        {
            if (User.tourSkill[2])
            {
                GameObject target = GameObject.Find("User");
                float distance = Vector3.Distance(target.transform.position, transform.position);

                if (distance > 70)
                    return;

                this.name = "Destroy";
            }
        }
    }

    
}
