勉強用テキスト　困ったら見るように！
基本的にAI君丸出しだけど許して(´;ω;｀)
人によっては個性的な子がいると思うから、いざというときはほかのAI君を頼るように！

★初めに
↑
を検索かけてくれると、一気に項目が出るように作ったから出来たら使って。
※を検索すると豆知識みたいなのはいってるよ。多分２年生組はみんなわかると思う。
あと、聞かれたらこたえられるように作っているから、わざわざ、飛んで火にいる夏の虫にならないように。
聴かれなかったら答えなくても、教えなくてもよし。
どこから作ってもいいようにはしているけど、途中で動くようにはしてないかも。ちょっと様子見てほしい。

一応ループ自体はできているから安心して。

※豆知識
変数って何？＃めっちゃ簡単に言うと、常に変わる可能性のある値のこと逆は定数って言います。

※命名規則について
名前の付け方に一定のルールを決めること。
ちなみに「名前を統一する」こと自体は 命名規則を統一する と言うのが一番自然。

★TitleManagerについて
 using UnityEngine;
※豆知識unityの基本的な機能？
MonoBehaviour、[SerializeField]は基本的にunityの機能。
Unityが用意してくれている機能を使いますという意味を
using UnityEngine;この一文で書く！

using UnityEngine.SceneManagement;
// ↑ Unityの「Scene（シーン）」を切り替える機能を使えるようにする。
//   今回はSceneManager.LoadScene()でゲーム本編へ移動するために必要。

public class TitleManager : MonoBehaviour
// ↑ 「TitleManager」という名前のスクリプトを作る。
//   : MonoBehaviour によって、このスクリプトをUnityのGameObjectに
//   取り付けて使えるようになる。

classというのはこのプログラムをひとまとまりとして作るよというもの
そのまとまりの名前が今回はTitleManager
つまり、TitleManagerというクラスを作りますということ。

{
    [SerializeField] private string gameSceneName = "ingaeme";
    // ↑ ゲーム本編のScene名を保存する変数。
     これはプライベートのままInspectorから値を設定できるて言っているだけ
    //
    // [SerializeField]
    // unityの機能の一つでprivateなのにUnityのInspectorから値を変更できるようにする。
    //
    // private
    // → 基本的にこのTitleManagerの中だけで使う変数。
    ※豆知識　publicとprivateの違い。
    とても簡単に言うと、publicは値をほかのスクリプトに影響することができる
    privateはその値をそのscript内でしか使うことができない
    // string
    // → 「文字」を入れるための型。
    // gameSceneName
    // → 変数の名前。「ゲーム本編のScene名」という意味。
    // = "ingaeme";
    // → 最初から「ingaeme」という文字を入れておく。
    //
    // UnityのInspectorで「ingaeme」を別のScene名に変更することもできる。


    public void StartGame()
    // ↑ 「StartGame」という名前の処理を作る。
    //
    // public
    // → UnityのButtonのOnClickなど、外部から呼び出せるようにする。
    //
    // void
    // → この処理を実行したあと、何かの値を返さない。一回きりでしか使わない
    //
    // StartGame()
    // → 「ゲームを開始するときに実行する処理」という名前。
    //
    // 例えばUnityの「ゲームスタート」ボタンの
    // On Click()にこのStartGame()を登録すると、
    // ボタンを押したときにこの処理が実行される。
    {
        Time.timeScale = 1f;
        // ↑ Unityのゲーム内時間を「通常の速度」に戻す。
        //
        // Time.timeScaleはゲーム内時間の進む速さを表す。
        //
        // 1f
        // → 通常速度。
        //
        // 0f
        // → ゲーム内時間を停止。
        //
        // 例えばゲームオーバー画面などで、
        // Time.timeScale = 0f;
        // にしてゲームを止めていた場合、
        // そのままタイトル画面→ゲーム開始すると
        // ゲーム本編まで時間が止まったままになる可能性がある。
        //
        // そのためゲーム開始時に「1」に戻している。


        SceneManager.LoadScene(gameSceneName);
        // ↑ 指定したSceneを読み込んで、Sceneを切り替える。
        //
        // SceneManager
        // → UnityのSceneを管理する機能。
        ※豆知識　そのためシーンを遷移する場合、SceneManagerというscriptを作ってはならない。
        unityの機能が先に自分の作ったスクリプトを先に呼んでしまうため、順番がおかしくなってうまく動かないということが起こる
        // LoadScene
        // → Sceneを読み込む命令。
        // (gameSceneName)
        // → どのSceneを読み込むか指定している。ここは作ったシーン、どれを読むか言っているだけ。
        //
        // 今回gameSceneNameには
        // "ingaeme"
        // が入っているので、
        // 実際には、
        // SceneManager.LoadScene("ingaeme");
        // と同じことをしている。
        // つまり、
        // 「ingaemeという名前のSceneを読み込んでください」
        // という命令。
    }
    // ↑ StartGame()の処理ここまで。


}
// ↑ TitleManagerというクラス全体ここまで。

→日本語にするとこのスクリプトは...
ゲームスタートボタンを押す
        ↓
StartGame()を実行
        ↓
ゲーム時間を1倍速に戻す
        ↓
「ingaeme」というSceneを読み込む
        ↓
ゲーム本編スタート
上記の流れになる。



★CameraFollowのスクリプト開設
using UnityEngine;
// ↑ Unityの基本機能を使えるようにする。
//   Transform、Vector3、Vector2、Mathf、Timeなどを使うために必要。

/* 
/// <summary>
/// プレイヤーにカメラを追従させる。ステージ端でのクランプ機能付き。
/// </summary>
*/
// ↑ これは「コメント」なのでプログラムの動作には影響しない。
※ワンポイント豆知識
→//や/*はメモとして使っている場面が多い。やった内容、書いた内容を忘れないようにつけるもの
コメントアウトともいう。自分で作るようになったら、何をどう追加したのかを書くようにしよう。
// 
//   このスクリプトが何をするものなのかを説明している。
// 
//   「プレイヤーにカメラを追従させる」
//   → プレイヤーが動いたらカメラも動く。
//
//   「ステージ端でのクランプ機能付き」
//   → カメラがステージの外まで行かないように、
//      移動できる範囲を制限できる。


public class CameraFollow : MonoBehaviour
// ↑ 「CameraFollow」という名前のクラスを作る。
//
//   : MonoBehaviour
//   → UnityのGameObjectに取り付けて使えるスクリプトにする。
//
//   今回はこのスクリプトを「Main Camera」に取り付けて使う。


