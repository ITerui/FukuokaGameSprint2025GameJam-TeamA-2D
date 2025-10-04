using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("プレイヤー")]
    public Player player1;
    public Player player2;

    [Header("マークUI")]
    public Image markImage;

    [Header("フライング設定")]
    public int foulDamage = 1;
    public float nextMarkDelayAfterFoul = 1f;

    [Header("進化上限")]
    public int maxEvolutionCount = 3;
    private int p1EvolutionCount = 0;
    private int p2EvolutionCount = 0;

    [Header("リザルトシーン")]
    public string resultScene_P1Win;
    public string resultScene_P2Win;
    public string resultScene_P1OverEvolution;
    public string resultScene_P2OverEvolution;

    private bool roundActive = false;
    private bool waitingForInput = false;

    void Start()
    {
        if (markImage != null) markImage.gameObject.SetActive(false);
        StartNextRound();
    }

    void Update()
    {
        if (!roundActive) return;

        // --- Player1 ---
        if (Input.GetKeyDown(player1.attackKey))
        {
            if (waitingForInput) OnPlayerAttack(player1); // マーク表示中 → 攻撃
            else OnPlayerFoul(player1);                  // マーク未表示 → フライング
        }

        if (Input.GetKeyDown(player1.evolutionKey))
        {
            if (!waitingForInput) // マーク非表示時のみ進化
                player1.Evolve();
        }

        // --- Player2 ---
        if (Input.GetKeyDown(player2.attackKey))
        {
            if (waitingForInput) OnPlayerAttack(player2);
            else OnPlayerFoul(player2);
        }

        if (Input.GetKeyDown(player2.evolutionKey))
        {
            if (!waitingForInput) // マーク非表示時のみ進化
                player2.Evolve();
        }
    }

    // --- 攻撃 ---
    public void OnPlayerAttack(Player player)
    {
        roundActive = false;
        waitingForInput = false;
        if (markImage != null) markImage.gameObject.SetActive(false);

        Player opponent = (player.playerID == 1) ? player2 : player1;
        if (opponent != null)
        {
            opponent.TakeDamage(player.attackPower);
            Debug.Log($"Player{player.playerID} 攻撃! Player{opponent.playerID} に {player.attackPower} ダメージ");
        }

        Invoke(nameof(StartNextRound), 1f);
    }

    // --- フライング ---
    public void OnPlayerFoul(Player player)
    {
        roundActive = false;
        waitingForInput = false;

        Debug.Log($"Player{player.playerID} フライング! ペナルティ {foulDamage} ダメージ");
        player.TakeDamage_NoGauge(foulDamage);

        if (markImage != null) markImage.gameObject.SetActive(false);
        Invoke(nameof(StartNextRound), nextMarkDelayAfterFoul);
    }

    // --- 進化通知 ---
    public void OnPlayerEvolve(Player player)
    {
        if (player.playerID == 1)
        {
            p1EvolutionCount++;
            if (p1EvolutionCount > maxEvolutionCount)
            {
                Debug.Log("Player1 進化しすぎ → 敗北");
                SceneManager.LoadScene(resultScene_P2OverEvolution);
            }
        }
        else if (player.playerID == 2)
        {
            p2EvolutionCount++;
            if (p2EvolutionCount > maxEvolutionCount)
            {
                Debug.Log("Player2 進化しすぎ → 敗北");
                SceneManager.LoadScene(resultScene_P1OverEvolution);
            }
        }
    }

    // --- ラウンド開始 ---
    void StartNextRound()
    {
        roundActive = true;
        waitingForInput = false;
        if (markImage != null) markImage.gameObject.SetActive(false);

        float delay = Random.Range(1f, 3f);
        Invoke(nameof(ShowMark), delay);
    }

    void ShowMark()
    {
        if (!roundActive) return;
        if (markImage != null) markImage.gameObject.SetActive(true);
        waitingForInput = true;
    }
}
