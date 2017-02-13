using UnityEngine;
using System.Collections;

/// 

/// Item 클래스. 
/// 각 아이템은 이 클래스를 가지고 있습니다.
/// 

public class ItemScript : MonoBehaviour
{
    public enum State
    {
        idle
    }
    public State state;
    // 아이콘을 표시할 스프라이트 이름입니다.
    private string m_strSpriteName;
    // 아이콘을 표시하는 Sprite 클래스 입니다.
    // 여기에 아이템 아이콘 이미지 세팅 할꺼에요.
    public UISprite m_sprIcon;
    public UILabel m_label;

    // public InventoryController inventoryCtrl;
    void Awake()
    {
        state = State.idle;
        // StartCoroutine(FSM());

        // tT = GameObject.Find("Inventory Manager").GetComponent<TestCode_SaveManager>().Test_int;
    }
    /*IEnumerator FSM()
    {
        while (true)
        {
            yield return StartCoroutine(state.ToString());
        }
    }
    IEnumerator idle()
    {
        if (state == State.idle)
        {

        }
            yield return new WaitForSeconds(0.1f);
    }*/
    // 정보를 설정하는 함수 입니다.
    public void SetInfo(string spriteName)
    {
        // 같은 아틀라스에 있으니 스프라이트 이름 찾아 넣어주면 이미지가 바껴요.
        m_sprIcon.spriteName = spriteName;
    }

    public void SetInfo_1(int spriteValue)
    {
        m_label.text = spriteValue.ToString();
    }
    public void DestroyItem()
    {
        Destroy(this.gameObject);
    }
}
