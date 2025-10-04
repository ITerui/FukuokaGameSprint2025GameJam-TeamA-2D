using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogHPBar : MonoBehaviour
{
    public Slider CathpBar;
    public static float DogMaxHP = 20;
    public static float DognowHp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void HitDamage(float damage)
    {
        DognowHp -= damage;
        DognowHp = Mathf.Clamp(DognowHp, 0, DogMaxHP);
    }
}