{
    [SerializeField] private Transform target;
    // ↑ カメラが追いかける対象を保存する変数。
    //
    //   Transform
    //   → GameObjectの「位置・回転・大きさ」などを管理するコンポーネント。
    //
    //   target
    //   → 「追いかける相手」という意味の変数名。
    //
    //   [SerializeField]
    //   → privateだけどUnityのInspectorから設定できるようにする。
    //
    //   Unityではここに「Player」を設定する。
    //
    //   つまり、
    //
    //   target = PlayerのTransform
    //
    //   という状態にする。


    [SerializeField] private float smoothSpeed = 5f;
    // ↑ カメラがプレイヤーに追いつく速さを設定する変数。
    //
    //   float
    //   → 小数を扱える数字の型。
    //
    //   smoothSpeed
    //   → カメラの追従をどれくらい滑らかにするかを表す名前。
    //
    //   5f
    //   → 初期値として「5」を入れている。
    //
    //   数字を大きくすると、基本的にはカメラが
    //   プレイヤーの動きについていきやすくなる。
    //
    //   小さくすると、カメラがゆっくり追いかけるようになる。
    //
    //   この値もInspectorから変更できる。


    [SerializeField] private Vector3 offset = new Vector3(0f, 1f, -10f);
    // ↑ プレイヤーからカメラをどれくらい離すかを設定する。
    //
    //   Vector3
    //   → X・Y・Zの3つの数字をまとめて扱うもの。
    //
    //   X → 横方向
    //   Y → 縦方向
    //   Z → 奥行き方向
    //
    //   new Vector3(0f, 1f, -10f)
    //   → X = 0
    //      Y = 1
    //      Z = -10
    //
    //   つまりプレイヤーの位置から、
    //
    //      横 → 0
    //      縦 → +1
    //      奥行き → -10
    //
    //   の位置にカメラを置く。
    //
    //   2Dゲームの場合、カメラがZ=-10、
    //   プレイヤーがZ=0付近という設定がよくある。

※豆知識　ちなみにXYZわからない子いたら、それは個人でフォローしてね。関数よ、関数。

    [Header("移動範囲の制限（ステージの端で使用）")]
    // ↑ Inspector上に見出しを表示する。
    //
    //   この下にある変数が、
    //   「カメラの移動範囲を制限するための設定」
    //   だと分かりやすくするためのもの。
    //
    //   プログラムの動作そのものにはほぼ影響しない。
    //   Inspectorを整理して見やすくするための機能。


    [SerializeField] private bool useClamp = false;
    // ↑ カメラの移動範囲を制限するかどうかを設定する。
    //
    //   bool
    //   → true（はい）かfalse（いいえ）の2種類だけを持つ型。
    //
    //   useClamp
    //   → 「クランプ機能を使うか？」という意味。
    //
    //   false
    //   → 最初はクランプを使わない。
    //
    //   Inspectorでチェックを入れるとtrueになり、
    //   カメラの移動範囲が制限される。


    [SerializeField] private Vector2 minPosition;
    // ↑ カメラが移動できる「最低位置」を設定する。
    //
    //   Vector2
    //   → X・Yの2つの数字をまとめて扱う。
    //
    //   minPosition
    //   → minimum position（最小位置）。
    //
    //   例えば、
    //
    //   X = -10
    //   Y = -5
    //
    //   なら、カメラはそれより左・下には行かない。


    [SerializeField] private Vector2 maxPosition;
    // ↑ カメラが移動できる「最高位置」を設定する。
    //
    //   maxPosition
    //   → maximum position（最大位置）。
    //
    //   例えば、
    //
    //   X = 100
    //   Y = 20
    //　
    //   なら、カメラはそれより右・上には行かない。


    private void LateUpdate()
    // ↑ 毎フレーム実行されるUnityの特別な関数。
    //
    //   Update()と似ているが、
    //   LateUpdate()は「通常のUpdate処理が終わったあと」に実行される。
    //
    //   カメラ追従では、
    //
    //   Playerが動く
    //       ↓
    //   Playerの位置が確定する
    //       ↓
    //   カメラがPlayerの位置を見る
    //
    //   という順番にしたいため、LateUpdate()がよく使われる。

    {
        if (target == null)
        // ↑ targetが設定されているか確認する。
        //
        //   null
        //   → 「何も入っていない」という意味。
        //
        //   target == null
        //   → 「追いかける対象が設定されていない？」
        //   という条件。

        {
            return;
            // ↑ targetが設定されていなかったら、
            //   ここでLateUpdate()の処理を終了する。
            //
            //   targetがない状態で
            //   target.position
            //   を使うとエラーになる。
            //　
            //   それを防ぐための安全対策。
            ちなみにエラー内容はNULLって言って、何も入ってないよって警告されます。
        }

        Vector3 desiredPosition = target.position + offset;
        // ↑ カメラが最終的に行きたい位置を計算する。
        //
        //   target.position
        //   → プレイヤーの現在位置。
        //
        //   offset
        //   → プレイヤーからカメラをどれだけ離すか。
        //
        //   それを足すことで、
        //
        //   「プレイヤーの位置 + カメラの距離」
        //
        //   というカメラの目的地を作る。
        //
        //   例えばプレイヤーが、
        //
        //   X = 10
        //   Y = 5
        //   Z = 0
        //
        //   だった場合、
        //
        //   offset = (0, 1, -10)
        //
        //   なので、
        //
        //   desiredPosition = (10, 6, -10)
        //
        //   になる。

        Vector3 smoothed = Vector3.Lerp(
            transform.position,
            desiredPosition,
            1f - Mathf.Exp(-smoothSpeed * Time.deltaTime)
        );
        // ↑ カメラをいきなり目的地へ移動させず、
        //   「滑らかに近づける」処理。
        //
        //   Vector3.Lerp()
        //   → 2つの位置の間を補間するUnityの機能。
        //
        //   transform.position
        //   → 現在のカメラの位置。
        //
        //   desiredPosition
        //   → カメラが最終的に行きたい位置。
        //
        //   つまり、
        //
        //   現在のカメラ位置
        //          ↓
        //       少しずつ
        //          ↓
        //   プレイヤーに合わせた位置
        //
        //   と移動させている。
        //
        //   そのため、プレイヤーが動いたときに
        //   カメラが「ガクッ」と瞬間移動するのではなく、
        //   スーッと追いかけるように見える。
        //
        //
        //   Mathf.Exp()
        //   → 指数関数を計算するUnityの数学機能。
        //
        //   Time.deltaTime
        //   → 前のフレームから今回のフレームまでに
        //      何秒経過したか。
        //
        //   この計算方法によって、
        //   フレームレートが変わっても比較的自然な
        //   滑らかさになるようにしている。


        if (useClamp)
        // ↑ 「クランプ機能を使う設定になっているか？」を確認する。
        //
        //   useClamp == true
        //   なら、この中の処理を実行する。
        //
        //   falseなら、この中を全部飛ばす。


        {
            smoothed.x = Mathf.Clamp(
                smoothed.x,
                minPosition.x,
                maxPosition.x
            );
            // ↑ カメラのX座標が、
            //   minPosition.x ～ maxPosition.x
            //   の範囲内に収まるようにする。
            //
            //   Mathf.Clamp()
            //   → 数字を指定した範囲内に収める機能。
            //
            //   例えば、
            //
            //   最小値 = 0
            //   最大値 = 100
            //
            //   だった場合、
            //
            //   -20 → 0
            //   50  → 50
            //   150 → 100
            //
            //   になる。
            //
            //   つまりカメラがステージの左端・右端を
            //   超えないようにしている。


            smoothed.y = Mathf.Clamp(
                smoothed.y,
                minPosition.y,
                maxPosition.y
            );
            // ↑ 今度はY座標を制限する。
            //
            //   Xと同じ仕組み。
            //
            //   カメラがステージの上端・下端を
            //   超えないようにしている。
            //
            //   これによって、
            //
            //       ┌───────────────┐
            //       │   ステージ    │
            //       │               │
            //       │   カメラ      │
            //       │    の範囲     │
            //       │               │
            //       └───────────────┘
            //
            //   のように移動範囲を決められる。


        }


        transform.position = smoothed;
        // ↑ 最終的に計算した位置を、
        //   実際のカメラの位置に設定する。
        //
        //   transform
        //   → このスクリプトが付いているGameObjectのTransform。
        //
        //   今回はCameraにこのスクリプトを付けるので、
        //   transform.position
        //   は「カメラの位置」になる。
        //
        //   つまり、
        //
        //   計算した位置
        //       ↓
        //   smoothed
        //       ↓
        //   カメラの位置に設定
        //
        //   という流れ。


    }
    // ↑ LateUpdate()終了。


}
// ↑ CameraFollowクラス終了。

Camera Follow

Target
└── Player             ← 追いかける相手

Smooth Speed
└── 5                  ← 追従の滑らかさ

Offset
├── X = 0
├── Y = 1
└── Z = -10            ← プレイヤーからのカメラ位置

移動範囲の制限（ステージの端で使用）

Use Clamp
└── □                  ← チェックすると範囲制限ON

Min Position
├── X
└── Y                  ← カメラの最低位置

Max Position
├── X
└── Y                  ← カメラの最高位置


Playerの位置
    ↓
target.position
    ↓
＋ offset
    ↓
desiredPosition
    ↓
Lerpで滑らかにする
    ↓
smoothed
    ↓
クランプで範囲制限
    ↓
transform.position
    ↓
実際のCameraが動く



★EnemyPatrolのスクリプトについて
using UnityEngine;
// ↑ Unityの基本機能を使えるようにする。
//   Vector3、Time、Mathf、MonoBehaviourなどを使うために必要。
/*
/// <summary>
/// 敵の左右往復移動＋プレイヤーへの接触ダメージ。
/// アニメーション不要のシンプルな敵として想定（スプライト固定でOK）。
/// </summary>
*/
// ↑ これはコメント。
//   プログラムの動作には影響しない。
// 
//   このスクリプトが何をするものなのかを書いている。
//
//   今回のEnemyPatrolは、
//
//   ① 敵を左右に往復させる
//   ② プレイヤーに触れたらダメージを与える
//
//   という2つの仕事をする。


public class EnemyPatrol : MonoBehaviour
// ↑ 「EnemyPatrol」というクラスを作る。
//
//   Enemy = 敵
//   Patrol = 巡回する・行ったり来たりする
//
//   : MonoBehaviour
//   → UnityのGameObjectに取り付けて使えるスクリプトにする。
//
//   つまり、このスクリプトを敵のGameObjectに付けることで、
//   その敵が左右に動くようになる。


