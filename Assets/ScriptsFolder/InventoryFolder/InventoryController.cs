using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 인벤토리를 관리하는 클래스 입니다.
/// 버튼을 누를때마다 아이템을 넣도록 할께요.
/// </summary>
public class InventoryController : MonoBehaviour
{    
    public GameObject m_gObjSampleItem;
    // 스크롤뷰를 reposition 하기위해 선언합니다.
    public UIScrollView m_scrollView;
    // 그리드를 reset position 하기위해 선언합니다.
    public UIGrid m_grid;

    public UISprite[] item_Sprite;
    public UISprite[] upgrade_Sprite;
    public UILabel[] upgrade_Label;

    public int item_1;
    public int item_2;
    public int item_3;
    
    public string Test_1;
    public int startItemCheck;
    //-----------------------------------
    public int      item_Part_Count;
    public string   item_Part_Name;
    
    public TestCode_SaveManager testCode_SaveManager;
    public InvenManager invenManager;
    public GameManager gameManager;

    void Start()
    {
        //TestCode_SaveManager.Instance.Initialize();
        item_Sprite = m_grid.GetComponentsInChildren<UISprite>();

        PlayerUpgradeItemQuantity();
        
    }    
    public void GetItem(Inven_Item_Type type, int count = 1)
    {
        TestCode_SaveManager.Instance.Add(type, count);
    }
    /// <summary>
    /// 수량 업뎃
    /// </summary>
    public void UpdataItemDeta()
    {
        item_Part_Name = testCode_SaveManager.int_Name.ToString();
        item_Part_Count = testCode_SaveManager.item_int;

        if (item_Sprite != null)
        {
            for (int i = 0; i < item_Sprite.Length; i++)
            {
                if (item_Part_Name == item_Sprite[i].spriteName)
                {
                    ItemScript itemUpdate = item_Sprite[i].gameObject.GetComponent<ItemScript>();
                    itemUpdate.SetInfo_1(item_Part_Count);
                    break;
                }
            }
        }
        else
        {
            UpdateItemDetaDestroy();
        }  
    }
    /// <summary>
    /// 아이템창 키면서 생성한다 [처음 시작할때 생성한다]
    /// </summary>
    public void StartItemLoader()
    {
        item_Part_Name = testCode_SaveManager.int_Name.ToString();
        item_Part_Count = testCode_SaveManager.item_int;

        if (startItemCheck == 0)
        {
            if (item_Part_Count > 0)
            {
                //item_Part_Name = testCode_SaveManager.int_Name.ToString();
                //item_Part_Count = testCode_SaveManager.item_int;

                GameObject gObjItem = NGUITools.AddChild(m_grid.gameObject, m_gObjSampleItem);
                gObjItem.SetActive(true);

                ItemScript itemScript = gObjItem.GetComponent<ItemScript>();
                itemScript.SetInfo(item_Part_Name);
                itemScript.SetInfo_1(item_Part_Count);

                m_grid.Reposition();
                m_scrollView.ResetPosition();

                item_Sprite = m_grid.GetComponentsInChildren<UISprite>();
            }
        }
        else
        {
            return;
        }
    }
    /// <summary>
    /// 아이템 업뎃 전 삭제
    /// </summary>
    public void UpdateItemDetaDestroy()
    {
        if (item_Sprite != null)
        {
            for (int i = 0; i < item_Sprite.Length; i++)
            {

                ItemScript itemUpdate = item_Sprite[i].gameObject.GetComponent<ItemScript>();
                itemUpdate.DestroyItem();
            }
        }
        if (item_Sprite != null)
            item_Sprite = null;

        m_grid.Reposition();
        m_scrollView.ResetPosition();
    }
    public void SaveInven()
    {
        TestCode_SaveManager.Instance.InvenSave();
    }   
    public void PlayerUpgradeItemQuantity()
    {
        upgrade_Label[0].name = upgrade_Sprite[0].spriteName;
        upgrade_Label[1].name = upgrade_Sprite[1].spriteName;
        upgrade_Label[2].name = upgrade_Sprite[2].spriteName;

        if (item_Sprite != null)
        {
            for (int i = 0; i < item_Sprite.Length; i++)
            {
                if (item_Sprite[i].spriteName == "1000")
                {
                    upgrade_Label[i].text = item_Sprite[i].GetComponent<ItemScript>().m_label.text + "/5";
                    item_1 = int.Parse(item_Sprite[i].GetComponent<ItemScript>().m_label.text);
                    if (item_1 <= 0)
                    {
                        upgrade_Label[i].text = "0/5";
                    }
                }
                if (item_Sprite[i].spriteName == "1001")
                {
                    upgrade_Label[i].text = item_Sprite[i].GetComponent<ItemScript>().m_label.text + "/5";
                    item_2 = int.Parse(item_Sprite[i].GetComponent<ItemScript>().m_label.text);
                    if (item_2 <= 0)
                    {
                        upgrade_Label[i].text = "0/5";
                    }
                }
                if (item_Sprite[i].spriteName == "1002")
                {
                    upgrade_Label[i].text = item_Sprite[i].GetComponent<ItemScript>().m_label.text + "/2";
                    item_3 = int.Parse(item_Sprite[i].GetComponent<ItemScript>().m_label.text);
                    if (item_3 <= 0)
                    {
                        upgrade_Label[i].text = "0/5";
                    }
                }
            }
        }
    }
    public void WeaponUpgrade()
    {
        if (item_1 >= 5 && item_2 >= 5 && item_3 >= 2)
        {
            invenManager.GetItem(Inven_Item_Type.Material_A_Parts, -5);
            invenManager.GetItem(Inven_Item_Type.Material_B_Mtr, -5);
            invenManager.GetItem(Inven_Item_Type.Material_C_Money, -2);

            PlayerPrefs.SetInt("Modelling", 1);
            SaveInven();
            gameManager.ModellingCheck();
            PlayerUpgradeItemQuantity();
            UpdateItemDetaDestroy();
            startItemCheck = 0;
        }
        else
        {
            return;
        }
    }
}