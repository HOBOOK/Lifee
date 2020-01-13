using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour {

    // Use this for initialization
    public List<Item> myitems;
    void Start () {
        myitems = MyItems.LoadItemXML();

    }

    string[] clothbonus =
    {
        "<color='magenta'>자동하트획득 " + User.itemLevel[0] * 1 + "%</color> 증가",
         "<color='magenta'>직장보너스금액 " + User.itemLevel[1] * 100 + "원</color> 증가",
          "<color='magenta'>여행보너스하트 " + User.itemLevel[2] * 10 + "</color> 증가",
           "<color='magenta'>여행경비 " + User.itemLevel[3] * 1 + "원</color> 감소",
            "<color='magenta'>탭당하트게이지 " + User.itemLevel[4] * 10 + "%</color> 증가"
    };

    public void OnClick()
    {
        int selecteditem = System.Convert.ToInt32(gameObject.name);

        if (!GameObject.Find("CanvasOverlay/PanelCloset/PanelItems").transform.GetChild(1).gameObject.activeSelf)
        {
            GameObject.Find("CanvasOverlay/PanelCloset/PanelItems").transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("CanvasOverlay/PanelCloset/PanelItems").transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = myitems[selecteditem].name + " ★"+User.itemLevel[selecteditem];
            GameObject.Find("CanvasOverlay/PanelCloset/PanelItems").transform.GetChild(1).transform.GetChild(3).name = myitems[selecteditem].id.ToString();
            GameObject.Find("CanvasOverlay/PanelCloset/PanelItems").transform.GetChild(1).transform.GetChild(3).GetChild(0).GetComponent<Text>().text = User.ChangeUnit((User.itemLevel[selecteditem]+1) * 1000);
            GameObject.Find("CanvasOverlay/PanelCloset/PanelItems").transform.GetChild(1).transform.GetChild(2).GetComponent<Text>().text = myitems[selecteditem].description + "\r\n\r\n" + clothbonus[selecteditem];
            GameObject.Find("CanvasOverlay/PanelCloset/PanelItems").transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/" + myitems[selecteditem].icon);
        }
        else
            GameObject.Find("CanvasOverlay/PanelCloset/PanelItems").transform.GetChild(1).gameObject.SetActive(false);
    }
}