{
    [Header("移動設定")]
    // ↑ UnityのInspectorに「移動設定」という見出しを表示する。
    //
    //   これはInspectorを見やすくするためのもの。
    //   ゲームの動作そのものには影響しない。


    [SerializeField] private float moveSpeed = 2f;
    // ↑ 敵が移動する速度を設定する変数。
    //
    //   float
    //   → 小数を扱える数字の型。
    //
    //   moveSpeed
    //   → 移動速度という意味。
    //
    //   = 2f
    //   → 最初の速度を「2」にする。
    //
    //   [SerializeField]
    //   → privateだけどUnityのInspectorから変更できる。
    //
    //   例えばInspectorで「5」にすると、
    //   敵の移動速度が速くなる。


    [SerializeField] private float patrolDistance = 3f;
    // ↑ 敵が左右にどこまで移動するかを決める変数。
    //
    //   patrol = 巡回
    //   distance = 距離
    //
    //   3fなので、敵は最初の位置から
    //   左右それぞれ3くらいの距離まで移動する。
    //
    //   例えば敵の最初のX座標が10なら、
    //
    //   左 → X = 7
    //   中央 → X = 10
    //   右 → X = 13
    //
    //   あたりを往復する。


    [Header("接触ダメージ")]
    // ↑ Inspectorに「接触ダメージ」という見出しを表示する。
    //   これもInspectorを整理するためのもの。


    [SerializeField] private int contactDamage = 1;
    // ↑ 敵がプレイヤーに接触したときに与えるダメージ量。
    //
    //   int
    //   → 整数を扱う型。
    //
    //   1、2、3、10などを扱える。
    //
    //   contactDamage = 1
    //   → 接触すると「1ダメージ」。
    //
    //   Inspectorから2や5などに変更することもできる。


    private Vector3 startPos;
    // ↑ 敵が最初にいた位置を保存する変数。
    //
    //   Vector3
    //   → X・Y・Zの3つの座標をまとめて扱う。
    //
    //   startPos
    //   → start position（開始位置）の略。
    //
    //   この敵が「どこからスタートしたのか」を覚えておく。
    //
    //   例えば最初の位置が、
    //
    //   X = 10
    //   Y = 5
    //   Z = 0
    //
    //   なら、
    //
    //   startPos
    //   ↓
    //   (10, 5, 0)
    //
    //   を保存する。


    private int direction = 1;
    // ↑ 敵が今どちら向きに動いているかを保存する。
    //
    //   1
    //   → 右方向
    //
    //   -1
    //   → 左方向
    //
    //   最初は1なので、ゲーム開始時は右に進む。
    //
    //   例えば、
    //
    //   direction = 1
    //   → 右へ
    //
    //   direction = -1
    //   → 左へ
    //
    //   という仕組み。


    private void Awake()
    // ↑ UnityがGameObjectを読み込んだときに自動的に一度だけ呼び出す関数。
    //
    //   自分でAwake()を呼び出す必要はない。
    //
    //   敵がゲームに登場したときの初期設定などに使う。


    {
        startPos = transform.position;
        // ↑ 敵の現在位置をstartPosに保存する。
        //
        //   transform.position
        //   → この敵GameObjectの現在位置。
        //
        //   例えば敵が、
        //
        //   X = 5
        //   Y = 2
        //   Z = 0
        //
        //   に置かれていたら、
        //
        //   startPos = (5, 2, 0)
        //
        //   になる。
        //
        //   これを後で、
        //
        //   「最初の位置からどれくらい離れた？」
        //
        //   と判断するために使う。


    }
    // ↑ Awake()終了。


    private void Update()
    // ↑ Unityが毎フレーム自動的に呼び出す関数。
    //
    //   ゲームが動いている間、何度も実行される。
    //
    //   今回はここで敵を少しずつ移動させる。


    {
        float step = direction * moveSpeed * Time.deltaTime;
        // ↑ 今回の1フレームで敵を何個分動かすか計算する。
        //
        //   direction
        //   → 1なら右、-1なら左。
        //
        //   moveSpeed
        //   → 移動速度。
        //
        //   Time.deltaTime
        //   → 前のフレームから今回のフレームまでに
        //      経過した時間。
        //
        //   これを掛けることで、
        //
        //   「毎秒○○の速度で動く」
        //
        //   という動きにできる。
        //
        //   例えば、
        //
        //   direction = 1
        //   moveSpeed = 2
        //   Time.deltaTime = 0.016
        //
        //   なら、
        //
        //   step = 1 × 2 × 0.016
        //        = 0.032
        //
        //   となる。
        //
        //   つまり、そのフレームでは0.032だけ移動する。


        transform.position += Vector3.right * step;
        // ↑ 実際に敵の位置を移動させる。
        //
        //   Vector3.right
        //   → (1, 0, 0)
        //
        //   つまり「右方向」を表す。
        //
        //   そこにstepを掛けることで、
        //   実際に移動する距離を作る。
        //
        //   そして、
        //
        //   transform.position += ...
        //
        //   によって現在の位置にその移動量を追加する。
        //
        //
        //   direction = 1 の場合
        //   → 右へ移動
        //
        //   direction = -1 の場合
        //   → 左へ移動
        //
        //   なので、この1行だけで
        //   「右にも左にも動ける」ようになっている。


        if (Mathf.Abs(transform.position.x - startPos.x) >= patrolDistance)
        // ↑ 敵が最初の位置からどれくらい離れたかを確認する。
        //
        //   transform.position.x
        //   → 現在の敵のX座標。
        //
        //   startPos.x
        //   → 敵が最初にいたX座標。
        //
        //   これを引くことで、
        //
        //   「最初の位置からX方向にどれくらい離れた？」
        //
        //   が分かる。
        //
        //   例えば、
        //
        //   最初 = X 10
        //   現在 = X 13
        //
        //   なら、
        //
        //   13 - 10 = 3
        //
        //   となる。
        //
        //
        //   Mathf.Abs()
        //   → 絶対値を求める。
        //
        //   例えば、
        //
        //   Abs(3)  = 3
        //   Abs(-3) = 3
        //
        //   となる。
        //
        //   つまり右に3離れていても、
        //   左に3離れていても「3」と判断できる。
        //
        //
        //   >= patrolDistance
        //   → 「移動距離が設定した距離以上になった？」
        //
        //   patrolDistanceが3なら、
        //
        //   3以上離れたら
        //   ↓
        //   方向転換！


        {
            direction *= -1;
            // ↑ 敵の移動方向を反転させる。
            //
            //   *= -1 は、
            //
            //   direction = direction * -1
            //
            //   と同じ。
            //
            //   例えば、
            //
            //   direction = 1
            //   ↓
            //   1 × -1
            //   ↓
            //   -1
            //
            //   つまり右から左になる。
            //
            //   逆に、
            //
            //   direction = -1
            //   ↓
            //   -1 × -1
            //   ↓
            //   1
            //
            //   左から右になる。
            //
            //   これによって敵が左右を往復できる。


            Vector3 scale = transform.localScale;
            // ↑ 敵の現在の大きさ・向きをscaleにコピーする。
            //
            //   localScale
            //   → GameObjectの大きさを表す値。
            //
            //   X、Y、Zの3つの値を持っている。
            //
            //   例えば、
            //
            //   (1, 1, 1)
            //
            //   なら通常サイズ。


            scale.x *= -1f;
            // ↑ X方向の大きさをマイナスにする。
            //
            //   例えば、
            //
            //   scale.x = 1
            //
            //   だったら、
            //
            //   scale.x = -1
            //
            //   になる。
            //
            //   UnityではSpriteのXスケールをマイナスにすると、
            //   スプライトが左右反転する。
            //
            //   そのため、
            //
            //   敵が右を向いている
            //       ↓
            //   方向転換
            //       ↓
            //   敵の画像も左右反転
            //
            //   という動きになる。


            transform.localScale = scale;
            // ↑ 変更したscaleを実際の敵GameObjectに適用する。
            //
            //   さっき、
            //
            //   scale.x *= -1f;
            //
            //   で左右反転させたので、
            //   ここで実際の敵も左右反転する。


        }


    }
    // ↑ Update()終了。


    private void OnCollisionEnter2D(Collision2D collision)
    // ↑ 敵が何かの2D Colliderと衝突した瞬間に
    //   Unityが自動的に呼び出す関数。
    //
    //   OnCollisionEnter2D
    //   → 「2Dの物体と衝突した瞬間」
    //
    //   例えば、
    //
    //   敵
    //   ↓
    //   ← プレイヤー
    //
    //   のようにぶつかったときに実行される。
    //
    //   collision
    //   → 何と衝突したのかという情報が入っている。


    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
        // ↑ 衝突したGameObjectに
        //   「IDamageableを持っているか？」を確認する。
        //
        //   collision.gameObject
        //   → 今ぶつかったGameObject。
        //
        //   例えばプレイヤーとぶつかったなら、
        //   ここにはPlayerのGameObjectが入っている。
        //
        //
        //   TryGetComponent<IDamageable>()
        //   → そのGameObjectからIDamageableを探す。
        //
        //   IDamageable
        //   → 「ダメージを受けることができるもの」
        //      というルールを作るためのインターフェース。
        //
        //   これが付いている相手なら、
        //   ダメージを与えられると判断する。
        //
        //
        //   out var damageable
        //   → 見つかったIDamageableを
        //      damageableという変数に入れる。
        //
        //
        //   つまりこのif文全体では、
        //
        //   「ぶつかった相手はダメージを受けられる？」
        //
        //   と確認している。


        {
            damageable.TakeDamage(contactDamage);
            // ↑ ダメージを与える。
            //
            //   damageable
            //   → ダメージを受けられる相手。
            //
            //   TakeDamage()
            //   → 「ダメージを受ける処理」。
            //
            //   contactDamage
            //   → 与えるダメージ量。
            //
            //   今回はcontactDamage = 1なので、
            //
            //   TakeDamage(1)
            //
            //   が実行される。
            //
            //   つまり、
            //
            //   敵とプレイヤーが接触
            //       ↓
            //   プレイヤーがIDamageableを持っている
            //       ↓
            //   TakeDamage(1)
            //       ↓
            //   プレイヤーが1ダメージ受ける
            //
            //   という流れ。


        }
    }
    // ↑ OnCollisionEnter2D()終了。


}
// ↑ EnemyPatrolクラス終了。


