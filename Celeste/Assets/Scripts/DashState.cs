using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState :PlayerBaseState
{

    private int i = 0;
    private Player player;
    public DashState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("dash enter");

    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            player.transform.Translate(Vector2.right * Time.deltaTime * player.dashSpeed + Vector2.up * Time.deltaTime * player.dashSpeed);
            player.canDash = false;
            i++;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            player.transform.Translate(Vector2.right * Time.deltaTime * player.dashSpeed + Vector2.down * Time.deltaTime * player.dashSpeed);
            player.canDash = false;
            i++;
        }

        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            player.transform.Translate(Vector2.left * Time.deltaTime * player.dashSpeed + Vector2.up * Time.deltaTime * player.dashSpeed);
            player.canDash = false;
            i++;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            player.transform.Translate(Vector2.left * Time.deltaTime * player.dashSpeed + Vector2.down * Time.deltaTime * player.dashSpeed);
            player.canDash = false;
            i++;
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.Translate(Vector2.right * Time.deltaTime * player.dashSpeed);
            player.canDash = false;
            i++;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.transform.Translate(Vector2.left * Time.deltaTime * player.dashSpeed);
            player.canDash = false;
            i++;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            player.transform.Translate(Vector2.up * Time.deltaTime * player.dashSpeed);
            player.canDash = false;
            i++;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            player.transform.Translate(Vector2.down * Time.deltaTime * player.dashSpeed);
            player.canDash = false;
            i++;
        }

        else
        {
            if (player.forward == -1)
            {
                player.transform.Translate(Vector2.left * Time.deltaTime * player.dashSpeed);
                i++;
            }
            else
            {
                player.transform.Translate(Vector2.right * Time.deltaTime * player.dashSpeed);
                i++;
                //playerrigidbody.velocity = new Vector2(player.dashSpeed.0);
                //playerrigidbody.AddForce(new Vector2(100, 0), ForceMode2D.Impulse);
            }

        }


        if(i==5)
        player.SetPlayerState(new MoveState(player));

    }
    public void Finish()
    {
        Debug.Log("dash finish");

    }
}
