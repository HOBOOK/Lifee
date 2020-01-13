using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class passiveSkillManager : MonoBehaviour
{
	void Update ()
    {
        for (int i = 0; i < this.transform.GetChild(0).GetChild(0).childCount; i++)
        {
            //레벨
            this.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(2).GetChild(0).GetComponent<Text>().text = User.passiveLevel[i].ToString();
            if (User.passiveLevel[i] == 0)
            {
                //자물쇠
                this.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetChild(1).gameObject.SetActive(true);
                //레벨업버튼
                this.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(4).gameObject.SetActive(false);
                //learn버튼
                this.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(5).gameObject.SetActive(true);
            }
            else
            {
                //자물쇠
                this.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetChild(1).gameObject.SetActive(false);
                //레벨업버튼
                this.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(4).gameObject.SetActive(true);
                //레벨업필요하트
                this.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(0).GetComponent<Text>().text = "필요하트 100";
                //learn버튼
                this.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(5).gameObject.SetActive(false);

            }
        }
    }
}
