using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _Structs { }



public struct attacks
{
    public string name;
    public string type;
    public string cat;
    public int power;
    public int accuracy;
    public int pp;

    public attacks(string aName, string aType, string aCat, int aPower, int aAccuracy, int aPP)
    {
        name = aName;
        type = aType;
        cat = aCat;
        power = aPower;
        accuracy = aAccuracy;
        pp = aPP;
    }

}

public struct attackIndex
{
    public attacks attack;
    public int level;

    public attackIndex(attacks a, int l)
    {
        attack = a;
        level = l;
    }
}

public struct masterAttackList
{
    public string name;
    public List<attackIndex> attackIndex;

    public masterAttackList(string n, List<attackIndex> a)
    {
        name = n;
        attackIndex = a;
    }
}


[System.Serializable]
public struct dmgMult
{
    public float normal;
    public float fighting;
    public float flying;
    public float poison;
    public float ground;
    public float rock;
    public float bug;
    public float ghost;
    public float steel;
    public float fire;
    public float water;
    public float grass;
    public float electric;
    public float psychic;
    public float ice;
    public float dragon;
    public float dark;
    public float fairy;

    public dmgMult(float no, float fig, float fl, float po, float gro, float ro, float bu, float gh, float st, float fi,
                   float wa, float gra, float el, float ps, float ic, float dr, float da, float fa)
    {
        normal = no;
        fighting = fig;
        flying = fl;
        poison = po;
        ground = gro;
        rock = ro;
        bug = bu;
        ghost = gh;
        steel = st;
        fire = fi;
        water = wa;
        grass = gra;
        electric = el;
        psychic = ps;
        ice = ic;
        dragon = dr;
        dark = da;
        fairy = fa;
    }
}

public struct pokemon_dmg_multipliers
{
    public string name;
    public dmgMult damage;

    public pokemon_dmg_multipliers(string n, dmgMult b)
    {
        name = n;
        damage = b;
    }
}

[System.Serializable]
public struct move_DmgReport
{
    public float damage;
    public float heal;
    public float recoil;
    public string stageName;
    public int stageDelta;
    public string stagePokemon;

    public move_DmgReport(float damage, float heal, float recoil, string stageName, int stageDelta, string stagePokemon)
    {
        this.damage = damage;
        this.heal = heal;
        this.recoil = recoil;
        this.stageName = stageName;
        this.stageDelta = stageDelta;
        this.stagePokemon = stagePokemon;
    }
}

