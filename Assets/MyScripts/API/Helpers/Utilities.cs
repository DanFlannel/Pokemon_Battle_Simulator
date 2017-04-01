using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FBG.Base
{
    public static class Utilities {

        /// <summary>
        /// this is only to check if this affects the pokemon moving
        /// </summary>
        /// <param name="self">the pokemon to check</param>
        /// <returns></returns>
        public static nonVolitleMove isMoveHaltedByNV(PokemonBase self)
        {
            string text = "";
            if (self.status_A != nonVolitileStatusEffects.none)
            {
                if (self.status_A == nonVolitileStatusEffects.paralized)
                {
                    return Paralized(self);
                }
                if (self.status_A == nonVolitileStatusEffects.sleep)
                {
                    return Sleep(self);
                }

                if (self.status_A == nonVolitileStatusEffects.frozen)
                {
                    return Frozen(self);
                }
            }
            return new nonVolitleMove(text, false);
        }

        private static nonVolitleMove Paralized(PokemonBase self)
        {
            int rnd = UnityEngine.Random.Range(1, 4);
            if (rnd == 1)
            {
                string text = self.Name + " is Paralized!";
                return new nonVolitleMove(text, true);
            }
            return new nonVolitleMove("", false);
        }

        private static nonVolitleMove Frozen(PokemonBase self)
        {
            string text = "";
            bool isAffected = false;
            int rnd = UnityEngine.Random.Range(1, 10);
            if (rnd >= 2)
            {
                text = self.Name + " is Frozen!";
                isAffected = true;
            }
            else
            {
                self.status_A = nonVolitileStatusEffects.none;
                text = self.Name + " thawed out!";
            }
            return new nonVolitleMove(text, isAffected);
        }

        private static nonVolitleMove Sleep(PokemonBase self)
        {
            string text = "";
            bool isAffected = false;
            self.nonVolDuration--;
            if (self.nonVolDuration == 0)
            {
                text = self.Name + " woke up!";
            }
            else
            {
                text = self.Name + " is fast asleep!";
                isAffected = true;
            }
            return new nonVolitleMove(text, isAffected);
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

