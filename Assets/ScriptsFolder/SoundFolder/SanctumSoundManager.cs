using UnityEngine;
using System.Collections;

public class SanctumSoundManager : MonoBehaviour {

    public Animator ani;
    public AudioClip[] sound;

    void Start()
    {
        ani = GetComponent<Animator>();
        AudioListener.volume = 1;
    }
    public void FlagSound()
    {        
        AudioSource.PlayClipAtPoint(sound[0], transform.position, 1f);
    }
}