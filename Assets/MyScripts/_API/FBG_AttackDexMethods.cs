using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FatBobbyGaming
{
    public static class FBG_AttackDexMethods
    {
        public static AttackJsonData Get(this AttackData a, string atkName)
        {
            for (int i = 0; i < a.attacks.Length; i++)
            {
                if (atkName.ToLower() == a.attacks[i].name.ToLower())
                {
                    return a.attacks[i];
                }
            }
            return null;
        }


        public static bool checkFlag(this AttackData a, string atkName, string flag)
        {
            AttackJsonData atk = a.Get(atkName);

            for(int i = 0; i < atk.flags.Length; i++)
            {
                if (flag.ToLower() == atk.flags[i].ToLower())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
