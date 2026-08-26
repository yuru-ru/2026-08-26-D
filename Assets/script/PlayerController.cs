using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーの移動・ジャンプ・向き反転・Animator制御
///
/// Animator Parameters:
///     Speed      : Float
///     IsGrounded : Bool
///     Jump       : Trigger
///
/// 必要なCollider:
///     Player : BoxCollider2D + Rigidbody2D
///     Ground : Collider2D
///
/// GroundのLayerを「Ground」に設定してください。
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("ジャンプ設定")]
    [SerializeField] private float jumpForce = 12f;

    [Header("地面設定")]
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator animator;

    private float moveInput;
    private bool isGrounded;

    private bool facingRight = true;

    // Animator Parameter
    private static readonly int SpeedHash =
        Animator.StringToHash("Speed");

    private static readonly int IsGroundedHash =
        Animator.StringToHash("IsGrounded");

    private static readonly int JumpTriggerHash =
        Animator.StringToHash("Jump");


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        // 最初に地面判定
        CheckGrounded();

        // 移動入力
        GetMoveInput();

        // ジャンプ入力
        CheckJumpInput();

        // Animator更新
        UpdateAnimatorParameters();

        // 向き変更
        FlipCheck();
    }


    private void FixedUpdate()
    {
        Move();
    }


    /// <summary>
    /// 左右入力
    /// </summary>
    private void GetMoveInput()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
        {
            moveInput = 0f;
            return;
        }

        moveInput = 0f;

        if (keyboard.aKey.isPressed ||
            keyboard.leftArrowKey.isPressed)
        {
            moveInput = -1f;
        }
        else if (keyboard.dKey.isPressed ||
                 keyboard.rightArrowKey.isPressed)
        {
            moveInput = 1f;
        }
    }


    /// <summary>
    /// ジャンプ入力
    /// </summary>
    private void CheckJumpInput()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
            return;

        bool jumpPressed =
            keyboard.spaceKey.wasPressedThisFrame ||
            keyboard.wKey.wasPressedThisFrame ||
            keyboard.upArrowKey.wasPressedThisFrame;

        // 地面にいるときだけジャンプ
        if (jumpPressed && isGrounded)
        {
            Jump();
        }
    }


    /// <summary>
    /// ジャンプ
    /// </summary>
    private void Jump()
    {
        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            jumpForce
        );

        // ジャンプ中は接地していない
        isGrounded = false;

        // Animatorへジャンプ通知
        animator.SetTrigger(JumpTriggerHash);
    }


    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        rb.linearVelocity = new Vector2(
            moveInput * moveSpeed,
            rb.linearVelocity.y
        );
    }


    /// <summary>
    /// BoxCollider2Dを使った接地判定
    /// </summary>
    private void CheckGrounded()
    {
        Bounds bounds = boxCollider.bounds;

        // BoxColliderの下側を少しだけ広げる
        Vector2 checkCenter = new Vector2(
            bounds.center.x,
            bounds.min.y - 0.02f
        );

        Vector2 checkSize = new Vector2(
            bounds.size.x * 0.9f,
            0.08f
        );

        Collider2D hit = Physics2D.OverlapBox(
            checkCenter,
            checkSize,
            0f,
            groundLayer
        );

        isGrounded = hit != null;
    }


    /// <summary>
    /// Animatorの値を更新
    /// </summary>
    private void UpdateAnimatorParameters()
    {
        animator.SetFloat(
            SpeedHash,
            Mathf.Abs(moveInput)
        );

        animator.SetBool(
            IsGroundedHash,
            isGrounded
        );
    }


    /// <summary>
    /// 向き変更
    /// </summary>
    private void FlipCheck()
    {
        if (moveInput > 0f && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0f && facingRight)
        {
            Flip();
        }
    }


    /// <summary>
    /// キャラクター反転
    /// </summary>
    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }


    /// <summary>
    /// 接地判定をSceneビューに表示
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();

        if (col == null)
            return;

        Bounds bounds = col.bounds;

        Vector2 checkCenter = new Vector2(
            bounds.center.x,
            bounds.min.y - 0.02f
        );

        Vector2 checkSize = new Vector2(
            bounds.size.x * 0.9f,
            0.08f
        );

        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(
            checkCenter,
            checkSize
        );
    }
}