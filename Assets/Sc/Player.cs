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

    [Header("進化用UIイメージ")]
    public Image characterImage;         // 変更対象のImage
    public Sprite[] evolutionSprites;    // 進化段階の画像配列

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

            // --- 画像を切り替える ---
            if (characterImage != null && evolutionSprites.Length > 0)
            {
                int index = Mathf.Clamp(evolutionLevel, 0, evolutionSprites.Length - 1);
                characterImage.sprite = evolutionSprites[index];

                // --- 大きさと位置を変更 ---
                RectTransform rt = characterImage.rectTransform;
                rt.localScale = Vector3.one * (1 + 0.2f * evolutionLevel); // 段階的に拡大
                rt.anchoredPosition = new Vector2(0, 10 * evolutionLevel);  // 段階的に少し上に移動
            }

            Debug.Log($"Player{playerID} 進化! HP: {hp}/{maxHp}, 攻撃力: {attackPower}, 進化Lv: {evolutionLevel}");

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
