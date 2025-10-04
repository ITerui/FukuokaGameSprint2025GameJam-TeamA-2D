using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player1;
    public Player player2;

    [Header("UI")]
    public Image markImage; // マーク用の画像（CanvasのImage）

    private bool waitingForInput = false;   // マークが出ているか
    private bool roundActive = false;       // ラウンド中かどうか

    void Start()
    {
        NextRound();
    }

    void Update()
    {
        if (!roundActive) return;

        // --- 1Pの入力チェック ---
        if (Input.GetKeyDown(player1.attackKey))
        {
            if (waitingForInput)
                PlayerWin(player1, player2);
            else
                FalseStart(player1);
        }

        // --- 2Pの入力チェック ---
        if (Input.GetKeyDown(player2.attackKey))
        {
            if (waitingForInput)
                PlayerWin(player2, player1);
            else
                FalseStart(player2);
        }

        // --- 進化キー ---
        if (!waitingForInput) // マークが出ていないときだけ進化可能
        {
            if (Input.GetKeyDown(player1.evolutionKey))
                player1.Evolve();

            if (Input.GetKeyDown(player2.evolutionKey))
                player2.Evolve();
        }
    }

    void PlayerWin(Player winner, Player loser)
    {
        EndRound();
        Debug.Log($"Player{winner.playerID} が先に押した！");
        loser.TakeDamage(winner.attackPower);

        Invoke(nameof(NextRound), 1.0f);
    }

    void FalseStart(Player player)
    {
        EndRound();
        Debug.Log($"Player{player.playerID} がフライング！");
        // デバッグ用：HP表示のみ
        Debug.Log($"Player{player.playerID} の現在HP: {player.hp}");

        Invoke(nameof(NextRound), 1.0f);
    }

    // 共通処理：ラウンド終了時
    void EndRound()
    {
        roundActive = false;
        waitingForInput = false;
        markImage.enabled = false;

        // 予約しているマーク表示Invokeをキャンセル
        CancelInvoke(nameof(ShowMark));
    }

    void NextRound()
    {
        roundActive = true;
        waitingForInput = false;
        markImage.enabled = false;

        float delay = Random.Range(1f, 3f);
        Invoke(nameof(ShowMark), delay);
    }

    void ShowMark()
    {
        markImage.enabled = true;
        waitingForInput = true;
    }
}
