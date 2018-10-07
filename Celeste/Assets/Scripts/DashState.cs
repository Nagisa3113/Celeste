using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState :IBaseState
{
    private Player player;
    private int i = 0;
    public int dashTime = 15;//冲刺持续时间
    public int dashDirect = 0;//冲刺方向
    public bool startDash = false;//开始冲刺

    public DashState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        startDash = true;
        player.playerRigidbody.gravityScale = 0;
        player.playerRigidbody.velocity = Vector2.zero;
        Debug.Log("dash enter");
    }

    public void Update()
    {
        if (startDash)
        {
            player.canDash = false;
            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                dashDirect = 1;
            }
            else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                dashDirect = 3;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                dashDirect = 7;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                dashDirect = 5;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                dashDirect = 2;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                dashDirect = 6;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                dashDirect = 8;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                dashDirect = 4;
            }
            else
            {
                if (player.forward == -1)
                {
                    dashDirect = 6;
                }
                else
                {
                    dashDirect = 2;
                    //playerrigidbody.velocity = new Vector2(player.dashSpeed.0);
                    //playerrigidbody.AddForce(new Vector2(100, 0), ForceMode2D.Impulse);
                }

            }
            startDash = false;
        }

        if (i == dashTime)
            player.SetPlayerState(new MoveState(player));
    }

    public void FixedUpdate()
    {
        if (dashDirect != 0)
        {
            switch (dashDirect)
            {
                case 1:
                    player.transform.Translate(Vector2.right * Time.deltaTime * player.dashSpeed + Vector2.up * Time.deltaTime * player.dashSpeed);
                    break;
                case 2:
                    player.transform.Translate(Vector2.right * Time.deltaTime * player.dashSpeed);
                    break;

                case 3:
                    player.transform.Translate(Vector2.right * Time.deltaTime * player.dashSpeed + Vector2.down * Time.deltaTime * player.dashSpeed);
                    break;

                case 4:
                    player.transform.Translate(Vector2.down * Time.deltaTime * player.dashSpeed);
                    break;

                case 5:
                    player.transform.Translate(Vector2.left * Time.deltaTime * player.dashSpeed + Vector2.down * Time.deltaTime * player.dashSpeed);
                    break;

                case 6:
                    player.transform.Translate(Vector2.left * Time.deltaTime * player.dashSpeed);
                    break;

                case 7:
                    player.transform.Translate(Vector2.left * Time.deltaTime * player.dashSpeed + Vector2.up * Time.deltaTime * player.dashSpeed);
                    break;

                case 8:
                    player.transform.Translate(Vector2.up * Time.deltaTime * player.dashSpeed);
                    break;
            }
            i++;
        }
    }

    public void Finish()
    {
        player.playerRigidbody.gravityScale = player.normalGravity;
        Debug.Log("dash finish");
    }
}
