using UnityEngine;
using System.Collections;

public class Monster_Sanctum : MonoBehaviour {

    Animator ani;
    int animationCount;

	// Use this for initialization
	void Awake () {
        ani = GetComponent<Animator>();
	}
	
    public void MonsterButtonOn()
    {
        animationCount = Random.Range(0, 2);
        if (animationCount == 0)
        ani.SetBool("TestAttack", true);
        if (animationCount == 1)
            ani.SetBool("TestAttack1", true);
    }
    public void MonsterButtonOff()
    {
        ani.SetBool("TestAttack", false);
        ani.SetBool("TestAttack1", false);
    }
}
