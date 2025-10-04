using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int playerID; // 1 or 2

    [Header("キー設定")]
    public KeyCode attackKey;    // 攻撃用キー
    public KeyCode evolutionKey; // 進化用キー

    [Header("ステータス")]

    public int maxHp = 10;
    public int hp = 10;          // 体力
    public int attackPower = 2;  // 攻撃力
    public int evolutionGauge = 0; // 進化ゲージ
    public int maxEvolution;  // ゲージの最大値（調整可）

    [Header("UI")]
    [SerializeField] private BarGauge HpBar = null; // 本当はここで設定するの良くない。キャラ増やしたりしたときに困る。拡張性がない。
    [SerializeField] private BarGauge EvolutionBar = null; // 本当はここで設定するの良くない。キャラ増やしたりしたときに困る。拡張性がない。

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
            int a = 0;
            // 進化処理
            evolutionGauge = 0;
            hp += 3;          // 回復量（調整可）
            if (hp > maxHp) hp = maxHp;
            attackPower += 2 +a; // 攻撃力アップ
            a += 1;

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
        if(HpBar != null)
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

    }
}
