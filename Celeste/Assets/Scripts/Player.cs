using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public IBaseState state;
    public Rigidbody2D playerRigidbody;
    public GameObject playerObject;
    public LayerMask groundLayer;//用于检测地面
    public LayerMask wallLayer;//用于检测墙体


    public AnimationCurve jumpCurve;//跳跃速度
    public AnimationCurve moveCurve;
    public AnimationCurve runCurve;
    public AnimationCurve slowCurve;

    public int forward = 1;//玩家朝向，1为右，-1为左

    public float timeCounter = 0;//移动计时器
    public float h;//
    public float moveSpeed = 5;//移动速度
    public float jumpSpeed = 10;//跳跃速度
    public float dashSpeed = 40;//冲刺速度
    public float normalGravity;//获得玩家重力
    public float slideSpeed = 2;//爬墙速度
    public float slideTime = 3;//最大爬墙时间
    public float maxFallSpeed = -6;//最大下落速度

    public bool onGround = false;//是否在地面
    public bool canDash = false;//是否能冲刺
    public bool onWall = false;//是否在墙上

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        normalGravity = playerRigidbody.gravityScale;
    }

    public Player()
    {
        state = new MoveState(this);
    }

    public void SetPlayerState(IBaseState newState)
    {
        state.Finish();
        state = newState;
        state.Enter();
    }



    private void Update()
    {
        onGround = Physics2D.Raycast(transform.position, Vector2.down + Vector2.left, 0.85f, groundLayer) || Physics2D.Raycast(transform.position, Vector2.down + Vector2.right, 0.85f, groundLayer);
        onWall = (Physics2D.Raycast(transform.position+new Vector3(0,-0.5f,0), Vector2.left, 0.52f, wallLayer) || Physics2D.Raycast(transform.position+new Vector3(0,-0.5f,0), Vector2.right, 0.52f, wallLayer));
        if (onGround)
        {
            canDash = true;
            slideTime = 3f;
        }

        state.Update();

    }



    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.LeftArrow))
        {
            timeCounter += Time.fixedDeltaTime;
            if(timeCounter<0.3f)
            h  = moveCurve.Evaluate(timeCounter);
        }
        else
        {
            timeCounter -= Time.fixedDeltaTime;
            if(timeCounter>0)
            h = slowCurve.Evaluate(timeCounter);
        }
       

        if (timeCounter > 0.3f)
            timeCounter = 0.3f;
        if (timeCounter < 0)
            timeCounter = 0;



        Vector2 velocity = playerRigidbody.velocity;
        if (velocity.y < maxFallSpeed)
        {
            velocity.y = maxFallSpeed;
            playerRigidbody.velocity = velocity;
        }

        if (canDash)
            playerObject.GetComponent<Renderer>().material.color = Color.blue;
        else
            playerObject.GetComponent<Renderer>().material.color = Color.green;


        if (Input.GetAxis("Horizontal") < 0)
            forward = -1;
        else if (Input.GetAxis("Horizontal") > 0)
            forward = 1;



        state.FixedUpdate();


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Thorn") 
        {
            playerObject.SetActive(false);
                
        }
    }




}
