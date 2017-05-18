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
                Debug.Log(string.Format("{0} is paralized and can't move", self.Name));
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
                Debug.Log(string.Format("{0} is frozen and can't move", self.Name));
                isAffected = true;
            }
            else
            {
                self.status_A = nonVolitileStatusEffects.none;
                text = self.Name + " thawed out!";
                Debug.Log(string.Format("{0} thawed out", self.Name));
            }
            return new nonVolitleMove(text, isAffected);
        }

        private static nonVolitleMove Sleep(PokemonBase self)
        {
            string text = "";
            bool isAffected = false;
            self.nvDur--;
            if (self.nvDur == 0)
            {
                text = self.Name + " woke up!";
                Debug.Log(string.Format("{0} woke up!", self.Name));
                self.status_A = nonVolitileStatusEffects.none;
            }
            else
            {
                text = self.Name + " is fast asleep!";
                Debug.Log(string.Format("{0} is asleep and can't move!", self.Name));
                isAffected = true;
            }
            return new nonVolitleMove(text, isAffected);
        }

        /// <summary>
        /// This takes in a list and generates a random list of unique integers based off that number
        /// </summary>
        /// <param name="attackMoves"></param>
        /// <returns></returns>
        public static List<int> generateRandomList(int maxPossibility, int length)
        {
            List<int> rndNumberList = new List<int>();

            //Debug.Log("Range: " + totalPossibleMoves);
            //Debug.Log("List Cout: " + list.Count);
            int numToAdd = -1;
            //if the pokemon has more than 4 moves that it can learn, then we pick from those randomly
            if (maxPossibility > length)
            {
                for (int i = 0; i < length; i++)
                {
                    numToAdd = UnityEngine.Random.Range(0, maxPossibility);
                    while (rndNumberList.Contains(numToAdd))
                    {
                        numToAdd = UnityEngine.Random.Range(0, maxPossibility);
                    }
                    rndNumberList.Add(numToAdd);
                }
            }
            //this ensures that all possible moves are added for pokemon with less than or equal to 4 moves
            else
            {
                //Debug.LogWarning(string.Format("{0} total moves {1}", Name, totalPossibleMoves));

                int totalMoves = 0;
                for (int i = 0; i < length; i++)
                {
                    if (totalMoves < maxPossibility)
                    {
                        numToAdd = i;
                        totalMoves++;
                    }
                    else
                    {
                        numToAdd = UnityEngine.Random.Range(0, totalMoves);
                    }
                    rndNumberList.Add(numToAdd);
                }
            }
            //Debug.Log(string.Format("Name: {0} Total: {1} indexes: {2} {3} {4} {5}",
            //    Name, totalPossibleMoves,
            //    rndNumberList[0], rndNumberList[1], rndNumberList[2], rndNumberList[3]));
            return rndNumberList;
        }

        public static bool probability(float prob, float bounds)
        {
            bool chance = false;
            float guess = Random.Range(0, bounds - 1);
            //Debug.Log(guess + " : " + prob);
            if (guess < prob)
            {
                chance = true;
            }
            return chance;
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

