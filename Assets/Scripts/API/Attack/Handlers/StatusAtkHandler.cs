using UnityEngine;

using Base;

namespace Attack
{
    public class StatusAtkHandler : StatusAtkMethods, IAttackHandler
    {
        public PokemonBase target { get; set; }
        public PokemonBase self { get; set; }

        public void setPokemon(PokemonBase tar, PokemonBase s, ref MoveResults mr)
        {
            target = tar;
            self = s;
            moveRes = mr;

            damage = 0;
            heal = 0;
            recoil = 0;

            stageName = "";
            stageDiff = 0;
            stagePokemon = "";

            s.nextAttack = "";

            ignoreReflect = ignoreLightScreen = false;
        }

        public StatusAtkHandler(PokemonBase tar, PokemonBase s, ref MoveResults mr)
        {
            setPokemon(tar, s, ref mr);
        }

        public move_DmgReport result(string name)
        {
            string tempname = name.ToLower();
            switch (tempname)
            {
                default:
                    Debug.Log("No status move with name " + name + " found");
                    break;

                //raises users defense by 2 stagesage
                case "acid armor":
                    acidArmor(self);
                    break;

                //raises users speed by 2 stages
                case "agility":
                    agility(self);
                    break;

                //raises users spDefense by 2 stages
                case "amnesia":
                    amnesia(self);
                    break;

                //raises users defense by 2 stages
                case "barrier":
                    barrier(self);
                    break;

                //confuses opponenet
                case "confuse ray":
                    confuseRay(target);
                    break;

                //chages users type of its first move
                case "conversion":
                    conversion(self);
                    break;

                //raises uers defense by 1 stage
                case "defense curl":
                    defenseCurl(self);
                    break;

                //disables enemies last move for a few turns
                case "disable":
                    disable(self, target, tempname);
                    break;
                //raises user evasive stage by one
                case "double team":
                    doubleTeam(self);
                    break;

                //lowers opponents accuracy by 1 stage
                case "flash":
                    flash(target);
                    break;

                //increases crit ratio...
                case "focus energy":
                    focusEnergy(self);
                    break;

                case "growl":
                    growl(target);
                    break;

                case "growth":
                    growth(self);
                    break;

                case "harden":
                    harden(self);
                    break;

                case "haze":
                    haze(self, target);
                    break;

                case "hypnosis":
                    hypnosis(target);
                    break;

                //lower enemy accuracy by 1 stage
                case "kinesis":
                    kinesis(target);
                    break;

                case "leech seed":
                    leechSeed(target);
                    break;

                case "leer":
                    leer(target);
                    break;

                case "light screen":
                    lightScreen(self);
                    break;

                case "lovely kiss":
                    lovelyKiss(target);
                    break;

                case "meditate":
                    meditate(self);
                    break;

                //preforms any move in the game at random?
                case "metronome":
                    metronome(self, target);
                    break;

                //copies the opponents last move and replaces mimic with that
                case "mimic":
                    mimic(self);
                    break;

                //raise evasion by 1 stage STOMP and STEAMROLLER do double damage against a minimized opponent
                case "minimize":
                    minimize(self);
                    break;

                //preforms the opponents last move....
                case "mirror move":
                    mirrorMove(self, target);
                    break;

                //no negative stat changes to self or allies for 5 turns
                case "mist":
                    mist(self);
                    break;

                case "poison gas":
                    poisonGas(target);
                    break;

                case "poison powder":
                    poisonPowder(target);
                    break;

                case "recover":
                    recover(self);
                    break;

                //halves the damage from physical attacks for 5 turns
                case "reflect":
                    reflect(self);
                    break;

                //user falls asleep for 2 turns but health is fully recovered
                case "rest":
                    rest(self, 2);
                    break;

                //opponent switches pokemon out
                case "roar":
                    roar(target);
                    break;

                //lowers opponent accuracy by one stage
                case "sand attack":
                    sandAttack(target);
                    break;

                case "screech":
                    screech(target);
                    break;

                case "sharpen":
                    sharpen(self);
                    break;

                //puts the user to sleep for 1-3 turns
                case "sing":
                    sing(target);
                    break;

                //lower accuracy by one stage
                case "smokescreen":
                    smokeScreen(target);
                    break;

                case "soft boiled":
                    softBoiled(self);
                    break;

                //This does nothing
                case "splash":
                    splash();
                    break;

                //puts the opponent to sleep for 1-3 turns
                case "spore":
                    spore(target);
                    break;

                case "string shot":
                    stringShot(target);
                    break;

                case "stun spore":
                    stunSpore(target);
                    break;

                case "substitute":
                    substitute(self);
                    break;

                case "supersonic":
                    supersonic(target);
                    break;

                case "swords dance":
                    swordsDance(self);
                    break;

                case "tail whip":
                    tailWhip(target);
                    break;

                case "teleport":
                    teleport();
                    break;

                case "thunder wave":
                    thunderWave(target);
                    break;

                //increasingly does more toxic damage at the end of each turn, starts at 1/16
                case "toxic":
                    toxic(self, target);
                    break;

                //takes the attacks of the opponent
                case "transform":
                    transform(self, target);
                    break;

                //blows the opponent away if they are a lower level
                case "whirlwind":
                    whirlwind(target);
                    break;

                case "withdraw":
                    withdraw(self);
                    break;
            }
            Debug.Log(string.Format("dmg {0} heal {1} recoil {2} stageName {3} stageDiff {4} hit {5}", damage, heal, recoil, stageName, stageDiff, moveRes.hit.sucess));

            ignoreLightScreen = false;
            ignoreReflect = false;
            move_DmgReport report = new move_DmgReport(damage, heal, recoil, stageName, stageDiff, stagePokemon);
            return report;
        }
    }
}