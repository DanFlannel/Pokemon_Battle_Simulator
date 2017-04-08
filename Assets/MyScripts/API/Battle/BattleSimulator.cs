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

        public List<battleHistory> moveHistory = new List<battleHistory>();

        public BattleGUI battleGUI;

        public GifRenderer redSprite;
        public GifRenderer blueSprite;

        public bool isRedMoveCalculated;
        public bool isBlueMoveCalculated;

        private int redMoveIndex;
        private int blueMoveIndex;

        private bool isRedSwapping;
        private bool isBlueSwapping;

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

            isRedSwapping = false;
            isBlueSwapping = false;

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
            battleGUI.setSimulator(ref instance);

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

        public bool redTeamAttack(int index)
        {
            redMoveIndex = index;
            if(redTeam.curPokemon.curPP[index] < 0)
            {
                return false;
            }
            isRedMoveCalculated = true;
            return true;
        }

        //this is the random AI attack
        public void blueTeamAttack()
        {
            blueMoveIndex = getRndMoveIndex();
            isBlueMoveCalculated = true;
        }

        private int getRndMoveIndex()
        {
            int len = blueTeam.pokemon[blueIndex].atkMoves.Count;
            int rnd = Random.Range(0, len);
            string atkName = blueTeam.pokemon[blueIndex].atkMoves[rnd];
            if (blueTeam.curPokemon.curPP[rnd] <= 0)
            {
                atkName = "";
            }

            while (atkName == "")
            {
                rnd = Random.Range(0, len);
                if (blueTeam.curPokemon.curPP[rnd] > 0)
                {
                    atkName = blueTeam.curPokemon.atkMoves[rnd];
                }
            }
            return rnd;
        }

        //TODO add things to the coroutine queue
        private void turnController()
        {
            if(isRedMoveCalculated && isBlueMoveCalculated)
            {


                UnityEngine.Debug.Log(string.Format("red team index: {0} swap {1} blue team index {2} swap {3}", redMoveIndex, isRedSwapping, blueMoveIndex, isBlueSwapping));

                //if the red team goes first...
                if (redTeam.curPokemon.Speed >= blueTeam.curPokemon.Speed)
                {
                    redTeam.takeTurn(redMoveIndex, isRedSwapping);
                    blueTeam.takeTurn(blueMoveIndex, isBlueSwapping);
                }
                //if the blue team goes first...
                else
                {
                    blueTeam.takeTurn(redMoveIndex, isRedSwapping);
                    redTeam.takeTurn(blueMoveIndex, isBlueSwapping);
                }

                resetTurn();
            }
        }

        private void resetTurn()
        {
            isRedMoveCalculated = false;
            isBlueMoveCalculated = false;
            isRedSwapping = false;
            isBlueSwapping = false;
            blueIndex = blueTeam.curIndex;
            redIndex = redTeam.curIndex;
        }

        public void addMoveHistory(MoveResults res, PokemonBase attacker)
        {
            battleHistory hist = new battleHistory(attacker.Name, res);
            moveHistory.Add(hist);
        }

        public void changeSprites(TeamPokemon team)
        {
            GifRenderer r = redSprite;
            if(team == blueTeam)
            {
                r = blueSprite;
            }

            battleGUI.changePokemonSprite(r, team.curPokemon.Name, team.curPokemon.ID);
        }

        public void swapPokemon(TeamPokemon team, int index)
        {
            if (team == redTeam)
            {
                print("red team is swapping");
                redMoveIndex = index;
                isRedSwapping = true;
                isRedMoveCalculated = true;
                blueTeamAttack();
            }
            else
            {
                blueMoveIndex = index;
                isBlueSwapping = true;
                isBlueMoveCalculated = true;
            }
        }
    }
}
