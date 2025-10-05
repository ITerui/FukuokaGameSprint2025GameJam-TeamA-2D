using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameToResult : MonoBehaviour
{
    [SerializeField] private FadeManager fadeManager;

    // 犬と猫の進化段階を保存する変数
    private int dogEvolutionLevel = 1;
    private int catEvolutionLevel = 1;

    // 勝敗情報（1: 犬勝ち, -1: 猫勝ち, 0: 引き分け）
    private int battleResult = 0;

    void Update()
    {
        // キー入力で犬と猫の進化段階を変更（例）
        if (Input.GetKey(KeyCode.Alpha1)) { dogEvolutionLevel = 1; }
        if (Input.GetKey(KeyCode.Alpha2)) { dogEvolutionLevel = 2; }
        if (Input.GetKey(KeyCode.Alpha3)) { dogEvolutionLevel = 3; }

        if (Input.GetKey(KeyCode.Alpha4)) { catEvolutionLevel = 1; }
        if (Input.GetKey(KeyCode.Alpha5)) { catEvolutionLevel = 2; }
        if (Input.GetKey(KeyCode.Alpha6)) { catEvolutionLevel = 3; }

        // 勝敗の決定（ここでは単純にランダムで決定）
        if (Input.GetKey(KeyCode.Return))
        {
            battleResult = Random.Range(0, 2) == 0 ? 1 : -1;  // 1は犬勝ち、-1は猫勝ち
            // battleResult = 0 は引き分け（任意で設定）

            // 犬と猫の進化段階、勝敗情報を保存
            PlayerPrefs.SetInt("DogEvolutionLevel", dogEvolutionLevel);
            PlayerPrefs.SetInt("CatEvolutionLevel", catEvolutionLevel);
            PlayerPrefs.SetInt("BattleResult", battleResult);  // 勝敗情報を保存

            // リザルトシーンに遷移
            fadeManager.FadeToScene("ResultScene");
        }
    }
}