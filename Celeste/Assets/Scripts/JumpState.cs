using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState :IBaseState
{
    private Player player;


    private float jumpTimeCounnter;
    private float jumpTime = 0.3f;
    private bool isJumping;
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
            jumpTimeCounnter = jumpTime;
            velocity.y = player.jumpSpeed;
            velocity.x = h * player.moveSpeed;
            player.playerRigidbody.velocity = velocity;
        }

        if (Input.GetKey(KeyCode.C) && isJumping) 
        {
            if (jumpTimeCounnter > 0.1f) 
            {
                velocity.y = player.jumpSpeed;
                velocity.x = h * player.moveSpeed;
                player.playerRigidbody.velocity = velocity;
                jumpTimeCounnter -= Time.deltaTime;
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

            player.transform.Translate(Vector2.up * Time.deltaTime * player.dashSpeed);
            player.SetPlayerState(new MoveState(player));

        }


    }

    public void Finish()
    {
        Debug.Log("jump finish");

    }

}
