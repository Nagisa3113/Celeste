﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : FSMState
{

    public MoveState()
    {
        stateID = StateID.Move;
        AddTransition(Transition.JumpPress, StateID.Jump);
        AddTransition(Transition.DashPress, StateID.Dash);
        AddTransition(Transition.SlidePress, StateID.Slide);
    }

    public override void DoBeforeEntering(Player player)
    {
        player.GetComponent<Rigidbody2D>().gravityScale = player.normalGravity;

        Debug.Log("move enter");
    }


    public override void Update(Player player)
    {
        Vector2 velocity;

        if (player.onLeftWall && (int)InputHandler.Instance.DirAxis.x == -1
             || player.onRightWall && (int)InputHandler.Instance.DirAxis.x == 1)
        {
            //player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1);
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().gravityScale = 5f;
        }
        else
        {
            player.GetComponent<Rigidbody2D>().gravityScale = player.normalGravity;
            velocity.x = player.moveBase * InputHandler.Instance.DirAxis.x * player.moveSpeed;
            velocity.y = player.GetComponent<Rigidbody2D>().velocity.y;
            player.GetComponent<Rigidbody2D>().velocity = velocity;
        }


        if (InputHandler.Instance.JumpButton.Down && player.coyote_counter > 0)
        {
            player.FSM.PerformTransition(Transition.JumpPress, player);
        }
        else if (InputHandler.Instance.DashButton.Down && player.canDash)
        {
            player.FSM.PerformTransition(Transition.DashPress, player);
        }
        else if (InputHandler.Instance.SlideButton.Held && player.canSlide && player.onWall)
        {
            player.FSM.PerformTransition(Transition.SlidePress, player);
        }

    }

    public override void DoBeforeLeaving(Player player)
    {
        player.GetComponent<Rigidbody2D>().gravityScale = player.normalGravity;
        Debug.Log("move finish");
    }
}
