using UnityEngine;
using UnityEngine.InputSystem;

/// 〇処理内容
/// プレイヤーの攻撃処理。
/// プレイヤーには攻撃アニメーションを用意しない前提のため、
/// 見た目の演出は行わず「攻撃判定の発生」のみを担当するシンプルな実装。
/// エフェクトを足したい場合はAttack()内でParticleやSE再生を追加してください。

public class PlayerAttack : MonoBehaviour
{
    [Header("攻撃設定")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.6f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackCooldown = 0.4f;

    private float lastAttackTime = -999f;

    private void Update()
    {
        var keyboard = Keyboard.current;
        var mouse = Mouse.current;

        bool attackPressed = (keyboard != null && keyboard.jKey.wasPressedThisFrame)
            || (mouse != null && mouse.leftButton.wasPressedThisFrame);

        if (attackPressed && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    private void Attack()
    {
        if (attackPoint == null) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
