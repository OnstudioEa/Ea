using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class MovieManager : MonoBehaviour {
    
    // Use this for initialization
    void Awake()
    {
        Handheld.PlayFullScreenMovie("title.mp4", Color.black, FullScreenMovieControlMode.Hidden);
    }
}