敵が最初にいた場所を記憶
        ↓
右に移動
        ↓
最初の場所から3離れた？
        ↓
      YES
        ↓
方向を反転
        ↓
敵の画像も左右反転
        ↓
左に移動
        ↓
最初の場所から3離れた？
        ↓
      YES
        ↓
また方向を反転
        ↓
これをずっと繰り返す


敵
 ↓
プレイヤーに衝突
 ↓
「IDamageableを持ってる？」
 ↓
 YES
 ↓
TakeDamage(1)
 ↓
プレイヤーに1ダメージ

direction　左か右か行くときのスイッチ
direction = 1;　なら右　逆なら左

EnemyPatrol
     ↓
「IDamageableを持っている相手か？」



★GAMEMANAGER　これゲームの根幹となる重要script
using UnityEngine;
// ↑ Unityの基本機能を使えるようにする。
//   MonoBehaviour、Transformなどを使うために必要。


/// <summary>
/// プレイヤーが設定したY座標より下に落ちた場合、
/// ゲームオーバーにする。
/// </summary>
// ↑ この部分は説明用のコメント。
//   プログラムの動作には影響しない。
//   
//   このスクリプトの仕事は、
//   
//   プレイヤーが下に落ちる
//          ↓
//   設定したY座標を下回る
//          ↓
//   GameManagerに「ゲームオーバーにして」と伝える
//   
//   というもの。



★FallDeath
public class FallDeath : MonoBehaviour
// ↑ FallDeathというクラスを作る。
//   
//   MonoBehaviourを継承しているので、
//   UnityのGameObjectに取り付けて使用できる。
{
    [Header("このY座標より下に行ったらゲームオーバー")]
    // ↑ UnityのInspectorに見出しを表示する。
    //   Inspectorを見やすくするためのもの。


    [SerializeField] private float deathY = -10f;
    // ↑ ゲームオーバーになるY座標を設定する。
    //
    //   float
    //   → 小数を扱える数字。
    //
    //   deathY
    //   → DeathになるY座標という意味。
    //
    //   -10f
    //   → 初期値はY=-10。
    //
    //   つまりプレイヤーが、
    //
    //   Y = -9
    //   ↓
    //   まだ大丈夫
    //
    //   Y = -10
    //   ↓
    //   条件によってはまだギリギリ
    //
    //   Y = -11
    //   ↓
    //   ゲームオーバー
    //
    //   となる。


    private bool alreadyDead;
    // ↑ 「すでに死亡処理をしたか」を記録する変数。
    //
    //   boolは、
    //
    //   true  → はい
    //   false → いいえ
    //
    //   の2種類だけ。
    //
    //   最初は自動的にfalse。
    //
    //   つまり、
    //
    //   alreadyDead = false
    //
    //   「まだ死んでいない」
    //
    //   という状態。


    private void Update()
    // ↑ ゲーム中、毎フレーム呼ばれる。
    //
    //   プレイヤーが今どこにいるかを毎フレーム確認するために使う。


    {
        if (alreadyDead)
        // ↑ すでに死亡処理をしているか確認。
        //
        //   alreadyDeadがtrueなら、
        //   ifの中を実行する。


            return;
            // ↑ それ以上何もしないでUpdateを終了する。
            //
            //   これによって、
            //
            //   落下
            //   ↓
            //   GameOver()
            //   ↓
            //   その後もUpdateが何度もGameOver()を呼ぶ
            //
            //   という状態を防いでいる。


        if (transform.position.y < deathY)
        // ↑ プレイヤーの現在のY座標が、
        //   deathYより小さいか確認する。
        //
        //   transform.position.y
        //   → このGameObjectの現在のY座標。
        //
        //   deathY = -10の場合、
        //
        //   Y = -5
        //   → -5 < -10ではない → セーフ
        //
        //   Y = -11
        //   → -11 < -10 → ゲームオーバー


        {
            alreadyDead = true;
            // ↑ 「死亡処理をもう行った」と記録する。
            //
            //   false
            //      ↓
            //   true
            //
            //   これによって以降のUpdateで
            //   GameOver()を何度も呼ばなくなる。


            if (GameManager.Instance != null)
            // ↑ GameManagerが存在するか確認する。
            //
            //   GameManager.Instance
            //   → 現在のGameManagerを取得する。
            //
            //   != null
            //   → 「GameManagerがちゃんと存在している？」
            //
            //   存在しない状態でGameOver()を呼ぶと
            //   エラーになる可能性があるため確認している。


            {
                GameManager.Instance.GameOver();
                // ↑ GameManagerにゲームオーバー処理をお願いする。
                //
                //   FallDeath自身が
                //   「ゲームオーバー画面を表示する」
                //   わけではない。
                //
                //   GameManagerに、
                //
                //   「プレイヤーが落ちたよ！
                //    ゲームオーバー処理をして！」
                //
                //   と伝えている。


            }
        }
    }
}

プレイヤーが落下
      ↓
Y座標を確認
      ↓
deathYより下？
      ↓
   YES
      ↓
alreadyDead = true
      ↓
GameManager.GameOver()
      ↓
ゲームオーバー処理

using UnityEngine;
// ↑ Unityの基本機能を使用する。


using UnityEngine.SceneManagement;
// ↑ Sceneを切り替える機能を使用する。
//   LoadScene()などが使えるようになる。


/*
/// <summary>
/// ゲーム全体を管理するスクリプト。
///
/// ・スコア
/// ・ゲームオーバー
/// ・ステージクリア
/// ・リトライ
/// ・タイトルへ戻る
/// </summary>
*/
// ↑ このGameManagerが担当する仕事の説明。
//
//   今回はゲーム全体に関わる処理をまとめている。


public class GameManager : MonoBehaviour
// ↑ GameManagerというクラスを作る。
//   UnityのGameObjectに取り付けて使用する。


