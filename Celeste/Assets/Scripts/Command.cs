using UnityEngine;
abstract class Command
{
    public abstract void execute(Player player);
}

class JumpCommand : Command
{
    public override void execute(Player player)
    {
        if (player.onGround || player.onWall)
            player.SetPlayerState(new JumpState(player));
    }

}

class DashCommand : Command
{
    public override void execute(Player player)
    {
        if (player.canDash)
            player.SetPlayerState(new DashState(player));
    }
}

class SlideCommand : Command
{
    public override void execute(Player player)
    {
        if (player.onWall && player.slideTime > 0)
            player.SetPlayerState(new SlideState(player));
    }
}

class InputHandler
{
    public Command handlerInput()
    {

        if (Input.GetKeyDown(KeyCode.Z)) return ButtonZ;
        if (Input.GetKeyDown(KeyCode.X)) return ButtonX;
        if (Input.GetKeyDown(KeyCode.C)) return buttonC;

        return null;
    }

    Command ButtonZ = new SlideCommand();
    Command ButtonX = new DashCommand();
    Command buttonC = new JumpCommand();

}
