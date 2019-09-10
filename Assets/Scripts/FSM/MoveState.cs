using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : FSMState
{
    /// <summary>
    /// input buffer
    /// </summary>

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


        if (InputHandler.Instance.JumpButton.Down && player.onGround || player.onWall)
        {
            player.FSM.PerformTransition(Transition.JumpPress, player);
        }

        if (InputHandler.Instance.DashButton.Down && player.canDash)
        {
            player.FSM.PerformTransition(Transition.DashPress, player);
        }

        if (InputHandler.Instance.SlideButton.Held && player.canSlide && player.onWall)
        {
            player.FSM.PerformTransition(Transition.SlidePress, player);
        }

    }

    public override void DoBeforeLeaving(Player player)
    {
        Debug.Log("move finish");
    }
}
