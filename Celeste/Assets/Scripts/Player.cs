using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float moveSpeed = 5;//移动速度
    public float jumpSpeed = 3;//跳跃速度
    public float dashSpeed = 40;//冲刺速度
    public float normalGravity;//获得玩家重力
    public float slideSpeed = 2;//攀爬速度

    public Rigidbody2D playerRigidbody;
    public int forward = 1;//玩家朝向
    public bool onGround = false;//是否在地面
    public bool canDash = true;//是否能冲刺
    public bool onWall = false;//是否在墙上
    public LayerMask groundLayer;//用于检测地面
    public LayerMask wallLayer;//用于检测墙体

    public PlayerBaseState state;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        normalGravity = playerRigidbody.gravityScale;
    }

    public Player()
    {
        state = new MoveState(this);
    }

    public void SetPlayerState(PlayerBaseState newState)
    {

        state.Finish();
        state = newState;
        state.Enter();
    }
    public void Update()
    {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);
        onWall = (Physics2D.Raycast(transform.position, Vector2.left, 0.52f, wallLayer) || Physics2D.Raycast(transform.position, Vector2.right, 0.52f, wallLayer));
        if (Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer))
        {
            canDash = true;

        }
        state.Update();
    }
}
