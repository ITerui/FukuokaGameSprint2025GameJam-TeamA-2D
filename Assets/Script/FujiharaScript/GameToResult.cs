using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameToResult : MonoBehaviour
{
    [SerializeField] private FadeManager fadeManager;

    // ���ƔL�̐i���i�K��ۑ�����ϐ�
    private int dogEvolutionLevel = 1;
    private int catEvolutionLevel = 1;

    // ���s���i1: ������, -1: �L����, 0: ���������j
    private int battleResult = 0;

    void Update()
    {
        // �L�[���͂Ō��ƔL�̐i���i�K��ύX�i��j
        if (Input.GetKey(KeyCode.Alpha1)) { dogEvolutionLevel = 1; }
        if (Input.GetKey(KeyCode.Alpha2)) { dogEvolutionLevel = 2; }
        if (Input.GetKey(KeyCode.Alpha3)) { dogEvolutionLevel = 3; }

        if (Input.GetKey(KeyCode.Alpha4)) { catEvolutionLevel = 1; }
        if (Input.GetKey(KeyCode.Alpha5)) { catEvolutionLevel = 2; }
        if (Input.GetKey(KeyCode.Alpha6)) { catEvolutionLevel = 3; }

        // ���s�̌���i�����ł͒P���Ƀ����_���Ō���j
        if (Input.GetKey(KeyCode.Return))
        {
            battleResult = Random.Range(0, 2) == 0 ? 1 : -1;  // 1�͌������A-1�͔L����
            // battleResult = 0 �͈��������i�C�ӂŐݒ�j

            // ���ƔL�̐i���i�K�A���s����ۑ�
            PlayerPrefs.SetInt("DogEvolutionLevel", dogEvolutionLevel);
            PlayerPrefs.SetInt("CatEvolutionLevel", catEvolutionLevel);
            PlayerPrefs.SetInt("BattleResult", battleResult);  // ���s����ۑ�

            // ���U���g�V�[���ɑJ��
            fadeManager.FadeToScene("ResultScene");
        }
    }
}