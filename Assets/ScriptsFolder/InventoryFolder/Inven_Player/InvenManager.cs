using UnityEngine;
using System.Collections;

public class InvenManager : MonoBehaviour
{
    public UIPanel[] item_Panel;
    public GameObject[] invenObject;

    public InventoryController itemCtrl;
    
    void Awake()
    {
       // PlayerPrefs.DeleteAll(); 초기화
        item_Panel[0].gameObject.SetActive(false);
        item_Panel[1].gameObject.SetActive(false);

        TestCode_SaveManager.Instance.Initialize();
    }

    public void ItemWindowOn()
    {
        SaveInven();
        TestCode_SaveManager.Instance.Initialize();
        item_Panel[0].gameObject.SetActive(true);

        invenObject[1].gameObject.SetActive(true);
        invenObject[0].gameObject.SetActive(false);
    }
    public void ItemWindowOff()
    {
        SaveInven();
        itemCtrl.UpdateItemDetaDestroy();
        item_Panel[0].gameObject.SetActive(false);
        item_Panel[1].gameObject.SetActive(false);

        invenObject[1].gameObject.SetActive(false);
        invenObject[0].gameObject.SetActive(true);
    }
    public void Test_1()
    {
         GetItem(Inven_Item_Type.Material_A_Parts, 1);
    }
    public void Test_2()
    {
        GetItem(Inven_Item_Type.Material_A_Hp, 1);
    }
    public void Test_3()
    {
        GetItem(Inven_Item_Type.Material_C_Money, 1);
    }
    public void Test_Save()
    {
        itemCtrl.UpdateItemDetaDestroy();
        item_Panel[1].gameObject.SetActive(true);
    }
    /// <summary>
    /// 부분파괴 아이템 관련으로 사용중
    /// </summary>
    public void Parts_Get()
    {
        GetItem(Inven_Item_Type.Material_A_Parts, 1);
    }
    /// <summary>
    /// 아직 미정
    /// </summary>
    public void tTest_2()
    {
     //   GetItem(Inven_Item_Type.Metal_A);
    }
    /// <summary>
    /// 인게임에서 승리하였을 경우에 아이템을 획득할 수 있습니다. [단, 중간에 종료하거나 패배 하였을 경우에는 아이템을 얻지 못합니다.]
    /// </summary>
    public void tTest_Save()
    {
        SaveInven();
    }

    public void GetItem(Inven_Item_Type type, int count)
    {
        TestCode_SaveManager.Instance.Add(type, count);
    }

    public void SaveInven()
    {
        TestCode_SaveManager.Instance.InvenSave();
    }
}
