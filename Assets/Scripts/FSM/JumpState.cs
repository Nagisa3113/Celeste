using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : FSMState
{

    float jumpSpeed = 8;//跳跃速度
    float jumpTimeCounter;//计时器
    float jumpTime = 0.27f;//最大跳跃时间
    bool isRun;

    public JumpState()
    {
        stateID = StateID.Jump;

        AddTransition(Transition.ReMove, StateID.Move);
        AddTransition(Transition.SlidePress, StateID.Slide);
    }

    public override void DoBeforeEntering(Player player)
    {

        player.GetComponent<Rigidbody2D>().gravityScale = 0f;

        jumpTimeCounter = jumpTime;

        isRun = Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x) > 0;

        Debug.Log("jump enter");
    }
 
    public override void Update(Player player)
    {
        Vector2 velocity;


        if (Input.GetKey(KeyCode.C) && jumpTimeCounter > 0)
        {

            velocity.x = isRun
                ? player.runCurve.Evaluate(player.timeCounter) * Input.GetAxisRaw("Horizontal") * player.moveSpeed
                : velocity.x = player.moveBase * Input.GetAxisRaw("Horizontal") * player.moveSpeed;

            velocity.y = player.jumpCurve.Evaluate(jumpTimeCounter) * jumpSpeed;

            player.GetComponent<Rigidbody2D>().velocity = velocity;

            jumpTimeCounter -= Time.fixedDeltaTime;
        }


        else if(Input.GetKey(KeyCode.Z) && player.canSlide && player.onWall)
        {
            player.FSM.PerformTransition(Transition.SlidePress, player);
        }

        else//如果不加else会只执行一次
            player.FSM.PerformTransition(Transition.ReMove, player);

    }


    public override void DoBeforeLeaving(Player player)
    {
        player.GetComponent<Rigidbody2D>().gravityScale = player.normalGravity;
        Debug.Log("jump finish");
    }


}
