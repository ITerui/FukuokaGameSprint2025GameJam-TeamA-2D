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

    [Header("�i���pUI�C���[�W")]
    public Image characterImage;         // �ύX�Ώۂ�Image
    public Sprite[] evolutionSprites;    // �i���i�K�̉摜�z��

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

            // --- �摜��؂�ւ��� ---
            if (characterImage != null && evolutionSprites.Length > 0)
            {
                int index = Mathf.Clamp(evolutionLevel, 0, evolutionSprites.Length - 1);
                characterImage.sprite = evolutionSprites[index];

                // --- �傫���ƈʒu��ύX ---
                RectTransform rt = characterImage.rectTransform;
                rt.localScale = Vector3.one * (1 + 0.2f * evolutionLevel); // �i�K�I�Ɋg��
                rt.anchoredPosition = new Vector2(0, 10 * evolutionLevel);  // �i�K�I�ɏ�����Ɉړ�
            }

            Debug.Log($"Player{playerID} �i��! HP: {hp}/{maxHp}, �U����: {attackPower}, �i��Lv: {evolutionLevel}");

            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null) gm.OnPlayerEvolve(this);
        }
    }

    private void UpdateBars()
    {
        if (hpBarImage != null) hpBarImage.fillAmount = (float)hp / maxHp;
        if (evolutionBarImage != null) evolutionBarImage.fillAmount = (float)evolutionGauge / maxEvolution;
    }
}
