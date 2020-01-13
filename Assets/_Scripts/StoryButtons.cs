using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryButtons : MonoBehaviour
{
    string[] strTutorial =
    {
        "집메뉴를 클릭하세요.","이사하기 버튼 클릭","집메뉴를 클릭하세요."
    };
    public void NextTutorial()
    {
        if(Story.tutorialseqeuence==5)
        {
            if (MyFurnitrue.stuffLv[1] < 1 || MyFurnitrue.stuffLv[2] < 1)
                return;
        }
        Story.tutorialseqeuence++;
        Story.isScriptEnd = false;
        if(Story.tutorialseqeuence<6)
            GameObject.Find("Panels").transform.GetChild(6).gameObject.SetActive(true);
        if (Story.tutorialseqeuence >= strTutorial.Length)
            return;
        GameObject.Find("CanvasTutorial").transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = strTutorial[Story.tutorialseqeuence];

    }

    public void NextStory()
    {
        Story.storyseqeuence++;
        Story.isScriptEnd = false;
    }

    public void SkipScript()
    {
        Story.isScriptEnd = true;
    }
}
