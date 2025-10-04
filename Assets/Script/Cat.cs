using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private GameObject cat;
    public static int Damage;
    public static int EvoLevel;
    public static bool LevelUp1;
    // Start is called before the first frame update

    public enum CatEvo
    {
        None,
        OneEvo,
        TwoEvo,
        FinEvo,
    }

    void Start()
    {
        EvoLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelUp1)
        {
            EvoLevel += 1;
            //cat.SetActive(false);
        }
    }

    public void DamageUp()
    {
        Damage= 1+ EvoLevel;
        HPBar HitDamage();
    }
}
