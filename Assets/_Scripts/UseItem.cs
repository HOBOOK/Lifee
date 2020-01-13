using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour {

    // Use this for initialization
    public List<Item> myitems;
    void Start ()
    {
        myitems = MyItems.LoadItemXML();
    }


    public void UseTheItem()
    {
        if (!GameObject.Find("SoundOfUpgrade").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
            GameObject.Find("SoundOfUpgrade").GetComponent<AudioSource>().Play();
        try
        {
            
            int itemid = myitems[System.Convert.ToInt32(this.name)].id;
            if (User.heart - ((User.itemLevel[itemid]+1)*1000) < 0)
                return;
            User.itemLevel[itemid] += 1;
            User.heart-= User.itemLevel[itemid] * 1000;
            GameObject.Find("CanvasOverlay/PanelCloset/PanelItems").transform.GetChild(1).gameObject.SetActive(false);
            GameObject.Find("CanvasOverlay/PanelCloset/PanelItems").transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("CanvasOverlay/PanelCloset/PanelItems").transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = myitems[itemid].name + " ★" + User.itemLevel[itemid];
            string[] clothbonus =
            {
                "<color='magenta'>자동하트획득 " + User.itemLevel[0] * 1 + "%</color> 증가",
                 "<color='magenta'>직장보너스금액 " + User.itemLevel[1] * 100 + "원</color> 증가",
                  "<color='magenta'>여행보너스하트 " + User.itemLevel[2] * 1 + "%</color> 증가",
                   "<color='magenta'>자동여행경비 " + User.itemLevel[3] * 1 + "원</color> 감소",
                    "<color='magenta'>탭당하트게이지 " + User.itemLevel[4] * 10 + "%</color> 증가"
            };
            GameObject.Find("CanvasOverlay/PanelCloset/PanelItems").transform.GetChild(1).transform.GetChild(2).GetComponent<Text>().text = myitems[itemid].description + "\r\n\r\n" + clothbonus[itemid];
            this.transform.GetChild(0).GetComponent<Text>().text = User.ChangeUnit(((User.itemLevel[itemid] + 1) * 1000));
        }
        catch
        {

        }
        
    }
}
