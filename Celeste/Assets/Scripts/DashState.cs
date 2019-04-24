using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : FSMState
{
    private Player player;

    public DashState(Player player)
    {
        stateID = StateID.Dash;
        this.player = player;
        AddTransition(Transition.ReMove, StateID.Move);
        AddTransition(Transition.SlidePress, StateID.Slide);
    }

    public override void DoBeforeEntering()
    {
        player.playerRigidbody.gravityScale = 0;
        player.playerRigidbody.velocity = Vector2.zero;
        player.startDash = true;
        player.StartCoroutine("Dash");
        Debug.Log("dash enter");
    }

    public override void InputHandle()
    {

    }

    public override void Act()
    {
        if (player.startDash == false)
            player.fsm.PerformTransition(Transition.ReMove);
    }

    public override void DoBeforeLeaving()
    {
        player.playerRigidbody.gravityScale = player.normalGravity;
        Debug.Log("dash finish");
    }

}
