using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : FSMState
{

    public DashState()
    {
        stateID = StateID.Dash;
        AddTransition(Transition.ReMove, StateID.Move);
        AddTransition(Transition.SlidePress, StateID.Slide);
    }

    public override void DoBeforeEntering(Player player)
    {
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        player.StartCoroutine("Dash");
       
        Debug.Log("dash enter");
    }

    public override void InputHandle(Player player)
    {

    }

    public override void Update(Player player)
    {

    }

    public override void DoBeforeLeaving(Player player)
    {
        player.GetComponent<Rigidbody2D>().gravityScale = player.normalGravity;
        Debug.Log("dash finish");
    }

}
