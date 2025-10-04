using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] private GameObject cat;
    public static int Damage;
    public static int EvoLevel;
    public static bool LevelUp1;
    // Start is called before the first frame update

    public enum DogEvo
    {
        DogNone,
        DogOneEvo,
        DogTwoEvo,
        DogFinEvo,
    }

    static public DogEvo beforeEvo = DogEvo.DogNone;

    static public DogEvo nowCatEvo = DogEvo.DogNone;

    void Start()
    {
        EvoLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (nowCatEvo != beforeEvo)
        {
            EvoLevel += 1;
        }
        beforeEvo = nowCatEvo;
    }

    public void DamageUp()
    {
        Damage = 1 + EvoLevel;
    }
    public void Evo()
    {
        if (nowCatEvo < DogEvo.DogFinEvo)
        {
            nowCatEvo = (DogEvo)((int)nowCatEvo + 1);
        }
        else
        {
            //LoseBom();
        }
    }
}