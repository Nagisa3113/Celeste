using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState :IBaseState
{
    private Player player;
    private float jumpTimeCounter;//计时器
    private float jumpTime = 0.27f;//最大跳跃时间
    private bool isJumping;//是否跳跃
    private Vector2 velocity;

    public JumpState(Player player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.playerRigidbody.gravityScale = 0f;
        Debug.Log("jump enter");
    }

    public void Update()
    {
        if (player.onGround) 
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            if (Mathf.Abs(player.playerRigidbody.velocity.x) > 0)
                velocity.x = player.runCurve.Evaluate(player.timeCounter) * Input.GetAxisRaw("Horizontal") * player.moveSpeed;
            else
                velocity.x = player.h * Input.GetAxisRaw("Horizontal") * player.moveSpeed;

            player.playerRigidbody.velocity = velocity;
        }

        if (Input.GetKey(KeyCode.C) && isJumping) 
        {
            if (jumpTimeCounter > 0) 
            {
                velocity.y = player.jumpCurve.Evaluate(jumpTimeCounter) * player.jumpSpeed;
                if (Mathf.Abs(player.playerRigidbody.velocity.x) > 0)
                    velocity.x = player.runCurve.Evaluate(player.timeCounter) * Input.GetAxisRaw("Horizontal") * player.moveSpeed;
                else
                    velocity.x = player.h * Input.GetAxisRaw("Horizontal") * player.moveSpeed;
                player.timeCounter -= Time.fixedDeltaTime;
                player.playerRigidbody.velocity = velocity;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        else
        {
            player.SetPlayerState(new MoveState(player));
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            isJumping = false;
            player.SetPlayerState(new MoveState(player));

        }
        else if (player.onWall && Input.GetKey(KeyCode.Z)) 
        {
            player.playerRigidbody.velocity = new Vector2(0, player.slideSpeed * 10);
            player.SetPlayerState(new MoveState(player));
        }
    }


    public void FixedUpdate()
    {

    }

    public void Finish()
    {
        player.playerRigidbody.gravityScale = player.normalGravity;
        Debug.Log("jump finish");
    }

}
