using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int playerID; // 1 or 2

    [Header("�L�[�ݒ�")]
    public KeyCode attackKey;    // �U���p�L�[
    public KeyCode evolutionKey; // �i���p�L�[

    [Header("�X�e�[�^�X")]

    public int maxHp = 10;
    public int hp = 10;          // �̗�
    public int attackPower = 2;  // �U����
    public int evolutionGauge = 0; // �i���Q�[�W
    public int maxEvolution;  // �Q�[�W�̍ő�l�
    private bool Apr = false;
　
    [Header("UI")]
    public Image hpBarImage;
    public Image evolutionBarImage;
    
    public void Start()
    {
        if(HpBar != null)
        {
            HpBar.Setup(maxHp, maxHp);
        }

        if(EvolutionBar != null)
        {
            EvolutionBar.Setup(maxEvolution, 0);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        // �_���[�W�ʂɉ����Đi���Q�[�W����Z
        evolutionGauge += damage;
        Debug.Log($"[DEBUG] HP�X�V: {hp}/{maxHp}");
        if (evolutionGauge > maxEvolution) evolutionGauge = maxEvolution;

        UpdateBar();
        UpdateUIBar();

        Debug.Log($"Player{playerID} �� {damage} �_���[�W��󂯂��I HP: {hp}, �i���Q�[�W: {evolutionGauge}/{maxEvolution}");
    }

    public void Evolve()
    {
        if (evolutionGauge >= maxEvolution)
        {
            int a = 0;
            // �i������
            evolutionGauge = 0;
            hp += 3;          // �񕜗ʁi�����j
            if (hp > maxHp) hp = maxHp;
            if (Apr==true)
            {
                a -= 1;
            }
            attackPower += 2 +a；
            a += 2;
            Apr = true;


            UpdateBar();
            UpdateUIBar();

            Debug.Log($"Player{playerID} ���i���I HP: {hp}, �U����: {attackPower}");
        }
        else
        {
            Debug.Log($"Player{playerID} �͐i���ł��Ȃ��i�Q�[�W {evolutionGauge}/{maxEvolution}�j");
        }
    }

    private void UpdateBar()
    {
        if(hpBarImage != null)
        {
            hpBarImage.fillAmount = (float)hp / maxHp;
        }
    ｝
    private void UpdateUIBar()
    {
        if (EvolutionBar != null)
        {
            evolutionBar.Image.fillAmount = (float)evolutionGauge/maxEvolution;
        }
    }

    internal void TakeDamage_NoGauge(int foulDamage)
    {

    }
}
