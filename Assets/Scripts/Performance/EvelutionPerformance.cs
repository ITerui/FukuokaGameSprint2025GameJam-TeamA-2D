using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EvelutionPerformance : MonoBehaviour
{
    public Image PlayerImage;
    public Image EvolutedPlayerImage;

    // サイケデリック3Dオブジェクト（Inspectorで設定）
    public GameObject PsychedelicObject;

    // 新しく追加するCubeオブジェクト
    public GameObject PsychedelicCube;

    // 初期値を指定された通りに変更
    public float psychedelicTargetScale = 450f; // 出現時のスケール
    public Vector3 psychedelicCubeTargetScale = new Vector3(2500f, 2500f, 1f); // Cubeのスケール（縦横幅だけ大きく）

    public float psychedelicScaleDuration = 0.3f; // 拡大縮小にかかる時間

    public float blinkInterval = 0.2f;
    public float totalDuration = 4f;

    public float rotationSpeed = 360f;

    private Coroutine blinkCoroutine;
    private bool isRotating = false;

    private bool psychedelicAppearStarted = false;
    private bool psychedelicEndStarted = false;

    public enum State
    {
        None,
        PsychedelicAppear,
        StartEvolution,
        PsychedelicEnd
    }

    public State CurrentState = State.None;

    void Start()
    {
        // 初期化
        if (PsychedelicObject != null)
        {
            PsychedelicObject.SetActive(false);
            PsychedelicObject.transform.localScale = Vector3.zero;
        }
        if (PsychedelicCube != null)
        {
            PsychedelicCube.SetActive(false);
            PsychedelicCube.transform.localScale = Vector3.zero;
        }
    }

    void Update()
    {
        switch (CurrentState)
        {
            case State.PsychedelicAppear:
                if (!psychedelicAppearStarted)
                {
                    psychedelicAppearStarted = true;
                    psychedelicEndStarted = false;
                    StartPsychedelicAppear();
                }
                break;

            case State.StartEvolution:
                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                }
                blinkCoroutine = StartCoroutine(BlinkCoroutine());
                CurrentState = State.None;
                break;

            case State.PsychedelicEnd:
                if (!psychedelicEndStarted)
                {
                    psychedelicEndStarted = true;
                    psychedelicAppearStarted = false;
                    StartPsychedelicEnd();
                }
                break;
        }

        if (isRotating)
        {
            if (PlayerImage.gameObject.activeSelf)
            {
                PlayerImage.rectTransform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
            }
            if (EvolutedPlayerImage.gameObject.activeSelf)
            {
                EvolutedPlayerImage.rectTransform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
            }
        }
    }

    public void StartPsychedelicAppear()
    {
        if (PsychedelicObject != null && PsychedelicCube != null)
        {
            PsychedelicObject.SetActive(true);
            PsychedelicCube.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(PsychedelicScaleCoroutine(true));
        }
        else
        {
            Debug.LogError("PsychedelicObjectかPsychedelicCubeがセットされていません！");
            CurrentState = State.StartEvolution;
        }
    }

    public void StartPsychedelicEnd()
    {
        if (PsychedelicObject != null && PsychedelicCube != null)
        {
            StopAllCoroutines();
            StartCoroutine(PsychedelicScaleCoroutine(false));
        }
        else
        {
            Debug.LogError("PsychedelicObjectかPsychedelicCubeがセットされていません！");
            CurrentState = State.None;
        }
    }

    private IEnumerator PsychedelicScaleCoroutine(bool isAppearing)
    {
        float elapsed = 0f;

        Vector3 startScalePsy = isAppearing ? Vector3.zero : Vector3.one * psychedelicTargetScale;
        Vector3 endScalePsy = isAppearing ? Vector3.one * psychedelicTargetScale : Vector3.zero;

        Vector3 startScaleCube = isAppearing ? Vector3.zero : psychedelicCubeTargetScale;
        Vector3 endScaleCube = isAppearing ? psychedelicCubeTargetScale : Vector3.zero;

        while (elapsed < psychedelicScaleDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / psychedelicScaleDuration);

            PsychedelicObject.transform.localScale = Vector3.Lerp(startScalePsy, endScalePsy, t);
            PsychedelicCube.transform.localScale = Vector3.Lerp(startScaleCube, endScaleCube, t);

            yield return null;
        }

        PsychedelicObject.transform.localScale = endScalePsy;
        PsychedelicCube.transform.localScale = endScaleCube;

        if (isAppearing)
        {
            CurrentState = State.StartEvolution;
        }
        else
        {
            PsychedelicObject.SetActive(false);
            PsychedelicCube.SetActive(false);
            CurrentState = State.None;
        }
    }

    private IEnumerator BlinkCoroutine()
    {
        isRotating = true;

        float timer = 0f;
        bool showAfter = false;

        PlayerImage.gameObject.SetActive(true);
        EvolutedPlayerImage.gameObject.SetActive(false);

        // --- ここでマテリアルのTextureを更新 ---
        if (PsychedelicObject != null && EvolutedPlayerImage != null && EvolutedPlayerImage.sprite != null)
        {
            Renderer renderer = PsychedelicObject.GetComponent<Renderer>();
            if (renderer != null && renderer.material != null)
            {
                Texture spriteTexture = EvolutedPlayerImage.sprite.texture;
                renderer.material.mainTexture = spriteTexture;
            }
            else
            {
                Debug.LogWarning("PsychedelicObjectのRendererかMaterialがありません");
            }
        }
        else
        {
            Debug.LogWarning("PsychedelicObjectかEvolutedPlayerImageまたはSpriteが設定されていません");
        }

        while (timer < totalDuration)
        {
            showAfter = !showAfter;

            PlayerImage.gameObject.SetActive(!showAfter);
            EvolutedPlayerImage.gameObject.SetActive(showAfter);

            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        PlayerImage.gameObject.SetActive(false);
        EvolutedPlayerImage.gameObject.SetActive(true);

        isRotating = false;

        PlayerImage.rectTransform.rotation = Quaternion.identity;
        EvolutedPlayerImage.rectTransform.rotation = Quaternion.identity;

        CurrentState = State.PsychedelicEnd;

        blinkCoroutine = null;
    }
}
