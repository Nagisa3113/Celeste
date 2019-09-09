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

        if (player.onLeftWall)
            player.forward = Input.GetKey(KeyCode.RightArrow) ? 1 : -1;
        else if (player.onRightWall)
            player.forward = Input.GetKey(KeyCode.LeftArrow) ? -1 : 1;


        player.GetComponent<Rigidbody2D>().velocity
            = new Vector2(0, Input.GetAxisRaw("Vertical") * slideSpeed);

        if (Input.GetKeyDown(KeyCode.C))
        {
            player.FSM.PerformTransition(Transition.JumpPress, player);
        }


        if (player.onWall == false)
        {
            if (Input.GetKey(KeyCode.UpArrow)
                && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                player.StartCoroutine(SlideAutoMove(player));
            }
            player.FSM.PerformTransition(Transition.ReMove, player);

        }

        if (Input.GetKeyUp(KeyCode.Z) || slideCounter < 0)
        {
            player.FSM.PerformTransition(Transition.ReMove, player);
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
