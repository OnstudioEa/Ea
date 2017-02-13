using UnityEngine;
using System.Collections;

public class InvenManager : MonoBehaviour
{
    public UIPanel[] item_Panel;

    public InventoryController itemCtrl;

    void Awake()
    {
        item_Panel[0].gameObject.SetActive(false);
        item_Panel[1].gameObject.SetActive(false);

        TestCode_SaveManager.Instance.Initialize();
    }

    public void ItemWindowOn()
    {
        SaveInven();
        TestCode_SaveManager.Instance.Initialize();
        item_Panel[0].gameObject.SetActive(true);
    }
    public void ItemWindowOff()
    {
        SaveInven();
        itemCtrl.UpdateItemDetaDestroy();
        item_Panel[0].gameObject.SetActive(false);
        item_Panel[1].gameObject.SetActive(false);
    }


    public void Test_1()
    {
        GetItem(Inven_Item_Type.Powder_A);
        Debug.Log("1");
    }
    public void Test_2()
    {
        GetItem(Inven_Item_Type.Metal_A);
        Debug.Log("2");
    }
    public void Test_3()
    {
        GetItem(Inven_Item_Type.Part_A);
        Debug.Log("3");
    }
    public void Test_Save()
    {
        itemCtrl.UpdateItemDetaDestroy();
        item_Panel[1].gameObject.SetActive(true);
    }

    public void GetItem(Inven_Item_Type type, int count = 1)
    {
        TestCode_SaveManager.Instance.Add(type, count);
    }

    public void SaveInven()
    {
        TestCode_SaveManager.Instance.InvenSave();
    }
}
