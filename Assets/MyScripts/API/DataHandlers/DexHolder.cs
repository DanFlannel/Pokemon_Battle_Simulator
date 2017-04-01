using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBG.JSON;

namespace FBG.Data
{
    public class DexHolder : MonoBehaviour
    {
        public static PokedexData pokeDex;
        public static AttackData attackDex;

        // Use this for initialization
        void Awake()
        {
            pokeDex = LibJsonReader.createPokeDex();
            attackDex = AtkJsonReader.createAttackDex();
        }
    }
}