using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public static float MaxHP;
    private float nowHp;
    // Start is called before the first frame update
    void Start()
    {
        nowHp = MaxHP;
        MaxHP = 10;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void HitDamage()
    {

    }
}
