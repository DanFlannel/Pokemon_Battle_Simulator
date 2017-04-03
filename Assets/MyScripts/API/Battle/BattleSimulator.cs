using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using FBG.Base;
using FBG.Attack;
using FBG.JSON;
using FBG.Data;

namespace FBG.Battle
{
    public class BattleSimulator : MonoBehaviour
    {
        private static BattleSimulator instance = null;
        public static BattleSimulator Instance { get { return instance; } }

        public int teamSize = 1;
        public int redIndex;
        public int blueIndex;

        public TeamPokemon redTeam;
        public TeamPokemon blueTeam;

        public MoveResults redResult;
        public MoveResults blueResult;
        public List<battleHistory> moveHistory = new List<battleHistory>();

        public BattleGUI battleGUI;

        public GifRenderer redSprite;
        public GifRenderer blueSprite;

        public bool isRedMoveCalculated;
        public bool isBlueMoveCalculated;

        private int redAttackIndex;
        private int blueAttackIndex;

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
        }

        // Use this for initialization
        private void Start()
        {
            isRedMoveCalculated = false;
            isBlueMoveCalculated = false;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            redTeam = new TeamPokemon(teamSize);
            blueTeam = new TeamPokemon(teamSize);

            redTeam.assignEnemyTeam(ref blueTeam);
            blueTeam.assignEnemyTeam(ref redTeam);

            redTeam.generateRandomTeam();
            blueTeam.generateRandomTeam();

            //get the enviornment ready
            BattleEnviornment.init();

            battleGUI = this.GetComponent<BattleGUI>();
            battleGUI.checkButtonNames(redTeam.curPokemon);

            //UnityEngine.Debug.Log(string.Format("{0} {1}", redTeam.curPokemon.Name, redTeam.curPokemon.ID));
            battleGUI.changePokemonSprite(redSprite, redTeam.curPokemon.Name, redTeam.curPokemon.ID);
            battleGUI.changePokemonSprite(blueSprite, blueTeam.curPokemon.Name, blueTeam.curPokemon.ID);

            sw.Stop();
            print(string.Format("Time to load {0}ms", sw.ElapsedMilliseconds));
        }

        void Update()
        {
            capPokemonIndex();
            battleGUI.checkButtonNames(redTeam.curPokemon);
            //doTurn();
            turnController();
        }

        private void capPokemonIndex()
        {
            if(redIndex > teamSize - 1)
            {
                redIndex = teamSize - 1;
            }
            if(blueIndex > teamSize -1)
            {
                blueIndex = teamSize - 1;
            }

            if(redIndex < 0)
            {
                redIndex = 0;
            }
            if(blueIndex < 0)
            {
                blueIndex = 0;
            }
        }

        private void attackButtons(int index)
        {
            if(redTeam.curPokemon.atkMoves[index] == "")
            {
                return;
            }
            redTeam.getMoveResults(index);
            isRedMoveCalculated = true;
        }

        //this it the current button method
        public void redTeamAttack(int index)
        {
            redAttackIndex = index;
            isRedMoveCalculated = true;

            blueTeamAttack();
        }

        //this is the random AI attack
        private void blueTeamAttack()
        {
            blueAttackIndex = getRndMoveIndex();
            isBlueMoveCalculated = true;
        }

        private int getRndMoveIndex()
        {
            int len = blueTeam.pokemon[blueIndex].atkMoves.Count;
            int rnd = Random.Range(0, len);
            string atkName = blueTeam.pokemon[blueIndex].atkMoves[rnd];

            while (atkName == "")
            {
                rnd = Random.Range(0, len);
                atkName = blueTeam.pokemon[blueIndex].atkMoves[rnd];
            }
            return rnd;
        }

        //THIS WILL BE THE NEW TURN CONTROLLER
        //The co-routines will be called from within the take turns
        //we have to split it up in case a pokemon swaps before the other team goes so we handle recalculating
        //also to prevent generating a queue of coroutines and having to clear it due to pokemon dying
        private void turnController()
        {
            if(isRedMoveCalculated && isBlueMoveCalculated)
            {
                isRedMoveCalculated =  false;
                isBlueMoveCalculated = false;

                //if the red team goes first...
                if (redTeam.curPokemon.Speed >= blueTeam.curPokemon.Speed)
                {
                    redTeam.takeTurn(redAttackIndex);
                    blueTeam.takeTurn(blueAttackIndex);
                }
                //if the blue team goes first...
                else
                {
                    blueTeam.takeTurn(redAttackIndex);
                    redTeam.takeTurn(blueAttackIndex);
                }
            }
        }

        public void addMoveHistory(MoveResults res, PokemonBase attacker)
        {
            battleHistory hist = new battleHistory(attacker.Name, res);
            moveHistory.Add(hist);
        }

        //this swaps the index??
        public void swapPokemon(TeamPokemon curTeam, int newIndex)
        {
            if (curTeam == blueTeam)
            {

            }
            else
            {

            }
        }
    }
}
