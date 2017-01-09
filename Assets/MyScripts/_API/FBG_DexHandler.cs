using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FatBobbyGaming
{
    //A class that we need that will just hold our attack dex and pokedex not limiting them to anything
    public class FBG_DexHandler : MonoBehaviour
    {

        public static PokedexData pokeDex;
        public static AttackData attackDex;

        // Use this for initialization
        void Awake()
        {
            pokeDex = FBG_JsonReader.createPokeDex();
            attackDex = FBG_JsonAttack.createAttackDex();
        }
    }
}
