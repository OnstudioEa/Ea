using UnityEngine;
using System.Collections;

public class UIScript : MonoBehaviour {
    public UIManager uiManager;

    public GameObject thisOb;

	void Start () {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        thisOb = this.gameObject;
	}

    void OnClick()
    {
        uiManager.arrowPos[0] = thisOb;
        uiManager.UIPosConvey();
    }
}
