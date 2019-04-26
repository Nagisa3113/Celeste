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
        player.playerRigidbody.gravityScale = 0;
        player.playerRigidbody.velocity = Vector2.zero;
        player.startDash = true;
        player.StartCoroutine("Dash");
        Debug.Log("dash enter");
    }

    public override void InputHandle(Player player)
    {

    }

    public override void Update(Player player)
    {
        if (player.startDash == false)
            player.fsm.PerformTransition(Transition.ReMove, player);
    }

    public override void DoBeforeLeaving(Player player)
    {
        player.playerRigidbody.gravityScale = player.normalGravity;
        Debug.Log("dash finish");
    }

}
