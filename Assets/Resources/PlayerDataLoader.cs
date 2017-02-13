using UnityEngine;
using System.Collections;

public class Action : MonoBehaviour
{
    public virtual void PlayerDamage()
    {

    }
    public virtual void MonsterDamage()
    {

    }
}
public class PlayerDataLoader : Action
{
    public State state;

    public const string path = "Manager";

    public int testNumber;

    public int playerLevel;
    public int playerHp;
    public float playerPower;

    public int testMonNumber;

    public int monsterLevel;
    public int monsterHp;
    public int monsterPower;
    //-------------------------------------------------Monster HP
    public UISlider monsterHPBar;
    public float monsterNowHP;
    private float monsterValue;
    //-------------------------------------------------Player MP
    public UISlider playerMPBar;
    float            playerMaxMP;
    public float     playerNowMP;
    private float    playerValue;
    //-------------------------------------------------Player HP
    public UISlider playerHPBar;
    float            playerNowHP;
    private float    playerHPValue;
    //-------------------------------------------------Animation
    public UISprite playerSkillBar;
    float            playerMaxSkill;
    public float     playerNowSkill;
    private float    playerValueSkill;
    //-------------------------------------------------Animation
    int                 animationDelay_Monster;
    public MonsterShaderChange monsterShaderChange;
    public MonsterShaderChange monsterShaderChange_1;
    int                 animationDelay_Player;
    public MonsterShaderChange monsterShaderChange_2;
    public MonsterShaderChange monsterShaderChange_3;
    public MonsterShaderChange monsterShaderChange_4;
    //-------------------------------------------------겨루기 관련
    public GameObject cam_1;
    public GameObject cam_2;
    public GameObject cam_3;

    public Transform player_OB;
    public Transform monster_OB;
    float             gorggyAttack_float;
    public bool      groggyAttack_bool;
    //-------------------------------------------------궁
    BoxCollider ultimateColl;
    public int  ultimateTime;
    //-------------------------------------------------메터리얼
    public Material[] player_Mt;
    float mt_float;
    public float mt_Time;
    float mt_float_1;
    //-------------------------------------------------크리티컬
    int     critical_Check;
    float    critical_Damage;

    int     critical_Check_Monster;
    float    critical_Damage_Monster;
    //-------------------------------------------------스킬
    int         skill_Count;
    public float skillDelayTime_1;
    public float skillDelayTime_2;
    //-------------------------------------------------그로기시스템 관련
    int groggyCount;
    float parts_HP;
    int parts_Count;
    public int buttonActionGayge;
    public GameObject partsOb;
    //-------------------------------------------------라벨 관련
    public UILabel skill_Label_1;
    public UILabel skill_Label_2;
    public UILabel playerHP_Label;
    public UILabel playerMP_Label;
    public UILabel monsterHP_Label;

    public UIPanel[] uiPanel;
    public UIPanel    buttonActionPanel;
    public UIPanel    sliderPanel;
    public UIPanel    buttonPanelActive_1;
    public GameObject buttonPanelActive_2;

    public GameObject[] taggedAction;

    PlayerControl   playerControl;
    MonsterControl  monsterControl;
    ShakeCamera     shakeCam;
    Animator        ani_Player;
    Animator        ani_Monster;
    public Motor    motor;

    void Awake()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        monsterControl = GameObject.Find("Monster").GetComponent<MonsterControl>();

        ani_Player = GameObject.Find("Player").GetComponent<Animator>();
        ani_Monster = GameObject.Find("Monster").GetComponent<Animator>();
        shakeCam = GameObject.Find("CamManager").GetComponent<ShakeCamera>();

        ultimateColl = GameObject.Find("UltimateButton").GetComponent<BoxCollider>();
        
        player_Mt[0].SetFloat("_node_3052", 0);
        player_Mt[1].SetFloat("_node_3052", 0);
        player_Mt[2].SetFloat("_node_3052", 0);
        player_Mt[3].SetFloat("_node_3052", 1);
        player_Mt[4].SetFloat("_node_3052", 1);
        player_Mt[5].SetFloat("_node_3052", 1);
        mt_float_1 = 1;

        player_OB = player_OB.transform;
        monster_OB = monster_OB.transform;

