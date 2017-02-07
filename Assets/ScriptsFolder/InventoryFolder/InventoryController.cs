using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 인벤토리를 관리하는 클래스 입니다.
/// 버튼을 누를때마다 아이템을 넣도록 할께요.
/// </summary>
public class InventoryController : MonoBehaviour
{    
    // 새로 만들어진 아이템들을 모아둡니다.(삭제 및 수정 등을 하기위해서)
    public List<ItemScript> m_lItems = new List<ItemScript>();
    // 우리가 만든 SampleItem을 복사해서 만들기 위해 선언합니다.
    public GameObject m_gObjSampleItem;
    // 스크롤뷰를 reposition 하기위해 선언합니다.
    public UIScrollView m_scrollView;
    // 그리드를 reset position 하기위해 선언합니다.
    public UIGrid m_grid;

    //-----------------------------------
    public int item_Powder;
    public int item_Metal;
    public int item_Part;

    public string item_Part_Name;
    
    public TestCode_SaveManager testCode_SaveManager;
    //-----------------------------------

    GameObject gObjItem;
    ItemScript itemScript;
    
    // Use this for initialization
    void Start()
    {
        TestCode_SaveManager.Instance.Initialize();
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
    public void GetItem(Inven_Item_Type type, int count = 1)
    {
        TestCode_SaveManager.Instance.Add(type, count);
        //TestItemCode();
    }

    public void TestItemCode()
    {
        Debug.Log("1");
        item_Part_Name = testCode_SaveManager.int_Name.ToString();
        item_Part = testCode_SaveManager.item_int;

        gObjItem = NGUITools.AddChild(m_grid.gameObject, m_gObjSampleItem);
        gObjItem.SetActive(true);

        itemScript = gObjItem.GetComponent<ItemScript>();
        itemScript.SetInfo(item_Part_Name);
        itemScript.SetLabel(item_Part);

        m_grid.Reposition();
        m_scrollView.ResetPosition();

        m_lItems.Add(itemScript);
    }
   
    public void SaveTestItemCode()
    {
        for (int i = 0; i < m_lItems.Count; i++)
        {
            Debug.Log("2");
            Destroy(m_lItems[i].gameObject);
            m_lItems.RemoveAt(i);
        }
    }
    public void SaveInven()
    {
        TestCode_SaveManager.Instance.InvenSave();
    }   
}