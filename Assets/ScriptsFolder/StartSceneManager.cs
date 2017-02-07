using UnityEngine;
using System.Collections;

public class StartSceneManager : MonoBehaviour {

    public UIPanel panel;
    public UIButton gameStartButtonColl;
    public UISprite touch;

    public float timeCount;

	// Use this for initialization
	void Awake() {

        panel.gameObject.SetActive(true);
        touch.gameObject.SetActive(false);
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
                touch.gameObject.SetActive(true);
                panel.gameObject.SetActive(false);
            }
        }
	
	}
}
