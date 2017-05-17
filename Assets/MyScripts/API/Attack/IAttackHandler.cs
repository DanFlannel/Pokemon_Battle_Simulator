using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBG.Base;

public interface IAttackHandler  {

    PokemonBase target { get; set; }
    PokemonBase self { get; set; }
    MoveResults moveRes { get; set; }

    void setPokemon(PokemonBase tar, PokemonBase s, MoveResults mr);

}
