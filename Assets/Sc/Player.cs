using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int playerID; // 1 or 2

    [Header("キー設定")]
    public KeyCode attackKey;    // 攻撃キー
    public KeyCode evolutionKey; // 進化キー
    public KeyCode foulKey;      // フライングキー

    [Header("ステータス")]
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

    // 通常ダメージ（ゲージ増加あり）
    public void TakeDamage(int damage)
    {
        hp -= damage;
        evolutionGauge += damage;
        if (evolutionGauge > maxEvolution) evolutionGauge = maxEvolution;
        UpdateBars();
        Debug.Log($"Player{playerID} ダメージ: {damage} HP: {hp}/{maxHp} ゲージ: {evolutionGauge}/{maxEvolution}");
    }

    // フライングダメージ（ゲージは増えない）
    public void TakeDamage_NoGauge(int damage)
    {
        hp -= damage;
        UpdateBars();
        Debug.Log($"Player{playerID} フライングペナルティ: {damage} ダメージ HP: {hp}/{maxHp}");
    }

    // 進化処理
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
            Debug.Log($"Player{playerID} 進化! HP: {hp}/{maxHp}, 攻撃力: {attackPower}, 進化Lv: {evolutionLevel}");

            // GameManager に通知
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
                gm.OnPlayerEvolve(this);
        }
        else
        {
            Debug.Log($"Player{playerID} は進化できない ゲージ: {evolutionGauge}/{maxEvolution}");
        }
    }

    private void UpdateBars()
    {
        if (hpBarImage != null) hpBarImage.fillAmount = (float)hp / maxHp;
        if (evolutionBarImage != null) evolutionBarImage.fillAmount = (float)evolutionGauge / maxEvolution;
    }
}
