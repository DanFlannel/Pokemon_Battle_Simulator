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
        void Start()
        {
            PokedexData pokeDex = FBG_JsonReader.createPokeDex();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
