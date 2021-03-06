﻿using UnityEngine;
using System.Collections;

public class InvenManager : MonoBehaviour
{
    public UIPanel[] item_Panel;
    public GameObject[] invenObject;

    public UILabel[] money_LB;
    
    public InvenVirtualJoysticks moveJoystick;

    public int settingCount;
    int m;

    void Awake()
    {
    //    PlayerPrefs.DeleteAll(); //초기화
        StartSetting();

        item_Panel[0].gameObject.SetActive(false);
        item_Panel[1].gameObject.SetActive(false);

        TestCode_SaveManager.Instance.Initialize();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            MoneyGet();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                PlayerPrefs.SetInt("Money", 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
            GetItem(Inven_Item_Type.Material_A_Parts, 1);
        if (Input.GetKeyDown(KeyCode.S))
            GetItem(Inven_Item_Type.Material_B_Mtr, 1);
        //if (Input.GetKeyDown(KeyCode.D))
        //    GetItem(Inven_Item_Type.Material_C_Money, 1);
        if (Input.GetKeyDown(KeyCode.F))
            TestCode_SaveManager.Instance.Initialize();
        if (Input.GetKeyDown(KeyCode.G))
            SaveInven();

    }

    /// <summary>
    /// 돈 획득 메소드
    /// </summary>
    public void MoneyGet()
    {
        m = Random.Range(4500, 5500);
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + m);
        money_LB[0].text = PlayerPrefs.GetInt("Money").ToString(); //인게임에 적용시켜야 오류없음
       // Debug.Log("돈 4500~5500");
    }
    public void UpgradeWindowOn()
    {
        //SaveInven();
        item_Panel[1].gameObject.SetActive(true);
        invenObject[0].gameObject.SetActive(false);
        moveJoystick.JostickReset();
    }
    public void ItemWindowOn()
    {
        //SaveInven();
        TestCode_SaveManager.Instance.Initialize();
        item_Panel[0].gameObject.SetActive(true);
        invenObject[0].gameObject.SetActive(false);
        moveJoystick.JostickReset();
        CamInvenPos();
    }
    public void ItemWindowOff()
    {
        //SaveInven();
        //itemCtrl.UpdateItemDetaDestroy();
        item_Panel[0].gameObject.SetActive(false);
        item_Panel[1].gameObject.SetActive(false);
        CamPosReset();
        if (settingCount == 2)
        {
            invenObject[0].gameObject.SetActive(true);
        }
    }
    public void CamInvenPos()
    {
        invenObject[3].transform.position = invenObject[5].transform.position;
        invenObject[3].transform.rotation = invenObject[5].transform.rotation;
    }
    public void CamPosReset()
    {
        invenObject[3].transform.position = invenObject[4].transform.position;
        invenObject[3].transform.rotation = invenObject[4].transform.rotation;
    }
    //public void Test_1()
    //{
    //     GetItem(Inven_Item_Type.Material_A_Parts, 1);
    //}
    //public void Test_2()
    //{
    //    GetItem(Inven_Item_Type.Material_A_Mtr, 1);
    //}
    //public void Test_3()
    //{
    //    GetItem(Inven_Item_Type.Material_C_Money, 1);
    //}
    /*public void Test_Save()
    {
        itemCtrl.UpdateItemDetaDestroy();
        item_Panel[1].gameObject.SetActive(true);
    }*/
    /// <summary>
    /// 부분파괴 아이템 관련으로 사용중
    /// </summary>
    public void Parts_Get()
    {
        GetItem(Inven_Item_Type.Material_A_Parts, 1);
       // Debug.Log("부분파괴 아이템 습득");
    }
    /// <summary>
    /// 기본적으로 얻는 아이템
    /// </summary>
    public void tTest_2()
    {
        GetItem(Inven_Item_Type.Material_B_Mtr, Random.Range(2,4));
       // Debug.Log("기본아이템 2~3개 획득");
    }

    public void GetItem(Inven_Item_Type type, int count)
    {
        TestCode_SaveManager.Instance.Add(type, count);
    }

    /// <summary>
    /// 인게임에서 승리하였을 경우에 아이템을 획득할 수 있습니다. [단, 중간에 종료하거나 패배 하였을 경우에는 아이템을 얻지 못합니다.]
    /// </summary>
    public void SaveInven()
    {
        TestCode_SaveManager.Instance.InvenSave();
    }

    public void StartSetting()
    {
        invenObject[0].gameObject.SetActive(false);
        invenObject[1].gameObject.SetActive(true);
        invenObject[2].gameObject.SetActive(false);
        settingCount = 1;
    }
    public void StartButtonSetting()
    {
        invenObject[0].gameObject.SetActive(true);
        invenObject[1].gameObject.SetActive(false);
        invenObject[2].gameObject.SetActive(true);
        item_Panel[2].gameObject.SetActive(false);
        settingCount = 2;
    }
}
