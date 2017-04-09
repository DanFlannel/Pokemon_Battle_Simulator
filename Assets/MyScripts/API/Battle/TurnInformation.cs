﻿using FBG.Base;
using FBG.Data;
using System.Collections;
using System.Collections.Generic;


namespace FBG.Battle {
    public class TurnInformation {

        public int priority;
        public int speed;
        public bool isSwapping;
        public TeamPokemon team;
        public PokemonBase pokemon;
        public int moveIndex;

        public TurnInformation(PokemonBase pokemon, int moveIndex, bool isSwapping)
        {
            this.isSwapping = isSwapping;
            this.pokemon = pokemon;
            speed = pokemon.Speed;
            priority = 0;
            team = pokemon.team;
            this.moveIndex = moveIndex;
            if (!this.isSwapping)
            {
                priority = DexHolder.attackDex.Get(pokemon.atkMoves[moveIndex]).priority;
            }
        }

    }
}
