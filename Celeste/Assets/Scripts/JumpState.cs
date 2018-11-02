using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IBaseState
{
    private Player player;
    private float jumpTimeCounter;//计时器
    private float jumpTime = 0.27f;//最大跳跃时间
    private bool isRun;
    private Vector2 velocity;

    public JumpState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.slideJump = false;
        player.offJump = false;
        player.playerRigidbody.gravityScale = 0f;

        jumpTimeCounter = jumpTime;

        if (Mathf.Abs(player.playerRigidbody.velocity.x) > 0)
            isRun = true;

        Debug.Log("jump enter");

    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        if (player.lastState is SlideState)
        {
            if (player.onLeftWall && player.forward == -1 || player.onRightWall && player.forward == 1)
            {
                player.StartCoroutine("SlideJump");
                if (player.slideJump)
                    player.SetPlayerState(new SlideState(player));
            }

            else
            {
                player.StartCoroutine("OffJump");
                if (player.offJump)
                    player.SetPlayerState(new MoveState(player));
            }

        }

        else if(Input.GetKey(KeyCode.C) && jumpTimeCounter > 0)//为什么不能用while
        {
            velocity.y = player.jumpCurve.Evaluate(jumpTimeCounter) * player.jumpSpeed;

            if (isRun)
                velocity.x = player.runCurve.Evaluate(player.timeCounter) * Input.GetAxisRaw("Horizontal") * player.moveSpeed;
            else
                velocity.x = player.moveBase * Input.GetAxisRaw("Horizontal") * player.moveSpeed;

            player.playerRigidbody.velocity = velocity;

            jumpTimeCounter -= Time.deltaTime;
        }

        else//如果不加else会只执行一次
            player.SetPlayerState(new MoveState(player));
    }



    public void Finish()
    {
        player.playerRigidbody.gravityScale = player.normalGravity;
        Debug.Log("jump finish");
    }

}
