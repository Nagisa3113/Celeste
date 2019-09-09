using UnityEngine;

public class InputHandler
{
    public void Update(Player player)
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            player.moveBase = player.moveCurve.Evaluate(player.timeCounter);

            player.timeCounter = player.timeCounter <= 0.3f ?
                player.timeCounter += Time.fixedDeltaTime : 0.3f;
        }
        else
        {
            player.moveBase = player.slowCurve.Evaluate(player.timeCounter);
            player.timeCounter = player.timeCounter >= 0f ?
            player.timeCounter -= Time.fixedDeltaTime : 0f;
        }

        if (Input.GetAxis("Horizontal") < 0)
            player.forward = -1;
        if (Input.GetAxis("Horizontal") > 0)
            player.forward = 1;
    }


    //Command Handle()
    //{
    //    if (Input.GetKeyDown(KeyCode.Z)) return ButtonZ;
    //    if (Input.GetKeyDown(KeyCode.X)) return ButtonX;
    //    if (Input.GetKeyDown(KeyCode.C)) return buttonC;

    //    return null;
    //}
    
    //Command ButtonZ = new SlideCommand();
    //Command ButtonX = new DashCommand();
    //Command buttonC = new JumpCommand();



    public static Vector2 GetDashDirect(Player player)
    {
        Vector2 dashDirect;

        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            dashDirect = new Vector2(1, 1);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            dashDirect = new Vector2(1, -1);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            dashDirect = new Vector2(-1, 1); ;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            dashDirect = new Vector2(-1, 11);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            dashDirect = new Vector2(1, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            dashDirect = new Vector2(-1, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            dashDirect = new Vector2(0, 1);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            dashDirect = new Vector2(0, -1);
        }
        else
        {
            dashDirect = new Vector2(player.forward, 0);

            //GetComponent<Rigidbody2D>().velocity = new Vector2(player.dashSpeed.0);
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 0), ForceMode2D.Impulse);

        }

        return dashDirect;
    }



}

abstract class Command
{
    public virtual void Execute(Player player)
    {

    }

}

class JumpCommand : Command
{
    public override void Execute(Player player)
    {

    }
}

class DashCommand : Command
{
    public override void Execute(Player player)
    {

    }
}

class SlideCommand : Command
{
    public override void Execute(Player player)
    {

    }
}
