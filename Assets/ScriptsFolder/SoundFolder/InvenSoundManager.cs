using UnityEngine;
using System.Collections;

public class InvenSoundManager : MonoBehaviour {

    public AudioClip[] sound;

	void Start () {
        AudioListener.volume = 1;
        FlagSound();
    }
	public void FlagSound()
    {
        AudioSource.PlayClipAtPoint(sound[0], transform.position, 1f);
    }
}
