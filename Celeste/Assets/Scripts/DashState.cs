using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : IBaseState
{
    private Player player;


    public DashState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.playerRigidbody.gravityScale = 0;
        player.playerRigidbody.velocity = Vector2.zero;
        player.startDash = true;
        player.StartCoroutine("Dash");
        Debug.Log("dash enter");
    }

    public void Update()
    {
    }

    public void FixedUpdate()
    {
        if (player.startDash == false)
            player.SetPlayerState(new MoveState(player));

        if (player.lastState is SlideState && player.onWall)
            player.SetPlayerState(new SlideState(player));

    }

    public void Finish()
    {
        player.playerRigidbody.gravityScale = player.normalGravity;
        Debug.Log("dash finish");
    }

}
