using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{
    public State state;
    public Animator ani;

    public bool attackCheck;
    public bool skillCheck;
    public bool defendCheck;
        
    GameObject weaponeObMax;
    GameObject weaponeObMin;
    GameObject targetMonster;
    GameObject weaponeSlash;

    Transform target;
    Transform trans;
           
    float closestDistSqr;
    float dist;
    float dist_1;
    float distPos;

    public int hitCount;

    public BoxCollider attack_Coll;
    public BoxCollider skill_1_Coll;
    public BoxCollider skill_2_Coll;
    public BoxCollider defend_Coll;

    public GameObject[] playerEffect;

    public AudioClip[]  player_Sound;
    int                 player_Sound_Count;
    AudioClip test;

    public int          attackCount_Effect;

    public GameObject   taggedAction;
    List<Action> action = new List<Action>();

    PlayerDataLoader        playerAttackData;
    VirtualJoysticks        virtualjoystick;
    public Motor            motor;
    
    void Awake()
    {      
        state = State.idle;
        //StartCoroutine(FSM());
        ani = GetComponent<Animator>();

        playerAttackData = GameObject.Find("GameManager").GetComponent<PlayerDataLoader>();
        virtualjoystick = GameObject.Find("BackgroundImage").GetComponent<VirtualJoysticks>();

        weaponeObMax = GameObject.Find("B");
        weaponeObMin = GameObject.Find("A");
        targetMonster = GameObject.Find("Monster");
        weaponeSlash = GameObject.Find("trail_bone");
        
        target = targetMonster.transform;
        trans = gameObject.transform;

        weaponeSlash.gameObject.SetActive(false);
        closestDistSqr = Mathf.Infinity;
        distPos = 0.7f;
        
        CheckList();
        ani.SetBool("AttackMove", true);
    }
    void CheckList()
    {
        if (taggedAction.name == "GameManager")
        {
            action.Add(taggedAction.GetComponent<PlayerDataLoader>());
        }
    }
    void TargetCheck()
    {
        if (targetMonster != null)
        {
            Vector3 obPos = targetMonster.transform.position;
            dist = (obPos - weaponeObMax.transform.position).sqrMagnitude;
            dist_1 = (obPos - weaponeObMin.transform.position).sqrMagnitude;
            if (dist < distPos || dist_1 < distPos)
            {
                if (dist < closestDistSqr)
                {
                    action[0].PlayerDamage();
                    if (attackCount_Effect == 1)
                        playerEffect[4].gameObject.SetActive(true);
                    if (attackCount_Effect == 2)
                        playerEffect[5].gameObject.SetActive(true);
                    if (attackCount_Effect == 3)
                        playerEffect[6].gameObject.SetActive(true);
                }
            }
        }
    }
    /*  IEnumerator FSM()
      {
          while (true)
          {
              yield return StartCoroutine(state.ToString());
          }
      }
      IEnumerator idle()
      {
          if (state == State.idle)
          {

          }

          yield return new WaitForSeconds(0.5f);
      }
      IEnumerator attack()
      {
          if (state == State.attack)
          {

          }
          yield return new WaitForSeconds(0.11f);
      }*/
    public void AttackButton()
    {
        if (attackCheck)
        {
            ani.SetBool("Attack1", true);
            ani.SetBool("Attack2", true);
            ani.SetBool("Attack3", true);
            motor.moveSpeed = 0;
        }
    }
    public void AttackStart()
    {
        ani.SetBool("Attack1", false);
        ani.SetBool("Attack2", false);
        ani.SetBool("Attack3", false);
        ani.SetBool("AttackMove", false);
        attackCheck = false;
    }
    public void AttackEnd()
    {
        state = State.idle;
        // 액션,애니메이션
        skillCheck = false;
        attackCheck = true;
        defendCheck = false;
        ani.SetBool("AttackMove", true);
        ani.SetBool("Attack1", false);
        ani.SetBool("Attack2", false);
        ani.SetBool("Attack3", false);
        ani.SetBool("Hit", false);
        // 버튼콜린더
        attack_Coll.enabled = true;
        defend_Coll.enabled = true;
        // 이펙트
        playerEffect[0].gameObject.SetActive(false);

        attackCount_Effect = 1;
        playerEffect[4].gameObject.SetActive(false);
        if (ani.GetBool("Skill1") == true || ani.GetBool("Skill2") == true)
        {
            playerEffect[11].gameObject.SetActive(false);
            playerEffect[12].gameObject.SetActive(false);
            ani.SetBool("Skill1", false);
            ani.SetBool("Skill2", false);
            ani.SetBool("Hit", false);
            return;
        }
        if (ani.GetBool("Ultimated") == true)
        {
            ani.SetBool("Hit", false);
            ani.SetBool("Ultimated", false);
            playerAttackData.cam_3.gameObject.SetActive(false);
            playerEffect[9].gameObject.SetActive(false);
            playerEffect[10].gameObject.SetActive(false);
            playerAttackData.cam_1.gameObject.SetActive(true);
        }
    }
    public void AttackFalse()
    {
        ani.SetBool("Attack1", false);
        ani.SetBool("Attack2", false);
        ani.SetBool("Attack3", false);
        attackCheck = false;
        defendCheck = false;
        skillCheck = false;
        ani.SetBool("AttackMove", true);
    }
    public void IdleStart()
    {
        state = State.idle;
        skillCheck = false;
        weaponeSlash.gameObject.SetActive(false);
    }
    public void AttackTrue()
    {
        state = State.idle;
        attackCheck = true;
    }
    public void AttackHit()
    {
        TargetCheck();
        PlayerSound();
    }
    public void MoveSpeedCheck()
    {
        motor.moveSpeed = 7;
    }
    public void MoveSpeedCheckOff()
    {
        motor.moveSpeed = 0;
    }
    public void DefendButtonOn()
    {
        if (ani.GetBool("Defend") == false && playerAttackData.playerNowMP > 5)
        {
            ani.SetBool("Defend", true);
            ani.SetBool("AttackMove", true);
        }
    }
    /// <summary>
    /// 방어관련
    /// </summary>
    public void DefendButtonOff()
    {
        ani.SetBool("Defend", false);
        if (motor.state == Motor.State.idle)
        {
            ani.SetBool("AttackMove", true);
            defendCheck = false;
        }
    }
    /// <summary>
    /// 구르기
    /// </summary>
    public void DefendStart()
    {
        defendCheck = true;
        playerAttackData.playerNowMP -= 5;
        motor.moveSpeed = 7; // 수치
        skillCheck = true;
        weaponeSlash.gameObject.SetActive(false);
        playerEffect[0].gameObject.SetActive(false);
    }
    /// <summary>
    ///  막기
    /// </summary>
    public void DefendStart_1()
    {
        defendCheck = true;
        weaponeSlash.gameObject.SetActive(false);
        attackCheck = true;
        attackCount_Effect = 1;

        playerEffect[4].gameObject.SetActive(false);
        playerEffect[0].gameObject.SetActive(false);

        ani.SetBool("Attack1", false);
        ani.SetBool("Attack2", false);
        ani.SetBool("Attack3", false);
        playerEffect[3].gameObject.SetActive(false);

        motor.moveSpeed = 7; // 수치
    }
    public void DefendCollOn()
    {
        defend_Coll.enabled = true;
    }
    public void DefendCollOff()
    {
        if (motor.state == Motor.State.move)
            defend_Coll.enabled = false;
    }
    /// <summary>
    /// 스킬 버튼
    /// </summary>
    public void SkillButton1()
    {
        if (ani.GetBool("Skill2") == false)
        {
            ani.SetBool("Skill1", true);
            skillCheck = true;
            attack_Coll.enabled = false;
            skill_1_Coll.enabled = false;
            defend_Coll.enabled = false;
            playerAttackData.skillDelayTime_1 = 14;
        }
    }
    public void SkillButton2()
    {
        if (ani.GetBool("Skill1") == false)
        {
            ani.SetBool("Skill2", true);
            attack_Coll.enabled = false;
            skill_2_Coll.enabled = false;
            defend_Coll.enabled = false;
            playerAttackData.skillDelayTime_2 = 18;

            motor.moveSpeed = 7;
            ani.SetBool("AttackMove", true);
        }
    }
    public void SkillEffect()
    {
        //3단공격
        if (ani.GetBool("Skill1") == true)
        {
            playerEffect[11].gameObject.SetActive(true);
           // skill_1.gameObject.SetActive(true);
        }
        else
        {
            playerEffect[12].gameObject.SetActive(true);
          //  skill_2.gameObject.SetActive(true);
        }
    }
    public void SkillManagerOn()
    {
        skillCheck = true;
    }
    public void SkillManagerOff()
    {
        skillCheck = false;
    }
    /// <summary>
    /// 애니메이션 이벤트 및 이펙트 관련
    /// </summary>
    public void OnAttack_B()
    {
        if (playerAttackData.ultimateTime <= 0)
        {
            weaponeSlash.gameObject.SetActive(true);
        }
    }
    public void OffAttack_B()
    {
        weaponeSlash.gameObject.SetActive(false);
        DefendCollOn();
    }
    /// <summary>
    /// 이펙트 관련
    /// </summary>
    public void AttackEffect_1()
    {
        state = State.attack;
        DefendCollOff();
        attackCount_Effect = 1;
        playerEffect[5].gameObject.SetActive(false);
        if (playerAttackData.ultimateTime > 0)
        {
            if (ani.GetBool("Attack1") == true)
            {
                playerEffect[0].gameObject.SetActive(true);
                playerEffect[1].gameObject.SetActive(false);
            }
        }
        else
            return;
    }
    public void AttackEffect_2()
    {
        state = State.attack;
        DefendCollOff();
        attackCount_Effect = 2;
        playerEffect[6].gameObject.SetActive(false);
        if (playerAttackData.ultimateTime > 0)
        {
            if (ani.GetBool("Attack2") == true)
            {
                playerEffect[1].gameObject.SetActive(true);
                playerEffect[2].gameObject.SetActive(false);
            }
        }
        else
            return;
    }
    public void AttackEffect_3()
    {
        state = State.attack;
        DefendCollOff();
        attackCount_Effect = 3;
        playerEffect[4].gameObject.SetActive(false);
        if (playerAttackData.ultimateTime > 0)
        {
            if (ani.GetBool("Attack3") == true)
            {
                playerEffect[2].gameObject.SetActive(true);
                playerEffect[0].gameObject.SetActive(false);
            }
        }
        else
            return;
    }
    /// <summary>
    /// 특정 조이스틱 범위위치 공격시 자동타겟
    /// </summary>
    public void AttackActionLook()
    {
        float dist = Vector3.Distance(target.position, trans.position);
        if (dist < 1)
        {
            if (virtualjoystick.inputVector.z >= 0.6f && virtualjoystick.inputVector.z <= 1.0f)
            {
                PlayerDefendLook();
            }
            else
                motor.FaceMovementDirection();
        }
        else
            motor.FaceMovementDirection();
    }
    /// <summary>
    /// 겨루기시 방향
    /// </summary>
    public void AttackLook()
    {
        motor.moveJoystick.inputVector = Vector3.zero;
        motor.moveJoystick.joystickImg.rectTransform.anchoredPosition = Vector3.zero;
        motor.moveSpeed = 0;
        motor.state = Motor.State.idle;
        ani.SetBool("Move", false);
        ani.SetBool("Win", false);
        ani.SetBool("Lose", false);
        skillCheck = true;
        Handheld.Vibrate(); //진동

        transform.LookAt(targetMonster.transform);
    }
    
    /// <summary>
    /// 피격모션 시작 이벤트
    /// </summary>
    public void PlayerHitMotion()
    {
        motor.moveJoystick.inputVector = Vector3.zero;
        motor.moveJoystick.joystickImg.rectTransform.anchoredPosition = Vector3.zero;
        motor.moveSpeed = 0;
        motor.state = Motor.State.idle;
        ani.SetBool("Move", false);
        skillCheck = true;
        DefendCollOff();
        transform.LookAt(targetMonster.transform);
    }
    public void PlayerDefendLook()
    {
        transform.LookAt(targetMonster.transform);
    }
    /// <summary>
    /// 피격모션에 끝날때 관련된 이벤트
    /// </summary>
    public void EndHitMotion()
    {
        hitCount = 15;
        skillCheck = false;
        defendCheck = false;
        motor.moveSpeed = 7;
        ani.SetBool("Hit", false);
        ani.SetBool("Hit1", false);
        DefendCollOn();
        AttackEnd();
    }
    /// <summary>
    /// 변신이펙트 이벤트
    /// </summary>
    public void UltimatedAction()
    {
        //메터리얼
        playerAttackData.monsterShaderChange_2.rend.sharedMaterial = playerAttackData.monsterShaderChange_2.material[2];
        playerAttackData.monsterShaderChange_3.rend.sharedMaterial = playerAttackData.monsterShaderChange_3.material[2];
        playerAttackData.monsterShaderChange_4.rend.sharedMaterial = playerAttackData.monsterShaderChange_4.material[2];

        // 이펙트
        playerEffect[7].gameObject.SetActive(true);
        playerEffect[8].gameObject.SetActive(true);
        
    }
    /// <summary>
    /// 플레이어 컷씬
    /// </summary>
    public void CutAction()
    {
        ani.SetBool("Win", false);
        ani.SetBool("Lose", false);
    }
    /// <summary>
    /// 공격 사운드 관련
    /// </summary>
    public void PlayerSound()
    {
        player_Sound_Count = Random.Range(0, 2);
        if (player_Sound_Count == 0)
            AudioSource.PlayClipAtPoint(player_Sound[0], trans.position);
        else
            if (player_Sound_Count == 1)
            AudioSource.PlayClipAtPoint(player_Sound[1], trans.position);
    }
}



