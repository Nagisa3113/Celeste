using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public LayerMask objectLayer;

    public AnimationCurve jumpCurve;
    public AnimationCurve moveCurve;
    public AnimationCurve runCurve;
    public AnimationCurve slowCurve;

    public int forward = 1;//1 for left,-1 for right

    public float normalGravity;

    public float moveBase;
    public float timeCounter;
    public float moveSpeed = 8;

    public float maxFallSpeed = -15;

    public bool onGround;

    public bool canDash;
    public bool canSlide;

    public bool onLeftWall;
    public bool onRightWall;
    public bool onWall
    {
        get { return onLeftWall || onRightWall; }
    }

    /// <summary>
    /// player can jump while left ground
    /// </summary>
    public int coyote_counter;
    public int coyote_max = 4;

    FSMSystem fsm;
    public FSMSystem FSM
    {
        get
        { return fsm; }
    }

    PhysicsUpdate physics;
    SpriteUpdate sprite;

    public event Action<int> PosChangeEvent;

    void Awake()
    {
        fsm = new FSMSystem();
        fsm.AddState(new MoveState());
        fsm.AddState(new JumpState());
        fsm.AddState(new DashState());
        fsm.AddState(new SlideState());

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
    }

    void FixedUpdate()
    {
        fsm.CurrentState?.Update(this);

        physics.Update(this);

        if (transform.position.x < 14.5f)
        {
            PosChangeEvent?.Invoke(0);
        }
        else if (transform.position.x > 14.5f)
        {
            PosChangeEvent?.Invoke(1);
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

    Vector2 limitedVelocity;

    public PhysicsUpdate(Player player)
    {
        rigidbody2D = player.GetComponent<Rigidbody2D>();
        limitedVelocity.y = player.maxFallSpeed;
    }

    public void Update(Player player)
    {
        player.coyote_counter = player.coyote_counter > 0 ?
             player.coyote_counter - 1 : 0;


        if (rigidbody2D.velocity.y < player.maxFallSpeed)
        {
            limitedVelocity.x = rigidbody2D.velocity.x;
            rigidbody2D.velocity = limitedVelocity;
        }

        player.onLeftWall
            = Physics2D.Raycast(player.transform.position + new Vector3(0, -0.5f, 0), Vector2.left, 0.52f, player.objectLayer);
        player.onRightWall
            = Physics2D.Raycast(player.transform.position + new Vector3(0, -0.5f, 0), Vector2.right, 0.52f, player.objectLayer);

        if (player.onGround == true)
        {
            player.coyote_counter = player.coyote_max;
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

