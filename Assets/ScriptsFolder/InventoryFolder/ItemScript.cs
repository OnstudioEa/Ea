using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour
{
    // 아이콘을 표시할 스프라이트 이름입니다.
    private string m_strSpriteName;
    // 아이콘을 표시하는 Sprite 클래스 입니다.
    // 여기에 아이템 아이콘 이미지 세팅 할꺼에요.
    public UISprite m_sprIcon;
    public UILabel m_label;

    public UISprite[] m_Up_sprIcon;
        
    void Awake()
    {

    }
    // 정보를 설정하는 함수 입니다.
    public void SetInfo(string spriteName)
    {
        // 같은 아틀라스에 있으니 스프라이트 이름 찾아 넣어주면 이미지가 바껴요.
        m_sprIcon.spriteName = spriteName;

        m_strSpriteName = spriteName;
    }

    public void SetInfo_1(int spriteValue)
    {
        m_label.text = spriteValue.ToString();
    }
    public void DestroyItem()
    {
        Destroy(this.gameObject);
    }
    void OnClick()
    {
        m_Up_sprIcon[0].gameObject.SetActive(true);
        m_Up_sprIcon[0].spriteName = m_strSpriteName;

        if (m_strSpriteName == "1002")
        {
            m_Up_sprIcon[1].spriteName = "1000";
            m_Up_sprIcon[2].spriteName = "WeaPone";
            m_Up_sprIcon[1].gameObject.SetActive(true);
            m_Up_sprIcon[2].gameObject.SetActive(true);
        }
        else
        {
            m_Up_sprIcon[1].gameObject.SetActive(false);
            m_Up_sprIcon[2].gameObject.SetActive(false);
        }
        
    }
}
