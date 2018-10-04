using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState :PlayerBaseState
{
    private Player player;
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
        if (player.onGround)
        {
            Vector2 velocity = player.playerRigidbody.velocity;
            velocity.y = player.jumpSpeed;
            player.playerRigidbody.velocity = velocity;
            player.SetPlayerState(new MoveState(player));

        }

        else if (player.onWall)
        {
            Vector2 velocity = player.playerRigidbody.velocity;
            velocity.x = -player.forward * player.jumpSpeed * 10;
            player.playerRigidbody.velocity = velocity;
            player.SetPlayerState(new MoveState(player));
            player.state.Enter();
        }

    }

    public void Finish()
    {
        Debug.Log("jump finish");

    }

}
