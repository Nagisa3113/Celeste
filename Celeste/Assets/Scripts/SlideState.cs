using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : FSMState
{
    private Player player;

    public SlideState(Player player)
    {
        stateID = StateID.Slide;
        this.player = player;
        AddTransition(Transition.ReMove, StateID.Move);
        AddTransition(Transition.JumpPress, StateID.Jump);
    }
    public override void DoBeforeEntering()
    {
        player.playerRigidbody.gravityScale = 0;
        player.canDash = true;
        Debug.Log("slide enter");
    }


    public override void InputHandle()
    {
        if (player.onLeftWall)
        {
            if (Input.GetKey(KeyCode.RightArrow))
                player.forward = 1;
            else
                player.forward = -1;
        }
        if (player.onRightWall)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                player.forward = -1;
            else
                player.forward = 1;
        }

        if (Input.GetKeyDown(KeyCode.C))
            player.fsm.PerformTransition(Transition.JumpPress);

        if (Input.GetKeyUp(KeyCode.Z) || player.onWall == false || player.slideTime < 0)
        {
            player.fsm.PerformTransition(Transition.ReMove);
        }
    }

    public override void Act()
    {
        player.slideTime -= Time.deltaTime;



        float v = Input.GetAxisRaw("Vertical");
        player.playerRigidbody.velocity = new Vector2(0, v * player.slideSpeed);

    }

    public override void DoBeforeLeaving()
    {
        player.playerRigidbody.gravityScale = player.normalGravity;
        player.playerRigidbody.velocity = Vector2.zero;
        Debug.Log("slide finish");
    }


}
