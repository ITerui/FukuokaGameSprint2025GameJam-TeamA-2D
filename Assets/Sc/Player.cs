using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int playerID; // 1 or 2

    [Header("キー設定")]
    public KeyCode attackKey;    // 攻撃用キー
    public KeyCode evolutionKey; // 進化用キー

    [Header("ステータス")]

    public int hp = 10;          // 体力
    public int maxHp;
    public int attackPower = 1;  // 攻撃力
    public int evolutionGauge = 0; // 進化ゲージ
    public int maxEvolution;  // ゲージの最大値（調整可）
    
    [Header("UI")]
    public Image hpBarImage;
    public Image evolutionBarImage;

    public void TakeDamage(int damage)
    {
        hp -= damage;

        // ダメージ量に応じて進化ゲージを加算
        evolutionGauge += damage;
        Debug.Log($"[DEBUG] HP更新: {hp}/{maxHp}");
        if (evolutionGauge > maxEvolution) evolutionGauge = maxEvolution;

        UpdateBar();

        Debug.Log($"Player{playerID} が {damage} ダメージを受けた！ HP: {hp}, 進化ゲージ: {evolutionGauge}/{maxEvolution}");
    }

    public void Evolve()
    {
        if (evolutionGauge >= maxEvolution)
        {
            // 進化処理
            evolutionGauge = 0;
            hp += 3;          // 回復量（調整可）
            if (hp > maxHp) hp = maxHp;

            attackPower += 1; // 攻撃力アップ

            UpdateBar();
            UpdateUIBar();

            Debug.Log($"Player{playerID} が進化！ HP: {hp}, 攻撃力: {attackPower}");
        }
        else
        {
            Debug.Log($"Player{playerID} は進化できない（ゲージ {evolutionGauge}/{maxEvolution}）");
        }
    }

    private void UpdateBar()
    {

        if (hpBarImage !=null)
        {
            hpBarImage.fillAmount = (float)hp / maxHp;
        }
    }
    private void UpdateUIBar()
    {
        if (evolutionBarImage != null)
        {
            evolutionBarImage.fillAmount = (float)hp / maxHp;
        }
    }
}
