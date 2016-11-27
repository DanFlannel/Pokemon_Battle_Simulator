using UnityEngine;
using System.Collections;

public class _Enums { }

public enum nonVolitileStatusEffects
{
    none,
    burned,
    paralized,
    poisioned,
    toxic,
    frozen,
    sleep
};

public enum volitileStatusEffects
{
    none,
    confused,
    infatuated
};

public enum pokemonPosition
{
    underground,
    normal, 
    flying
};

public enum attackStatus
{
    Charging,
    Normal,
    Recharging
};

public enum PokemonGeneration
{
    Gen1,
    Gen2,
    Gen3,
    Gen4,
    Gen5,
    Gen6,
    Gen7
};

public enum PokemonTeam
{
    redTeam,
    blueTeam
};