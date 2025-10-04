using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerID; // 1 or 2

    [Header("�L�[�ݒ�")]
    public KeyCode attackKey;    // �U���p�L�[
    public KeyCode evolutionKey; // �i���p�L�[

    [Header("�X�e�[�^�X")]
    public int hp = 10;          // �̗�
    public int attackPower = 1;  // �U����
    public int evolutionGauge = 0; // �i���Q�[�W
    public int maxEvolution = 10;  // �Q�[�W�̍ő�l�i�����j

    public void TakeDamage(int damage)
    {
        hp -= damage;

        // �_���[�W�ʂɉ����Đi���Q�[�W�����Z
        evolutionGauge += damage;
        if (evolutionGauge > maxEvolution) evolutionGauge = maxEvolution;

        Debug.Log($"Player{playerID} �� {damage} �_���[�W���󂯂��I HP: {hp}, �i���Q�[�W: {evolutionGauge}/{maxEvolution}");
    }

    public void Evolve()
    {
        if (evolutionGauge >= maxEvolution)
        {
            // �i������
            evolutionGauge = 0;
            hp += 3;          // �񕜗ʁi�����j
            attackPower += 1; // �U���̓A�b�v

            Debug.Log($"Player{playerID} ���i���I HP: {hp}, �U����: {attackPower}");
        }
        else
        {
            Debug.Log($"Player{playerID} �͐i���ł��Ȃ��i�Q�[�W {evolutionGauge}/{maxEvolution}�j");
        }
    }
}
