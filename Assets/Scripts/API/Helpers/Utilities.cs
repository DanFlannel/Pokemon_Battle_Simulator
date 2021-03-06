﻿using System.Collections.Generic;

using UnityEngine;

namespace Base
{
    public static class Utilities
    {
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
                string text = self.Name + " is Paralized and can't move!";
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
                text = self.Name + " is Frozen and can't move!";
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
            self.nvDur--;
            if (self.nvDur == 0)
            {
                text = self.Name + " woke up!";
                self.status_A = nonVolitileStatusEffects.none;
            }
            else
            {
                text = self.Name + " is fast asleep and can't move!";
                isAffected = true;
            }
            return new nonVolitleMove(text, isAffected);
        }

        /// <summary>
        /// This takes in a list and generates a random list of unique integers based off that number
        /// </summary>
        /// <param name="attackMoves"></param>
        /// <returns></returns>
        public static List<int> generateRandomList(string name, int maxPossibility, int length)
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
            //Debug.Log(string.Format("RND Numbers: Name: {0} Max: {1} List: {2} {3} {4} {5}",name, maxPossibility,rndNumberList[0], rndNumberList[1], rndNumberList[2], rndNumberList[3]));
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