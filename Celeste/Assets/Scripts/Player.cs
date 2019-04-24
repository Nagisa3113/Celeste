using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    public GameObject playerObject;

    public FSMSystem fsm = new FSMSystem();
    public FSMState jumpState;
    public FSMState moveState;
    public FSMState dashState;
    public FSMState slideState;

    public LayerMask objectLayer;//检测

    public AnimationCurve jumpCurve;//跳跃速度
    public AnimationCurve moveCurve;
    public AnimationCurve runCurve;
    public AnimationCurve slowCurve;

    public int forward = 1;//玩家朝向，1为右，-1为左
    public float normalGravity;//获得玩家重力
    public float maxFallSpeed = -6;//最大下落速度
    public bool onGround;//是否在地面

    public float moveBase = 0;
    public float timeCounter = 0;//移动计时器
    public float moveSpeed = 8;//移动速度
    public float jumpSpeed = 8;//跳跃速度


    public bool startDash;//开始冲刺
    public bool canDash;//是否能冲刺
    public int dashDirect;//冲刺方向
    public int dashTime = 15;//冲刺持续时间
    public float dashSpeed = 10;//冲刺速度

    public bool onWall;//是否在墙上
    public bool onLeftWall;//左边
    public bool onRightWall;//右边  
    public float slideSpeed = 2;//爬墙速度
    public float slideTime = 20f;//最大爬墙时间
    public bool slideJump;//爬墙跳跃
    public bool offJump;


    public int buffer_counter;
    public int buffer_max = 30;
    public int coyote_counter;
    public int coyote_max = 30;

    PhysicsUpdate physics = new PhysicsUpdate();
    SpriteUpdate sprite = new SpriteUpdate();
    InputHandler inputHandler = new InputHandler();

    private void Awake()
    {
        fsm = new FSMSystem();
        moveState = new MoveState(this);
        jumpState = new JumpState(this);
        dashState = new DashState(this);
        slideState = new SlideState(this);

        fsm.AddState(moveState);
        fsm.AddState(jumpState);
        fsm.AddState(dashState);
        fsm.AddState(slideState);
    }

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        normalGravity = playerRigidbody.gravityScale;
        playerRigidbody.velocity = Vector2.zero;

    }


    private void Update()
    {

        sprite.Update(this);

        fsm.CurrentState.InputHandle();

        if (fsm.CurrentState != null)
            fsm.CurrentState.Act();
    }


    private void FixedUpdate()
    {
        physics.Update(this);
        inputHandler.Update(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Thorn")
        {
            playerObject.SetActive(false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        foreach(ContactPoint2D contact in collision.contacts)
        {
            if ((int)contact.normal.y == 1)
            {
                onGround = true;
            }
            if ((int)contact.normal.x == 1)
            {
                onLeftWall = true;
            }
            else if ((int)contact.normal.x == -1)
            {
                onRightWall = true;
            }
        }
    

    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        //coyote_counter = 0;

        onGround = false;
        onLeftWall = false;
        onRightWall = false;
    }


    public IEnumerator Dash()
    {
        if (startDash)
        {
            canDash = false;
            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                dashDirect = 1;
            }
            else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                dashDirect = 3;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                dashDirect = 7;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                dashDirect = 5;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                dashDirect = 2;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                dashDirect = 6;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                dashDirect = 8;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                dashDirect = 4;
            }
            else
            {
                if (forward == -1)
                {
                    dashDirect = 6;
                }
                else
                {
                    dashDirect = 2;
                    //playerrigidbody.velocity = new Vector2(player.dashSpeed.0);
                    //playerrigidbody.AddForce(new Vector2(100, 0), ForceMode2D.Impulse);
                }
            }
        }
        if (dashDirect != 0)
            for (int i = 0; i < dashTime; i++)
            {
                switch (dashDirect)
                {
                    case 1:
                        transform.Translate(Vector2.right * Time.deltaTime * dashSpeed + Vector2.up * Time.deltaTime * dashSpeed);
                        break;
                    case 2:
                        transform.Translate(Vector2.right * Time.deltaTime * dashSpeed);
                        break;

                    case 3:
                        transform.Translate(Vector2.right * Time.deltaTime * dashSpeed + Vector2.down * Time.deltaTime * dashSpeed);
                        break;

                    case 4:
                        transform.Translate(Vector2.down * Time.deltaTime * dashSpeed);
                        break;

                    case 5:
                        transform.Translate(Vector2.left * Time.deltaTime * dashSpeed + Vector2.down * Time.deltaTime * dashSpeed);
                        break;

                    case 6:
                        transform.Translate(Vector2.left * Time.deltaTime * dashSpeed);
                        break;

                    case 7:
                        transform.Translate(Vector2.left * Time.deltaTime * dashSpeed + Vector2.up * Time.deltaTime * dashSpeed);
                        break;

                    case 8:
                        transform.Translate(Vector2.up * Time.deltaTime * dashSpeed);
                        break;
                }
                yield return null;
            }
        startDash = false;
        dashDirect = 0;
    }

    public IEnumerator SlideMove()
    {
        for (int i = 0; i < 7; i++)
        {
            transform.Translate(new Vector2(forward, 0) * Time.fixedDeltaTime * 4f);
            yield return null;
        }
    }

    public IEnumerator SlideJump()
    {
        for (int i = 0; i < 7; i++)
        {
            transform.Translate(Vector2.up * Time.deltaTime * 2f);
            yield return null;
        }
        slideJump = true;
    }

    public IEnumerator OffJump()
    {
        for (int i = 0; i < 7; i++)
        {
            transform.Translate(new Vector2(forward, 0) * Time.deltaTime * 2f + Vector2.up * Time.deltaTime * 3f);
            yield return null;
        }
        offJump = true;
    }

}

class PhysicsUpdate
{
    public void Update(Player player)
    {
        //player.onGround = Physics2D.Raycast(player.transform.position, Vector2.down + Vector2.left, 0.85f, player.objectLayer) || Physics2D.Raycast(player.transform.position, Vector2.down + Vector2.right, 0.85f, player.objectLayer);
        //player.onLeftWall = Physics2D.Raycast(player.transform.position + new Vector3(0, -0.5f, 0), Vector2.left, 0.52f, player.objectLayer);
        //player.onRightWall = Physics2D.Raycast(player.transform.position + new Vector3(0, -0.5f, 0), Vector2.right, 0.52f, player.objectLayer);
        player.onWall = player.onLeftWall || player.onRightWall;

        if (player.forward == -1)
            player.transform.localScale = new Vector3(-0.65f, 1, 1);
        else
            player.transform.localScale = new Vector3(0.65f, 1, 1);

        if (player.onGround)
        {
            player.canDash = true;
            player.slideTime = 10f;
        }


        Vector2 velocity = player.playerRigidbody.velocity;
        if (velocity.y < player.maxFallSpeed)
        {
            velocity.y = player.maxFallSpeed;
            player.playerRigidbody.velocity = velocity;
        }


    }
}

class SpriteUpdate
{
    public void Update(Player player)
    {
        if (player.canDash)
            player.playerObject.GetComponent<Renderer>().material.color = Color.blue;
        else
            player.playerObject.GetComponent<Renderer>().material.color = Color.green;

    }
}