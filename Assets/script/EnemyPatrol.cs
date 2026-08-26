using UnityEngine;

/// <summary>
/// 敵の左右往復移動＋プレイヤーへの接触ダメージ。
/// アニメーション不要のシンプルな敵として想定（スプライト固定でOK）。
/// </summary>
public class EnemyPatrol : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float patrolDistance = 3f;

    [Header("接触ダメージ")]
    [SerializeField] private int contactDamage = 1;

    private Vector3 startPos;
    private int direction = 1;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float step = direction * moveSpeed * Time.deltaTime;
        transform.position += Vector3.right * step;

        if (Mathf.Abs(transform.position.x - startPos.x) >= patrolDistance)
        {
            direction *= -1;
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(contactDamage);
        }
    }
}
