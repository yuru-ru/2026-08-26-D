using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ダメージを受けられるオブジェクトが実装するインターフェース。
/// プレイヤー・敵の両方に同じHealthコンポーネントを付けて使い回せる。
/// </summary>
public interface IDamageable
{
    void TakeDamage(int amount);
}

public class Health : MonoBehaviour, IDamageable
{
    [Header("体力設定")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float invincibleDuration = 1f;

    [Header("イベント（インスペクターでUI更新等に接続可能）")]
    public UnityEvent onDamaged;
    public UnityEvent onDeath;

    private int currentHealth;
    private bool isInvincible;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private void Awake()
    {
        currentHealth = Mathf.Max(1, maxHealth);
    }

    private void OnValidate()
    {
        maxHealth = Mathf.Max(1, maxHealth);
        invincibleDuration = Mathf.Max(0f, invincibleDuration);
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible || currentHealth <= 0 || amount <= 0)
        {
            return;
        }

        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);
        onDamaged?.Invoke();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibleFrame());
        }
    }

    private IEnumerator InvincibleFrame()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
    }

    private void Die()
    {
        if (currentHealth > 0)
        {
            return;
        }

        onDeath?.Invoke();
        gameObject.SetActive(false);
    }
}
