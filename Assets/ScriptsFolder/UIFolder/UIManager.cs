using UnityEngine;
using System.Collections;

public class Convey : MonoBehaviour
{
    public virtual void UIPosConvey()
    {

    }
}
public class UIManager : Convey
{
    public State state;

    public UISprite[] arrowOb;
    public GameObject[] arrowPos;

    public Transform player;
    public Transform[] ui_Ob;

    float dist;
    float dist_1;
    float dist_2;

    public float tt_1;

    bool windowOnCheck;

    public InvenManager invenManager;
   
    void Awake()
    {
        state = State.idle;
        StartCoroutine(FSM());

        player = GameObject.Find("Player").gameObject.transform;
        ui_Ob[0] = GameObject.Find("forge").gameObject.transform;
        ui_Ob[1] = GameObject.Find("tresure_box").gameObject.transform;
        //ui_Ob[2] = gameObject.transform;

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
        PositionCheck();
        yield return new WaitForSeconds(0.1f);
    }
    public void PositionCheck()
    {
        Vector3 obPos = ui_Ob[0].transform.position;
        dist = (obPos - player.position).sqrMagnitude;

        Vector3 obPos1 = ui_Ob[1].transform.position;
        dist_1 = (obPos1 - player.position).sqrMagnitude;

        if (dist < tt_1)
        {
            if (windowOnCheck == false)
            {
                windowOnCheck = true;
                invenManager.UpgradeWindowOn();
                Debug.Log("0이 가깝다!!!");
                return;
            }
        }
        else
        {
            if (dist_1 < tt_1)
            {
                if (windowOnCheck == false)
                {
                    windowOnCheck = true;
                    invenManager.ItemWindowOn();
                    Debug.Log("1이 가깝다!!!");
                    return;
                }
            }
            else
            {
                windowOnCheck = false;
                invenManager.ItemWindowOff();
            }
        }
    }
    public override void UIPosConvey()
    {
        arrowOb[0].transform.position = arrowPos[0].transform.position;
    }
}
