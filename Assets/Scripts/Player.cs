using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public LayerMask objectLayer;

    /// <summary>
    /// 速度曲线
    /// </summary>
    public AnimationCurve jumpCurve;
    public AnimationCurve moveCurve;
    public AnimationCurve runCurve;
    public AnimationCurve slowCurve;

    public int forward = 1;//玩家朝向，1为右，-1为左

    public float normalGravity;
    public float maxFallSpeed = -10;

    public float moveBase;
    public float timeCounter;//移动计时器
    public float moveSpeed = 10;//移动速度


    public bool onGround;

    public bool canDash;
    public bool canSlide;

    public bool onWall;
    public bool onLeftWall;
    public bool onRightWall;

    public Vector2 velocity;

    public int coyote_counter;
    public int coyote_max = 4;

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

    public event Action<int> PosChangeEvent;

    void Awake()
    {
        fsm = new FSMSystem();
        FSM.AddState(new MoveState());
        FSM.AddState(new JumpState());
        FSM.AddState(new DashState());
        FSM.AddState(new SlideState());

        physics = new PhysicsUpdate(this);
        sprite = new SpriteUpdate(this);

        PosChangeEvent += Camera.main.GetComponent<SceneManager>().CameraFollow;
        PosChangeEvent += Camera.main.GetComponent<SceneManager>().ArchivePosChange;

    }

    void Start()
    {
        normalGravity = GetComponent<Rigidbody2D>().gravityScale;
    }



    void Update()
    {
        sprite.Update(this);

        if (FSM.CurrentState != null)
        {
            FSM.CurrentState.Update(this);
        }
    }


    void FixedUpdate()
    {
        if (coyote_counter > 0)
            coyote_counter--;

        physics.Update(this);

        if (PosChangeEvent != null)
        {
            if (transform.position.x < 14.5f)
            {
                PosChangeEvent(0);
            }
            if (transform.position.x > 14.5f)
            {
                PosChangeEvent(1);
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Interacitve>() != null)
        {
            collision.gameObject.GetComponent<Interacitve>().Interaction(this);
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

}

class PhysicsUpdate
{

    Rigidbody2D rigidbody2D;

    public PhysicsUpdate(Player player)
    {
        rigidbody2D = player.GetComponent<Rigidbody2D>();
    }

    public void Update(Player player)
    {
        player.velocity = rigidbody2D.velocity;

        player.onLeftWall
            = Physics2D.Raycast(player.transform.position + new Vector3(0, -0.5f, 0), Vector2.left, 0.52f, player.objectLayer);
        player.onRightWall
            = Physics2D.Raycast(player.transform.position + new Vector3(0, -0.5f, 0), Vector2.right, 0.52f, player.objectLayer);

        player.onWall = player.onLeftWall || player.onRightWall;



        if(player.onGround == true)
        {
            player.coyote_counter = player.coyote_max;
        }


        Vector2 velocity = rigidbody2D.velocity;

        if (velocity.y < player.maxFallSpeed)
        {
            velocity.y = player.maxFallSpeed;
            rigidbody2D.velocity = velocity;
        }

        if (Math.Abs(InputHandler.Instance.DirAxis.x) > 0.05f)
        {
            player.forward = InputHandler.Instance.DirAxis.x > 0 ? 1 : -1;

            player.moveBase = player.moveCurve.Evaluate(player.timeCounter);

            player.timeCounter = player.timeCounter <= 0.3f ?
                player.timeCounter += Time.fixedDeltaTime : 0.3f;
        }
        else
        {
            player.moveBase = player.slowCurve.Evaluate(player.timeCounter);
            player.timeCounter = player.timeCounter > 0f ?
            player.timeCounter -= Time.fixedDeltaTime : 0f;
        }

    }
}

class SpriteUpdate
{
    Renderer renderer;

    public SpriteUpdate(Player player)
    {
        renderer = player.gameObject.GetComponent<Renderer>();
    }

    public void Update(Player player)
    {
        renderer.material.color = player.canDash ? Color.blue : Color.green;
    }
}

