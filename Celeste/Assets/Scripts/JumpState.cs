using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState :IBaseState
{
    private Player player;
    private float jumpTimeCounter;//计时器
    private float jumpTime = 0.25f;//最大跳跃时间
    private bool isJumping;//是否跳跃
    private Vector2 velocity;

    public JumpState(Player player)
    {
        this.player = player;
    }
    public void Enter()
    {
        Debug.Log("jump enter");
    }

    public void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (player.onGround) 
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            velocity.x = h * player.moveSpeed;
            player.playerRigidbody.velocity = velocity;
        }

        if (Input.GetKey(KeyCode.C) && isJumping) 
        {
            if (jumpTimeCounter > 0) 
            {
                velocity.y = player.jumpSpeed.Evaluate(jumpTimeCounter);
                velocity.x = h * player.moveSpeed;
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
        Debug.Log("jump finish");
    }

}
