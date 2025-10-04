using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fademanager : MonoBehaviour
{
    [SerializeField] private Image fadeManager;
    [SerializeField] private float fadeDuration = 1.0f;

    private void Awake()
    {
        // 
        fadeManager.gameObject.SetActive(true);
        fadeManager.color = Color.black;
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    private IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = 1 - Mathf.Clamp01(t / fadeDuration);
            fadeManager.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadeManager.gameObject.SetActive(false); 
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        fadeManager.gameObject.SetActive(true);
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / fadeDuration);
            fadeManager.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}