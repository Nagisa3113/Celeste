using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public LayerMask objectLayer;//检测

    public AnimationCurve jumpCurve;//跳跃速度
    public AnimationCurve moveCurve;
    public AnimationCurve runCurve;
    public AnimationCurve slowCurve;

    public int forward = 1;//玩家朝向，1为右，-1为左

    public float normalGravity;//获得玩家重力
    public float maxFallSpeed = -7;//最大下落速度

    public float moveBase;
    public float timeCounter;//移动计时器
    public float moveSpeed = 8;//移动速度

    public bool onGround;//是否在地面
    public bool canDash;//是否能冲刺
    public bool canSlide;//是否能攀爬

    public bool onWall;//是否在墙上
    public bool onLeftWall;//左边
    public bool onRightWall;//右边  

    //public int coyote_counter;
    //public int coyote_max = 30;

    FSMSystem fsm;
    public FSMSystem FSM
    {
        get
        {
            return fsm;
        }
    }

    PhysicsUpdate physics;
    SpriteUpdate sprite;
    InputHandler inputHandler;

    void Awake()
    {
        fsm = new FSMSystem();

        FSM.AddState(new MoveState());
        FSM.AddState(new JumpState());
        FSM.AddState(new DashState());
        FSM.AddState(new SlideState());

        physics = new PhysicsUpdate();
        sprite = new SpriteUpdate();
        inputHandler = new InputHandler();
    }

    void Start()
    {
        normalGravity = GetComponent<Rigidbody2D>().gravityScale;
    }


    void Update()
    {
        sprite.Update(this);

        FSM.CurrentState.InputHandle(this);

        if (FSM.CurrentState != null)
            FSM.CurrentState.Update(this);
    }


    void FixedUpdate()
    {
        physics.Update(this);
        inputHandler.Update(this);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Thorn")
        {
            this.gameObject.SetActive(false);
        }

        if (collision.collider.name == "DashGem")
        {
            collision.collider.gameObject.SetActive(false);
            this.canDash = true;
        }

    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if ((int)contact.normal.y == 1)
            {
                onGround = true;
                canDash = true;
                canSlide = true;

            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
    }



    public IEnumerator Dash()
    {
        int dashTime = 15;//冲刺持续时间
        float dashSpeed = 12;//冲刺速度
        canDash = false;
        Vector2 direct = inputHandler.GetDashDirect(this);

        for (int i = 0; i < dashTime; i++)
        {
            transform.Translate(direct * Time.deltaTime * dashSpeed);
            yield return null;
        }
        fsm.PerformTransition(Transition.ReMove, this);
    }

    public IEnumerator SlideMove()
    {
        float speed = 4f;
        for (int i = 0; i < 7; i++)
        {
            transform.Translate(new Vector2(forward, 0) * Time.fixedDeltaTime * speed);
            yield return null;
        }
    }

    public IEnumerator SlideJump()
    {
        float speed = 5f;
        for (int i = 0; i < 15; i++)
        {
            Debug.Log(Time.fixedDeltaTime);
            transform.Translate(Vector2.up * Time.fixedDeltaTime * speed);
            yield return null;
        }

    }

    public IEnumerator OffJump()
    {
        float speed = 10f;

        for (int i = 0; i < 10; i++)
        {
            transform.Translate(new Vector2(forward, 0) * Time.fixedDeltaTime * speed
                 + Vector2.up * Time.fixedDeltaTime * speed);
            yield return null;
        }
    }

}

class PhysicsUpdate
{

    public void Update(Player player)
    {
        player.onLeftWall
            = Physics2D.Raycast(player.transform.position + new Vector3(0, -0.5f, 0), Vector2.left, 0.52f, player.objectLayer);
        player.onRightWall
            = Physics2D.Raycast(player.transform.position + new Vector3(0, -0.5f, 0), Vector2.right, 0.52f, player.objectLayer);

        player.onWall = player.onLeftWall || player.onRightWall;

        Vector2 velocity = player.GetComponent<Rigidbody2D>().velocity;

        if (velocity.y < player.maxFallSpeed)
        {
            velocity.y = player.maxFallSpeed;
            player.GetComponent<Rigidbody2D>().velocity = velocity;
        }


    }
}

class SpriteUpdate
{
    public void Update(Player player)
    {
        if (player.canDash)
            player.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        else
            player.gameObject.GetComponent<Renderer>().material.color = Color.green;

    }
}



