using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyInfoManager : MonoBehaviour
{
    List<Exp> expTable;
    private string[] ListofMemberDesc;

    void MemberDescUpdate()
    {
        
        ListofMemberDesc[0] =
            "이름 : 모찌 <color='yellow'>level " + User.memberlevel[0] +"</color>\r\n" +
            "<집> " + (65- (User.memberlevel[0]*5)) + "초마다 <color='magenta'>"+ (User.charm).ToString("N0")+ "</color> 하트 생성\r\n" + 
            "<여행> 20초마다 여행시간 <color='magenta'>"+(5+(User.memberlevel[0]*5))+"</color> 증가\r\n" +
            "\r\n" +
            "<color='yellow'>\"애교만점 귀요미 웰시코기\"</color>";

        if(!User.isDead)
        {
            ListofMemberDesc[1] =
            "이름 : 신애 <color='yellow'>level " + User.memberlevel[1] + "</color>\r\n" +
            "<집> <color='magenta'>" + (120 - (User.memberlevel[1] * 15)) + "초</color>마다 1분간 하트속도2배\r\n" +
            "<직장> <color='magenta'>" + (5000 * User.memberlevel[1]).ToString("N0") + "원</color> 추가 보상금\r\n" +
            "\r\n" +
            "<color='yellow'>\"사랑해\"</color>";
        }
        else
        {
            ListofMemberDesc[1] =
            "이름 : 경호 <color='yellow'>level " + User.memberlevel[1] + "</color>\r\n" +
            "<집> <color='magenta'>" + (120 - (User.memberlevel[1] * 15)) + "초</color>마다 1분간 하트속도2배\r\n" +
            "<직장> <color='magenta'>" + (5000 * User.memberlevel[1]).ToString("N0") + "원</color> 추가 보상금\r\n" +
            "\r\n" +
            "<color='yellow'>\"사랑해\"</color>";
        }
        

        ListofMemberDesc[2] =
            "이름 : 붕붕이 <color='yellow'>level " + User.memberlevel[2] + "</color>\r\n" +
            "<집> " + (145 - (User.memberlevel[2] * 25)) + "초마다 <color='magenta'>1</color> 파편 획득\r\n" +
            "모든스탯 <color='magenta'>" + (User.memberlevel[2] * 3) + "%</color> 증가\r\n" +
            "\r\n" +
            "<color='yellow'>\"너와 함께라면 어디든지\"</color>";

        ListofMemberDesc[3] =
            "이름 : 뚜니 <color='yellow'>level " + User.memberlevel[3] + "</color>\r\n" +
            "<집> 20초마다 <color='magenta'>" + User.memberlevel[3] + "</color> 의 파편 획득\r\n" +
            "<여행> 하트획득 <color='magenta'>" + (User.memberlevel[3]*10) + "%</color> 증가\r\n" +
            "모든스탯 <color='magenta'>" + (User.memberlevel[3] * 5) + "%</color> 증가\r\n" +
            "\r\n" +
            "<color='yellow'>\"귀여운 나의 딸! 까꿍\"</color>";
    }
    void Start ()
    {
        expTable = MyItems.LoadIExpXML();
        ListofMemberDesc = new string[4];
    }

	void Update ()
    {
        //PanelMyInfo
        if (this.gameObject != null)
        {
            if (this.transform.GetChild(1).gameObject.activeSelf)
            {
                this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 1);
                this.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 0.3f);
            }
            else if(this.transform.GetChild(2).gameObject.activeSelf)
            {
                this.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 1);
                this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 0.3f);
            }
               

            MemberDescUpdate();
            //tab1
            this.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "직장레벨 : " + User.jobLevel.ToString();//직업레벨
            this.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = "여행레벨 : " + User.tourLevel.ToString();//여행레벨

            this.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = User.jobExp.ToString("N0") + "/" + expTable[User.jobLevel - 1].needExp;
            this.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = User.tourExp.ToString("N0") + "/" + expTable[User.tourLevel - 1].needExp;

            this.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetComponent<Slider>().value = User.jobExp/expTable[User.jobLevel-1].needExp;
            this.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetChild(3).GetChild(1).GetComponent<Slider>().value = User.tourExp / expTable[User.tourLevel - 1].needExp;

            //tab2
            for (int i = 0; i < User.member.Length; i++)
            {

                this.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetChild(1).GetChild(0).GetComponent<Text>().text = ListofMemberDesc[i];

                if (User.member[i] == 0)
                {
                    this.transform.GetChild(1).GetChild(0).GetChild(i + 1).gameObject.SetActive(false);
                    this.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetChild(2).gameObject.SetActive(true);
                    this.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetChild(3).gameObject.SetActive(false);
                    this.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetChild(4).gameObject.SetActive(true);
                }
                else
                {
                    if(i==1)
                    {
                        if (User.isDead)
                            this.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Member/boy");
                        else
                            this.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Member/love");

                    }
                    this.transform.GetChild(1).GetChild(0).GetChild(i + 1).gameObject.SetActive(true);
                    this.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetChild(2).gameObject.SetActive(false);
                    this.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetChild(3).gameObject.SetActive(true);
                    this.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetChild(3).GetChild(0).name = ((User.memberlevel[i] + i) * 50).ToString("N0");
                    this.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetChild(3).GetChild(0).GetComponent<Text>().text = ((User.memberlevel[i] + i) * 50).ToString("N0");
                    this.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetChild(4).gameObject.SetActive(false);
                }
            }
            this.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetChild(4).GetChild(1).GetComponent<Text>().text = User.money.ToString("N0");
            this.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetChild(5).GetChild(1).GetComponent<Text>().text = User.particle.ToString("N0");

        }
    }
}
