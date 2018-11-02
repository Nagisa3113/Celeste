using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : IBaseState
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
        player.slideTime -= Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.Z) || player.onWall == false || player.slideTime < 0)
        {
            player.SetPlayerState(new MoveState(player));
        }

    }


    public void FixedUpdate()
    {
        float v = Input.GetAxisRaw("Vertical");
        player.playerRigidbody.velocity = new Vector2(0, v * player.slideSpeed);

    }

    public void Finish()
    {
 
        player.playerRigidbody.gravityScale = player.normalGravity;
        player.playerRigidbody.velocity = Vector2.zero;
        Debug.Log("slide finish");
    }


}
