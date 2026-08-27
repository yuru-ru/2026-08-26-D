using UnityEngine;

/// 〇処理内容
/// PlayerのHealth死亡イベントをGameManagerへ接続する。
[RequireComponent(typeof(Health))]
public class PlayerDeath : MonoBehaviour
{
    private Health health;


    private void Awake()
    {
        health = GetComponent<Health>();
    }


    private void OnEnable()
    {
        if (health != null)
        {
            health.onDeath.AddListener(OnDeath);
        }
    }


    private void OnDisable()
    {
        if (health != null)
        {
            health.onDeath.RemoveListener(OnDeath);
        }
    }


    private void OnDeath()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }
}
