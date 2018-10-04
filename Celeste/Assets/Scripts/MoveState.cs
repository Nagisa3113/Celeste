using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : PlayerBaseState
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

        Vector2 velocity = player.playerRigidbody.velocity;
        float h = Input.GetAxisRaw("Horizontal");
        player.playerRigidbody.velocity = new Vector2(h * player.moveSpeed, velocity.y);


        if (velocity.x < 0)
            player.forward = -1;
        else if (velocity.x > 0)
            player.forward = 1;

        if (Input.GetKeyDown(KeyCode.C))
            player.SetPlayerState(new JumpState(player));
        else if (player.canDash && Input.GetKeyDown(KeyCode.X))
            player.SetPlayerState(new DashState(player));
        else if (player.onWall && Input.GetKey(KeyCode.Z))
            player.SetPlayerState(new SlideState(player));
    }

    public void Finish()
    {
        Debug.Log("move finish");
    }


}
