using UnityEngine;

/// <summary>
/// プレイヤーの移動・ジャンプ・向き反転・Animator制御をまとめて行うスクリプト。
/// Animatorには以下の3パラメータのみを用意すればOK（Idle/Walk/Jumpの3状態想定）。
///   Speed      : float  (0=Idle, 0より大きい=Walk への遷移に使用)
///   IsGrounded : bool   (falseの間はJumpステートを維持)
///   Jump       : trigger(ジャンプ開始の瞬間にJumpステートへ遷移させる)
///
/// Animator Controller側の設定例:
///  - Idle -> Walk  : 条件 Speed > 0.1
///  - Walk -> Idle  : 条件 Speed < 0.1
///  - Any State -> Jump : 条件 Jump (trigger)
///  - Jump -> Idle  : 条件 IsGrounded == true
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 12f;

    [Header("接地判定")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;

    private float moveInput;
    private bool isGrounded;
    private bool facingRight = true;

    // 文字列比較を避けるためAnimatorパラメータはHash化しておく
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
    private static readonly int JumpTriggerHash = Animator.StringToHash("Jump");

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 入力取得
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        UpdateAnimatorParameters();
        FlipCheck();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        Move();
    }

    private void Move()
    {
        // Unity 6以降は rb.linearVelocity、それより前のバージョンは rb.velocity を使用してください
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        animator.SetTrigger(JumpTriggerHash);
    }

    private void CheckGrounded()
    {
        if (groundCheck == null) return;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void UpdateAnimatorParameters()
    {
        animator.SetFloat(SpeedHash, Mathf.Abs(moveInput));
        animator.SetBool(IsGroundedHash, isGrounded);
    }

    private void FlipCheck()
    {
        if (moveInput > 0 && !facingRight) Flip();
        else if (moveInput < 0 && facingRight) Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
