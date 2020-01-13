using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyManager : MonoBehaviour
{
    private string furnitruename;

    public void Buy()
    {
        List<Furnitrue> fList = MyItems.LoadFurnitrueXML();
        furnitruename = GameObject.Find("Canvas/Panels/PanelQuestion").transform.GetChild(2).name;
        for (int i =0; i < fList.Count; i++)
        {
            if(fList[i].name==furnitruename&&GameObject.Find("House"+User.house+"/Furniture/" + fList[i].id)!=null)
            {
                if(fList[i].price>User.money)
                {
                    break;
                }
                GameObject.Find("House" + User.house + "/Furniture/" + fList[i].id).gameObject.SetActive(true);
                GameObject.Find("Canvas/Panels").transform.GetChild(6).GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(5).gameObject.SetActive(false);
                GameObject.Find("Canvas/Panels").transform.GetChild(6).GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>().text = fList[i].name + " Lv 1";
                User.money -= fList[i].price;
                MyFurnitrue.stuffLv[i] = 1;
                MyFurnitrue.SaveDate();
                GameObject.Find("Canvas/Panels/PanelQuestion").gameObject.SetActive(false);
                break;
            }
            else
            {
                Debug.Log(fList[i].id);
            }
        }
        
    }
}
