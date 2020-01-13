using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

    public int sequence;
    private float scripttime;
    private float fade = 0.0f;
    private string[] introscript = 
    {
        "이것은 <color='yellow'>"+ User.myname +"</color> 의 이야기다.",
        "행복하게 산다는 게 무엇일까?",
        "그 해답을 찾기 위해 무엇이든지 열심히 해왔다.",
        "그래서 평범하게 대학교도 졸업하고 힘들게 취업도 했다.",
        "...",
        "아직 모르겠다. 하지만......",
        "귀여운 강아지와..!",
        "좋은 아내..!",
        "귀여운 딸..!",
        "이렇게 화목한 가족이 되어 좋은 집에 사는 게 내 꿈이야!",
        "이제부터 시작이다!! 나는 성공할 거야!",
        "",
    };
    private string[] introscriptgirl =
    {
        "이것은 <color='yellow'>"+ User.myname +"</color> 의 이야기다.",
        "행복하게 산다는 게 무엇일까?",
        "그 해답을 찾기 위해 무엇이든지 열심히 해왔다.",
        "그래서 평범하게 대학교도 졸업하고 힘들게 취업도 했다.",
        "...",
        "아직 모르겠다. 하지만......",
        "귀여운 강아지와..!",
        "멋진 남편..!",
        "어여쁜 딸..!",
        "이렇게 화목한 가족이 되어 좋은 집에 사는 게 내 꿈이야!",
        "이제부터 시작이다!! 나는 성공할 거야!",
        "",
    };
    private Text scriptText;


    void Start ()
    {
        User.LoadData();
        sequence = 0;
        scripttime = 0.0f;
        scriptText = GameObject.Find("btnIntro").GetComponentInChildren<Text>();
    }
	
	void Update ()
    {
        scripttime += 1*Time.deltaTime;
        
        
        if (scripttime > 5)
        {
            
            NextIntro();
            scripttime = 0;
            fade = 0;
        }
        if (scripttime > 2.5f)
        {
            fade -= 0.5f * Time.deltaTime;
            scriptText.color = new Color(1, 1, 1, fade);
        }
        else
        {
            fade += 0.5f * Time.deltaTime;
            scriptText.color = new Color(1, 1, 1, fade);
        }
    }

    
    public void NextIntro()
    {
        if (sequence==introscript.Length)
        {
            GoHome();
        }
        else
        {
            if(User.isDead)
                scriptText.text = introscriptgirl[sequence];
            else
                scriptText.text = introscript[sequence];
            sequence++;
        }
    }
    public void SkipIntro()
    {
        SceneManager.LoadScene(User.map);
        if (GameObject.Find("btnSkip") != null)
            GameObject.Find("btnSkip").SetActive(false);
        if (GameObject.Find("btnIntro") != null)
            GameObject.Find("btnIntro").SetActive(false);
    }

    public void GoHome()
    {
        SceneManager.LoadScene(3);
    }
}
