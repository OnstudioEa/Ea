using UnityEngine;
using System.Collections;

public class MovieManager : MonoBehaviour {
    
    // Use this for initialization
    void Awake()
    {
        ((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
    }
}
