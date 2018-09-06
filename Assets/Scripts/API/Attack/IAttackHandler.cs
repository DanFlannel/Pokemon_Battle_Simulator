using Base;

public interface IAttackHandler
{
    PokemonBase target { get; set; }
    PokemonBase self { get; set; }
    MoveResults moveRes { get; set; }

    void setPokemon(PokemonBase tar, PokemonBase s, ref MoveResults mr);
}