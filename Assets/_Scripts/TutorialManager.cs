using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TutorialManager : MonoBehaviour
{
    //튜토리얼중 안내판 좌표
    List<Vector2> poslist = new List<Vector2>(5);

    string[] story1script = 
    {
        "(튜토리얼)",
        "(당신은 드디어 독립을 하여 \n당신만의 인생을 시작하게 되었습니다.)",
        "(행복하트를 쌓아서 \n성장하세요.)",
        "드디어 사회 초년생으로서 시작이다!",
        "우선 계약한 집으로 가볼까?",
        "(튜토리얼을 시작합니다.)"
    };

    string[] story2script =
    {
        "여기가 내가 살아갈 집이군!",
        "가구가 필요하겠는데? 침대와 책상을 구매해볼까?",
        "(집 메뉴를 클릭하여 침대와 책상을 구매하세요.)",
        "(침대는 하루의 피로를 회복하고 다음날로 진행시킵니다.)",
        "(책상은 하루를 되돌아보고 결산을 합니다.)"
    };
    public string scriptText;
    public int scriptsize;
    public float scriptTime;
    public float scriptDelayTime;
    //색포함
    string isAlert = "(";

	void Start ()
    {
        Story.LoadData();
        if (Story.tutorialseqeuence < 13)
        {
            User.storysequence = 1;
            Story.storyseqeuence = 0;
            Story.tutorialseqeuence = 0;
            Story.isScriptEnd = false;
            User.house = 0;
            MyFurnitrue.stuffLv[1] = 0;
            MyFurnitrue.stuffLv[2] = 0;
            User.money = 50000;
            User.SaveDate();
            Story.SaveDate();
        }
        //안내판 좌표 DB불러오기
        poslist.Add(new Vector2(110, -150));
        poslist.Add(new Vector2(450, -310));
        poslist.Add(new Vector2(110, -150));
        poslist.Add(new Vector2(-150, 60));
        poslist.Add(new Vector2(-150, 60));
        scriptTime = 0;
    }

    public void NextStory()
    {

        Story.storyseqeuence++;
        Story.isScriptEnd = false;
    }
    void Update ()
    {
        
        switch (User.storysequence)
        {
            
            case 1:
                GameObject.Find("CanvasTutorial").transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                //User.isPause = true;
                //튜토리얼 시퀀스
                switch(Story.tutorialseqeuence)
                {
                    case 0:
                        GameObject.Find("Main Camera").GetComponent<FollowCam>().enabled = true;
                        ScriptOnGUI(story1script);
                        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
                        GameObject.Find("CanvasOverlay").GetComponent<Canvas>().enabled = false;
                        if (Story.storyseqeuence > story1script.Length - 1)
                        {
                            GameObject.Find("CanvasTutorial").transform.GetChild(0).gameObject.SetActive(true);
                            GameObject.Find("CanvasTutorial").transform.GetChild(2).GetChild(0).gameObject.SetActive(false);

                            GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
                            GameObject.Find("CanvasOverlay").GetComponent<Canvas>().enabled = true;
                        }
                        break;
                    case 2:
                        GameObject.Find("CanvasTutorial").GetComponent<Canvas>().enabled = false;
                        Story.storyseqeuence = 0;
                        break;
                    case 3:
                        GameObject.Find("CanvasOverlay").GetComponent<Canvas>().enabled = false;
                        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
                        GameObject.Find("Main Camera").GetComponent<FollowCam>().enabled = false;
                        GameObject.Find("Main Camera").transform.position = new Vector3(0.0f, 24f, -22f);
                        GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(40, 0, 0);
                        GameObject.Find("CanvasTutorial").GetComponent<Canvas>().enabled = true;
                        GameObject.Find("CanvasTutorial").transform.GetChild(0).gameObject.SetActive(false);
                        GameObject.Find("CanvasTutorial").transform.GetChild(1).gameObject.SetActive(false);
                        GameObject.Find("CanvasTutorial").transform.GetChild(2).gameObject.SetActive(true);
                        GameObject.Find("CanvasTutorial").transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                        ScriptOnGUI(story2script);
                        if (Story.storyseqeuence > story2script.Length - 1)
                        {
                            GameObject.Find("CanvasTutorial").transform.GetChild(0).gameObject.SetActive(true);
                            GameObject.Find("CanvasTutorial").transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
                            Story.tutorialseqeuence = 4;
                            Story.storyseqeuence = 0;
                        }
                        break;
                        
                    case 4:
                        
                        GameObject.Find("CanvasTutorial").GetComponent<Canvas>().enabled = true;
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).gameObject.SetActive(true);
                        GameObject.Find("CanvasOverlay").GetComponent<Canvas>().enabled = true;
                        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).GetChild(5).gameObject.SetActive(true);
                        break;

                    case 5:
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).GetChild(5).GetChild(4).gameObject.SetActive(false);
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).GetChild(5).GetChild(5).gameObject.SetActive(false);
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).GetChild(5).GetChild(6).gameObject.SetActive(true);
                        break;
                    case 6:
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).gameObject.SetActive(true);
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).GetChild(5).gameObject.SetActive(false);
                        GameObject.Find("Panels").transform.GetChild(6).gameObject.SetActive(false);
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).GetChild(7).gameObject.SetActive(true);
                        break;
                    case 7:
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).GetChild(7).gameObject.SetActive(false);
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
                        break;
                    case 8:
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
                        break;
                    case 9:
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).GetChild(2).gameObject.SetActive(true);
                        break;
                    case 10:
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).GetChild(2).gameObject.SetActive(false);
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).GetChild(3).gameObject.SetActive(true);
                        break;
                    case 11:
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).GetChild(3).gameObject.SetActive(false);
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).GetChild(4).gameObject.SetActive(true);
                        break;
                    case 12:
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).GetChild(4).gameObject.SetActive(false);
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).GetChild(6).gameObject.SetActive(true);

                        break;
                    case 13:
                        
                        GameObject.Find("CanvasTutorial").transform.GetChild(3).gameObject.SetActive(false);
                        GameObject.Find("CanvasTutorial").GetComponent<Canvas>().enabled = false;
                        GameObject.Find("CanvasOverlay").GetComponent<Canvas>().enabled = true;
                        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
                        Story.storyseqeuence = 0;
                        break;

                }
                if (GameObject.Find("CanvasTutorial").transform.GetChild(0)!= null&&Story.tutorialseqeuence<5)
                {
                    GameObject.Find("CanvasTutorial").transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().offsetMin = new Vector2(0, 1);
                    GameObject.Find("CanvasTutorial").transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().offsetMax = new Vector2(0, 1);
                    GameObject.Find("CanvasTutorial").transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition = poslist[Story.tutorialseqeuence];
                }


                break;
            case 2:
                GameObject.Find("CanvasTutorial").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("CanvasTutorial").transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("CanvasTutorial").transform.GetChild(2).GetChild(0).gameObject.SetActive(false);

                break;
            default:
                GameObject.Find("CanvasTutorial").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("CanvasTutorial").transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("CanvasTutorial").transform.GetChild(2).GetChild(0).gameObject.SetActive(false);

                break;
        }

        
        
        
    }

    

    public void ScriptOnGUI(string[] script)
    {
        scriptTime += 0.5f;
        //대사창
        if (GameObject.Find("CanvasTutorial").transform.GetChild(2) != null)
        {
            if (Story.storyseqeuence > script.Length - 1)
            {
                Story.isScriptEnd = true;
                return;
            }
            if (scriptsize > script[Story.storyseqeuence].Length)
            {
                Story.isScriptEnd = true;
                scriptsize = 0;
                return;
            }
            if (Story.isScriptEnd)
            {
                if (script[Story.storyseqeuence].Contains(isAlert))
                {
                    GameObject.Find("CanvasTutorial/Pop").transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "<color='red'>" + script[Story.storyseqeuence] + "</color>";
                }
                else
                {
                    GameObject.Find("CanvasTutorial/Pop").transform.GetChild(0).GetChild(0).GetComponent<Text>().text = script[Story.storyseqeuence];
                }
                scriptDelayTime += Time.deltaTime;
                if (scriptDelayTime > 3.0)
                {
                    NextStory();
                    scriptDelayTime = 0;
                }
            }
            if (scriptTime > 1.0f && !Story.isScriptEnd)
            {
                if (Story.storyseqeuence > script.Length - 1)
                {
                    Story.isScriptEnd = true;
                    return;
                }
                if (script[Story.storyseqeuence].Contains(isAlert))
                {
                    GameObject.Find("CanvasTutorial/Pop").transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "<color='red'>" + script[Story.storyseqeuence].Substring(0, scriptsize) + "</color>";
                }
                else
                {
                    GameObject.Find("CanvasTutorial/Pop").transform.GetChild(0).GetChild(0).GetComponent<Text>().text = script[Story.storyseqeuence].Substring(0, scriptsize);

                }
                scriptsize++;
                scriptTime = 0;
            }
        }
    }
}
