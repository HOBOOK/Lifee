using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    private string[] strStatusInfo = {"지식", "체력", "매력", "도덕","운" };
    private string[] strImageRoot = { "knowledge", "health", "charm" ,"moral","lucky"};
    private string[] ListofJobSkillDesc = new string[9];
    private string[] ListofPassiveSkillDesc = new string[3];
    private string[] ListofTourSkillDesc = new string[3];

    void PassiveTourSkillDescUpdate()
    {
        ListofPassiveSkillDesc[0] = "여행, 직장 경험치가 <color='yellow'>" + (User.passiveLevel[0] * 10) + "%</color> 증가해요.";
        ListofPassiveSkillDesc[1] = "하루 결산 비용이 <color='yellow'>" + (User.passiveLevel[1] * 5) + "%</color> 가 감소해요.";
        ListofPassiveSkillDesc[2] = "자기계발 비용(스탯)이 <color='yellow'>" + (User.passiveLevel[2] * 5) + "</color>% 감소해요.";

        ListofTourSkillDesc[0] = "부스터가 <color='yellow'>" + (2.5f+(User.healingLevel[0]*0.5f)) + "초</color>가 되요.";
        ListofTourSkillDesc[1] = "자석효과가 <color='yellow'>" + (User.healingLevel[1]+3) + "</color>초가 되요.";
        ListofTourSkillDesc[2] = "새가 지저귀면 <color='yellow'>" + (User.healingLevel[2]) + "</color>의 추가파편을 얻고, 하트 상승률이 증가해요.";
    }
    void PassiveTourSkillDescSetUpdate()
    {
        ListofPassiveSkillDesc[0] = "여행, 직장 경험치가 <color='yellow'>10%</color> 증가해요.";
        ListofPassiveSkillDesc[1] = "하루 결산 비용이 <color='yellow'>10%</color> 가 감소해요.";
        ListofPassiveSkillDesc[2] = "자기계발 비용(스탯)이 <color='yellow'>5%</color> 감소해요.";

        ListofTourSkillDesc[0] = "부스터가 <color='yellow'>3초</color>가 되요.";
        ListofTourSkillDesc[1] = "자석효과가 <color='yellow'>4</color>초가 되요.";
        ListofTourSkillDesc[2] = "새가 지저귀면 <color='yellow'>1</color>의 추가파편을 얻고 하트 상승률이 증가해요.";
    }

    void SkillDescUpdate()
    {
        ListofJobSkillDesc[0] = "5초간 시간이 <color='yellow'>" + (User.skillLevel[0] * 5) + "</color> 증가해요.";
        ListofJobSkillDesc[1] = "열정으로 5초간 초당 <color='yellow'>" + (User.JOB_POWER*User.skillLevel[1]).ToString("N0") + "pts</color> 의 성과를 얻어요.";
        ListofJobSkillDesc[2] = "5초간 탭당 <color='yellow'>" + (User.skillLevel[2] * 100) + "pts</color>를 추가 획득해요.";
        ListofJobSkillDesc[3] = "<color='yellow'>" + (4 + User.skillLevel[3]) + "초</color>간 시간이 정지되요.";
        ListofJobSkillDesc[4] = "10초간 경제력을 <color='yellow'>" + (User.skillLevel[4] * 10) + "%</color> 증가시켜요";
        ListofJobSkillDesc[5] = "5초간 초당 최대체력의 <color='yellow'>" + (User.skillLevel[5]) + "%</color>의 pts를 획득.";
        ListofJobSkillDesc[6] = "발동시 끝날때까지 초당 <color='yellow'>" + (User.JOB_POWER*User.skillLevel[6]*2).ToString("N0") + "pts</color>  획득.";
        ListofJobSkillDesc[7] = "10초간 보너스 성과가 <color='yellow'>" + (User.skillLevel[7] * 20) + "%</color> 증가해요.";
        ListofJobSkillDesc[8] = "즉시 <color='yellow'>" + (User.JOB_POWER*User.skillLevel[8] * 10).ToString("N0") + "pts</color>를 획득해요.";
    }
    void SkillDescSetUpdate()
    {
        ListofJobSkillDesc[0] = "5초간 시간이 <color='yellow'>5</color> 증가해요.";
        ListofJobSkillDesc[1] = "열정으로 5초간 초당 <color='yellow'>" + (User.JOB_POWER * User.skillLevel[0]).ToString("N0") + "pts</color> 의 성과를 얻어요.";
        ListofJobSkillDesc[2] = "5초간 탭당 <color='yellow'>100pts</color>를 추가 획득해요.";
        ListofJobSkillDesc[3] = "<color='yellow'>5초</color>간 시간이 정지되요.";
        ListofJobSkillDesc[4] = "10초간 경제력을 <color='yellow'>100%</color> 증가시켜요";
        ListofJobSkillDesc[5] = "5초간 초당 최대체력의 <color='yellow'>1%</color> pts 획득.";
        ListofJobSkillDesc[6] = "발동시 끝날때까지 초당 <color='yellow'>"+User.JOB_POWER.ToString("N0")+ "pts</color>  획득.";
        ListofJobSkillDesc[7] = "10초간 보너스 성과가 <color='yellow'>20%</color> 증가해요.";
        ListofJobSkillDesc[8] = "즉시 <color='yellow'>" + (User.JOB_POWER * 10).ToString("N0") + "pts</color>를 획득해요.";
    }

    void Update()
    {
        if (this.gameObject.activeInHierarchy)
        {
            if(User.status==0)
                User.status = 6;

            //탭스텟스킬
            if (this.transform.GetChild(4).gameObject.activeSelf)
            {
                this.transform.GetChild(0).GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 1);
                this.transform.GetChild(1).GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 0.3f);
            }
            else if (this.transform.GetChild(5).gameObject.activeSelf)
            {
                this.transform.GetChild(1).GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 1);
                this.transform.GetChild(0).GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 0.3f);
            }

            //탭기술
            if (this.transform.GetChild(5).gameObject.activeSelf)
            {
                if(this.transform.GetChild(5).GetChild(3).gameObject.activeSelf)
                {
                    this.transform.GetChild(5).GetChild(0).GetComponent<Image>().color = new Color(30 / 255f, 30 / 255f, 30 / 255f, 200 / 255f);
                    this.transform.GetChild(5).GetChild(1).GetComponent<Image>().color = new Color(30 / 255f, 30 / 255f, 30 / 255f, 0.3f);
                    this.transform.GetChild(5).GetChild(2).GetComponent<Image>().color = new Color(30 / 255f, 30 / 255f, 30 / 255f, 0.3f);
                    this.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<Text>().color = new Color(220 / 255f, 1, 180 / 255f, 1);
                    this.transform.GetChild(5).GetChild(1).GetChild(0).GetComponent<Text>().color = new Color(220 / 255f, 1, 180 / 255f, 0.3f);
                    this.transform.GetChild(5).GetChild(2).GetChild(0).GetComponent<Text>().color = new Color(220 / 255f, 1, 180 / 255f, 0.3f);
                }
                else if (this.transform.GetChild(5).GetChild(4).gameObject.activeSelf)
                {
                    this.transform.GetChild(5).GetChild(1).GetComponent<Image>().color = new Color(30 / 255f, 30 / 255f, 30 / 255f, 200 / 255f);
                    this.transform.GetChild(5).GetChild(0).GetComponent<Image>().color = new Color(30 / 255f, 30 / 255f, 30 / 255f, 0.3f);
                    this.transform.GetChild(5).GetChild(2).GetComponent<Image>().color = new Color(30 / 255f, 30 / 255f, 30 / 255f, 0.3f);

                    this.transform.GetChild(5).GetChild(1).GetChild(0).GetComponent<Text>().color = new Color(220 / 255f, 1, 180 / 255f, 1);
                    this.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<Text>().color = new Color(220 / 255f, 1, 180 / 255f, 0.3f);
                    this.transform.GetChild(5).GetChild(2).GetChild(0).GetComponent<Text>().color = new Color(220 / 255f, 1, 180 / 255f, 0.3f);
                }
                else if (this.transform.GetChild(5).GetChild(5).gameObject.activeSelf)
                {
                    this.transform.GetChild(5).GetChild(2).GetComponent<Image>().color = new Color(30 / 255f, 30 / 255f, 30 / 255f, 200/255f);
                    this.transform.GetChild(5).GetChild(0).GetComponent<Image>().color = new Color(30 / 255f, 30 / 255f, 30 / 255f, 0.3f);
                    this.transform.GetChild(5).GetChild(1).GetComponent<Image>().color = new Color(30 / 255f, 30 / 255f, 30 / 255f, 0.3f);

                    this.transform.GetChild(5).GetChild(2).GetChild(0).GetComponent<Text>().color = new Color(220 / 255f, 1, 180 / 255f, 1);
                    this.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<Text>().color = new Color(220 / 255f, 1, 180 / 255f, 0.3f);
                    this.transform.GetChild(5).GetChild(1).GetChild(0).GetComponent<Text>().color = new Color(220 / 255f, 1, 180 / 255f, 0.3f);
                }
            }

            for (int i = 0; i < strStatusInfo.Length; i++)
            {
                switch (strImageRoot[i])
                {
                    case "knowledge":
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/Slot" + (i + 1) + "/statInfo").GetComponent<Text>().text = "경제력이 <color='magenta'>"+(User.knowledge*0.3f).ToString("N0")+"</color>, 여행력이 <color='magenta'>" + (User.knowledge * 0.2f).ToString("N0")+"</color> 올라요.";
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/Slot" + (i + 1) + "/stat/stat_" + strImageRoot[i]).GetComponent<Text>().text = User.knowledge.ToString("N0");
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/Slot" + (i + 1) + "/PanelCost/cost_" + strImageRoot[i]).GetComponent<Text>().text = User.ChangeUnit(GetCost(User.knowledge));
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/mystat").transform.GetChild(i).GetChild(1).GetComponent<Text>().text = (User.knowledge+ User.bonusStat1).ToString("N0")+"\r\n(" +User.knowledge.ToString("N0") + "+<color='yellow'>" + User.bonusStat1.ToString("N0")+ "</color>)";
                        break;
                    case "health":
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/Slot" + (i + 1) + "/statInfo").GetComponent<Text>().text = "경제력이 <color='magenta'>" + (User.health * 0.2f).ToString("N0") + "</color>, 여행력이 <color='magenta'>" + (User.health * 0.3f).ToString("N0") + "</color> 올라요.";
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/Slot" + (i + 1) + "/stat/stat_" + strImageRoot[i]).GetComponent<Text>().text = User.health.ToString("N0");
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/Slot" + (i + 1) + "/PanelCost/cost_" + strImageRoot[i]).GetComponent<Text>().text = User.ChangeUnit(GetCost(User.health));
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/mystat").transform.GetChild(i).GetChild(1).GetComponent<Text>().text = (User.health + User.bonusStat2).ToString("N0") + "\r\n(" + User.health.ToString("N0") + "+<color='yellow'>" + User.bonusStat2.ToString("N0") + "</color>)";
                        break;
                    case "charm":
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/Slot" + (i + 1) + "/statInfo").GetComponent<Text>().text = "경제력과 여행력이 <color='magenta'>" + (User.charm*0.5f).ToString("N1") + "%</color> 올라요.";
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/Slot" + (i + 1) + "/stat/stat_" + strImageRoot[i]).GetComponent<Text>().text = User.charm.ToString("N0");
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/Slot" + (i + 1) + "/PanelCost/cost_" + strImageRoot[i]).GetComponent<Text>().text = User.ChangeUnit(GetCost(User.charm));
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/mystat").transform.GetChild(i).GetChild(1).GetComponent<Text>().text = (User.charm+ User.bonusStat3).ToString("N0") + "\r\n(" + User.charm.ToString("N0") + "+<color='yellow'>" + User.bonusStat3.ToString("N0") + "</color>)";
                        break;
                    case "moral":
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/Slot" + (i + 1) + "/statInfo").GetComponent<Text>().text = "여행력이 <color='magenta'>" + (User.moral * 0.5f).ToString("N0") + "</color> 올라요.";
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/Slot" + (i + 1) + "/stat/stat_" + strImageRoot[i]).GetComponent<Text>().text = User.moral.ToString("N0");
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/Slot" + (i + 1) + "/PanelCost/cost_" + strImageRoot[i]).GetComponent<Text>().text = User.ChangeUnit(GetCost(User.moral));
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/mystat").transform.GetChild(i).GetChild(1).GetComponent<Text>().text = (User.moral + User.bonusStat4).ToString("N0") + "\r\n(" + User.moral.ToString("N0") + "+<color='yellow'>" + User.bonusStat4.ToString("N0") + "</color>)";
                        break;
                    case "lucky":
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/Slot" + (i + 1) + "/statInfo").GetComponent<Text>().text = "경제력이 <color='magenta'>" + (User.lucky * 0.5f).ToString("N0") + "</color> 올라요.";
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/Slot" + (i + 1) + "/stat/stat_" + strImageRoot[i]).GetComponent<Text>().text = User.lucky.ToString("N0");
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/Slot" + (i + 1) + "/PanelCost/cost_" + strImageRoot[i]).GetComponent<Text>().text = User.ChangeUnit(GetCost(User.lucky));
                        GameObject.Find("Canvas/Panels/PanelManage/myStat/Scroll View/Viewport/Content/mystat").transform.GetChild(i).GetChild(1).GetComponent<Text>().text = (User.lucky + User.bonusStat5).ToString("N0") + "\r\n(" + User.lucky.ToString("N0") + "+<color='yellow'>" + User.bonusStat5.ToString("N0") + "</color>)";
                        break;
                }
            }
            //particle

            //tab1
            if (this.transform != null)
            {
                this.transform.GetChild(4).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = User.ChangeUnit((User.JOB_POWER * 0.7f)) + " ~ " + User.ChangeUnit((User.JOB_POWER * 1.2f));
                this.transform.GetChild(4).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = User.ChangeUnit((User.TOUR_POWER));
            }
            
            //tab2-1
            for (int i = 0; i < this.transform.GetChild(5).GetChild(3).GetChild(0).GetChild(0).childCount; i++)
            {
                //레벨
                this.transform.GetChild(5).GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(2).GetChild(0).GetComponent<Text>().text = User.passiveLevel[i].ToString();

                if (User.passiveLevel[i] == 0)
                {
                    PassiveTourSkillDescSetUpdate();
                    this.transform.GetChild(5).GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(3).GetComponent<Text>().text = ListofPassiveSkillDesc[i];

                    //자물쇠
                    this.transform.GetChild(5).GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetChild(1).gameObject.SetActive(true);
                    //레벨업버튼
                    this.transform.GetChild(5).GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(4).gameObject.SetActive(false);
                    //learn버튼
                    this.transform.GetChild(5).GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(5).gameObject.SetActive(true);
                }
                else
                {
                    PassiveTourSkillDescUpdate();
                    this.transform.GetChild(5).GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(3).GetComponent<Text>().text = ListofPassiveSkillDesc[i];

                    //자물쇠
                    this.transform.GetChild(5).GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetChild(1).gameObject.SetActive(false);
                    //레벨업버튼
                    this.transform.GetChild(5).GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(4).gameObject.SetActive(true);
                    //레벨업필요하트
                    this.transform.GetChild(5).GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(0).GetComponent<Text>().text = GetHeartCost(User.passiveLevel[i], i).ToString("N0");
                    if (User.passiveLevel[i] >= User.house*2)
                    {
                        this.transform.GetChild(5).GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "<color='red'>집Lv "+(User.house+1)+"</color>";
                    }
                    else
                        this.transform.GetChild(5).GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "레벨업";

                    //learn버튼
                    this.transform.GetChild(5).GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(5).gameObject.SetActive(false);

                }
            }
            //tab2-2
            for (int i = 0; i < 9; i++)
            {
                //레벨
                this.transform.GetChild(5).GetChild(4).GetChild(0).GetChild(0).GetChild(i).GetChild(2).GetChild(0).GetComponent<Text>().text = User.skillLevel[i].ToString();
                if (User.skillLevel[i] == 0)
                {
                    //스킬정보
                    SkillDescSetUpdate();//스킬정보업뎃
                    this.transform.GetChild(5).GetChild(4).GetChild(0).GetChild(0).GetChild(i).GetChild(3).GetComponent<Text>().text = ListofJobSkillDesc[i];

                    //자물쇠
                    this.transform.GetChild(5).GetChild(4).GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetChild(1).gameObject.SetActive(true);
                    //레벨업버튼
                    this.transform.GetChild(5).GetChild(4).GetChild(0).GetChild(0).GetChild(i).GetChild(4).gameObject.SetActive(false);
                    //learn버튼
                    this.transform.GetChild(5).GetChild(4).GetChild(0).GetChild(0).GetChild(i).GetChild(5).gameObject.SetActive(true);
                }
                else
                {
                    //스킬정보
                    SkillDescUpdate();//스킬정보업뎃
                    this.transform.GetChild(5).GetChild(4).GetChild(0).GetChild(0).GetChild(i).GetChild(3).GetComponent<Text>().text = ListofJobSkillDesc[i];

                    //자물쇠
                    this.transform.GetChild(5).GetChild(4).GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetChild(1).gameObject.SetActive(false);
                    //레벨업버튼
                    this.transform.GetChild(5).GetChild(4).GetChild(0).GetChild(0).GetChild(i).GetChild(4).gameObject.SetActive(true);
                    //레벨업필요하트
                    this.transform.GetChild(5).GetChild(4).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(0).GetComponent<Text>().text = GetHeartCost(User.skillLevel[i], i).ToString("N0");
                    if(i>5)
                    {
                        if (User.skillLevel[i] >= User.jobLevel-14)
                            this.transform.GetChild(5).GetChild(4).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "<color='red'>직장Lv " + (User.jobLevel + 1) + "</color>";
                        else if(User.skillLevel[i]>=10)
                            this.transform.GetChild(5).GetChild(4).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "MAX";

                        else
                            this.transform.GetChild(5).GetChild(4).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "레벨업";

                    }
                    else if(i>2)
                    {
                        if (User.skillLevel[i] >= User.jobLevel - 9)
                            this.transform.GetChild(5).GetChild(4).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "<color='red'>직장Lv " + (User.jobLevel + 1) + "</color>";
                        else if (User.skillLevel[i] >= 10)
                            this.transform.GetChild(5).GetChild(4).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "MAX";
                        else
                            this.transform.GetChild(5).GetChild(4).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "레벨업";

                    }
                    else
                    {
                        if (User.skillLevel[i] >= User.jobLevel)
                            this.transform.GetChild(5).GetChild(4).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "<color='red'>직장Lv " + (User.jobLevel + 1) + "</color>";
                        else if (User.skillLevel[i] >= 10)
                            this.transform.GetChild(5).GetChild(4).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "MAX";
                        else
                            this.transform.GetChild(5).GetChild(4).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "레벨업";

                    }
                    //learn버튼
                    this.transform.GetChild(5).GetChild(4).GetChild(0).GetChild(0).GetChild(i).GetChild(5).gameObject.SetActive(false);

                }

            }

            //tab2-3
            for (int i = 0; i < this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).childCount; i++)
            {
                //레벨
                this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).GetChild(i).GetChild(2).GetChild(0).GetComponent<Text>().text = User.healingLevel[i].ToString();

                if (User.healingLevel[i] == 0)
                {
                    PassiveTourSkillDescSetUpdate();
                    this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).GetChild(i).GetChild(3).GetComponent<Text>().text = ListofTourSkillDesc[i];

                    //자물쇠
                    this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetChild(1).gameObject.SetActive(true);
                    //레벨업버튼
                    this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).GetChild(i).GetChild(4).gameObject.SetActive(false);
                    //learn버튼
                    this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).GetChild(i).GetChild(5).gameObject.SetActive(true);
                }
                else
                {
                    PassiveTourSkillDescUpdate();
                    this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).GetChild(i).GetChild(3).GetComponent<Text>().text = ListofTourSkillDesc[i];

                    //자물쇠
                    this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetChild(1).gameObject.SetActive(false);
                    //레벨업버튼
                    this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).GetChild(i).GetChild(4).gameObject.SetActive(true);
                    //레벨업필요하트
                    this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(0).GetComponent<Text>().text = GetHeartCost(User.healingLevel[i], i).ToString("N0");
                    switch (i)
                    {
                        case 0:
                            if (User.healingLevel[i] >= User.tourLevel)
                                this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "<color='red'>여행Lv " + (User.tourLevel + 1) + "</color>";
                            else if(User.healingLevel[i]>=10)
                                this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "MAX";

                            else
                                this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "레벨업";

                            break;
                        case 1:
                            if (User.healingLevel[i] >= User.tourLevel- 9)
                                this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "<color='red'>여행Lv " + (User.tourLevel + 1) + "</color>";
                            else if (User.healingLevel[i] >= 10)
                                this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "MAX";
                            else
                                this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "레벨업";

                            break;
                        case 2:
                            if (User.healingLevel[i] >= User.tourLevel - 14)
                                this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "<color='red'>여행Lv " + (User.tourLevel +1) + "</color>";
                            else if (User.healingLevel[i] >= 10)
                                this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "MAX";
                            else
                                this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetChild(2).GetComponent<Text>().text = "레벨업";
                            break;
                    }

                    //learn버튼
                    this.transform.GetChild(5).GetChild(5).GetChild(0).GetChild(0).GetChild(i).GetChild(5).gameObject.SetActive(false);

                }
            }

        }
    }
    private float GetHeartCost(float level, int t)
    {
        float cost = 0;
        float bonus = t * 10;
        if (level == 0)
        {
            cost = 20+bonus;
        }
        else
        {
            cost = (level * 20) + bonus;
        }

        return cost;
    }


    public int GetCost(float stat)
    {
        float cost = 0.0f;
        if (stat <= 100)
            cost = stat;
        else if (stat <= 200)
            cost = stat * 2;
        else if (stat <= 300)
            cost = stat * 4;
        else if (stat <= 400)
            cost = stat * 8;
        else if (stat <= 500)
            cost = stat * 16;
        else if (stat <= 600)
            cost = stat * 32;
        else if (stat <= 700)
            cost = stat * 64;
        else if (stat <= 800)
            cost = stat * 128;
        else if (stat <= 900)
            cost = stat * 256;
        else if (stat <= 1000)
            cost = stat * 512;
        else
            cost = stat * stat;
        cost -= cost * (User.passiveLevel[2] * 0.05f);
        return System.Convert.ToInt32(cost);
    }
}
