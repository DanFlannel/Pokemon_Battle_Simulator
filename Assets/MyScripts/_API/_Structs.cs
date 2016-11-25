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



public struct Pokemon
{
    public int pokemonID;
    public string name;
    public int hp;
    public int attack;
    public int defense;
    public int specialAttack;
    public int specialDefense;
    public int speed;
    public bool canEvolve;
    public string type1;
    public string type2;


    public Pokemon(int sID, string sName, int sHP, int sAttack, int sDefense, int sSpecialA, int sSpecialD, int sSpeed, bool sCanEvolve, string t1, string t2)
    {
        pokemonID = sID;
        name = sName;
        hp = sHP;
        attack = sAttack;
        defense = sDefense;
        speed = sSpeed;
        specialAttack = sSpecialA;
        specialDefense = sSpecialD;
        canEvolve = sCanEvolve;
        type1 = t1;
        type2 = t2;
    }
}

public struct MoveResult
{
    public float damage;
    public float heal;
    public float recoil;

    public MoveResult(float d, float h, float r)
    {
        damage = d;
        heal = h;
        recoil = r;
    }
}


