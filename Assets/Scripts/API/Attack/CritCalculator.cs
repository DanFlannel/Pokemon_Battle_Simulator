using FBG.Base;
using FBG.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritCalculator
{
    public bool sucess;

    public CritCalculator(PokemonBase self, string atkName)
    {
        int critProb = critChance(self, atkName);
        //Debug.Log("Crit chance: 1 /" + critProb);
        bool crit = isCrit(critProb);
        sucess = crit;
    }

    /// <summary>
    /// Calculates the 1/16 chance every move has for getting a critical strike
    /// <param name="chance">the chance probability either (1/8) or (1/16)</param>
    /// <returns>true if the move crit, false if it did not</returns>
    ///</summary>
    private static bool isCrit(int chance)
    {
        float divider = 1f / (float)chance;
        divider *= 100f;
        //Debug.Log("crit chance: " + divider);
        return Utilities.probability(divider, 100f);
    }

    /// <summary>
    /// Handles the crit ratio of the pokemon and of the attack move
    /// </summary>
    /// <param name="atkName"> the name of the attack</param>
    /// <returns>the crit chance of the move either (1/8) or (1/16)</returns>
    private static int critChance(PokemonBase self, string atkName)
    {
        int stage = self.critRatio_stage;
        int atkratio = DexHolder.attackDex.GetCirtRatio(atkName);
        int total = stage + atkratio;
        int final;

        if (atkratio != 0 || stage != 0)
        {
            Debug.Log(string.Format("stage: {0} attackRatio: {1} final: {2}", stage, atkratio, total));
        }

        switch (total)
        {
            default:
                final = 16;
                break;

            case 0:
                final = 16;
                break;

            case 1:
                final = 8;
                break;

            case 2:
                final = 2;
                break;

            case 3:
                final = 1;
                break;

            case 4:
                final = 1;
                break;

            case 5:
                final = 1;
                break;

            case 6:
                final = 1;
                break;
        }
        return final;
    }
}