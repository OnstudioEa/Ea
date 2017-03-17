using UnityEngine;
using System.Collections;

public class SanctumScript : MonoBehaviour {
    
    public UIPanel selectStage_Panel;
    public UIPanel stage_Panel;
    public UIPanel world_Panel;
    public UIPanel dungeon_Panel;

    public UISprite[] spritOb;
    public UIPanel[] panelOb;
    
    public GameObject[] monsters;

    public int channerCount;

    void Awake () {

        channerCount = 1;
        StageStartButton();
        world_Panel.gameObject.SetActive(true);
        dungeon_Panel.gameObject.SetActive(false);

	}
    
    public void StageStartButton()
    {
        selectStage_Panel.gameObject.SetActive(true);
        world_Panel.gameObject.SetActive(true);
    }
    public void Stage_1Button()
    {
        world_Panel.gameObject.SetActive(false);
        spritOb[0].gameObject.SetActive(false);
        stage_Panel.gameObject.SetActive(true);
        spritOb[1].gameObject.GetComponent<TweenPosition>().Play(true);
        channerCount = 2;
    }
    public void DungeonButton()
    {
        stage_Panel.gameObject.SetActive(false);
        spritOb[0].gameObject.SetActive(true);
        dungeon_Panel.gameObject.SetActive(true);

        monsters[0].gameObject.SetActive(true);

        channerCount = 3;
    }
    public void BackButton()
    {
        if (channerCount == 3)
        {
            ObjectTween();
            channerCount = 2;
            stage_Panel.gameObject.SetActive(true);
            spritOb[0].gameObject.SetActive(false);
            dungeon_Panel.gameObject.SetActive(false);
            return;
        }
        else
        {
            if(channerCount == 2)
            {
                ObjectTween();
                channerCount = 1;
                world_Panel.gameObject.SetActive(true);
                spritOb[0].gameObject.SetActive(true);
                stage_Panel.gameObject.SetActive(false);
                return;
            }
        }
    }
    public void ObjectTween()
    {
        if (channerCount == 2)
        {
            panelOb[0].gameObject.GetComponent<TweenAlpha>().ResetToBeginning();
            panelOb[0].gameObject.GetComponent<TweenAlpha>().Play(true);
            spritOb[1].gameObject.GetComponent<TweenPosition>().ResetToBeginning();
            return;
        }
        else
        {
            if(channerCount == 3)
            {
                spritOb[1].gameObject.GetComponent<TweenPosition>().ResetToBeginning();
                spritOb[1].gameObject.GetComponent<TweenPosition>().Play(true);
            }
        }
        Debug.Log("트윈 리셋 후 재실행");
    }
}
