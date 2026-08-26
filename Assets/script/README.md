# 2Dアクションゲーム基本セット セットアップ手順

前提: 主人公のアニメーションは **Idle(止まる)・Walk(歩く)・Jump(ジャンプ)** の3つのみ。

## 含まれるスクリプト

| ファイル | 役割 | アタッチ先 |
|---|---|---|
| PlayerController.cs | 移動・ジャンプ・向き反転・Animator制御 | プレイヤー |
| PlayerAttack.cs | 攻撃判定（アニメーションなし） | プレイヤー |
| Health.cs | 体力管理・被ダメージ（IDamageable実装） | プレイヤー・敵 共通 |
| EnemyPatrol.cs | 左右往復＋接触ダメージ | 敵 |
| CameraFollow.cs | プレイヤー追従カメラ | Main Camera |
| GameManager.cs | スコア・ゲームオーバー・リスタート | 空オブジェクト(GameManager) |

## 1. プレイヤーオブジェクトの設定

1. プレイヤー用GameObjectを作成し、以下をアタッチ:
   - `Sprite Renderer`
   - `Rigidbody2D`（Gravity Scale: 3〜5程度、Freeze Rotation Z にチェック）
   - `Collider2D`（BoxCollider2DやCapsuleCollider2D）
   - `Animator`
   - `PlayerController.cs`
   - `Health.cs`
   - `PlayerAttack.cs`（攻撃を作る場合）
2. プレイヤーの足元に空のGameObject「GroundCheck」を子として配置し、
   `PlayerController` の `Ground Check` にドラッグ＆ドロップ。
3. 地面用のオブジェクトに `Ground` レイヤーを作成して設定し、
   `PlayerController` の `Ground Layer` にそのレイヤーを指定。

## 2. Animator Controllerの作成（3ステートのみ）

1. `Assets > Create > Animator Controller` で新規作成し、プレイヤーのAnimatorにセット。
2. ステートを3つ作成: `Idle` / `Walk` / `Jump`（それぞれ対応するスプライトアニメーションを割り当て）。
3. Parametersタブで以下を追加:
   - `Speed`（Float）
   - `IsGrounded`（Bool、デフォルトtrue）
   - `Jump`（Trigger）
4. 遷移(Transition)を設定:
   - `Idle → Walk` : 条件 `Speed > 0.1`
   - `Walk → Idle` : 条件 `Speed < 0.1`
   - `Any State → Jump` : 条件 `Jump`（トリガー）
   - `Jump → Idle` : 条件 `IsGrounded == true`
   - 各遷移の `Has Exit Time` はオフにしておくと反応が良くなります。

## 3. 敵オブジェクトの設定

1. 敵用GameObjectに `Sprite Renderer` `Rigidbody2D`（Body TypeはDynamicのままでOK）
   `Collider2D` `EnemyPatrol.cs` `Health.cs` をアタッチ。
2. `EnemyPatrol` の `Contact Damage` で接触ダメージ量を設定。

## 4. カメラの設定

1. Main Cameraに `CameraFollow.cs` をアタッチし、`Target` にプレイヤーをセット。
2. ステージ端でカメラを止めたい場合は `Use Clamp` をオンにして
   `Min/Max Position` を設定。

## 5. ゲーム管理オブジェクト

1. 空のGameObjectを作成し「GameManager」と名付けて `GameManager.cs` をアタッチ。
2. プレイヤーの `Health` コンポーネントの `On Death` イベント（インスペクター上）に
   `GameManager.GameOver()` を接続すると、死亡時にゲームが一時停止します。

## 補足・カスタマイズポイント

- `rb.linearVelocity` はUnity 6以降のプロパティ名です。それより古いバージョンを
  使っている場合は `PlayerController.cs` 内の該当箇所を `rb.velocity` に置き換えてください。
- 攻撃にアニメーションを付けたくなった場合は、Animatorに `Attack` トリガーと
  Attackステートを追加し、`PlayerAttack.cs` の `Attack()` 内で
  `animator.SetTrigger("Attack")` を呼ぶだけで拡張できます。
- 落下中の表現が欲しい場合は、Jumpステート内で `Rigidbody2D.linearVelocity.y` の
  符号によってBlend Treeで上昇/下降スプライトを切り替える方法もあります
  （今回は3ステート限定のため未実装）。
