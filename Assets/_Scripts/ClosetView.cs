using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosetView : MonoBehaviour
{
    public List<GameObject> AllSlot;
    public RectTransform InventorRect;
    public GameObject Slot;

    private float InventoryWidth;
    private float InventoryHeight;

    public int WidthSlotCount;
    public int HeightSlotCount;
    public int Gap;
    public int SlotSize;
    public int EmptySlot;

    public List<Item> myitems;

    void Awake()
    {
        

        InventoryWidth = (SlotSize * WidthSlotCount) + (Gap * WidthSlotCount) + Gap;
        InventoryHeight = (SlotSize * HeightSlotCount) + (Gap * HeightSlotCount) + Gap;

        InventorRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, InventoryWidth);
        InventorRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, InventoryHeight);

        //Rect Transform Left,Top,Right,Bottom 수치
        //InventorRect.offsetMin = new Vector2(20, 0);
        //InventorRect.offsetMax = new Vector2(0, 0);

        int cnt = 0;

        myitems = MyItems.LoadItemXML();
        for (int i = 0; i < myitems.Count; i++)
        {
            cnt++;
        }

        for (int _x = 0; _x < WidthSlotCount; _x++)
        {
            GameObject CreateSlot = Instantiate(Slot);
            RectTransform SlotRect = CreateSlot.GetComponent<RectTransform>();
            Image SlotImage = SlotRect.GetChild(0).GetComponent<Image>();

            CreateSlot.name = "Slot_"+ _x;
            CreateSlot.transform.SetParent(this.transform);
            SlotRect.localPosition = new Vector3((SlotSize * _x) + (Gap * (_x + 1) + (SlotSize * 1 / 2)), -InventoryHeight/2, 0);
            SlotRect.localRotation = Quaternion.Euler(Vector3.zero);
            SlotRect.localScale = Vector3.one;
            SlotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, SlotSize);
            SlotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, SlotSize);

            SlotImage.name = myitems[_x].id.ToString();
            SlotImage.sprite = Resources.Load<Sprite>("Items/" + myitems[_x].icon);


            AllSlot.Add(CreateSlot);
        }


        EmptySlot = AllSlot.Count;
    }
}
