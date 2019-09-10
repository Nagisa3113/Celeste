using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : FSMState
{
    float slideSpeed = 2;//爬墙速度
    float slideTime = 30f;//最大爬墙时间
    float slideCounter = 0f;//最大爬墙时间,待修复

    public SlideState()
    {
        stateID = StateID.Slide;
        AddTransition(Transition.ReMove, StateID.Move);
        AddTransition(Transition.JumpPress, StateID.Jump);
        AddTransition(Transition.DashPress, StateID.Dash);

    }

    public override void DoBeforeEntering(Player player)
    {

        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        slideCounter = slideTime;
        //待验证
        player.canDash = true;

        Debug.Log("slide enter");
    }

    public override void Update(Player player)
    {
        if (player.onWall == false)
        {
            if ((int)InputHandler.Instance.DirAxis.y == 1)
            {
                player.StartCoroutine(SlideAutoMove(player));
            }

            player.FSM.PerformTransition(Transition.ReMove, player);

        }

        if (InputHandler.Instance.SlideButton.Up || slideCounter < 0)
        {
            player.FSM.PerformTransition(Transition.ReMove, player);
        }


        if (InputHandler.Instance.DashButton.Down && player.canDash)
        {
            player.FSM.PerformTransition(Transition.DashPress, player);
        }

        player.forward = (int)InputHandler.Instance.DirAxis.x == 0 ?
             player.onLeftWall ? -1 : 1 :
             (int)InputHandler.Instance.DirAxis.x;


        player.GetComponent<Rigidbody2D>().velocity
            = new Vector2(0, InputHandler.Instance.DirAxis.y * slideSpeed);


        if (InputHandler.Instance.JumpButton.Down)
        {
            player.FSM.PerformTransition(Transition.JumpPress, player);
        }

        player.canSlide = slideCounter > 0;
        slideCounter -= Time.deltaTime;
    }

    public override void DoBeforeLeaving(Player player)
    {
        player.GetComponent<Rigidbody2D>().gravityScale = player.normalGravity;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Debug.Log("slide finish");
    }


    public IEnumerator SlideAutoMove(Player player)
    {
        float speed = 4f;
        for (int i = 0; i < 7; i++)
        {
            player.transform.Translate(new Vector2(player.forward, 0) * Time.fixedDeltaTime * speed);
            yield return null;
        }
    }


}
