using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1.0f;

    private void Awake()
    {
        // 最初はフェードイン
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.black;
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName)
    {
        if (fadeImage == null)
        {
            Debug.LogError("fadeImage is null!");
            return;
        }

        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    private IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = 1 - Mathf.Clamp01(t / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadeImage.gameObject.SetActive(false); // フェード完了後に非表示
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        fadeImage.gameObject.SetActive(true);
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
