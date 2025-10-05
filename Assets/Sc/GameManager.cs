using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("プレイヤー")]
    public Player player1;
    public Player player2;

    [Header("マークUI")]
    public Image markImage;

    [Header("フライング設定")]
    public float nextMarkDelayAfterFoul = 1f;
    public float nextMinMarkWaitTime = 1f;
    public float nextMaxMarkWaitTime = 5f;
    public float PerformanceTime = 2f;
    public float DrawTime = 2f;

    [Header("進化上限")]
    public int maxEvolutionCount = 3;

    [Header("リザルトシーン")]
    public string resultScene_P1Win;
    public string resultScene_P2Win;
    public string resultScene_P1OverEvolution;
    public string resultScene_P2OverEvolution;

    [Header("BGM")]
    public AudioClip bgmClip;

    public float ReturnIdleTime = 30.0f;

    private bool roundActive = false;
    private bool waitingForInput = false;

    private AudioSource audioSource;
    [SerializeField]
    public PerformanceManager AttackPerforamanceManager;

    void Start()
    {
        AttackPerforamanceManager.InitLoc();
        if (markImage != null) markImage.gameObject.SetActive(false);
        StartNextRound();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.loop = true;
        audioSource.volume = 0.2f;
        audioSource.Play();
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
            {
                OnPlayerEvolve(player1);
            }
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
            {
                OnPlayerEvolve(player2);
            }
        }
    }

    // --- 攻撃 ---
    public void OnPlayerAttack(Player player)
    {
        CancelInvoke(nameof(OnDraw));
        float distance = player.playerID == 1 ? 1f : -1f;
        AttackPerforamanceManager.OnAttackPerformance(distance);

        roundActive = false;
        waitingForInput = false;
        if (markImage != null) markImage.gameObject.SetActive(false);

        player.OnAttack();

        Player opponent = (player.playerID == 1) ? player2 : player1;
        if (opponent != null)
        {
            opponent.TakeDamage(player.attackPower);
            Debug.Log($"Player{player.playerID} 攻撃! Player{opponent.playerID} に {player.attackPower} ダメージ");
        }

        Invoke(nameof(StartNextRound), PerformanceTime);
    }

    // --- フライング ---
    public void OnPlayerFoul(Player player)
    {
        CancelInvoke(nameof(ShowMark));
        CancelInvoke(nameof(OnDraw));

        roundActive = false;
        waitingForInput = false;

        float foulDamage = player.attackPower * 0.5f;

        Debug.Log($"Player{player.playerID} フライング! ペナルティ {foulDamage} ダメージ");
        player.TakeDamage_NoGauge((int)foulDamage);

        if (markImage != null) markImage.gameObject.SetActive(false);
        Invoke(nameof(StartNextRound), PerformanceTime);
    }

    // --- 進化通知 ---
    public void OnPlayerEvolve(Player player)
    {
        CancelInvoke(nameof(ShowMark));
        CancelInvoke(nameof(OnDraw));

        roundActive = false;
        waitingForInput = false;

        if (player.playerID == 1)
        {
            if (player.EvoCount >= maxEvolutionCount)
            {
                Debug.Log("Player1 進化しすぎ → 敗北");
                SceneManager.LoadScene(resultScene_P2OverEvolution);
            }
            else
            {
                player.Evolve();
            }
        }
        else if (player.playerID == 2)
        {
            if (player.EvoCount >= maxEvolutionCount)
            {
                Debug.Log("Player2 進化しすぎ → 敗北");
                SceneManager.LoadScene(resultScene_P1OverEvolution);
            }
            else
            {
                player.Evolve();
            }
        }

        Invoke(nameof(StartNextRound), PerformanceTime);
    }

    // --- ラウンド開始 ---
    void StartNextRound()
    {
        AttackPerforamanceManager.EndAttackPerformance();
        roundActive = true;
        waitingForInput = false;
        if (markImage != null) markImage.gameObject.SetActive(false);

        float delay = Random.Range(nextMinMarkWaitTime, nextMaxMarkWaitTime);
        Invoke(nameof(ShowMark), delay);

        player1.ReturIdle();
        player2.ReturIdle();
    }

    void ShowMark()
    {
        if (!roundActive) return;
        if (markImage != null) markImage.gameObject.SetActive(true);
        waitingForInput = true;

        Invoke(nameof(OnDraw), DrawTime);
    }

    void OnDraw()
    {
        roundActive = false;
        waitingForInput = false;
        if (markImage != null) markImage.gameObject.SetActive(false);

        player1.OnHit();
        player2.OnHit();

        Invoke(nameof(StartNextRound), PerformanceTime);
    }
}
