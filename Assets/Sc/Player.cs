using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int playerID; // 1 or 2

    [Header("�L�[�ݒ�")]
    public KeyCode attackKey;    // �U���L�[
    public KeyCode evolutionKey; // �i���L�[
    public KeyCode foulKey;      // �t���C���O�L�[

    [Header("�X�e�[�^�X")]
    public int hp = 10;
    public int maxHp = 10;
    public int attackPower = 1;
    public int evolutionGauge = 0;
    public int maxEvolution = 5;
    public int evolutionLevel = 0;

    [Header("UI")]
    public Image hpBarImage;
    public Image evolutionBarImage;

    private void Start()
    {
        UpdateBars();
    }

    // �ʏ�_���[�W�i�Q�[�W��������j
    public void TakeDamage(int damage)
    {
        hp -= damage;
        evolutionGauge += damage;
        if (evolutionGauge > maxEvolution) evolutionGauge = maxEvolution;
        UpdateBars();
        Debug.Log($"Player{playerID} �_���[�W: {damage} HP: {hp}/{maxHp} �Q�[�W: {evolutionGauge}/{maxEvolution}");
    }

    // �t���C���O�_���[�W�i�Q�[�W�͑����Ȃ��j
    public void TakeDamage_NoGauge(int damage)
    {
        hp -= damage;
        UpdateBars();
        Debug.Log($"Player{playerID} �t���C���O�y�i���e�B: {damage} �_���[�W HP: {hp}/{maxHp}");
    }

    // �i������
    public void Evolve()
    {
        if (evolutionGauge >= maxEvolution)
        {
            evolutionGauge = 0;
            evolutionLevel++;
            maxHp += 2;
            hp = maxHp;
            attackPower += 1;

            UpdateBars();
            Debug.Log($"Player{playerID} �i��! HP: {hp}/{maxHp}, �U����: {attackPower}, �i��Lv: {evolutionLevel}");

            // GameManager �ɒʒm
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
                gm.OnPlayerEvolve(this);
        }
        else
        {
            Debug.Log($"Player{playerID} �͐i���ł��Ȃ� �Q�[�W: {evolutionGauge}/{maxEvolution}");
        }
    }

    private void UpdateBars()
    {
        if (hpBarImage != null) hpBarImage.fillAmount = (float)hp / maxHp;
        if (evolutionBarImage != null) evolutionBarImage.fillAmount = (float)evolutionGauge / maxEvolution;
    }
}
