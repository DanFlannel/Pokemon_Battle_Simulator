using UnityEngine;

using JSON;

namespace Data
{
    public class DexHolder : MonoBehaviour
    {
        private static DexHolder instance;
        public static DexHolder Instance { get { return instance; } }

        public static PokedexData pokeDex;
        public static AttackData attackDex;

        // Use this for initialization
        private void Awake()
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