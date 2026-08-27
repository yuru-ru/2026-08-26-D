using UnityEngine;
using UnityEngine.SceneManagement;

/// 〇処理内容
/// ゲーム全体を管理するスクリプト。
///
/// ・スコア
/// ・ゲームオーバー
/// ・ステージクリア
/// ・リトライ
/// ・タイトルへ戻る

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("スコア")]
    [SerializeField] private int score = 0;

    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject clearPanel;

    [Header("シーン設定")]
    [SerializeField] private string titleSceneName = "Title";

    private bool gameFinished = false;


    private void Awake()
    {
        // GameManagerが2つ以上あったら削除
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // 今回はDontDestroyOnLoadを使わない
        // シーンを読み込むたびに新しいGameManagerを作る
    }


    private void Start()
    {
        Time.timeScale = 1f;

        gameFinished = false;

        // 最初は両方の画面を非表示
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (clearPanel != null)
        {
            clearPanel.SetActive(false);
        }
    }


    /// <summary>
    /// スコアを増やす
    /// </summary>
    public void AddScore(int amount)
    {
        score += amount;
    }


    /// <summary>
    /// スコアを取得
    /// </summary>
    public int GetScore()
    {
        return score;
    }


    /// <summary>
    /// ゲームオーバー
    /// </summary>
    public void GameOver()
    {
        if (gameFinished)
            return;

        gameFinished = true;

        Time.timeScale = 0f;

        if (clearPanel != null)
        {
            clearPanel.SetActive(false);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }


    /// <summary>
    /// ステージクリア
    /// </summary>
    public void Clear()
    {
        if (gameFinished)
            return;

        gameFinished = true;

        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (clearPanel != null)
        {
            clearPanel.SetActive(true);
        }
    }


    /// <summary>
    /// 現在のステージを最初からやり直す
    /// </summary>
    public void RestartLevel()
    {
        Time.timeScale = 1f;

        gameFinished = false;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }


    /// <summary>
    /// タイトル画面へ戻る
    /// </summary>
    public void LoadTitle()
    {
        Time.timeScale = 1f;

        gameFinished = false;

        SceneManager.LoadScene(titleSceneName);
    }


    /// <summary>
    /// 次のステージへ進む
    /// </summary>
    public void NextStage()
    {
        Time.timeScale = 1f;

        gameFinished = false;

        int currentScene =
            SceneManager.GetActiveScene().buildIndex;

        int nextScene = currentScene + 1;

        if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            LoadTitle();
        }
    }
}