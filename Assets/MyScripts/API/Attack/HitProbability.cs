using FBG.Base;
using FBG.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitProbability {

    private bool accuracyHit { get { return checkAccuracy_and_Hit(self, target); } }
    private bool targetSelf { get { return ((DexHolder.attackDex.getAttackJsonData(attackName).target == "self") || (DexHolder.attackDex.getAttackJsonData(attackName).target == "all")); } }
    private int accuracy { get { return fetchAccuracy(); } }
    private bool semiInvulnerable { get { return (target.position != pokemonPosition.normal); } }

    private string attackName;
    private PokemonBase self;
    private PokemonBase target;

    private pokemonPosition ignoreCase;
    private bool ignoreAllSemi;

    public bool sucess;

    public HitProbability(PokemonBase self, PokemonBase target, string attackName)
    {
        this.self = self;
        this.target = target;
        this.attackName = attackName;
        ignoreAllSemi = false;
        sucess = calculateHit();
    }

    private bool calculateHit()
    {
        //if we are targeting ourselves (status moves) or all (weather type moves) we automatically hit!
        if (targetSelf)
        {
            //Debug.Log("hit because it targets self or all");
            return true;
        }

        if(!ignoreAllSemi && (semiInvulnerable && ignoreCase != target.position))
        {
            //Debug.Log("reutrning false because the enemy is semi invulnerable");
            return false;
        }
        //Debug.Log(string.Format("returning accuracy based hit: {0}", accuracyHit));
        return accuracyHit;
    }

    public void ignore(pokemonPosition pos)
    {
        Debug.Log("Recalculating because of ignore semi invulerable clause");
        ignoreCase = pos;
        sucess = calculateHit();
    }

    public void ignore(bool b)
    {
        ignoreAllSemi = b;
        sucess = calculateHit();
    }


    private bool ignoreAcc_Evade(string atkName)
    {
        string name = atkName.ToLower();

        string[] ignoreMoves = { "swift", "fissure", "guillotine", "horn drill" };
        for (int i = 0; i < ignoreMoves.Length; i++)
        {
            if (name == ignoreMoves[i])
            {
                Debug.Log("This move ignores accuracy and evasion");
                return true;
            }
        }
        return false;
    }

    private int fetchAccuracy()
    {
        int acc = 0;
        switch (attackName)
        {
            default:
                acc = DexHolder.attackDex.getAttack(attackName).accuracy;
                break;
        }

        return acc;
    }

    private bool checkAccuracy_and_Hit(PokemonBase self, PokemonBase tar)
    {
        float accStage = self.acc_stage;
        float accMod = accStage + 3f;
        if (accStage >= 1)
        {
            accMod *= 100;
            accMod /= 3f;
        }
        else
        {
            accMod = 300f / accMod;

        }

        float evadeStage = tar.evasive_stage;
        float evadeMod = evadeStage + 3;

        if (evadeStage >= 1)
        {
            evadeMod = 300f / evadeMod;
        }
        else
        {
            evadeMod *= 100;
            evadeMod /= 3f;
        }

        float prob = (accuracy) * (accMod / evadeMod);
        if (ignoreAcc_Evade(attackName))
        {
            prob = accuracy;
        }

        Debug.Log(string.Format("move acc {0} self acc {1} target evasion {2} total probability {3} * {4} = {5}", accuracy, accMod, evadeMod, (accuracy), (accMod / evadeMod), prob));

        if (prob >= 100)
        {
            return true;
        }

        return Utilities.probability(prob, 100f);
    }
}
