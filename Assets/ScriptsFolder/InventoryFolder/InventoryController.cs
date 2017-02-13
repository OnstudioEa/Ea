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

    public string Test_1;

    //-----------------------------------
    public int item_Part_Count;

    public string item_Part_Name;
    
    public TestCode_SaveManager testCode_SaveManager;

    void Start()
    {
         //TestCode_SaveManager.Instance.Initialize();

        item_Sprite = m_grid.GetComponentsInChildren<UISprite>();
        
    }    
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "AddItem_P_A"))
        {
            GetItem(Inven_Item_Type.Powder_A);
        }
        if (GUI.Button(new Rect(10, 50, 100, 30), "AddItem_M_A"))
        {
            GetItem(Inven_Item_Type.Metal_A);
        }
        if (GUI.Button(new Rect(10, 90, 100, 30), "AddItem_P_A"))
        {
             GetItem(Inven_Item_Type.Part_A);            
        }
        if (GUI.Button(new Rect(10, 130, 100, 30), "Save"))
        {
            SaveInven();            
        }
    }
    public void GetItem(Inven_Item_Type type, int count = -1)
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
    /// <summary>
    /// 아이템창 키면서 생성한다 [처음 시작할때 생성한다]
    /// </summary>
    public void StartItemLoader()
    {

        item_Part_Name = testCode_SaveManager.int_Name.ToString();
        item_Part_Count = testCode_SaveManager.item_int;

        Debug.Log(item_Part_Name.ToString());

        if (item_Part_Count > 0)
        {
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
    /// <summary>
    /// 아이템 업뎃 전 삭제
    /// </summary>
    public void UpdataItemDetaDestroy()
    {
        for (int i = 0; i < item_Sprite.Length; i++)
        {
            ItemScript itemUpdate = item_Sprite[i].gameObject.GetComponent<ItemScript>();
            itemUpdate.DestroyItem();
        }
        item_Sprite = null;

        m_grid.Reposition();
        m_scrollView.ResetPosition();
    }
    public void SaveInven()
    {
        TestCode_SaveManager.Instance.InvenSave();
    }   
}