{
    public static GameManager Instance { get; private set; }
    // ↑ GameManagerをゲーム中のどこからでも呼び出しやすくするためのもの。
    //
    //   これを「Singleton（シングルトン）」という仕組みに近い形で使っている。
    //
    //   例えば別のスクリプトから、
    //
    //   GameManager.Instance.GameOver();
    //
    //   と書けばGameManagerのGameOver()を呼べる。
    //
    //   Instance
    //   → 「現在使うGameManager」を保存する場所。
    //
    //   private set
    //   → Instanceの値を外部から勝手に変更できないようにしている。


    [Header("スコア")]
    // ↑ Inspectorの見出し。


    [SerializeField] private int score = 0;
    // ↑ 現在のスコア。
    //
    //   int
    //   → 整数。
    //
    //   最初は0点。


    [Header("UI")]
    // ↑ Inspectorの見出し。


    [SerializeField] private GameObject gameOverPanel;
    // ↑ ゲームオーバー画面のGameObjectを入れる変数。
    //
    //   Inspectorから、
    //
    //   Game Over Panel
    //        ↓
    //   この変数
    //
    //   という形で設定する。


    [SerializeField] private GameObject clearPanel;
    // ↑ クリア画面のGameObjectを入れる変数。


    [Header("シーン設定")]
    // ↑ Inspectorの見出し。


    [SerializeField] private string titleSceneName = "Title";
    // ↑ タイトルSceneの名前を保存する。
    //
    //   初期値は「Title」。


    private bool gameFinished = false;
    // ↑ ゲームが終了状態になったかを記録する。
    //
    //   false
    //   → まだゲーム中。
    //
    //   true
    //   → ゲームオーバーまたはクリア済み。
    //
    //   これによって、
    //
    //   ゲームオーバーとクリアが同時に発生する
    //
    //   などを防ぐ。


    private void Awake()
    // ↑ GameObjectが読み込まれたときに一度呼ばれる。
    //
    //   GameManagerの初期設定を行う。


    {
        // GameManagerが2つ以上あったら削除
        if (Instance != null && Instance != this)
        // ↑ すでに別のGameManagerが存在しているか確認。
        //
        //   Instance != null
        //   → すでにGameManagerが存在する。
        //
        //   Instance != this
        //   → 今読み込まれたGameManagerとは別のGameManager。
        //
        //   つまり、
        //
        //   「GameManagerが2個以上ある？」
        //
        //   と確認している。


        {
            Destroy(gameObject);
            // ↑ 重複しているGameManagerを削除する。


            return;
            // ↑ ここでAwake()を終了する。


        }


        Instance = this;
        // ↑ 今のGameManagerを、
        //   「ゲームで使用するGameManager」として登録する。
        //
        //   これによって、
        //
        //   GameManager.Instance
        //
        //   でアクセスできるようになる。


        // 今回はDontDestroyOnLoadを使わない
        // シーンを読み込むたびに新しいGameManagerを作る
        //
        // ↑ これは重要。
        //
        //   DontDestroyOnLoad(gameObject);
        //
        //   を使っていないので、Sceneを切り替えると
        //   GameManagerもSceneと一緒に破棄される。
        //
        //   次のSceneでは新しいGameManagerが作られる。


    }


    private void Start()
    // ↑ Sceneの開始時に一度呼ばれる。


    {
        Time.timeScale = 1f;
        // ↑ ゲーム時間を通常速度に戻す。
        //
        //   1 = 通常速度
        //   0 = 停止
        //
        //   ゲームオーバー・クリア後に
        //   0になっていた場合に備えている。


        gameFinished = false;
        // ↑ 新しいゲームなので、
        //   「まだゲーム終了していない」に戻す。


        // 最初は両方の画面を非表示
        if (gameOverPanel != null)
        // ↑ Game Over Panelが設定されているか確認。


        {
            gameOverPanel.SetActive(false);
            // ↑ ゲームオーバー画面を非表示にする。


        }


        if (clearPanel != null)
        // ↑ Clear Panelが設定されているか確認。


        {
            clearPanel.SetActive(false);
            // ↑ クリア画面を非表示にする。


        }
    }


    /// <summary>
    /// スコアを増やす
    /// </summary>
    public void AddScore(int amount)
    // ↑ スコアを増やすための関数。
    //
    //   amount
    //   → 増やしたい点数。
    //
    //   例えば、
    //
    //   AddScore(10);
    //
    //   とすると10点増える。


    {
        score += amount;
        // ↑ 現在のscoreにamountを追加。
        //
        //   scoreが100
        //   amountが10
        //
        //   なら、
        //
        //   100 + 10 = 110
        //
        //   になる。


    }


    /// <summary>
    /// スコアを取得
    /// </summary>
    public int GetScore()
    // ↑ 現在のスコアを取得する関数。
    //
    //   intなので整数を返す。


    {
        return score;
        // ↑ 現在のscoreを呼び出した側に返す。


    }


    /// <summary>
    /// ゲームオーバー
    /// </summary>
    public void GameOver()
    // ↑ ゲームオーバー処理。


    {
        if (gameFinished)
            return;
        // ↑ すでにゲームが終了していたら何もしない。
        //
        //   例えばすでにクリアしているのに
        //   プレイヤーが落ちた場合などを防ぐ。


        gameFinished = true;
        // ↑ ゲームを終了状態にする。


        Time.timeScale = 0f;
        // ↑ ゲーム内時間を停止する。
        //
        //   敵やプレイヤーなどの動きを止める。


        if (clearPanel != null)
        {
            clearPanel.SetActive(false);
            // ↑ クリア画面が出ていたら消す。
        }


        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            // ↑ ゲームオーバー画面を表示する。
        }
    }


    /// <summary>
    /// ステージクリア
    /// </summary>
    public void Clear()
    // ↑ ステージクリア処理。


    {
        if (gameFinished)
            return;
        // ↑ すでに終了していたら何もしない。


        gameFinished = true;
        // ↑ ゲーム終了状態にする。


        Time.timeScale = 0f;
        // ↑ ゲームを停止する。


        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
            // ↑ ゲームオーバー画面を消す。
        }


        if (clearPanel != null)
        {
            clearPanel.SetActive(true);
            // ↑ クリア画面を表示する。
        }
    }


    /// <summary>
    /// 現在のステージを最初からやり直す
    /// </summary>
    public void RestartLevel()
    // ↑ 現在のSceneを最初からやり直す。


    {
        Time.timeScale = 1f;
        // ↑ 止まっていたゲーム時間を通常に戻す。
        //
        //   これは非常に重要。
        //
        //   ゲームオーバー時はTime.timeScale=0なので、
        //   リトライするときに1に戻している。


        gameFinished = false;
        // ↑ ゲーム終了状態を解除する。


        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
        // ↑ 現在プレイしているSceneをもう一度読み込む。
        //
        //   GetActiveScene()
        //   → 現在のSceneを取得。
        //
        //   buildIndex
        //   → Build Settings上でのScene番号。
        //
        //   その番号をLoadScene()に渡している。
        //
        //   結果、
        //
        //   現在のScene
        //       ↓
        //   もう一度読み込み
        //       ↓
        //   ステージ最初から


    }


    /// <summary>
    /// タイトル画面へ戻る
    /// </summary>
    public void LoadTitle()
    // ↑ タイトルSceneへ移動する。


    {
        Time.timeScale = 1f;
        // ↑ ゲーム時間を通常に戻す。


        gameFinished = false;
        // ↑ 終了状態を解除。


        SceneManager.LoadScene(titleSceneName);
        // ↑ titleSceneNameに設定されているSceneを読み込む。
        //
        //   初期値は「Title」なので、
        //
        //   Title Sceneへ移動する。


    }


    /// <summary>
    /// 次のステージへ進む
    /// </summary>
    public void NextStage()
    // ↑ 次のSceneへ移動する。


    {
        Time.timeScale = 1f;
        // ↑ 時間を通常に戻す。


        gameFinished = false;
        // ↑ ゲーム終了状態を解除。


        int currentScene =
            SceneManager.GetActiveScene().buildIndex;
        // ↑ 現在のScene番号を取得する。
        //
        //   例えば現在SceneがBuild Index 1なら、
        //
        //   currentScene = 1


        int nextScene = currentScene + 1;
        // ↑ 次のScene番号を作る。
        //
        //   現在が1なら、
        //   次は2。


        if (nextScene < SceneManager.sceneCountInBuildSettings)
        // ↑ 次のSceneが存在するか確認する。
        //
        //   sceneCountInBuildSettings
        //   → Build Settingsに登録されているSceneの数。
        //
        //   例えばSceneが、
        //
        //   0 Title
        //   1 Stage1
        //   2 Stage2
        //
        //   の3つならsceneCountは3。
        //
        //   次のScene番号が3以上なら、
        //   もう次のSceneがない。


        {
            SceneManager.LoadScene(nextScene);
            // ↑ 次のSceneを読み込む。


        }
        else
        {
            LoadTitle();
            // ↑ 次のSceneがないならタイトルへ戻る。
        }
    }
}

using UnityEngine;
// ↑ Unityの基本機能を使う。
//   Collider2Dなどに必要。


/*
/// <summary>
/// プレイヤーがゴールに触れたらステージクリア。
/// </summary>
*/
// ↑ このスクリプトの説明。


public class Goal : MonoBehaviour
// ↑ Goalというクラスを作る。
//   ゴールのGameObjectに取り付けて使う。


