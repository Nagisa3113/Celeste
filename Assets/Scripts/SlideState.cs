using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : FSMState
{
    float slideSpeed = 2;//爬墙速度
    float slideTime = 30f;//最大爬墙时间
    float slideCounter = 0f;//最大爬墙时间,待修复

    public SlideState()
    {
        stateID = StateID.Slide;
        AddTransition(Transition.ReMove, StateID.Move);
        AddTransition(Transition.JumpPress, StateID.Jump);
    }

    public override void DoBeforeEntering(Player player)
    {

        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        slideCounter = slideTime;
        //待验证
        player.canDash = true;

        Debug.Log("slide enter");
    }


    public override void InputHandle(Player player)
    {

        if (player.onLeftWall)
        {
            if (Input.GetKey(KeyCode.RightArrow))
                player.forward = 1;
            else
                player.forward = -1;
        }
        else if (player.onRightWall)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                player.forward = -1;
            else
                player.forward = 1;
        }

        player.GetComponent<Rigidbody2D>().velocity 
            = new Vector2(0, Input.GetAxisRaw("Vertical") * slideSpeed);

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (player.onLeftWall && player.forward == -1 
                || player.onRightWall && player.forward == 1)
            {
                player.StartCoroutine("SlideJump");
            }
            else
            {
                player.StartCoroutine("OffJump");
            }
        }



        if (player.onWall == false)
        {
            if(Input.GetKey(KeyCode.UpArrow) 
                && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                player.StartCoroutine("SlideMove");
            }
            player.FSM.PerformTransition(Transition.ReMove, player);

        }

        if (Input.GetKeyUp(KeyCode.Z) || slideCounter < 0)
        {
            player.FSM.PerformTransition(Transition.ReMove, player);
        }

    }

    public override void Update(Player player)
    {
        player.canSlide = slideCounter > 0;

        slideCounter -= Time.deltaTime;


    }

    public override void DoBeforeLeaving(Player player)
    {
        player.GetComponent<Rigidbody2D>().gravityScale = player.normalGravity;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Debug.Log("slide finish");
    }


}
