using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int playerID; // 1 or 2

    [Header("�L�[�ݒ�")]
    public KeyCode  attackKey;    // �U���p�L�[
    public KeyCode evolutionKey; // �i���p�L�[

    [Header("�X�e�[�^�X")]

    public int maxHp = 10;
    public int hp = 10;          // �̗�
    public int attackPower = 2;  // �U����
    public int evolutionGauge = 0; // �i���Q�[�W
    public int maxEvolutionGauge = 20;  // �Q�[�W�̍ő�l�i�����j
    public int EvoCount = 0;

    [Header("UI")]
    [SerializeField] private BarGauge HpBar = null; // �{���͂����Őݒ肷��̗ǂ��Ȃ��B�L�������₵���肵���Ƃ��ɍ���B�g�������Ȃ��B
    [SerializeField] private BarGauge EvolutionBar = null; // �{���͂����Őݒ肷��̗ǂ��Ȃ��B�L�������₵���肵���Ƃ��ɍ���B�g�������Ȃ��B

    public CharacterSpriteManager ImageManager;
    public Image CharacterImage;

    public void Start()
    {
        if (EvolutionBar != null)
        {
            EvolutionBar.Setup(maxEvolutionGauge, 0);
        }
        EvoCount = 0;
        ChangeIdleImage(EvoCount);

        hp = maxHp;
        if (HpBar != null)
        {
            HpBar.Setup(hp, maxHp);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        // �_���[�W�ʂɉ����Đi���Q�[�W����Z
        evolutionGauge += damage;
        Debug.Log($"[DEBUG] HP�X�V: {hp}/{maxHp}");
        if (evolutionGauge > maxEvolutionGauge) evolutionGauge = maxEvolutionGauge;

        UpdateBar();
        UpdateUIBar();
        ChangeHitImage(EvoCount);

        Debug.Log($"Player{playerID} �� {damage} �_���[�W��󂯂��I HP: {hp}, �i���Q�[�W: {evolutionGauge}/{maxEvolutionGauge}");
    }

    public void Evolve()
    {
        if (evolutionGauge >= maxEvolutionGauge)
        {
            int a = 0;
            // �i������
            evolutionGauge = 0;
            hp += 3;          // �񕜗ʁi�����j
            if (hp > maxHp) hp = maxHp;
            attackPower += 2 + a; // �U���̓A�b�v
            a += 1;

            UpdateBar();
            UpdateUIBar();

            EvoCount++;
            ChangeIdleImage(EvoCount);

            Debug.Log($"Player{playerID} ���i���I HP: {hp}, �U����: {attackPower}");
        }
        else
        {
            Debug.Log($"Player{playerID} �͐i���ł��Ȃ��i�Q�[�W {evolutionGauge}/{maxEvolution}�j");
            Debug.Log($"Player{playerID} �͐i���ł��Ȃ��i�Q�[�W {evolutionGauge}/{maxEvolutionGauge}�j");
        }
    }

    private void UpdateBar()
    {
        if (HpBar != null)
        {
            HpBar.SetValue(hp);
        }
    }
    private void UpdateUIBar()
    {
        if (EvolutionBar != null)
        {
            EvolutionBar.SetValue(evolutionGauge);
        }
    }

    internal void TakeDamage_NoGauge(int foulDamage)
    {
        OnHit();

        hp -= foulDamage;
        UpdateBar();
    }

    public void ReturIdle()
    {
        ChangeIdleImage(EvoCount);
    }

    public void OnAttack()
    {
        ChangeAttackImage(EvoCount);
    }

    public void OnHit()
    {
        ChangeHitImage(EvoCount);
    }

    void ChangeIdleImage(int InEvoCount)
    {
        CharacterImage.sprite = ImageManager.GetIdleSplite(InEvoCount);
    }
    void ChangeAttackImage(int InEvoCount)
    {
        CharacterImage.sprite = ImageManager.GetAttackSplite(InEvoCount);
    }
    void ChangeHitImage(int InEvoCount)
    {
        CharacterImage.sprite = ImageManager.GetHitSplite(InEvoCount);
    }
}