{
    private void OnTriggerEnter2D(Collider2D other)
    // ↑ 「Triggerに何かが入った瞬間」にUnityが自動的に呼ぶ。
    //
    //   例えば、
    //
    //   Player
    //      ↓
    //   GoalのTrigger
    //
    //   に入った瞬間に実行される。
    //
    //   other
    //   → Triggerに入ってきた相手のCollider2D。


    {
        if (!other.CompareTag("Player"))
        // ↑ 入ってきた相手のTagが「Player」ではないか確認。
        //
        //   ! は「～ではない」という意味。
        //
        //   つまり、
        //
        //   「Playerじゃないなら」


            return;
            // ↑ 何もしないで終了。
            //
            //   敵などがゴールに触れても
            //   クリアにならない。


        if (GameManager.Instance != null)
        // ↑ GameManagerが存在するか確認。


        {
            GameManager.Instance.Clear();
            // ↑ GameManagerにClear()をお願いする。
            //
            //   つまり、
            //
            //   PlayerがGoalに触れる
            //          ↓
            //   GoalがClear()を呼ぶ
            //          ↓
            //   GameManagerがクリア画面を表示
        }
    }
}
Is Trigger
☑
プレイヤー
   ↓
ゴールのTriggerに入る
   ↓
OnTriggerEnter2D()
   ↓
Playerタグ？
   ↓
GameManager.Clear()

using System.Collections;
// ↑ IEnumeratorやコルーチン関連で使用する。



★Health
using UnityEngine;
// ↑ Unityの基本機能。


using UnityEngine.Events;
// ↑ UnityEventを使用するために必要。


/*
/// <summary>
/// ダメージを受けられるオブジェクトが実装するインターフェース。
/// プレイヤー・敵の両方に同じHealthコンポーネントを付けて使い回せる。
/// </summary>
*/
// ↑ この部分の説明。
//
//   「ダメージを受けることができるものには、
//    TakeDamage()という機能を持たせよう」
//   
//   というルールを作っている。


public interface IDamageable
// ↑ IDamageableという「インターフェース」を作る。
//
//   インターフェースは簡単に言うと、
//   
//   「こういう機能を持っていてください」
//   
//   というルール・契約書のようなもの。


{
    void TakeDamage(int amount);
    // ↑ IDamageableを使うものは、
    //   TakeDamage()を持っていなければならない。
    //
    //   amount
    //   → 受けるダメージ量。
    //
    //   例えば、
    //
    //   TakeDamage(1)
    //
    //   なら1ダメージ。


}

public class Health : MonoBehaviour, IDamageable
// ↑ Healthというクラスを作る。
//
//   MonoBehaviour
//   → UnityのGameObjectに取り付けられる。
//
//   IDamageable
//   → 「Healthはダメージを受けられますよ」
//      という宣言。
//
//   そのためHealthには必ず
//
//   TakeDamage()
//
//   を実装する必要がある。


{
    [Header("体力設定")]
    // ↑ Inspectorの見出し。


    [SerializeField] private int maxHealth = 3;
    // ↑ 最大HP。
    //
    //   初期値3。


    [SerializeField] private float invincibleDuration = 1f;
    // ↑ ダメージを受けた後の無敵時間。
    //
    //   1fなので1秒。


    [Header("イベント（インスペクターでUI更新等に接続可能）")]
    // ↑ Inspectorの見出し。


    public UnityEvent onDamaged;
    // ↑ ダメージを受けたときに実行するイベント。
    //
    //   UnityEventなのでInspectorから
    //   「ダメージを受けたら何をする？」
    //   を設定できる。


    public UnityEvent onDeath;
    // ↑ 死亡したときに実行するイベント。
    //
    //   今回はPlayerDeathがこのイベントを監視している。


    private int currentHealth;
    // ↑ 現在のHP。


    private bool isInvincible;
    // ↑ 現在無敵状態かどうか。
    //
    //   true
    //   → 無敵
    //
    //   false
    //   → 無敵ではない。


    public int CurrentHealth => currentHealth;
    // ↑ 現在HPを外部から取得できるようにする。
    //
    //   「見ること」はできるが、
    //   外部から直接書き換えにくい形にしている。


    public int MaxHealth => maxHealth;
    // ↑ 最大HPを外部から取得できるようにする。


    private void Awake()
    // ↑ GameObject読み込み時に一度実行。


    {
        currentHealth = Mathf.Max(1, maxHealth);
        // ↑ 現在HPを最大HPと同じにする。
        //
        //   Mathf.Max(1, maxHealth)
        //   → 最低でも1になるようにする。
        //
        //   maxHealth = 3なら、
        //
        //   currentHealth = 3
        //
        //   になる。


    }


    private void OnValidate()
    // ↑ UnityのInspectorで値を変更したときなどに呼ばれる。
    //
    //   不正な数値にならないように調整する。


    {
        maxHealth = Mathf.Max(1, maxHealth);
        // ↑ 最大HPが0以下にならないようにする。


        invincibleDuration = Mathf.Max(0f, invincibleDuration);
        // ↑ 無敵時間がマイナスにならないようにする。
    }


    public void TakeDamage(int amount)
    // ↑ ダメージを受ける処理。
    //
    //   IDamageableで決められていた
    //   TakeDamage()を実際に作っている。


    {
        if (isInvincible || currentHealth <= 0 || amount <= 0)
        // ↑ ダメージを受けていい状態か確認。
        //
        //   isInvincible
        //   → 今無敵ならダメージを受けない。
        //
        //   currentHealth <= 0
        //   → すでにHPが0なら受けない。
        //
        //   amount <= 0
        //   → ダメージが0以下なら受けない。


        {
            return;
            // ↑ 条件に当てはまったら何もしない。


        }


        currentHealth -= amount;
        // ↑ HPからダメージ量を引く。
        //
        //   HP3
        //   ダメージ1
        //
        //   3 - 1 = 2


        currentHealth = Mathf.Max(0, currentHealth);
        // ↑ HPがマイナスにならないようにする。
        //
        //   HP1に10ダメージ
        //   ↓
        //   -9
        //
        //   にはせず、
        //
        //   0
        //
        //   にする。


        onDamaged?.Invoke();
        // ↑ ダメージイベントを実行する。
        //
        //   ?. は、
        //
        //   「イベントが存在するなら実行」
        //
        //   という安全な書き方。


        if (currentHealth <= 0)
        // ↑ HPが0以下になったか確認。


        {
            Die();
            // ↑ 死亡処理を呼ぶ。


        }
        else
        {
            StartCoroutine(InvincibleFrame());
            // ↑ まだHPが残っているなら、
            //   無敵時間を開始する。
        }
    }


    private IEnumerator InvincibleFrame()
    // ↑ 無敵時間を作るコルーチン。
    //
    //   IEnumeratorを使うことで、
    //   「途中で待つ処理」ができる。


    {
        isInvincible = true;
        // ↑ 無敵状態ON。


        yield return new WaitForSeconds(invincibleDuration);
        // ↑ 設定した時間だけ待つ。
        //
        //   invincibleDurationが1なら1秒待つ。
        //
        //   その間、TakeDamage()は
        //   isInvincibleがtrueなので無視される。


        isInvincible = false;
        // ↑ 無敵状態OFF。


    }


    private void Die()
    // ↑ 死亡処理。


    {
        if (currentHealth > 0)
        // ↑ HPがまだ残っているなら、
        //   死亡処理をしない。


        {
            return;
        }


        onDeath?.Invoke();
        // ↑ 死亡イベントを実行する。
        //
        //   PlayerDeathがこれを受け取って、
        //   GameManager.GameOver()を呼ぶ。


        gameObject.SetActive(false);
        // ↑ このGameObjectを無効化する。
        //
        //   プレイヤーなら、
        //   プレイヤーGameObjectが消える。


    }
}



★PlayerController
using UnityEngine;
// ↑ Unityの基本機能。


using UnityEngine.InputSystem;
// ↑ 新しいInput Systemを使う。
//
//   Keyboard.currentなどが使える。


/*
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
*/
// ↑ このスクリプトに必要なUnity側の設定について書かれた説明。


[RequireComponent(typeof(Rigidbody2D))]
// ↑ このスクリプトを付けたGameObjectに
//   Rigidbody2DがなければUnityが自動的に追加する。


[RequireComponent(typeof(BoxCollider2D))]
// ↑ BoxCollider2Dも必要。


[RequireComponent(typeof(Animator))]
// ↑ Animatorも必要。


