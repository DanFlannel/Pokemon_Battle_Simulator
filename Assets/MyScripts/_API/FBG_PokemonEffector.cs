using UnityEngine;
using System.Collections;
using System;

namespace FatBobbyGaming
{
    public interface FBG_PokemonEffector
    {
        string name { get; set; }
        int duration { get; set; }
        FBG_Pokemon target { get; set; }

        void turnEffect();

        void endEffect();
    }

    public class repeatAttack_Confused : FBG_PokemonEffector
    {
        public string name { get; set; }
        public int duration { get; set; }
        public FBG_Pokemon target { get; set; }


        public repeatAttack_Confused(string s, int dur, FBG_Pokemon tar)
        {
            name = s;
            duration = dur;
            target = tar;
        }

        public void endEffect()
        {
            int rnd = UnityEngine.Random.Range(4, 5);
            FBG_Atk_Methods.isConfused(target, 100, rnd);
        }

        public void turnEffect()
        {
            target.team.addBind(1, 0);
            target.nextAttack = name;
            duration--;
        }
    }

    public class rage : FBG_PokemonEffector
    {
        public int duration { get; set; }
        public string name { get; set; }
        public FBG_Pokemon target { get; set; }

        public rage(string s, int dur, FBG_Pokemon tar)
        {
            name = s;
            duration = dur;
            target = tar;
        }

        public void endEffect() { }

        public void turnEffect()
        {
            //if we are hit by a direct contact our attack goes up by one stat
            //how do I check if the last attack was a direct contact one?
            //Move battle history -> get move name -> check to see if it was a direct contact

            string prevAttack = FBG_BattleSimulator.moveHistory[FBG_BattleSimulator.moveHistory.Count].attackName;
            AttackJsonData atk = FBG_JsonAttack.getAttack(FBG_BattleSimulator.attackDex, prevAttack);
            bool contact = FBG_JsonAttack.checkFlags("contact", atk);

            if (contact)
            {
                FBG_Atk_Methods.changeStats(FBG_consts.attack, 1, target);
            }
            duration--;
        }
    }

    public class disable : FBG_PokemonEffector
    {
        public int duration { get; set; }
        public string name { get; set; }
        public FBG_Pokemon target { get; set; }
        public string tMove;
        private int atkIndex;

        public disable(string s, int dur, FBG_Pokemon tar, string atkName)
        {
            name = s;
            duration = dur;
            target = tar;
            tMove = atkName;
        }

        public void endEffect()
        {
            target.atkMoves[atkIndex] = tMove;
            return;
        }

        public void turnEffect()
        {
            //we disable the last move used by the target so... 
            //we find the index of the move and we disable that button?
            //we can't diable their only move (struggle) ect...
            if(target.atkMoves.Count == 1)
            {
                return;
            }

            for(int i = 0; i < target.atkMoves.Count; i++)
            {
                if(tMove == target.atkMoves[i])
                {
                    atkIndex = i;
                    //going to have to work some AI around this one huh
                    target.atkMoves[i] = "";
                }
            }
        }
    }
}
