using UnityEngine;
using System.Collections;

public class LookCam : MonoBehaviour {

    public GameObject target_Player;

    Transform trans;
    
	// Use this for initialization
	void Start () {
        trans = transform;
	}
	
	// Update is called once per frame
	void Update () {

        trans.LookAt(target_Player.transform);
	
	}
}
