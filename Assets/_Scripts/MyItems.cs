using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;


[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public int type;
    public byte stackable;
    public int maxStack;
    public string description = "";
    public string icon;
}

[System.Serializable]
public class Furnitrue
{
    public string id;
    public string name;
    public string description;
    public int maxlevel;
    public int stat;
    public float requireUp;
    public int price;
}

[System.Serializable]
public class Exp
{
    public int level;
    public float needExp;
}

public class MyItems : MonoBehaviour
{ 
    public static List<Item> LoadItemXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/ItemDB");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlElement itemElement = xmlDoc["ItemDB"];

        List<Item> itemList = new List<Item>();

        foreach (XmlElement elem in itemElement.ChildNodes)
        {
            Item item = new Item();
            item.id = System.Convert.ToInt32(elem.GetAttribute("id"));
            item.name = elem.GetAttribute("name");
            item.type = System.Convert.ToInt32(elem.GetAttribute("type"));
            item.description = elem.GetAttribute("description");
            item.icon = elem.GetAttribute("icon");

            itemList.Add(item);
        }
        return itemList;
    }

    public static List<Furnitrue> LoadFurnitrueXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/FurnitrueDB");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlElement itemElement = xmlDoc["FurnitrueDB"];

        List<Furnitrue> furnitrueList = new List<Furnitrue>();

        foreach (XmlElement elem in itemElement.ChildNodes)
        {
            Furnitrue furnitrue = new Furnitrue();
            furnitrue.id = elem.GetAttribute("id");
            furnitrue.name = elem.GetAttribute("name");
            furnitrue.description = elem.GetAttribute("description");
            furnitrue.maxlevel = System.Convert.ToByte(elem.GetAttribute("maxlevel"));
            furnitrue.stat = System.Convert.ToInt32(elem.GetAttribute("stat"));
            furnitrue.requireUp = System.Convert.ToSingle(elem.GetAttribute("requireUp"));
            furnitrue.price = System.Convert.ToInt32(elem.GetAttribute("price"));

            furnitrueList.Add(furnitrue);
        }
        return furnitrueList;
    }

    public static List<Exp> LoadIExpXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/ExpDB");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlElement itemElement = xmlDoc["ExpDB"];

        List<Exp> itemList = new List<Exp>();

        foreach (XmlElement elem in itemElement.ChildNodes)
        {
            Exp exp = new Exp();
            exp.level = System.Convert.ToInt32(elem.GetAttribute("level"));
            exp.needExp = System.Convert.ToSingle(elem.GetAttribute("needExp"));

            itemList.Add(exp);
        }
        return itemList;
    }
}