        ultimateColl.enabled = false;
        buttonActionPanel.gameObject.SetActive(false);
        cam_3.gameObject.SetActive(false);

        state = State.idle;
        StartCoroutine(FSM());
        DataLoader();

        monsterNowHP = monsterHp;
        playerNowHP = playerHp;
        playerMaxMP = 20;
        playerNowMP = playerMaxMP;
        playerMaxSkill = 100;
        playerNowSkill = playerMaxSkill;
        playerNowSkill = 0;
        groggyCount = 2;
        parts_Count = 1;
        parts_HP = 900;
    }
    void DataLoader()
    {
        PlayerDataContainer gameData = PlayerDataContainer.Load(path);

        foreach (PlayerData data in gameData.playerData)
        {
            for (int i = 0; i <= data.level; i++)
            {
                if (data.level == testNumber)
                {
                    playerLevel = data.level;
                    playerHp = data.playerHp;
                    playerPower = data.power;
                }
                if (data.monsterLevel == testMonNumber)
                {
                    monsterLevel = data.monsterLevel;
                    monsterHp = data.monsterHp;
                    monsterPower = data.monsterPower;
                }
            }
        }
    }
    public override void PlayerDamage()
    {
        AttackDamage_Player();

        if (playerNowSkill < 100)
            playerNowSkill += 3;
        else
            if (playerNowSkill >= 100)
        {
            ultimateColl.enabled = true;
            taggedAction[2].gameObject.SetActive(true);
        }
        if (ultimateTime > 0)
        {
            playerNowHP += 10;
        }

        animationDelay_Monster = 2;
        ani_Monster.speed = 0.3f;
        monsterShaderChange.rend.sharedMaterial = monsterShaderChange.material[1];
        monsterShaderChange_1.rend.sharedMaterial = monsterShaderChange_1.material[1];

        if (ani_Monster.GetBool("Idle") == true)
        {
            // 플레이어 타격 진동
            shakeCam.ShakeCam(0.02f, 0.1f);
        }
        GroggyAndCompete();
    }
    public override void MonsterDamage()
    {
        if (playerNowSkill < 100)
            playerNowSkill += 2;
        else
            if (playerNowSkill >= 100)
            ultimateColl.enabled = true;

        if (playerControl.defendCheck == false && ultimateTime <= 0 && playerControl.ani.GetBool("Defend") == false) // 막기상태가 아닐때 데미지를 받는다. [평시]
        {
            if (playerControl.hitCount <= 0)
            {
                playerControl.PlayerHitMotion();
                if (ani_Monster.GetBool("BackAttack") == true || ani_Monster.GetBool("Attack3") == true)
                {
                    ani_Player.SetBool("Hit1", true);
                }
                else
                {
                    ani_Player.SetBool("Hit", true);
                }
            }

            animationDelay_Player = 2;
            monsterShaderChange_2.rend.sharedMaterial = monsterShaderChange_2.material[1];
            monsterShaderChange_3.rend.sharedMaterial = monsterShaderChange_3.material[1];
            monsterShaderChange_4.rend.sharedMaterial = monsterShaderChange_4.material[1];
            AttackDamage_Monster();
        }
        else
        {
            if (playerControl.defendCheck == false && playerNowHP > playerHp * 0.1f && playerControl.ani.GetBool("Defend") == false) //[궁시전] 체력 10%이하로 달지않음
            {
                AttackDamage_Monster();
            }
        }
        if (playerControl.defendCheck == true) // 막았을때에는 이펙트 발생
        {
            //   playerControl.playerEffect[3].gameObject.SetActive(true);
            playerControl.Defend_Effect_On();
            playerControl.PlayerDefendLook();
        }
    }
    IEnumerator FSM()
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
            HPBar();
            SkillDelayTimeManager();
            MaterialManager();
            MonsterGroggyAttack();
            
            if (playerNowMP < 0)
                playerControl.ani.SetBool("Defend", false);
            if (playerNowMP < 20)
                playerNowMP += 0.2f;
            if (buttonActionGayge > -10)
            {
                buttonActionGayge -= 2;
            }
            if (playerControl.hitCount > 0)
                playerControl.hitCount -= 1;
        }
        yield return new WaitForSeconds(0.1f);
    }
    /// <summary>
    /// HP에 관련된 함수
    /// </summary>
    public void HPBar()
    {
        // 플레이어 체력
        playerValue = playerNowHP / (float)(playerHp);
        playerHPBar.value = playerValue;
        // 플레이어 마나
        playerValue = playerNowMP / (float)(playerMaxMP);
        playerMPBar.value = playerValue;
        // 몬스터 체력
        monsterValue = monsterNowHP / (float)(monsterHp);
        monsterHPBar.value = monsterValue;
        // 플레이어 스킬 게이지
        playerValueSkill = playerNowSkill / (float)(playerMaxSkill);
        playerSkillBar.fillAmount = playerValueSkill;

        // 플레이어 방어
        if (ani_Player.GetBool("Move") == false && ani_Player.GetBool("Defend") == true && playerNowMP > 0)
        {
            playerNowMP -= 1;
        }
        //스킬라벨 관련
        skill_Label_1.text = skillDelayTime_1.ToString("f0");
        skill_Label_2.text = skillDelayTime_2.ToString("f0");
        playerHP_Label.text = playerNowHP.ToString("f0") + "/" + playerHp;
        playerMP_Label.text = playerNowMP.ToString("f0") + "/" + playerMaxMP;
        monsterHP_Label.text = monsterNowHP.ToString("f0") + "/" + monsterHp;
        //몬스터 메터리얼 관련 죽은 후 셰이더
        if (mt_Time > 0)
        {
            mt_Time -= 0.1f;

            mt_float_1 -= 0.02f;
            player_Mt[3].SetFloat("_node_3052", mt_float_1);
            player_Mt[4].SetFloat("_node_3052", mt_float_1);
            player_Mt[5].SetFloat("_node_3052", mt_float_1);

            playerControl.camObject[2].gameObject.SetActive(false);
            playerControl.camObject[0].gameObject.SetActive(true);
            Time.timeScale = 0.3f;
            if (mt_Time <= 6.5f) //카메라_1
            {
                playerControl.camObject[0].gameObject.SetActive(false);
                playerControl.camObject[1].gameObject.SetActive(true);
            }
            if (mt_Time <= 6f) //카메라_2
            {
                cam_1.gameObject.SetActive(true);
                playerControl.camObject[1].gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
            if (mt_Time <= 5f) //카메라_2
            {
                taggedAction[3].gameObject.SetActive(true);
                uiPanel[1].gameObject.SetActive(true);
            }
            if (mt_Time <= 0)
            {
                taggedAction[0].gameObject.SetActive(false);
                taggedAction[3].gameObject.SetActive(false);
                uiPanel[0].gameObject.SetActive(true);

                player_Mt[3].SetFloat("_node_3052", 1);
                player_Mt[4].SetFloat("_node_3052", 1);
                player_Mt[5].SetFloat("_node_3052", 1);

                Debug.Log("끝났습니다~");
            }
        }
    }
    /// <summary>
    /// 그로기 기습공격 관련
    /// </summary>
    void MonsterGroggyAttack()
    {
        if (gorggyAttack_float > 0 && groggyAttack_bool == true)
        {
            gorggyAttack_float -= 1;
        }
        else { 
        if (gorggyAttack_float <= 0 && groggyAttack_bool == true)
        {
                if (ani_Monster.GetBool("Groggy") == true && groggyAttack_bool == true)
                {
                    float dist = Vector3.Distance(player_OB.position, monster_OB.position);
                    if (dist <= 1)
                    {
                        ani_Monster.SetBool("GroggyAttack", true);
                        playerControl.AttackLook();
                        monsterControl.AttackLook();
                        groggyAttack_bool = false;
                        cam_1.gameObject.SetActive(false);
                        cam_2.gameObject.SetActive(true);

                        groggyCount -= 1;
                    }
                }
            }
        }
    }    
    public void ButtonActionOn()
    {
        buttonActionGayge += 5;
        if (buttonActionGayge >= 200)
        {
          //  Debug.Log("버튼액션 이김");
            motor.moveSpeed = 7;
          //  buttonActionGayge = 0;

            playerControl.AttackEnd();
            monsterControl.AttackEnd();
            ani_Monster.SetBool("Groggy", false);
            ani_Monster.SetBool("GroggyAttack", false);
            ani_Player.SetBool("Defend_1", false);
            buttonActionPanel.gameObject.SetActive(false);
            //cam_1.gameObject.SetActive(true);
            playerControl.camObject[2].gameObject.SetActive(true);
            cam_2.gameObject.SetActive(false);

            ani_Player.SetBool("Win", true);
            ani_Monster.SetBool("Lose", true);
                        
            monsterNowHP = 0;
        }
        if (buttonActionGayge <= 0)
        {
         //   Debug.Log("버튼액션 짐");
            motor.moveSpeed = 7;

            playerControl.AttackEnd();
            monsterControl.AttackEnd();
            ani_Monster.SetBool("Groggy", false);
            ani_Monster.SetBool("GroggyAttack", false);
            ani_Player.SetBool("Defend_1", false);
            buttonActionPanel.gameObject.SetActive(false);
            sliderPanel.gameObject.SetActive(true);
            buttonPanelActive_1.gameObject.SetActive(true);
            buttonPanelActive_2.gameObject.SetActive(true);
            cam_1.gameObject.SetActive(true);
            cam_2.gameObject.SetActive(false);

            monsterControl.monsterEffect[1].gameObject.SetActive(false);

            ani_Monster.SetBool("Win", true);
            ani_Player.SetBool("Lose", true);

            groggyCount += 1;
            monsterNowHP = monsterHp * 0.4f;
        }
    }
    /// <summary>
    /// 스킬 딜레이타임에 관련된 함수
    /// </summary>
    public void SkillDelayTimeManager()
    {
        if (ultimateTime > 0)
        {//[궁온]
            ultimateTime -= 1;

            if (mt_float < 1)
            {
                // 쉐이더 수치가 1보다 작을때 실행 [궁온]
                mt_float += 0.05f;
                player_Mt[0].SetFloat("_node_3052", mt_float);
                player_Mt[1].SetFloat("_node_3052", mt_float);
                player_Mt[2].SetFloat("_node_3052", mt_float);
            }

            if (playerNowHP > playerHp * 0.1f)
            { //[궁온] 피가 1씩 닳음;
                playerNowHP -= 1f;
            }
            if (ultimateTime <= 0)
            {
                 //[궁오프] 원래대로 돌아오게 됨
                playerPower = playerPower * 0.5f;
                ani_Player.speed = 1f;

                monsterShaderChange_2.rend.sharedMaterial = monsterShaderChange_2.material[0];
                monsterShaderChange_3.rend.sharedMaterial = monsterShaderChange_3.material[0];
                monsterShaderChange_4.rend.sharedMaterial = monsterShaderChange_4.material[0];
                playerControl.playerEffect[7].gameObject.SetActive(false);
                playerControl.playerEffect[8].gameObject.SetActive(false);
                
                player_Mt[0].SetFloat("_node_3052", 0);
                player_Mt[1].SetFloat("_node_3052", 0);
                player_Mt[2].SetFloat("_node_3052", 0);
                mt_float = 0;
            }
        }

        if (skillDelayTime_1 > 0)
        {
            skill_Label_1.gameObject.SetActive(true);
            skillDelayTime_1 -= 0.1f;
            if (skillDelayTime_1 <= 0)
            {
                skill_Label_1.gameObject.SetActive(false);
                playerControl.skill_1_Coll.enabled = true;
            }
        }
        if (skillDelayTime_2 > 0)
        {
            skill_Label_2.gameObject.SetActive(true);
            skillDelayTime_2 -= 0.1f;
            if (skillDelayTime_2 <= 0)
            {
                skill_Label_2.gameObject.SetActive(false);
                playerControl.skill_2_Coll.enabled = true;
            }
        }
    }
    /// <summary>
    /// 플레이어 데미지전달 관련
    /// </summary>
    void AttackDamage_Player()
    {
        //monsterNowHP -= playerPower;
        if (monsterNowHP <= monsterHp * 0.2f && groggyCount == 1)
        {

        }
        else
        {
            if (ani_Player.GetBool("Skill1") == false && ani_Player.GetBool("Skill2") == false)
            {
                critical_Check = Random.Range(1, 11);
                if (critical_Check <= 3)
                {
                    critical_Damage = Random.Range(1.4f, 2.0f);
                    monsterNowHP -= (playerPower * critical_Damage);
                    //Debug.Log("크리티컬");
                    return;
                }
                else
                {
                    critical_Damage = Random.Range(0.8f, 1.0f);
                    monsterNowHP -= (playerPower * critical_Damage);
                    //Debug.Log("평타");
                }
                return;
            }

            if (ani_Player.GetBool("Skill1") == true)
            {
                skill_Count += 1;
                if (skill_Count == 1)
                {
                    monsterNowHP -= playerPower * 1.8f;
                    return;
                }
                else
                {
                    if (skill_Count == 2)
                    {
                        monsterNowHP -= playerPower * 1.9f;
                        return;
                    }
                    else
                    {
                        if (skill_Count == 3)
                        {
                            monsterNowHP -= playerPower * 2f;
                            skill_Count = 0;
                            return;
                        }
                    }
                }
            }

            if (ani_Player.GetBool("Skill2") == true)
            {
                monsterNowHP -= playerPower * 1.7f;
            }
        }
    }
    /// <summary>
    /// 몬스터 데미지전달 관련
    /// </summary>
    void AttackDamage_Monster()
    {
        if (ani_Player.GetBool("Skill1") == true || ani_Player.GetBool("Skill2") == true)
        {
            playerNowHP -= monsterPower * 0.5f;
        }
        else
        {
            critical_Check_Monster = Random.Range(1, 11);
            if (critical_Check_Monster <= 1)
            {
                critical_Damage_Monster = Random.Range(1.2f, 1.5f);
                playerNowHP -= (monsterPower * critical_Damage_Monster);
                return;
            }
            else
            {
                playerNowHP -= monsterPower;
                return;
            }
        }
    }
    /// <summary>
    /// 그로기 및 겨루기 관련
    /// </summary>
    void GroggyAndCompete()
    {
        if (parts_Count == 1)
        {
            if (parts_HP <= 0)
            {
                parts_Count -= 1;
                ani_Monster.SetBool("PartsD", true);
                partsOb.gameObject.SetActive(false);
            }
            else
            {
                if (monsterNowHP <= monsterHp * 0.2f && groggyCount == 1)
                {

                }
                else
                if (ani_Monster.GetBool("Groggy") == true && parts_Count == 1 && groggyCount == 1)
                    parts_HP -= playerPower;
            }
        }
        if (monsterNowHP <= monsterHp * 0.5f && groggyCount == 2)
        {
            ani_Monster.SetBool("Groggy", true);
            groggyCount -= 1;
            return;
        }
        else if (monsterNowHP <= monsterHp * 0.2f && groggyCount == 1)
        {
            ani_Monster.SetBool("Groggy", true);
            gorggyAttack_float = 13;
            groggyAttack_bool = true;
            //groggyCount -= 1;
            return;
        }
    }
    /// <summary>
    /// 궁극기 관련
    /// </summary>
    public void UltimateButton()
    {
        ani_Player.SetBool("Ultimated", true);
        ani_Player.SetBool("AttackMove", false);

        playerControl.playerEffect[9].gameObject.SetActive(true);
        taggedAction[2].gameObject.SetActive(false);
        cam_3.gameObject.SetActive(true);
        cam_1.gameObject.SetActive(false);

        ultimateColl.enabled = false;

        playerPower = playerPower * 2;
        ani_Player.speed = 1.25f;
        playerNowSkill = 0;
        ultimateTime = 200;                
    }
    /// <summary>
    /// 메터리얼에 관련
    /// </summary>
    void MaterialManager()
    {
        if (animationDelay_Monster > 0)
        {
            animationDelay_Monster -= 1;
            if (animationDelay_Monster <= 0)
            {
                monsterShaderChange.rend.sharedMaterial = monsterShaderChange.material[0];
                monsterShaderChange_1.rend.sharedMaterial = monsterShaderChange_1.material[0];
                ani_Monster.speed = 1;
            }
        }
        if (animationDelay_Player > 0)
        {
            animationDelay_Player -= 1;
            if (animationDelay_Player <= 0)
            {
                monsterShaderChange_2.rend.sharedMaterial = monsterShaderChange_2.material[0];
                monsterShaderChange_3.rend.sharedMaterial = monsterShaderChange_3.material[0];
                monsterShaderChange_4.rend.sharedMaterial = monsterShaderChange_4.material[0];
            }
        }
    }
}

