using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPBar : MonoBehaviour
{
    [Header("HP Bar Images")]
    public Image BackgroundImage;   // HPバーの背景
    public Image HpImage;           // 現在HPバー
    public Image DamageImage;       // ダメージ用赤いバー

    [Header("Damage Bar Settings")]
    public float DelayDamageLerpTime = 1.0f;
    public float DamageLerpTime = 1.5f;

    [Header("HP Settings (Inspectorで編集可能)")]
    [SerializeField] private float MaxHP = 100f;
    [SerializeField] private float CurrentHP = 100f;

    private Coroutine DamageCoroutine;

    private void OnValidate()
    {
        if (!DamageImage)
        {
            DamageImage.fillAmount = HpImage.fillAmount;
        }

        MaxHP = Mathf.Max(1f, MaxHP);
        CurrentHP = Mathf.Clamp(CurrentHP, 0f, MaxHP);
        UpdateHPBar();

        StartDamageCoroutine();
    }

    public void Setup(float InMaxHP, float InCurrentHP)
    {
        MaxHP = InMaxHP;
        CurrentHP = Mathf.Clamp(InCurrentHP, 0, MaxHP);
        UpdateHPBar();

        if(!DamageImage)
        {
            DamageImage.fillAmount = HpImage.fillAmount;
        }
    }

    public void SetHP(float InCurrentHP)
    {
        if (!DamageImage)
        {
            DamageImage.fillAmount = HpImage.fillAmount;
        }

        CurrentHP = Mathf.Clamp(InCurrentHP, 0, MaxHP);
        UpdateHPBar();

        StartDamageCoroutine();
    }

    private void UpdateHPBar()
    {
        if (HpImage != null)
        {
            HpImage.fillAmount = CurrentHP / MaxHP;
        }
    }

    private void StartDamageCoroutine()
    {
        if(!DamageImage)
        {
            return;
        }
        
        if(DamageCoroutine != null)
        {
            StopCoroutine(DamageCoroutine);
        }

        DamageCoroutine = StartCoroutine(UpdateDamageBar());
    }

    private IEnumerator UpdateDamageBar()
    {
        if (DamageImage == null || HpImage == null)
        {
            yield break;
        }

        // 1. 指定秒数待つ
        float timer = 0f;
        while (timer < DelayDamageLerpTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // 2. Lerpで赤ゲージを追従
        float startFill = DamageImage.fillAmount;
        float targetFill = HpImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < DamageLerpTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / DamageLerpTime);
            DamageImage.fillAmount = Mathf.Lerp(startFill, targetFill, t);
            yield return null;
        }

        // 最後にピッタリ合わせる
        DamageImage.fillAmount = targetFill;
    }

}
