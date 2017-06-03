using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBG.Data;
using FBG.Base;

namespace FBG.Attack
{
    public class SpecialAtkHandler : SpecialAtkMethods, IAttackHandler
    {
        public PokemonBase target { get; set; }
        public PokemonBase self { get; set; }

        public SpecialAtkHandler(PokemonBase tar, PokemonBase s, ref MoveResults mr)
        {
            setPokemon(tar, s, ref mr);
        }

        public void setPokemon(PokemonBase tar, PokemonBase s, ref  MoveResults mr)
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

        public move_DmgReport result(string name, float baseDamage)
        {
            damage = baseDamage;
            string tempname = name.ToLower();
            switch (tempname)
            {
                default:
                    Debug.Log("No special attack with name " + name + " found");
                    break;

                case "absorb":
                    absorb();
                    break;

                case "acid":
                    acid(target);
                    break;

                case "aurora beam":
                    auroraBeam(target);
                    break;

                case "blizzard":
                    blizzard(target);
                    break;

                case "bubble":
                    bubble(target);
                    break;

                case "bubble beam":
                    bubbleBeam(target);
                    break;

                case "confusion":
                    confusion(target);
                    break;

                case "dragon rage":
                    dragonRage();
                    break;

                case "dream eater":
                    dreamEater(target);
                    break;

                case "ember":
                    ember(target);
                    break;

                case "fire blast":
                    fireBlast(target);
                    break;

                case "fire spin":           //Damages the target for 4-5 turns
                    fireSpin(target);
                    break;

                case "flamethrower":
                    flameThrower(target);
                    break;

                case "gust":
                    gust(target);
                    break;

                case "hydro pump":          //no additional effect
                    hydroPump();
                    break;

                case "hyper beam":          //cannot move next turn
                    hyperBeam(self, tempname, baseDamage);
                    break;

                case "ice beam":
                    iceBeam(target);
                    break;

                case "mega drain":
                    megaDrain();
                    break;

                case "night shade":
                    nightShade(target);
                    break;

                case "petal dance":         //attacks for 2-3 turns, cannot be switched out, then becomes confused
                    petalDance(tempname, self);
                    break;

                case "psybeam":
                    psybeam(target);
                    break;

                case "psychic":
                    psychic(target);
                    break;

                case "psywave":
                    psywave(target);
                    break;

                case "razor wind":          //charges the first turn then attacks the second
                    razorwind(self, tempname, baseDamage);
                    break;

                case "sludge":              //30% chance to poison the target
                    sludge(target);
                    break;

                case "smog":                //40% chance to poison the target
                    smog(target);
                    break;

                case "solar beam":          //charges on the fist turn, hits on the second
                    solarbeam(self, tempname, baseDamage);
                    break;

                case "sonic boom":
                    sonicBoom(target);
                    break;

                case "surf":                //does double damage if the pokemon used dive(introduced in gen3)
                    surf(target);
                    break;

                case "swift":               //ignores evasiveness and accuracy
                    swift();
                    break;

                case "thunder":
                    thunder(target);
                    break;

                case "thunder shock":
                    thunderShock(target);
                    break;

                case "thunderbolt":
                    thunderBolt(target);
                    break;

                case "tri attack":          //6.67% chance for each
                    triAttack(target);
                    break;

                case "water gun":           //no additional effect
                    waterGun();
                    break;

            }
            //Check for lightscreen to halve special attack damage

            Debug.Log(string.Format(" dmg {0} heal {1} recoil {2} stageName {3} stageDiff {4} hit {5}", damage, heal, recoil, stageName, stageDiff, moveRes.hit.sucess));

            if (target.team.hasLightScreen && !ignoreLightScreen)
            {
                Debug.Log("halving damage because of light screen");
                damage /= 2f;
            }

            ignoreLightScreen = false;
            ignoreReflect = false;
            move_DmgReport report = new move_DmgReport(damage, heal, recoil, stageName, stageDiff, stagePokemon);
            return report;
        }

    }
}
