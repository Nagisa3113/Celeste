using UnityEngine;

class InputHandler
{

    public void Update(Player player)
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            player.moveBase = player.moveCurve.Evaluate(player.timeCounter);
            if (player.timeCounter <= 0.3f)
                player.timeCounter += Time.fixedDeltaTime;
        }

        else
        {
            player.moveBase = player.slowCurve.Evaluate(player.timeCounter);
            if (player.timeCounter >= 0)
                player.timeCounter -= Time.fixedDeltaTime;
        }

        if (Input.GetAxis("Horizontal") < 0)
            player.forward = -1;
        if (Input.GetAxis("Horizontal") > 0)
            player.forward = 1;
    }



    public Command Handle()
    {
        if (Input.GetKeyDown(KeyCode.Z)) return ButtonZ;
        if (Input.GetKeyDown(KeyCode.X)) return ButtonX;
        if (Input.GetKeyDown(KeyCode.C)) return buttonC;

        return null;
    }

    private
    Command ButtonZ = new SlideCommand();
    Command ButtonX = new DashCommand();
    Command buttonC = new JumpCommand();


}

abstract class Command
{
    public virtual void Execute(Player player)
    {

    }

}

class JumpCommand:Command
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
