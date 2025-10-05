using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BarGauge : MonoBehaviour
{

    [Header("BarGauge Settings")]
    public Image BarGaugeImage = null;
    [SerializeField] private float MaxValue = 100f;
    [SerializeField] private float CurrentValue = 100f;
    [SerializeField] private GameObject DekasinkaEf;


    [Header("BarGauge Settings for HP")]
    public Image DamageBarGaugeImage = null;
    public float DelayDamageLerpTime = 1.0f;
    public float DamageLerpTime = 1.5f;

    private Coroutine DamageCoroutine = null;
    private bool isEffectRunning;

    private void OnValidate()
    {
        ForceFixSubBarGauge();

        MaxValue = Mathf.Max(1f, MaxValue);
        CurrentValue = Mathf.Clamp(CurrentValue, 0f, MaxValue);
        UpdateBarGauge();

        //StartDamageCoroutine();
    }

    public void Start()
    {
        DekasinkaEf.SetActive(false);
    }
    public void Update()
    {
        if (!isEffectRunning && CurrentValue == MaxValue)
        {
            StartCoroutine(DekaEf());
        }
    }

    
   IEnumerator DekaEf()
    {
        if(CurrentValue==MaxValue)
        {
            isEffectRunning = true;

            for (int i = 0; i < 5; i++)
            {
                DekasinkaEf.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                DekasinkaEf.SetActive(false);
                yield return new WaitForSeconds(0.5f);
            }

            isEffectRunning = false;
        }
    }

    public void Setup(float InMaxValue, float InCurrentValue)
    {
        MaxValue = InMaxValue;
        CurrentValue = Mathf.Clamp(InCurrentValue, 0, MaxValue);
        UpdateBarGauge();

        ForceFixSubBarGauge();
    }

    public void SetValue(float InCurrentValue)
    {
        ForceFixSubBarGauge();

        CurrentValue = Mathf.Clamp(InCurrentValue, 0, MaxValue);
        UpdateBarGauge();

        StartDamageCoroutine();
    }

    private void UpdateBarGauge()
    {
        if (BarGaugeImage != null)
        {
            BarGaugeImage.fillAmount = CurrentValue / MaxValue;
        }
    }

    private void StartDamageCoroutine()
    {
#if UNITY_EDITOR
        if(!Application.isPlaying)
        {
            return;
        }
#endif

        if (!DamageBarGaugeImage)
        {
            return;
        }

        if (DamageCoroutine != null)
        {
            StopCoroutine(DamageCoroutine);
        }

        DamageCoroutine = StartCoroutine(UpdateDamageBar());
    }

    private IEnumerator UpdateDamageBar()
    {
        if (DamageBarGaugeImage == null || BarGaugeImage == null)
        {
            yield break;
        }

        // 1. Žw’è•b”‘Ò‚Â
        float timer = 0f;
        while (timer < DelayDamageLerpTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // 2. Lerp‚ÅÔƒQ[ƒW‚ð’Ç]
        float startFill = DamageBarGaugeImage.fillAmount;
        float targetFill = BarGaugeImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < DamageLerpTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / DamageLerpTime);
            DamageBarGaugeImage.fillAmount = Mathf.Lerp(startFill, targetFill, t);
            yield return null;
        }

        // ÅŒã‚Éƒsƒbƒ^ƒŠ‡‚í‚¹‚é
        DamageBarGaugeImage.fillAmount = targetFill;
    }

    private void ForceFixSubBarGauge()
    {
        if (DamageBarGaugeImage != null)
        {
            DamageBarGaugeImage.fillAmount = BarGaugeImage.fillAmount;
        }
    }
}
