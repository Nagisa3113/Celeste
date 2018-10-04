using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState :IBaseState
{
    private Player player;
    private float curJump = 0;//当前跳跃
    private int minJump = 4;//最小跳跃
    private int maxJump = 10;//最大跳跃
    public JumpState(Player player)
    {
        this.player = player;
    }
    public void Enter()
    {
        Debug.Log("jump enter");
        curJump = minJump;
    }

    public void Update()
    {
        curJump++;
        if (player.onGround && Input.GetKeyUp(KeyCode.C))
        {
            Vector2 velocity = player.playerRigidbody.velocity;
            velocity.y = player.jumpSpeed * (curJump / maxJump);
            player.playerRigidbody.velocity = velocity;
            player.SetPlayerState(new MoveState(player));

        }
        else if (player.onGround && curJump >= maxJump) 
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
            velocity.y = player.jumpSpeed;
            player.playerRigidbody.velocity = velocity;
            player.SetPlayerState(new MoveState(player));

        }


    }

    public void Finish()
    {
        Debug.Log("jump finish");

    }

}
