using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : FSMState
{
    int buffer_counter;
    int buffer_max = 10;

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

    public override void InputHandle(Player player)
    {
        Vector2 velocity;

        if (player.onLeftWall && Input.GetKey(KeyCode.LeftArrow)
             || player.onRightWall && Input.GetKey(KeyCode.RightArrow))
        {
            //为什么修改重力不行
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1);
            //player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
        else
        {
            //player.GetComponent<Rigidbody2D>().gravityScale = player.normalGravity;
            velocity.x = player.moveBase * Input.GetAxisRaw("Horizontal") * player.moveSpeed;
            velocity.y = player.GetComponent<Rigidbody2D>().velocity.y;
            player.GetComponent<Rigidbody2D>().velocity = velocity;

        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            buffer_counter = 0;
        }

        if (buffer_counter < buffer_max)
        {
            buffer_counter++;
            if (Input.GetKey(KeyCode.C) && player.onGround || player.onWall)
                player.FSM.PerformTransition(Transition.JumpPress, player);
        }


        if (Input.GetKeyDown(KeyCode.X) && player.canDash)
        {
            player.FSM.PerformTransition(Transition.DashPress, player);
        }


        if (Input.GetKeyDown(KeyCode.Z) && player.canSlide && player.onWall)
        {
            player.FSM.PerformTransition(Transition.SlidePress, player);
        }


    }

    public override void Update(Player player)
    {

    }

    public override void DoBeforeLeaving(Player player)
    {
        Debug.Log("move finish");
    }
}
