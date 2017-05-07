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

        public int teamSize = 6;
        public int redIndex;
        public int blueIndex;

        public TeamPokemon redTeam;
        public TeamPokemon blueTeam;

        public List<battleHistory> moveHistory = new List<battleHistory>();

        public BattleGUI battleGUI;

        public GifRenderer redSprite;
        public GifRenderer blueSprite;

        [HideInInspector]
        public bool isRedMoveCalculated;
        [HideInInspector]
        public bool isBlueMoveCalculated;

        [HideInInspector]
        public int redMoveIndex;
        [HideInInspector]
        public int blueMoveIndex;

        [HideInInspector]
        public bool isRedSwapping;
        [HideInInspector]
        public bool isBlueSwapping;

        public bool isTurnRunning;

        public BattleRoutines routine;

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
            routine = this.gameObject.AddComponent<BattleRoutines>();
            AI_RND_Battle(teamSize);
        }

        private void AI_RND_Battle(int size)
        {
            TeamPokemon t1 = new TeamPokemon(teamSize, "player", ref instance);
            TeamPokemon t2 = new TeamPokemon(teamSize, "AI", ref instance);
            setupTeamBattle(size, t1, t2);
        }

        private void setupTeamBattle(int size, TeamPokemon red, TeamPokemon blue)
        {
            isRedMoveCalculated = false;
            isBlueMoveCalculated = false;

            isRedSwapping = false;
            isBlueSwapping = false;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            redTeam = red;
            blueTeam = blue;

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
            //turnController();
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
            StartCoroutine(routine.takeTurn(instance));

            /*
            routine.queue.AddCoroutineToQueue(routine.testWait(3f));
            routine.queue.AddCoroutineToQueue(routine.testWait(3f));
            routine.queue.AddCoroutineToQueue(routine.testWait(3f));

            routine.queue.StartQueue();
            */

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
            if(isRedMoveCalculated)
            {
                ClearLog.ClearLogConsole();

                blueTeamAttack();
                //UnityEngine.Debug.Log(string.Format("red team index: {0} swap {1} blue team index {2} swap {3}", redMoveIndex, isRedSwapping, blueMoveIndex, isBlueSwapping));

                List<TurnInformation> info = new List<TurnInformation>();
                info.Add(new TurnInformation(redTeam.curPokemon, redMoveIndex, isRedSwapping));
                info.Add(new TurnInformation(blueTeam.curPokemon, blueMoveIndex, isBlueSwapping));

                TurnOrder turn = new TurnOrder(info);
                for(int i = 0; i < turn.order.Count; i++)
                {
                    PokemonBase pkmn = turn.order[i].pokemon;
                    pkmn.team.takeTurn(turn.order[i].moveIndex, turn.order[i].isSwapping, pkmn);
                }

                if (turn.swapped)
                {
                    info = turn.speedOnly(redTeam.curPokemon, blueTeam.curPokemon);
                }

                for(int i = 0; i < turn.speedDetermined.Count; i++)
                {
                    PokemonBase pkmn = turn.speedDetermined[i].pokemon;
                    pkmn.team.checkEffectors(pkmn);
                    pkmn.team.endOfTurnDamage(pkmn);
                }

                redTeam.EndOfTurn();
                blueTeam.EndOfTurn();

                for(int i = 0; i < turn.order.Count; i++)
                {
                    PokemonBase pkmn = turn.order[i].pokemon;
                    turn.order[i].pokemon.team.isAlive(pkmn);
                }
                resetTurn();
            }
        }

        private List<TurnInformation> determineOrder(params TurnInformation[] t)
        {
            List<TurnInformation> speed = new List<TurnInformation>();

            List<TurnInformation> priorities = new List<TurnInformation>();

            for(int i = 0; i < t.Length; i++)
            {
                if(t[i].priority > 0)
                {
                    priorities.Add(t[i]);
                }
                else
                {
                    speed.Add(t[i]);
                }
            }
            return priorities;
        }

        public void resetTurn()
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

        public void changeSprites(ref TeamPokemon team)
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
                redMoveIndex = index;
                isRedSwapping = true;
                isRedMoveCalculated = true;
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
