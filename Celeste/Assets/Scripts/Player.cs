using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public IBaseState state;

    public Rigidbody2D playerRigidbody;
    public GameObject playerObject;

    public float moveSpeed = 5;//移动速度
    public float jumpSpeed = 3;//跳跃速度
    public float dashSpeed = 40;//冲刺速度
    public float normalGravity;//获得玩家重力
    public float slideSpeed = 2;//攀爬速度
    public float maxFallVelocity = -6;//最大下落速度

    public int forward = 1;//玩家朝向

    public bool onGround = false;//是否在地面
    public bool canDash = false;//是否能冲刺
    public bool onWall = false;//是否在墙上

    public LayerMask groundLayer;//用于检测地面
    public LayerMask wallLayer;//用于检测墙体

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
        }

        if (canDash)
            playerObject.GetComponent<Renderer>().material.color = Color.blue;
        else
            playerObject.GetComponent<Renderer>().material.color = Color.green;


        Vector2 velocity = playerRigidbody.velocity;
        if (velocity.y < maxFallVelocity)
        {
            velocity.y = -5f;
            playerRigidbody.velocity = velocity;
        }

    }





    private void FixedUpdate()
    {
        if (playerRigidbody.velocity.x < 0)
            forward = -1;
        else if (playerRigidbody.velocity.x > 0)
            forward = 1;
        state.Update();
    }





}
