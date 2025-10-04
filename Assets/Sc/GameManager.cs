using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player1;
    public Player player2;

    [Header("UI")]
    public Image markImage; // �}�[�N�p�̉摜�iCanvas��Image�j

    private bool waitingForInput = false;   // �}�[�N���o�Ă��邩
    private bool roundActive = false;       // ���E���h�����ǂ���

    void Start()
    {
        NextRound();
    }

    void Update()
    {
        if (!roundActive) return;

        // --- 1P�̓��̓`�F�b�N ---
        if (Input.GetKeyDown(player1.attackKey))
        {
            if (waitingForInput)
                PlayerWin(player1, player2);
            else
                FalseStart(player1);
        }

        // --- 2P�̓��̓`�F�b�N ---
        if (Input.GetKeyDown(player2.attackKey))
        {
            if (waitingForInput)
                PlayerWin(player2, player1);
            else
                FalseStart(player2);
        }

        // --- �i���L�[ ---
        if (!waitingForInput) // �}�[�N���o�Ă��Ȃ��Ƃ������i���\
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
        Debug.Log($"Player{winner.playerID} ����ɉ������I");
        loser.TakeDamage(winner.attackPower);

        Invoke(nameof(NextRound), 1.0f);
    }

    void FalseStart(Player player)
    {
        EndRound();
        Debug.Log($"Player{player.playerID} ���t���C���O�I");
        // �f�o�b�O�p�FHP�\���̂�
        Debug.Log($"Player{player.playerID} �̌���HP: {player.hp}");

        Invoke(nameof(NextRound), 1.0f);
    }

    // ���ʏ����F���E���h�I����
    void EndRound()
    {
        roundActive = false;
        waitingForInput = false;
        markImage.enabled = false;

        // �\�񂵂Ă���}�[�N�\��Invoke���L�����Z��
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
