using UnityEngine;
using System.Collections;

public class MonsterSoundManager : MonoBehaviour {

    public AudioClip[] monster_Sound;
    
    public void MonsterDie()
    {
        AudioSource.PlayClipAtPoint(monster_Sound[0], transform.position, 0.5f);
    }
    public void MonsterAttack1()
    {
        AudioSource.PlayClipAtPoint(monster_Sound[1], transform.position, 0.5f);
    }
    public void MonsterAttack2()
    {
        AudioSource.PlayClipAtPoint(monster_Sound[2], transform.position, 0.5f);
    }
    public void MonsterJumpAttack()
    {
        AudioSource.PlayClipAtPoint(monster_Sound[3], transform.position, 0.5f);
    }
    public void BackAttack()
    {
        AudioSource.PlayClipAtPoint(monster_Sound[4], transform.position, 0.5f);
    }
   /* public void FinishSound()
    {
        AudioSource.PlayClipAtPoint(monster_Sound[4], transform.position, 1);
    }*/
}
