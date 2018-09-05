using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBG.JSON;

namespace FBG.Data
{
    public class DexHolder : MonoBehaviour
    {
        private static DexHolder instance;
        public static DexHolder Instance { get { return instance; } }

        public static PokedexData pokeDex;
        public static AttackData attackDex;

        // Use this for initialization
        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }

            if (pokeDex == null)
            {
                pokeDex = LibJsonReader.createPokeDex();
            }
            if (attackDex == null)
            {
                attackDex = AtkJsonReader.createAttackDex();
            }
        }
    }
}