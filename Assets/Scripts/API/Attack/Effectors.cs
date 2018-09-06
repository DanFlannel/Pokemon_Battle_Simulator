using FBG.Base;
using FBG.Data;
using FBG.Battle;

namespace FBG.Attack
{
    public class Effectors { }

    public interface IEffector
    {
        string name { get; set; }
        int duration { get; set; }
        PokemonBase target { get; set; }

        void turnEffect();

        void endEffect();
    }

    public class repeatAttack_Confused : BaseMoves, IEffector
    {
        public string name { get; set; }
        public int duration { get; set; }
        public PokemonBase target { get; set; }

        public repeatAttack_Confused(string s, int dur, PokemonBase tar)
        {
            name = s;
            duration = dur;
            target = tar;
        }

        public void endEffect()
        {
            int rnd = UnityEngine.Random.Range(4, 5);
            isConfused(target, 100, rnd);
        }

        public void turnEffect()
        {
            target.team.addBind(1, 0, name);
            target.nextAttack = name;
            duration--;
        }
    }

    public class rage : BaseMoves, IEffector
    {
        public int duration { get; set; }
        public string name { get; set; }
        public PokemonBase target { get; set; }

        public rage(string s, int dur, PokemonBase tar)
        {
            name = s;
            duration = dur;
            target = tar;
        }

        public void endEffect()
        {
        }

        public void turnEffect()
        {
            //if we are hit by a direct contact our attack goes up by one stat
            //Move battle history -> get move name -> check to see if it was a direct contact

            string prevAttack = BattleSimulator.Instance.moveHistory[BattleSimulator.Instance.moveHistory.Count - 1].attackName;
            bool contact = DexHolder.attackDex.checkFlag(prevAttack, "contact");

            if (contact)
            {
                changeStats(Consts.attack, 1, target);
                //add a display text routine here
            }
            duration--;
        }
    }

    public class disable : IEffector
    {
        public int duration { get; set; }
        public string name { get; set; }
        public PokemonBase target { get; set; }
        public string tMove;
        private int atkIndex;

        public disable(string s, int dur, PokemonBase tar, string atkName)
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
            if (target.atkMoves.Count == 1)
            {
                return;
            }

            for (int i = 0; i < target.atkMoves.Count; i++)
            {
                if (tMove == target.atkMoves[i])
                {
                    atkIndex = i;
                    //going to have to work some AI around this one huh
                    target.atkMoves[i] = "";
                }
            }
        }
    }
}