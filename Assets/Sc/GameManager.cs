using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("�v���C���[")]
    public Player player1;
    public Player player2;

    [Header("�}�[�NUI")]
    public Image markImage;

    [Header("�t���C���O�ݒ�")]
    public int foulDamage = 1;
    public float nextMarkDelayAfterFoul = 1f;

    [Header("�i�����")]
    public int maxEvolutionCount = 3;
    private int p1EvolutionCount = 0;
    private int p2EvolutionCount = 0;

    [Header("���U���g�V�[��")]
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
            if (waitingForInput) OnPlayerAttack(player1); // �}�[�N�\���� �� �U��
            else OnPlayerFoul(player1);                  // �}�[�N���\�� �� �t���C���O
        }

        if (Input.GetKeyDown(player1.evolutionKey))
        {
            if (!waitingForInput) // �}�[�N��\�����̂ݐi��
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
            if (!waitingForInput) // �}�[�N��\�����̂ݐi��
                player2.Evolve();
        }
    }

    // --- �U�� ---
    public void OnPlayerAttack(Player player)
    {
        roundActive = false;
        waitingForInput = false;
        if (markImage != null) markImage.gameObject.SetActive(false);

        Player opponent = (player.playerID == 1) ? player2 : player1;
        if (opponent != null)
        {
            opponent.TakeDamage(player.attackPower);
            Debug.Log($"Player{player.playerID} �U��! Player{opponent.playerID} �� {player.attackPower} �_���[�W");
        }

        Invoke(nameof(StartNextRound), 1f);
    }

    // --- �t���C���O ---
    public void OnPlayerFoul(Player player)
    {
        roundActive = false;
        waitingForInput = false;

        Debug.Log($"Player{player.playerID} �t���C���O! �y�i���e�B {foulDamage} �_���[�W");
        player.TakeDamage_NoGauge(foulDamage);

        if (markImage != null) markImage.gameObject.SetActive(false);
        Invoke(nameof(StartNextRound), nextMarkDelayAfterFoul);
    }

    // --- �i���ʒm ---
    public void OnPlayerEvolve(Player player)
    {
        if (player.playerID == 1)
        {
            p1EvolutionCount++;
            if (p1EvolutionCount > maxEvolutionCount)
            {
                Debug.Log("Player1 �i�������� �� �s�k");
                SceneManager.LoadScene(resultScene_P2OverEvolution);
            }
        }
        else if (player.playerID == 2)
        {
            p2EvolutionCount++;
            if (p2EvolutionCount > maxEvolutionCount)
            {
                Debug.Log("Player2 �i�������� �� �s�k");
                SceneManager.LoadScene(resultScene_P1OverEvolution);
            }
        }
    }

    // --- ���E���h�J�n ---
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
