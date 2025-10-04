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

    static public CatEvo beforeEvo = CatEvo.None;

    static public CatEvo nowCatEvo = CatEvo.None;

    void Start()
    {
        EvoLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(nowCatEvo !=beforeEvo)
        {
            EvoLevel += 1;
        }
        beforeEvo = nowCatEvo;
    }

    public void DamageUp()
    {
        Damage= 1+ EvoLevel;
        HPBar.HitDamage();
    }
    public void Evo()
    {
        if (nowCatEvo < CatEvo.FinEvo)
        {
            nowCatEvo = (CatEvo)((int)nowCatEvo + 1);
        }
        else
        {
            //LoseBom();
        }
    }
}
