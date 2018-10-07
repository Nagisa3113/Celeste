using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IBaseState
{
    private Player player;

    public MoveState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.playerRigidbody.gravityScale = player.normalGravity;
        Debug.Log("move enter");

    }

    public void Update () 
    {
        if (Input.GetKeyDown(KeyCode.C) && (player.onGround || player.onWall))
            player.SetPlayerState(new JumpState(player));
        else if (player.canDash && Input.GetKeyDown(KeyCode.X))
            player.SetPlayerState(new DashState(player));
        else if (player.onWall && Input.GetKey(KeyCode.Z) && player.slideTime > 0) 
            player.SetPlayerState(new SlideState(player));
    }

    public void FixedUpdate()
    {
        Vector2 velocity = player.playerRigidbody.velocity;
        if (player.onWall)//接触墙且按方向键时在墙上慢速滑动
        {
            if (player.forward == -1)
            {
                if (player.h < 0 && player.playerRigidbody.velocity.y < 0)
                    player.playerRigidbody.gravityScale = 1;
                else
                    player.playerRigidbody.velocity = new Vector2(player.h * Input.GetAxisRaw("Horizontal") * player.moveSpeed, velocity.y);
            }
            if (player.forward == 1)
            {
                if (player.h > 0 && player.playerRigidbody.velocity.y < 0) 
                    player.playerRigidbody.gravityScale = 1;
                else
                    player.playerRigidbody.velocity = new Vector2(player.h * Input.GetAxisRaw("Horizontal") * player.moveSpeed, velocity.y);
            }
        }
        else 
        {
            player.playerRigidbody.velocity = new Vector2(player.h * Input.GetAxisRaw("Horizontal") * player.moveSpeed, velocity.y);
        }
    }

    public void Finish()
    {
        Debug.Log("move finish");
    }
}
