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

}