public class PlayerController : MonoBehaviour
// ↑ PlayerControllerというクラスを作る。


{
    [Header("移動設定")]
    [SerializeField] private float moveSpeed = 5f;
    // ↑ プレイヤーの左右移動速度。
    //
    //   Inspectorから変更可能。


    [Header("ジャンプ設定")]
    [SerializeField] private float jumpForce = 12f;
    // ↑ ジャンプの強さ。


    [Header("地面設定")]
    [SerializeField] private LayerMask groundLayer;
    // ↑ 「地面として判定するLayer」を設定する。
    //
    //   InspectorでGround Layerを指定する。


    private Rigidbody2D rb;
    // ↑ プレイヤーのRigidbody2Dを保存。


    private BoxCollider2D boxCollider;
    // ↑ プレイヤーのBoxCollider2Dを保存。


    private Animator animator;
    // ↑ プレイヤーのAnimatorを保存。


    private float moveInput;
    // ↑ 現在の左右入力。
    //
    //   -1 → 左
    //    0 → 動かない
    //   +1 → 右


    private bool isGrounded;
    // ↑ 地面にいるかどうか。


    private bool facingRight = true;
    // ↑ プレイヤーが右を向いているか。
    //
    //   最初は右向き。


    // Animator Parameter
    private static readonly int SpeedHash =
        Animator.StringToHash("Speed");
    // ↑ Animatorの「Speed」というパラメータを
    //   ハッシュ値に変換して保存。
    //
    //   Animator.SetFloat()で使用する。


    private static readonly int IsGroundedHash =
        Animator.StringToHash("IsGrounded");
    // ↑ AnimatorのIsGroundedパラメータ。


    private static readonly int JumpTriggerHash =
        Animator.StringToHash("Jump");
    // ↑ AnimatorのJumpパラメータ。


    private void Awake()
    // ↑ 最初に一度実行。


    {
        rb = GetComponent<Rigidbody2D>();
        // ↑ 自分のGameObjectについている
        //   Rigidbody2Dを取得する。


        boxCollider = GetComponent<BoxCollider2D>();
        // ↑ BoxCollider2Dを取得。


        animator = GetComponent<Animator>();
        // ↑ Animatorを取得。


    }


    private void Update()
    // ↑ 毎フレーム実行。


    {
        // 最初に地面判定
        CheckGrounded();
        // ↑ 今プレイヤーが地面にいるか確認。


        // 移動入力
        GetMoveInput();
        // ↑ A/Dや矢印キーが押されているか確認。


        // ジャンプ入力
        CheckJumpInput();
        // ↑ ジャンプキーが押されたか確認。


        // Animator更新
        UpdateAnimatorParameters();
        // ↑ アニメーション用の値を更新。


        // 向き変更
        FlipCheck();
        // ↑ プレイヤーが向く方向を確認。


    }


    private void FixedUpdate()
    // ↑ Rigidbody2Dを使った物理処理を行うための関数。
    //
    //   移動はここで行う。


    {
        Move();
        // ↑ 実際にプレイヤーを移動させる。


    }


    private void GetMoveInput()
    // ↑ 左右の入力を取得する。


    {
        Keyboard keyboard = Keyboard.current;
        // ↑ 現在のキーボードを取得。


        if (keyboard == null)
        // ↑ キーボードが取得できなかった場合。


        {
            moveInput = 0f;
            // ↑ 入力を0にする。


            return;
            // ↑ 処理終了。


        }


        moveInput = 0f;
        // ↑ 最初に入力を0にリセット。


        if (keyboard.aKey.isPressed ||
            keyboard.leftArrowKey.isPressed)
        // ↑ Aキーまたは左矢印キーが押されている？


        {
            moveInput = -1f;
            // ↑ 左入力。


        }
        else if (keyboard.dKey.isPressed ||
                 keyboard.rightArrowKey.isPressed)
        // ↑ Dキーまたは右矢印キーが押されている？


        {
            moveInput = 1f;
            // ↑ 右入力。


        }
    }


    private void CheckJumpInput()
    // ↑ ジャンプ入力を確認。


    {
        Keyboard keyboard = Keyboard.current;
        // ↑ キーボード取得。


        if (keyboard == null)
            return;
        // ↑ キーボードがなければ終了。


        bool jumpPressed =
            keyboard.spaceKey.wasPressedThisFrame ||
            keyboard.wKey.wasPressedThisFrame ||
            keyboard.upArrowKey.wasPressedThisFrame;
        // ↑ Space、W、上矢印のどれかを
        //   「このフレームに押したか」を確認。
        //
        //   wasPressedThisFrameなので、
        //   「押しっぱなし」ではなく
        //   「押した瞬間」を検出する。


        // 地面にいるときだけジャンプ
        if (jumpPressed && isGrounded)
        // ↑
        //   ジャンプキーを押した
        //   AND
        //   地面にいる
        //
        //   この両方が成立したらジャンプ。


        {
            Jump();
            // ↑ ジャンプ処理を実行。


        }
    }


    private void Jump()
    // ↑ 実際のジャンプ処理。


    {
        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            jumpForce
        );
        // ↑ Rigidbody2Dの速度を設定する。
        //
        //   X方向は現在の速度をそのまま維持。
        //
        //   Y方向だけjumpForceにする。
        //
        //   つまり、
        //
        //   横移動はそのまま
        //   ＋
        //   上方向へ強い速度
        //
        //   でジャンプさせる。


        // ジャンプ中は接地していない
        isGrounded = false;
        // ↑ ジャンプした瞬間に「空中」とする。


        // Animatorへジャンプ通知
        animator.SetTrigger(JumpTriggerHash);
        // ↑ AnimatorのJump Triggerを発動。
        //
        //   これによってジャンプアニメーションなどへ
        //   遷移できる。


    }


    private void Move()
    // ↑ 左右移動処理。


    {
        rb.linearVelocity = new Vector2(
            moveInput * moveSpeed,
            rb.linearVelocity.y
        );
        // ↑ Rigidbody2Dの速度を設定。
        //
        //   X = 入力 × 移動速度
        //
        //   Y = 現在のY速度
        //
        //   なので、
        //
        //   左右移動だけ変更して、
        //   重力による上下の動きはそのまま。


    }


    private void CheckGrounded()
    // ↑ プレイヤーが地面に立っているか確認する。


    {
        Bounds bounds = boxCollider.bounds;
        // ↑ BoxCollider2Dの範囲を取得。


        // BoxColliderの下側を少しだけ広げる
        Vector2 checkCenter = new Vector2(
            bounds.center.x,
            bounds.min.y - 0.02f
        );
        // ↑ 地面判定を行う場所を作る。
        //
        //   プレイヤーのColliderの下側付近。


        Vector2 checkSize = new Vector2(
            bounds.size.x * 0.9f,
            0.08f
        );
        // ↑ 地面判定用の小さな範囲を作る。
        //
        //   Colliderの横幅の90%程度。
        //   高さは0.08。


        Collider2D hit = Physics2D.OverlapBox(
            checkCenter,
            checkSize,
            0f,
            groundLayer
        );
        // ↑ 指定した範囲にGround LayerのColliderがあるか調べる。
        //
        //   見つかればhitにColliderが入る。
        //
        //   なければnull。


        isGrounded = hit != null;
        // ↑ 地面が見つかったらtrue。
        //
        //   見つからなかったらfalse。


    }


    private void UpdateAnimatorParameters()
    // ↑ Animatorに現在の状態を伝える。


    {
        animator.SetFloat(
            SpeedHash,
            Mathf.Abs(moveInput)
        );
        // ↑ AnimatorのSpeedに移動量を渡す。
        //
        //   moveInputが、
        //
        //   -1 → Absすると1
        //    0 → 0
        //   +1 → 1
        //
        //   になる。
        //
        //   そのため、
        //
        //   Speed = 0
        //   → Idle
        //
        //   Speed = 1
        //   → Run
        //
        //   のようなAnimator遷移に使える。


        animator.SetBool(
            IsGroundedHash,
            isGrounded
        );
        // ↑ AnimatorのIsGroundedに
        //   現在地が地面かどうかを渡す。
        //
        //   true → 地面
        //   false → 空中


    }


    private void FlipCheck()
    // ↑ キャラクターの向きを変更するか確認。


    {
        if (moveInput > 0f && !facingRight)
        // ↑ 右に動こうとしていて、
        //   現在左向きなら反転する。


        {
            Flip();
        }
        else if (moveInput < 0f && facingRight)
        // ↑ 左に動こうとしていて、
        //   現在右向きなら反転する。


        {
            Flip();
        }
    }


    private void Flip()
    // ↑ キャラクターを左右反転する。


    {
        facingRight = !facingRight;
        // ↑ trueとfalseを反転。
        //
        //   true → false
        //   false → true


        Vector3 scale = transform.localScale;
        // ↑ 現在の大きさを取得。


        scale.x *= -1f;
        // ↑ X方向をマイナスにする。
        //
        //   Spriteが左右反転する。


        transform.localScale = scale;
        // ↑ 反転したScaleを実際のPlayerに適用。


    }


    private void OnDrawGizmosSelected()
    // ↑ UnityのSceneビューでPlayerを選択しているとき、
    //   デバッグ用の図を表示する。


    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        // ↑ BoxCollider2Dを取得。


        if (col == null)
            return;
        // ↑ Colliderがなければ終了。


        Bounds bounds = col.bounds;
        // ↑ Colliderの範囲を取得。


        Vector2 checkCenter = new Vector2(
            bounds.center.x,
            bounds.min.y - 0.02f
        );
        // ↑ 実際の地面判定と同じ位置を計算。


        Vector2 checkSize = new Vector2(
            bounds.size.x * 0.9f,
            0.08f
        );
        // ↑ 実際の地面判定と同じ大きさを計算。


        Gizmos.color = Color.red;
        // ↑ Sceneビューに表示する線を赤色にする。


        Gizmos.DrawWireCube(
            checkCenter,
            checkSize
        );
        // ↑ 地面判定範囲を四角形として表示。
        //
        //   これによってSceneビューで、
        //
        //   「実際にどこを地面として判定しているの？」
        //
        //   を目で確認できる。


    }
}



