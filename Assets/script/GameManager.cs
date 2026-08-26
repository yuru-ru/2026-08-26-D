using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// スコア管理・ゲームオーバー・リスタートを担当するシングルトン。
/// プレイヤーのHealthコンポーネントのonDeathイベントにGameOver()を接続する想定。
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int score;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public int GetScore() => score;

    public void GameOver()
    {
        Time.timeScale = 0f;
        // ここでゲームオーバーUIをアクティブにする処理を追加してください
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
