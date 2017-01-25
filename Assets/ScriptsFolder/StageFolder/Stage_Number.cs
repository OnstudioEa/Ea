using UnityEngine;
using System.Collections;

public class Stage_Number : MonoBehaviour {

    public GameObject[] stage_Object;

	// Use this for initialization
	void Awake () {

        PlayerPrefs.SetInt("Stage", PlayerPrefs.GetInt("Stage"));

        if (PlayerPrefs.GetInt("Stage") == 1)
            stage_Object[0].gameObject.SetActive(true);
        if (PlayerPrefs.GetInt("Stage") == 2)
            stage_Object[1].gameObject.SetActive(true);
    }
}
