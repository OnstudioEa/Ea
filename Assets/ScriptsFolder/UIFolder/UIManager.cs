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

    void Awake()
    {
        state = State.idle;
        StartCoroutine(FSM());

        //player = gameObject.transform;
        //ui_Ob[0] = gameObject.transform;
        //ui_Ob[1] = gameObject.transform;
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
       // PositionCheck();
        yield return new WaitForSeconds(0.5f);
    }
    public void PositionCheck()
    {
        dist = Vector3.Distance(ui_Ob[0].position, player.position);
        if (dist < 2)
            Debug.Log("0이 가깝다!!!");
        dist_1 = Vector3.Distance(ui_Ob[1].position, player.position);
        if (dist < 2)
            Debug.Log("0이 가깝다!!!");
    }
    public override void UIPosConvey()
    {
        arrowOb[0].transform.position = arrowPos[0].transform.position;
        Debug.Log("위치업뎃");
    }
}
