using UnityEngine;
using System.Collections;

namespace FatBobbyGaming
{
    public static class FBG_Utils
    {

        /// <summary>
        /// this is only to check if this affects the pokemon moving
        /// </summary>
        /// <param name="self">the pokemon to check</param>
        /// <returns></returns>
        public static nonVolitleMove isMoveHaltedByNV(FBG_Pokemon self)
        {
            string text = "";
            if (self.status_A != nonVolitileStatusEffects.none)
            {
                if (self.status_A == nonVolitileStatusEffects.paralized)
                {
                    int rnd = UnityEngine.Random.Range(1, 4);
                    if (rnd == 1)
                    {
                        text = self.Name + " is Paralized!";
                        return new nonVolitleMove(text, true);
                    }
                }
                if (self.status_A == nonVolitileStatusEffects.sleep)
                {
                    self.nonVolDuration--;
                    if (self.nonVolDuration == 0)
                    {
                        text = self.Name + " woke up!";
                    }
                    else
                    {
                        text = self.Name + " is fast asleep!";
                        return new nonVolitleMove(text, true);
                    }

                }
                if (self.status_A == nonVolitileStatusEffects.frozen)
                {
                    int rnd = UnityEngine.Random.Range(1, 10);
                    if (rnd >= 2)
                    {
                        text = self.Name + " is Frozen!";
                        return new nonVolitleMove(text, true);
                    }
                    else
                    {
                        self.status_A = nonVolitileStatusEffects.none;
                        text = self.Name + " thawed out!";
                    }
                }
            }
            return new nonVolitleMove(text, false);
        }

    }

    public class nonVolitleMove
    {
        public string text;
        public bool isAffected;
       
        public nonVolitleMove(string t, bool b)
        {
            text = t;
            isAffected = b;
        }
    }
}
