using UnityEngine;
using System.Collections;

public class Armor : MonoBehaviour {

    public Material targetMaterial;
    public Shader shader;

    public float test;

	// Use this for initialization
	void Start () {

        test = 1;
        targetMaterial.SetFloat("_node_3052", test);
        
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0))
        {
            Debug.Log("눌림");
            test -= Time.deltaTime;
            targetMaterial.SetFloat("_node_3052", test);
        }
	
	}
}
