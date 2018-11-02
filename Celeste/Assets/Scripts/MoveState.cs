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

        if (player.lastState is SlideState && Input.GetKey(KeyCode.UpArrow)) 
        {
            player.StartCoroutine("SlideMove");
        }
    }
    public void Update()
    {
    }

    public void FixedUpdate()
    {
        Vector2 velocity = player.playerRigidbody.velocity;
        if (player.onLeftWall && Input.GetKey(KeyCode.LeftArrow) || player.onRightWall && Input.GetKey(KeyCode.RightArrow))
        {

            player.playerRigidbody.velocity = new Vector2(0, -1);
        }
        else
        {
            player.playerRigidbody.velocity = new Vector2(player.runCurve.Evaluate(player.timeCounter) * Input.GetAxisRaw("Horizontal") * player.moveSpeed, velocity.y);
        }
    }

    public void Finish()
    {
        Debug.Log("move finish");
    }
}
