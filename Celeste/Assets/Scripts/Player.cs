using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public IBaseState state;
    public IBaseState lastState;
    public Rigidbody2D playerRigidbody;
    public GameObject playerObject;



    public LayerMask groundLayer;//用于检测地面
    public LayerMask wallLayer;//用于检测墙体

    public AnimationCurve jumpCurve;//跳跃速度
    public AnimationCurve moveCurve;
    public AnimationCurve runCurve;
    public AnimationCurve slowCurve;

    public int forward = 1;//玩家朝向，1为右，-1为左
    public float normalGravity;//获得玩家重力
    public float maxFallSpeed = -6;//最大下落速度
    public bool onGround;//是否在地面

    public float h;//
    public float timeCounter = 0;//移动计时器
    public float moveSpeed = 5;//移动速度
    public float jumpSpeed = 10;//跳跃速度


    public bool startDash;//开始冲刺
    public bool canDash;//是否能冲刺
    public int dashDirect;//冲刺方向
    public int dashTime = 15;//冲刺持续时间
    public float dashSpeed = 40;//冲刺速度

    public bool onWall;//是否在墙上
    public bool onLeftWall;//左边
    public bool onRightWall;//右边
    public float slideSpeed = 2;//爬墙速度
    public float slideTime = 20f;//最大爬墙时间
    public bool slideJump;//爬墙跳跃
    public bool offJump;


    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        normalGravity = playerRigidbody.gravityScale;
        playerRigidbody.velocity = Vector2.zero;
        state = new MoveState(this);
        lastState = null;
    }


    public void SetPlayerState(IBaseState newState)
    {
        lastState = state;
        state.Finish();
        newState.Enter();
        state = newState;

    }


    private void Update()
    {
        onGround = Physics2D.Raycast(transform.position, Vector2.down + Vector2.left, 0.85f, groundLayer) || Physics2D.Raycast(transform.position, Vector2.down + Vector2.right, 0.85f, groundLayer);
        onLeftWall = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector2.left, 0.52f, wallLayer);
        onRightWall = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector2.right, 0.52f, wallLayer);
        onWall = onLeftWall || onRightWall;

        if (forward == -1)
            transform.localScale = new Vector3(-0.65f, 1, 1);
        else
            transform.localScale = new Vector3(0.65f, 1, 1);

        if (onGround)
        {
            canDash = true;
            slideTime = 10f;
        }
        if (canDash)
            playerObject.GetComponent<Renderer>().material.color = Color.blue;
        else
            playerObject.GetComponent<Renderer>().material.color = Color.green;

        state.Update();

        InputHandler hi = new InputHandler();
        Command command = hi.handlerInput();
        if (command != null)
            command.execute(this);
    }


    private void FixedUpdate()
    {
        if (onLeftWall)
        {
            if (Input.GetKey(KeyCode.RightArrow))
                forward = 1;
            else
                forward = -1;
        }
        if (onRightWall)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                forward = -1;
            else
                forward = 1;
        }
        Move();
        state.FixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Thorn")
        {

            playerObject.SetActive(false);

        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            timeCounter += Time.fixedDeltaTime;
            h = moveCurve.Evaluate(timeCounter);
        }
        else
        {
            timeCounter -= Time.fixedDeltaTime;
            h = slowCurve.Evaluate(timeCounter);
        }

        if (timeCounter > 0.3f) timeCounter = 0.3f;
        if (timeCounter < 0) timeCounter = 0;



        Vector2 velocity = playerRigidbody.velocity;
        if (velocity.y < maxFallSpeed)
        {
            velocity.y = maxFallSpeed;
            playerRigidbody.velocity = velocity;
        }


        if (Input.GetAxis("Horizontal") < 0)
            forward = -1;
        else if (Input.GetAxis("Horizontal") > 0)
            forward = 1;
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
            transform.Translate(new Vector2(forward, 0) * Time.deltaTime * dashSpeed * 0.5f);
            yield return null;
        }
    }

    public IEnumerator SlideJump()
    {
        for (int i = 0; i < 7; i++)
        {
            transform.Translate(Vector2.up * Time.deltaTime * dashSpeed * 0.2f);
            yield return null;
        }
        slideJump = true;
    }
    public IEnumerator OffJump()
    {
        for (int i = 0; i < 7; i++)
        {
            transform.Translate(new Vector2(forward, 0) * Time.deltaTime * dashSpeed * 0.2f + Vector2.up * Time.deltaTime * dashSpeed * 0.3f);
            yield return null;
        }
        offJump = true;
    }



}




