using UnityEngine;
using System.Collections;

public class StartSceneManager : MonoBehaviour {

    public UIPanel panel;
    public UIButton gameStartButtonColl;

    public UIPanel title_Panel;

    public float timeCount;

	// Use this for initialization
	void Awake() {

        title_Panel.gameObject.SetActive(false);
        panel.gameObject.SetActive(true); // 온스튜디오 이미지
        gameStartButtonColl.gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {

        if (timeCount > 0)
        {
            timeCount -= Time.deltaTime;
            if (timeCount <= 0)
            {
                gameStartButtonColl.gameObject.SetActive(true);
                panel.gameObject.SetActive(false);
                title_Panel.gameObject.SetActive(true);
            }
        }

    }
}
