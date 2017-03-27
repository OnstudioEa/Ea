using UnityEngine;
using System.Collections;

public class PlayerSoundManager : MonoBehaviour {

    public AudioClip[] player_Sound;
    
    /// <summary>
    /// 공격1
    /// </summary>
    public void Attack_1()
    {
        AudioSource.PlayClipAtPoint(player_Sound[0], transform.position, 1);
    }
    /// <summary>
    /// 공격2
    /// </summary>
    public void Attack_2()
    {
        AudioSource.PlayClipAtPoint(player_Sound[1], transform.position, 1);
    }
    /// <summary>
    /// 공격3
    /// </summary>
    public void Attack_3()
    {
        AudioSource.PlayClipAtPoint(player_Sound[2], transform.position, 1);
    }
    public void Skill1_1()
    {
        AudioSource.PlayClipAtPoint(player_Sound[3], transform.position, 1);
    }
    public void Skill1_2()
    {
        AudioSource.PlayClipAtPoint(player_Sound[4], transform.position, 1);
    }
    public void Skill1_3()
    {
        AudioSource.PlayClipAtPoint(player_Sound[5], transform.position, 1);
    }
    public void Skill2_1()
    {
        AudioSource.PlayClipAtPoint(player_Sound[8], transform.position, 1);
    }
    public void PlayerDie()
    {
        AudioSource.PlayClipAtPoint(player_Sound[6], transform.position, 1f);
    }
    public void Ultimated()
    {
        AudioSource.PlayClipAtPoint(player_Sound[7], transform.position, 1f);
    }
    public void Action()
    {
        AudioSource.PlayClipAtPoint(player_Sound[9], transform.position, 1f);
    }
}
