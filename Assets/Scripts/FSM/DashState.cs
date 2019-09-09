using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : FSMState
{

    public DashState()
    {
        stateID = StateID.Dash;
        AddTransition(Transition.ReMove, StateID.Move);
        AddTransition(Transition.SlidePress, StateID.Slide);
    }

    public override void DoBeforeEntering(Player player)
    {
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        player.StartCoroutine(Dash(player));

        Debug.Log("dash enter");
    }


    public override void Update(Player player)
    {

    }

    public override void DoBeforeLeaving(Player player)
    {
        player.GetComponent<Rigidbody2D>().gravityScale = player.normalGravity;
        Debug.Log("dash finish");
    }


    public IEnumerator Dash(Player player)
    {
        int dashTime = 12;//冲刺持续时间
        float dashSpeed = 15;//冲刺速度
        player.canDash = false;
        Vector2 direct = InputHandler.GetDashDirect(player);

        for (int i = 0; i < dashTime; i++)
        {
            player.transform.Translate(direct * Time.deltaTime * dashSpeed);
            yield return null;
        }
        player.FSM.PerformTransition(Transition.ReMove, player);
    }


}
