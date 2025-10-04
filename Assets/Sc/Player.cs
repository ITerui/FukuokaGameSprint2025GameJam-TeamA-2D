using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerID; // 1 or 2

    [Header("キー設定")]
    public KeyCode attackKey;    // 攻撃用キー
    public KeyCode evolutionKey; // 進化用キー

    [Header("ステータス")]
    public int hp = 10;          // 体力
    public int attackPower = 1;  // 攻撃力
    public int evolutionGauge = 0; // 進化ゲージ
    public int maxEvolution = 10;  // ゲージの最大値（調整可）

    public void TakeDamage(int damage)
    {
        hp -= damage;

        // ダメージ量に応じて進化ゲージを加算
        evolutionGauge += damage;
        if (evolutionGauge > maxEvolution) evolutionGauge = maxEvolution;

        Debug.Log($"Player{playerID} が {damage} ダメージを受けた！ HP: {hp}, 進化ゲージ: {evolutionGauge}/{maxEvolution}");
    }

    public void Evolve()
    {
        if (evolutionGauge >= maxEvolution)
        {
            // 進化処理
            evolutionGauge = 0;
            hp += 3;          // 回復量（調整可）
            attackPower += 1; // 攻撃力アップ

            Debug.Log($"Player{playerID} が進化！ HP: {hp}, 攻撃力: {attackPower}");
        }
        else
        {
            Debug.Log($"Player{playerID} は進化できない（ゲージ {evolutionGauge}/{maxEvolution}）");
        }
    }
}
