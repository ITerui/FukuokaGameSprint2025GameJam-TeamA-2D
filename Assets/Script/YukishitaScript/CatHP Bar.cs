using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatHPBar : MonoBehaviour
{
    [Header("HP")] public Image hpBarimage;

    [Header("ç≈ëÂHP")]public float maxHP;

    private float nowHp;
    private float targetFillAmount;

    // Start is called before the first frame update
    void Start()
    {
        nowHp = maxHP;
        targetFillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        hpBarimage.fillAmount = Mathf.Lerp(hpBarimage.fillAmount, targetFillAmount, Time.deltaTime * 5f);
    }
} 