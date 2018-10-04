using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState:IBaseState
{
    private Player player;

    public SlideState(Player player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.playerRigidbody.gravityScale = 0;
        player.canDash = true;
        Debug.Log("slide enter");
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z) || player.onWall == false) 
        {
            player.SetPlayerState(new MoveState(player));
        }

        float v = Input.GetAxisRaw("Vertical");

        player.playerRigidbody.velocity = new Vector2(0, v * player.slideSpeed);

        if (player.canDash && Input.GetKeyDown(KeyCode.X))
        {
            player.SetPlayerState(new DashState(player));
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            player.SetPlayerState(new JumpState(player));

        }
    }
    public void Finish()
    {
        Debug.Log("slide finish");
        player.playerRigidbody.gravityScale = player.normalGravity;

    }


}
