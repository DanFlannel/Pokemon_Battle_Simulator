using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBG.Base;
using FBG.Data;
using FBG.Battle;

namespace FBG.Attack
{
    public class StatusAtkHandler : BaseMoves, IAttackHandler
    {
        public PokemonBase target { get; set; }
        public PokemonBase self { get; set; }
        public MoveResults moveRes { get; set; }

        public float damage { get; set; }
        public float heal { get; set; }
        public float recoil { get; set; }
        public string stageName { get; set; }
        public int stageDiff { get; set; }

        public void setPokemon(PokemonBase tar, PokemonBase s, MoveResults mr)
        {
            target = tar;
            self = s;
            moveRes = mr;

            damage = 0;
            heal = 0;
            recoil = 0;
            stageName = "";
            stageDiff = 0;

            s.nextAttack = "";

            ignoreReflect = ignoreLightScreen = false;
        }

        public StatusAtkHandler(PokemonBase tar, PokemonBase s, MoveResults mr)
        {
            setPokemon(tar, s, mr);
        }

        public move_DmgReport result(string name)
        {
            string tempname = name.ToLower();
            int rnd;
            switch (tempname)
            {
                default:
                    Debug.Log("No status move with name " + name + " found");
                    break;

                //raises users defense by 2 stagesage
                case "acid armor":
                    changeStats(Consts.defense, 2, self);
                    stageName = Consts.defense;
                    stageDiff = 2;
                    break;

                //raises users speed by 2 stages
                case "agility":
                    changeStats(Consts.speed, 2, self);
                    stageName = Consts.speed;
                    stageDiff = 2;
                    break;

                //raises users spDefense by 2 stages
                case "amnesia":
                    changeStats(Consts.spDefense, 2, self);
                    stageName = Consts.spDefense;
                    stageDiff = 2;
                    break;

                //raises users defense by 2 stages
                case "barrier":
                    changeStats(Consts.defense, 2, self);
                    stageName = Consts.defense;
                    stageDiff = 2;
                    break;

                //confuses opponenet
                case "confuse ray":
                    rnd = Random.Range(1, 4);
                    isConfused(target, 10, rnd);
                    break;

                //chages users type of its first move
                case "conversion":
                    conversion(self);
                    break;

                //raises uers defense by 1 stage
                case "defense curl":
                    changeStats(Consts.defense, 1, self);
                    stageName = Consts.defense;
                    stageDiff = 1;
                    break;

                //disables enemies last move for a few turns
                case "disable":
                    if (self.Speed > target.Speed)
                    {
                        moveRes.hit = false;
                        break;
                    }
                    string moveName = BattleSimulator.Instance.moveHistory[BattleSimulator.Instance.moveHistory.Count].attackName;
                    disable dis = new disable(tempname, 4, target, moveName);

                    if (!hasEffector(target, tempname))
                    {
                        target.effectors.Add(dis);
                    }
                    break;
                //raises user evasive stage by one
                case "double team":
                    changeStats(Consts.evasion, 1, self);
                    stageName = Consts.evasion;
                    stageDiff = 1;
                    break;

                //lowers opponents accuracy by 1 stage
                case "flash":
                    changeStats(Consts.accuracy, -1, target);
                    stageName = Consts.accuracy;
                    stageDiff = -1;
                    break;

                //increases crit ratio...
                case "focus energy":
                    self.changeCritStage(2);
                    break;

                case "growl":
                    changeStats(Consts.attack, -1, target);
                    stageName = Consts.attack;
                    stageDiff = -1;
                    break;

                case "growth":
                    changeStats(Consts.spAttack, 1, self);
                    changeStats(Consts.attack, 1, self);
                    stageName = Consts.attack + " & " + Consts.spAttack;
                    stageDiff = 1;
                    break;

                case "harden":
                    changeStats(Consts.defense, 1, self);
                    stageName = Consts.defense;
                    stageDiff = 1;
                    break;

                case "haze":
                    self.resetStatStages();
                    target.resetStatStages();
                    stageName = "reset all stat changes";
                    stageDiff = 0;
                    break;

                case "hypnosis":
                    rnd = Random.Range(1, 3);
                    isSleep(target, 100, rnd);
                    break;

                //lower enemy accuracy by 1 stage
                case "kinesis":
                    changeStats(Consts.accuracy, -1, target);
                    stageName = Consts.accuracy;
                    stageDiff = -1;
                    break;

                case "leech seed":
                    self.team.hasLeechSeed = true;
                    break;

                case "leer":
                    changeStats(Consts.defense, -1, target);
                    stageName = Consts.defense;
                    stageDiff = -1;
                    break;

                case "light screen":
                    self.team.addLightScreen(5);
                    break;

                case "lovely kiss":
                    rnd = Random.Range(1, 3);
                    isSleep(target, 100, rnd);
                    break;

                case "meditate":
                    changeStats(Consts.attack, 1, self);
                    stageName = Consts.attack;
                    stageDiff = 1;
                    break;

                //preforms any move in the game at random?
                case "metronome":
                    rnd = Random.Range(0, MoveSets.attackList.Count);
                    string atkName = MoveSets.attackList[rnd].name;
                    AtkCalc.calculateAttackEffect(target, self, atkName);
                    break;

                //copies the opponents last move and replaces mimic with that
                case "mimic":
                    int n = 0;
                    int index = BattleSimulator.Instance.moveHistory.Count;
                    if(BattleSimulator.Instance.moveHistory.Count == 0)
                    {
                        moveRes.failed = true;
                        break;
                    }
                    string attack = BattleSimulator.Instance.moveHistory[index].attackName;
                    for (int i = 0; i < self.atkMoves.Count; i++)
                    {
                        if (self.atkMoves[i].ToLower() == "mimic")
                        {
                            n = i;
                            break;
                        }
                    }
                    self.atkMoves[n] = attack;
                    break;

                //raise evasion by 1 stage STOMP and STEAMROLLER do double damage against a minimized opponent
                case "minimize":
                    self.position = pokemonPosition.minimized;
                    changeStats(Consts.evasion, 1, self);
                    stageName = Consts.evasion;
                    stageDiff = 1;
                    break;

                //preforms the opponents last move....
                case "mirror move":
                    int mirrorMove = BattleSimulator.Instance.moveHistory.Count;
                    string mirrorAttack = BattleSimulator.Instance.moveHistory[mirrorMove].attackName;
                    AtkCalc.calculateAttackEffect(target, self, mirrorAttack);
                    break;

                //no negative stat changes to self or allies for 5 turns
                case "mist":
                    self.team.addMist();
                    break;

                case "poison gas":
                    isPosioned(target, 100);
                    break;

                case "poison powder":
                    isPosioned(target, 100);
                    break;

                case "recover":
                    heal = self.maxHP / 2f;
                    break;

                //halves the damage from physical attacks for 5 turns
                case "reflect":             
                    self.team.addReflect(5);
                    break;

                //user falls asleep for 2 turns but health is fully recovered
                case "rest":                
                    heal = self.maxHP;
                    isSleep(self, 100, 2);
                    break;

                //opponent switches pokemon out
                case "roar":
                    moveRes.failed = rndSwap(target);
                    break;

                //lowers opponent accuracy by one stage
                case "sand attack":         
                    changeStats(Consts.accuracy, -1, target);
                    stageName = Consts.accuracy;
                    stageDiff = -1;
                    break;

                case "screech":
                    changeStats(Consts.defense, -2, target);
                    stageName = Consts.defense;
                    stageDiff = -2;
                    break;

                case "sharpen":
                    changeStats(Consts.attack, 1, self);
                    stageName = Consts.attack;
                    stageDiff = 1;
                    break;

                //puts the user to sleep for 1-3 turns
                case "sing":                
                    rnd = Random.Range(1, 3);
                    isSleep(target, 100, rnd);
                    break;

                //lower accuracy by one stage
                case "smokescreen":         
                    changeStats(Consts.accuracy, -1, target);
                    stageName = Consts.accuracy;
                    stageDiff = -1;
                    break;

                case "soft boiled":
                    heal = self.maxHP / 2f;
                    break;

                //This does nothing
                case "splash":              
                    damage = 0;
                    break;

                //puts the opponent to sleep for 1-3 turns
                case "spore":               
                    rnd = UnityEngine.Random.Range(1, 3);
                    isSleep(target, 100, rnd);
                    break;

                case "string shot":
                    changeStats(Consts.speed, -2, target);
                    stageName = Consts.speed;
                    stageDiff = -2;
                    break;

                case "stun spore":
                    isParalized(target, 100);
                    break;

                case "substitute":
                    substitute(self, moveRes);
                    break;

                case "supersonic":
                    rnd = UnityEngine.Random.Range(1, 4);
                    isConfused(target, 100, rnd);
                    break;

                case "swords dance":
                    changeStats(Consts.attack, 2, self);
                    stageName = Consts.attack;
                    stageDiff = 2;
                    break;

                case "tail whip":
                    changeStats(Consts.defense, -1, target);
                    stageName = Consts.attack;
                    stageDiff = -1;
                    break;

                case "teleport":
                    //say something stupid here
                    //change the enviornment but not the weather
                    noAdditionalEffect();
                    break;

                case "thunder wave":
                    isParalized(target, 100);
                    break;

                //increasingly does more toxic damage at the end of each turn, starts at 1/16
                case "toxic":               
                    toxic(target);
                    break;

                //takes the attacks of the opponent
                case "transform":
                    self.atkMoves = target.atkMoves;
                    break;

                //blows the opponent away if they are a lower level
                case "whirlwind":
                    moveRes.failed = rndSwap(target);
                    break;

                case "withdraw":
                    changeStats(Consts.defense, 1, self);
                    stageName = Consts.defense;
                    stageDiff = 1;
                    break;

            }
            Debug.Log(string.Format("dmg {0} heal {1} recoil {2} stageName {3} stageDiff {4} hit {5}", damage, heal, recoil, stageName, stageDiff, moveRes.hit));

            ignoreLightScreen = false;
            ignoreReflect = false;
            move_DmgReport report = new move_DmgReport(damage, heal, recoil, stageName, stageDiff);
            return report;
        }

    }
}
