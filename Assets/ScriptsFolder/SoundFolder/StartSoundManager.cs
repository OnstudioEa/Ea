using UnityEngine;
using System.Collections;

public class StartSoundManager : MonoBehaviour {
    public GameObject[] soundOb;
    
    void Start()
    {
        AudioListener.volume = 1;
        soundOb[0].gameObject.SetActive(false);
        soundOb[1].gameObject.SetActive(false);
    }

    public void SoundOn()
    {
        soundOb[0].gameObject.SetActive(true);
    }
    public void TitleSound()
    {
        soundOb[1].gameObject.SetActive(true);
    }
}
