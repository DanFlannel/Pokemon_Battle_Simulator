using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FatBobbyGaming
{
    public class FBG_BattleSimulator : MonoBehaviour
    {
        public List<PokemonEntity> redTeam;
        public List<PokemonEntity> blueTeam;

        // Use this for initialization
        void Awake()
        {
            //this initalizes our pokedex
            PokedexData pokeDex = FBG_JsonReader.createPokeDex();
            //this initalizes the attack library
            FBG_Atk_Data.createAttackLibrary();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
