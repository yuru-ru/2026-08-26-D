using UnityEngine;

/// 〇処理内容
/// プレイヤーが設定したY座標より下に落ちた場合、
/// ゲームオーバーにする。
public class FallDeath : MonoBehaviour
{
    [Header("このY座標より下に行ったらゲームオーバー")]
    [SerializeField] private float deathY = -10f;

    private bool alreadyDead;


    private void Update()
    {
        if (alreadyDead)
            return;

        if (transform.position.y < deathY)
        {
            alreadyDead = true;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}