★PlayerDeat
using UnityEngine;
// ↑ Unityの基本機能。

/*
/// <summary>
/// PlayerのHealth死亡イベントをGameManagerへ接続する。
/// </summary>
*/
// ↑ このスクリプトの役割。


[RequireComponent(typeof(Health))]
// ↑ このGameObjectにはHealthが必要。
//
//   HealthがなければUnity側で追加される。


public class PlayerDeath : MonoBehaviour
// ↑ PlayerDeathというクラス。


{
    private Health health;
    // ↑ PlayerについているHealthを保存する。


    private void Awake()
    // ↑ 最初に一度呼ばれる。


    {
        health = GetComponent<Health>();
        // ↑ 自分のGameObjectについているHealthを取得。


    }


    private void OnEnable()
    // ↑ GameObjectが有効になったときに呼ばれる。
    //
    //   SetActive(true)などでも呼ばれる。


    {
        if (health != null)
        // ↑ Healthがちゃんと存在するか確認。


        {
            health.onDeath.AddListener(OnDeath);
            // ↑ Healthの「死亡イベント」に
            //   OnDeath()を登録する。
            //
            //   つまり、
            //
            //   Healthが死亡
            //       ↓
            //   onDeath
            //       ↓
            //   PlayerDeath.OnDeath()
            //
            //   という接続ができる。


        }
    }


    private void OnDisable()
    // ↑ GameObjectが無効になったときに呼ばれる。


    {
        if (health != null)
        // ↑ Healthが存在するか確認。


        {
            health.onDeath.RemoveListener(OnDeath);
            // ↑ 死亡イベントからOnDeath()を外す。
            //
            //   これをしないとイベントが重複登録される
            //   可能性があるため、解除している。


        }
    }


    private void OnDeath()
    // ↑ Healthから「死亡した」と通知されたときに呼ばれる。


    {
        if (GameManager.Instance != null)
        // ↑ GameManagerが存在するか確認。


        {
            GameManager.Instance.GameOver();
            // ↑ GameManagerにゲームオーバー処理をお願いする。
        }
    }
}

                    ┌──────────────────┐
                    │   GameManager    │
                    │                  │
                    │ ゲーム全体を管理 │
                    └────────┬─────────┘
                             │
              ┌──────────────┼──────────────┐
              │              │              │
              ↓              ↓              ↓
          GameOver()       Clear()      RestartLevel()
              ↑              ↑
              │              │
       ┌──────┴──────┐       │
       │             │       │
       │             │       │
       ↓             ↓       ↓
 FallDeath       PlayerDeath Goal
       │             │       │
       │             │       │
       │             ↓       │
       │           Health    │
       │             ↑       │
       │             │       │
       │          ダメージ    │
       │             │       │
       │          敵に接触    │
       │                     │
       └──落下───────────────┘

       EnemyPatrol
    ↓
OnCollisionEnter2D()
    ↓
相手にIDamageableがある？
    ↓
YES
    ↓
TakeDamage(1)
    ↓
Health
    ↓
currentHealth -= 1
    ↓
HPが0？
    ├── NO → 無敵時間
    │
    └── YES
          ↓
        Die()
          ↓
        onDeath
          ↓
      PlayerDeath
          ↓
      GameManager.GameOver()
          ↓
    Time.timeScale = 0
          ↓
    GameOverPanel表示

    Player
  ↓
Y座標が下がる
  ↓
FallDeath.Update()
  ↓
deathYより下？
  ↓
YES
  ↓
alreadyDead = true
  ↓
GameManager.GameOver()
  ↓
Time.timeScale = 0
  ↓
GameOverPanel表示

Player
  ↓
GoalのTriggerに入る
  ↓
Goal.OnTriggerEnter2D()
  ↓
Playerタグ？
  ↓
YES
  ↓
GameManager.Clear()
  ↓
Time.timeScale = 0
  ↓
GameOverPanelをOFF
  ↓
ClearPanelをON
  ↓
クリア画面

GameManager
    ↓
RestartLevel()

Retryボタン
    ↓
RestartLevel()
    ↓
Time.timeScale = 1
    ↓
gameFinished = false
    ↓
現在のSceneを再読み込み
    ↓
ゲーム開始

GameManager
    ↓
NextStage()

Nextボタン
    ↓
NextStage()
    ↓
現在のScene番号を取得
    ↓
+1
    ↓
次のSceneが存在する？
    │
    ├── YES
    │    ↓
    │  次のScene
    │
    └── NO
         ↓
       Titleへ

       | スクリプト              | 仕事                          |
| ------------------ | --------------------------- |
| `GameManager`      | **ゲーム全体の司令塔**               |
| `PlayerController` | **プレイヤーを動かす**               |
| `Health`           | **HP・ダメージ・死亡を管理**           |
| `PlayerDeath`      | **プレイヤー死亡をGameManagerに伝える** |
| `EnemyPatrol`      | **敵を左右に動かして接触ダメージを与える**     |
| `FallDeath`        | **落下したことをGameManagerに伝える**  |
| `Goal`             | **ゴールしたことをGameManagerに伝える** |
| `CameraFollow`     | **カメラをプレイヤーに追従させる**         |
| `TitleManager`     | **タイトルからゲームSceneへ移動する**     |


FallDeath
「落ちた！」

    ↓

GameManager
「了解。ゲームオーバーにする！」

    ↓

GameOverPanel
「表示！」

★animationについて　

　自分的に個いつかなら曲者で挫折する人出てくるかも。ちゃんとフォローしてあげてね。

まずアイドル状態のもの、ウォークまたはランのスプライト、ジャンプのスプライトの用意
これはどこからか、調達してください。
そのあと、animationclipで画像を動かせるようにします。
場合によってはそれぞれファイルを作ってもらって大丈夫。
その後アニメーターコントローラー使う

順番的に
素材→animationclip→animationcontroller
なんだけど、スプライトがうまく入らなくて困る子が出てくるかも。
物には寄るんだけど、そういう場合はスプライトエディター開いて設定になるかな
animationcontrollerは主に設定するのは矢印の方向ね

AI君使ってわかんなかったら、読んでもらって大丈夫。

★それぞれつけるもの
プレイヤー　リジットボディ、ボックスコライダー、プレイヤーコントローラー、Health
、、、大体わかるよね？(;^ω^)ちな、任せて大丈夫？必要なら、書いておくから伝えてね


★追記
私の技量でこれ作るのに５時間ぐらい？大体animationでてこずった。
あとclearとゲームおーばーのやつはじめしかうまくいかなくて格闘したぐらいかな。
１年生がどれぐらいできるのかにもよるけど、結構初期までしか作れなかったけど、その分１年生の個性は反映できると思う。
フォローよろしく。
ただ２日間だし、居残りたくもないだろうから、１日で半分教えられるといいな。２日目に完成とアレンジまで行ったら御の字
ちなみに、早く終われば２日目来なくていいです('◇')ゞ出席危ない人は来てください。２日目も。