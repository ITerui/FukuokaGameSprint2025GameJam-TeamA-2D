using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteManager : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> IdleSpriteList;
    [SerializeField]
    private List<Sprite> AttackSpriteList;
    [SerializeField]
    private List<Sprite> HitSpriteList;

    public Sprite GetIdleSplite(int InEvoCount)
    {
        return IdleSpriteList[InEvoCount];
    }
    public Sprite GetAttackSplite(int InEvoCount)
    {
        return AttackSpriteList[InEvoCount];
    }
    public Sprite GetHitSplite(int InEvoCount)
    {
        return HitSpriteList[InEvoCount];
    }
}
