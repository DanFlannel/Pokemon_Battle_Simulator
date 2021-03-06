﻿using Base;

public class MoveResults
{
    /// <summary>
    /// Name of the attack move
    /// </summary>
    public string atkName;

    /// <summary>
    /// Did the attack crit
    /// </summary>
    public bool crit;

    /// <summary>
    /// Did the attacker flinch
    /// </summary>
    public bool flinched;

    /// <summary>
    /// Did the attacker fail
    /// </summary>
    public bool failed;

    /// <summary>
    /// Damage report
    /// </summary>
    public move_DmgReport dmgReport;

    /// <summary>
    /// the name of the status affect affed during the move
    /// </summary>
    public nonVolitileStatusEffects statusAEffect;

    public volitileStatusEffects statusBEffect;

    /// <summary>
    /// name of the pokemon being affected by the status effect
    /// </summary>
    public string statusTarget;

    public HitProbability hit;

    public MoveResults(PokemonBase self, PokemonBase target, string s)
    {
        atkName = s;

        crit = false;
        flinched = false;
        failed = false;

        statusAEffect = nonVolitileStatusEffects.none;
        statusTarget = "";

        hit = new HitProbability(self, target, atkName);
        //Debug.Log(string.Format("Hit calcualted: {0}", hit.sucess));
    }
}