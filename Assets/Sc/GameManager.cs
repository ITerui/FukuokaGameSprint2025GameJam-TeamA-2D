using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("�v���C���[")]
    public Player player1;
    public Player player2;

    [Header("�}�[�NUI")]
    public Image markImage;

    [Header("�t���C���O�ݒ�")]
    public float nextMarkDelayAfterFoul = 1f;
    public float nextMinMarkWaitTime = 1f;
    public float nextMaxMarkWaitTime = 5f;
    public float PerformanceTime = 2f;
    public float DrawTime = 2f;

    [Header("�i�����")]
    public int maxEvolutionCount = 3;

    [Header("���U���g�V�[��")]
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
            if (waitingForInput) OnPlayerAttack(player1); // �}�[�N�\���� �� �U��
            else OnPlayerFoul(player1);                  // �}�[�N���\�� �� �t���C���O
        }

        if (Input.GetKeyDown(player1.evolutionKey))
        {
            if (!waitingForInput) // �}�[�N��\�����̂ݐi��
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
            if (!waitingForInput) // �}�[�N��\�����̂ݐi��
            {
                OnPlayerEvolve(player2);
            }
        }
    }

    // --- �U�� ---
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
            Debug.Log($"Player{player.playerID} �U��! Player{opponent.playerID} �� {player.attackPower} �_���[�W");
        }

        Invoke(nameof(StartNextRound), PerformanceTime);
    }

    // --- �t���C���O ---
    public void OnPlayerFoul(Player player)
    {
        CancelInvoke(nameof(ShowMark));
        CancelInvoke(nameof(OnDraw));

        roundActive = false;
        waitingForInput = false;

        float foulDamage = player.attackPower * 0.5f;

        Debug.Log($"Player{player.playerID} �t���C���O! �y�i���e�B {foulDamage} �_���[�W");
        player.TakeDamage_NoGauge((int)foulDamage);

        if (markImage != null) markImage.gameObject.SetActive(false);
        Invoke(nameof(StartNextRound), PerformanceTime);
    }

    // --- �i���ʒm ---
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
                Debug.Log("Player1 �i�������� �� �s�k");
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
                Debug.Log("Player2 �i�������� �� �s�k");
                SceneManager.LoadScene(resultScene_P1OverEvolution);
            }
            else
            {
                player.Evolve();
            }
        }

        Invoke(nameof(StartNextRound), PerformanceTime);
    }

    // --- ���E���h�J�n ---
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
