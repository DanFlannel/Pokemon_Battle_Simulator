using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FBG.Data
{
    public class DamageMultipliers
    {
        #region List of various arrays of damage multipliers
        private static dmgMult grass;
        private static dmgMult fire;
        private static dmgMult water;
        private static dmgMult dragon;
        private static dmgMult fighting;
        private static dmgMult bug;
        private static dmgMult normal;
        private static dmgMult electric;
        private static dmgMult ground;
        private static dmgMult poison;
        private static dmgMult psychic;
        private static dmgMult fairy;
        private static dmgMult dark;
        private static dmgMult flying;
        private static dmgMult ghost;
        private static dmgMult ice;
        private static dmgMult rock;
        private static dmgMult steel;
        #endregion

        public static void Init()
        {
            fire = new dmgMult(
                /*Normal*/  1f,
                /*Fighting*/1f,
                /*Flying*/1f,
                /*Poison*/1f,
                /*Ground*/2f,
                /*Rock*/2f,
                /*Bug*/.5f,
                /*Ghost*/1f,
                /*Steel*/.5f,
                /*Fire*/ .5f,
                /*Water*/2f,
                /*Grass*/.5f,
                /*Electric*/1f,
                /*Psychic*/1f,
                /*Ice*/.5f,
                /*Dragon*/ 1f,
                /*Dark*/1f,
                /*Fairy*/ .5f);

            water = new dmgMult(
                /*Normal*/  1f,
                /*Fighting*/1f,
                /*Flying*/1f,
                /*Poison*/1f,
                /*Ground*/1f,
                /*Rock*/1f,
                /*Bug*/1f,
                /*Ghost*/1f,
                /*Steel*/.5f,
                /*Fire*/ .5f,
                /*Water*/ .5f,
                /*Grass*/ 2f,
                /*Electric*/2f,
                /*Psychic*/ 1f,
                /*Ice*/.5f,
                /*Dragon*/1f,
                /*Dark*/1f,
                /*Fairy*/1f);

            bug = new dmgMult(
                /*Normal*/  1f,
                /*Fighting*/.5f,
                /*Flying*/2f,
                /*Poison*/1f,
                /*Ground*/.5f,
                /*Rock*/2f,
                /*Bug*/1f,
                /*Ghost*/1f,
                /*Steel*/1f,
                /*Fire*/ 2f,
                /*Water*/ 1f,
                /*Grass*/ .5f,
                /*Electric*/ 1f,
                /*Psychic*/ 1f,
                /*Ice*/ 1f,
                /*Dragon*/1f,
                /*Dark*/1f,
                /*Fairy*/1f);

            normal = new dmgMult(
                /*Normal*/  1f,
                /*Fighting*/2f,
                /*Flying*/1f,
                /*Poison*/1f,
                /*Ground*/1f,
                /*Rock*/1f,
                /*Bug*/1f,
                /*Ghost*/0f,
                /*Steel*/ 1f,
                /*Fire*/ 1f,
                /*Water*/ 1f,
                /*Grass*/ 1f,
                /*Electric*/ 1f,
                /*Psychic*/ 1f,
                /*Ice*/1f,
                /*Dragon*/ 1f,
                /*Dark*/1f,
                /*Fairy*/1f);

            poison = new dmgMult(
                /*Normal*/  1f,
                /*Fighting*/.5f,
                /*Flying*/1f,
                /*Poison*/.5f,
                /*Ground*/2f,
                /*Rock*/1f,
                /*Bug*/.5f,
                /*Ghost*/1f,
                /*Steel*/1f,
                /*Fire*/1f,
                /*Water*/1f,
                /*Grass*/ .5f,
                /*Electric*/ 1f,
                /*Psychic*/ 2f,
                /*Ice*/1f,
                /*Dragon*/1f,
                /*Dark*/ 1f,
                /*Fairy*/ .5f);

            electric = new dmgMult(
                /*Normal*/  1f,
                /*Fighting*/1f,
                /*Flying*/.5f,
                /*Poison*/1f,
                /*Ground*/2f,
                /*Rock*/1f,
                /*Bug*/1f,
                /*Ghost*/1f,
                /*Steel*/.5f,
                /*Fire*/1f,
                /*Water*/ 1f,
                /*Grass*/ 1f,
                /*Electric*/ .5f,
                /*Psychic*/ 1f,
                /*Ice*/ 1f,
                /*Dragon*/1f,
                /*Dark*/ 1f,
                /*Fairy*/ 1f);

            ground = new dmgMult(
                /*Normal*/  1f,
                /*Fighting*/1f,
                /*Flying*/1f,
                /*Poison*/.5f,
                /*Ground*/1f,
                /*Rock*/.5f,
                /*Bug*/1f,
                /*Ghost*/1f,
                /*Steel*/ 1f,
                /*Fire*/1f,
                /*Water*/2f,
                /*Grass*/ 2f,
                /*Electric*/ 0f,
                /*Psychic*/ 1f,
                /*Ice*/2f,
                /*Dragon*/1f,
                /*Dark*/ 1f,
                /*Fairy*/ 1f);

            fairy = new dmgMult(
                /*Normal*/  1f,
                /*Fighting*/.5f,
                /*Flying*/1f,
                /*Poison*/2f,
                /*Ground*/1f,
                /*Rock*/1f,
                /*Bug*/.5f,
                /*Ghost*/1f,
                /*Steel*/ 2f,
                /*Fire*/ 1f,
                /*Water*/1f,
                /*Grass*/ 1f,
                /*Electric*/ 1f,
                /*Psychic*/ 1f,
                /*Ice*/ 1f,
                /*Dragon*/ 0f,
                /*Dark*/ .5f,
                /*Fairy*/ 1f);

            fighting = new dmgMult(
                /*Normal*/1f,
                /*Fighting*/1f,
                /*Flying*/2f,
                /*Poison*/1f,
                /*Ground*/1f,
                /*Rock*/.5f,
                /*Bug*/.5f,
                /*Ghost*/1f,
                /*Steel*/1f,
                /*Fire*/ 1f,
                /*Water*/ 1f,
                /*Grass*/ 1f,
                /*Electric*/ 1f,
                /*Psychic*/ 2f,
                /*Ice*/ 1f,
                /*Dragon*/1f,
                /*Dark*/.5f,
                /*Fairy*/2f);

            psychic = new dmgMult(
                /*Normal*/1f,
                /*Fighting*/.5f,
                /*Flying*/1f,
                /*Poison*/1f,
                /*Ground*/1f,
                /*Rock*/1f,
                /*Bug*/2f,
                /*Ghost*/2f,
                /*Steel*/1f,
                /*Fire*/1f,
                /*Water*/1f,
                /*Grass*/ 1f,
                /*Electric*/ 1f,
                /*Psychic*/ .5f,
                /*Ice*/ 1f,
                /*Dragon*/1f,
                /*Dark*/ 2f,
                /*Fairy*/1f);

            grass = new dmgMult(
                /*Normal*/1f,
                /*Fighting*/1f,
                /*Flying*/2f,
                /*Poison*/2f,
                /*Ground*/.5f,
                /*Rock*/1f,
                /*Bug*/2f,
                /*Ghost*/1f,
                /*Steel*/1f,
                /*Fire*/2f,
                /*Water*/ .5f,
                /*Grass*/ .5f,
                /*Electric*/ .5f,
                /*Psychic*/ 1f,
                /*Ice*/2f,
                /*Dragon*/1f,
                /*Dark*/1f,
                /*Fairy*/ 1f);

            dragon = new dmgMult(
                /*Normal*/1f,
                /*Fighting*/1f,
                /*Flying*/1f,
                /*Poison*/1f,
                /*Ground*/1f,
                /*Rock*/1f,
                /*Bug*/1f,
                /*Ghost*/1f,
                /*Steel*/1f,
                /*Fire*/.5f,
                /*Water*/.5f,
                /*Grass*/ .5f,
                /*Electric*/ .5f,
                /*Psychic*/1f,
                /*Ice*/2f,
                /*Dragon*/ 2f,
                /*Dark*/ 1f,
                /*Fairy*/ 2f);

            dark = new dmgMult(
                /*Normal*/ 1f,
                /*Fighting*/ 2f,
                /*Flying*/ 1f,
                /*Poison*/ 1f,
                /*Ground*/ 1f,
                /*Rock*/ 1f,
                /*Bug*/ 2f,
                /*Ghost*/ .5f,
                /*Steel*/ 1f,
                /*Fire*/ 1f,
                /*Water*/ 1f,
                /*Grass*/ 1f,
                /*Electric*/ 1f,
                /*Psychic*/ 0f,
                /*Ice*/ 1f,
                /*Dragon*/ 1f,
                /*Dark*/ .5f,
                /*Fairy*/ 2f);

            flying = new dmgMult(
                /*Normal*/ 1f,
                /*Fighting*/ .5f,
                /*Flying*/ 1f,
                /*Poison*/ 1f,
                /*Ground*/ 0f,
                /*Rock*/ 2f,
                /*Bug*/ .5f,
                /*Ghost*/ 1f,
                /*Steel*/ 1f,
                /*Fire*/ 1f,
                /*Water*/ 1f,
                /*Grass*/ .5f,
                /*Electric*/ 2f,
                /*Psychic*/ 1f,
                /*Ice*/ 2f,
                /*Dragon*/ 1f,
                /*Dark*/ 1f,
                /*Fairy*/ 1f);

            ghost = new dmgMult(
                /*Normal*/ 0f,
                /*Fighting*/ 0f,
                /*Flying*/ 1f,
                /*Poison*/ .5f,
                /*Ground*/ 1f,
                /*Rock*/ 1f,
                /*Bug*/ .5f,
                /*Ghost*/ 2f,
                /*Steel*/ 1f,
                /*Fire*/ 1f,
                /*Water*/ 1f,
                /*Grass*/ 1f,
                /*Electric*/ 1f,
                /*Psychic*/ 1f,
                /*Ice*/ 1f,
                /*Dragon*/ 1f,
                /*Dark*/ 2f,
                /*Fairy*/ 1f);

            ice = new dmgMult(
                /*Normal*/ 1f,
                /*Fighting*/ 2f,
                /*Flying*/ 1f,
                /*Poison*/ 1f,
                /*Ground*/ 1f,
                /*Rock*/ 2f,
                /*Bug*/ 1f,
                /*Ghost*/ 1f,
                /*Steel*/ 2f,
                /*Fire*/ 2f,
                /*Water*/ 1f,
                /*Grass*/ 1f,
                /*Electric*/ 1f,
                /*Psychic*/ 1f,
                /*Ice*/ .5f,
                /*Dragon*/ 1f,
                /*Dark*/ 1f,
                /*Fairy*/ 1f);

            rock = new dmgMult(
                /*Normal*/ .5f,
                /*Fighting*/ 2f,
                /*Flying*/ .5f,
                /*Poison*/ .5f,
                /*Ground*/ 2f,
                /*Rock*/ 1f,
                /*Bug*/ 1f,
                /*Ghost*/ 1f,
                /*Steel*/ 2f,
                /*Fire*/ .5f,
                /*Water*/ 2f,
                /*Grass*/ 2f,
                /*Electric*/ 1f,
                /*Psychic*/ 1f,
                /*Ice*/ 1f,
                /*Dragon*/ 1f,
                /*Dark*/ 1f,
                /*Fairy*/ 1f);

            steel = new dmgMult(
                /*Normal*/ .5f,
                /*Fighting*/ 2f,
                /*Flying*/ .5f,
                /*Poison*/ 0f,
                /*Ground*/ 2f,
                /*Rock*/ .5f,
                /*Bug*/ .5f,
                /*Ghost*/ 1f,
                /*Steel*/ .5f,
                /*Fire*/ 2f,
                /*Water*/ 1f,
                /*Grass*/ .5f,
                /*Electric*/ 1f,
                /*Psychic*/ .5f,
                /*Ice*/ .5f,
                /*Dragon*/ .5f,
                /*Dark*/ 1f,
                /*Fairy*/ .5f);
        }

        public static dmgMult createMultiplier(string[] types)
        {
            string t1 = types[0];
            dmgMult type1 = getSingleType(t1);

            if (t1 == null)
            {
                //Debug.LogError("No types passed into damage multiplier");
                return type1;
            }

            if (types.Length == 1)
            {
                return type1;
            }
            else
            {
                string t2 = types[1];
                dmgMult type2 = getSingleType(t2);
                return multiplyTypes(type1, type2);
            }
        }

        private static dmgMult multiplyTypes(dmgMult t1, dmgMult t2)
        {
            dmgMult final = new dmgMult(
                t1.normal * t2.normal,
                t1.fighting * t2.fighting,
                t1.flying * t2.flying,
                t1.poison * t2.poison,
                t1.ground * t2.ground,
                t1.rock * t2.rock,
                t1.bug * t2.bug,
                t1.ghost * t2.ghost,
                t1.steel * t2.steel,
                t1.fire * t2.fire,
                t1.water * t2.water,
                t1.grass * t2.grass,
                t1.electric * t2.electric,
                t1.psychic * t2.psychic,
                t1.ice * t2.ice,
                t1.dragon * t2.dragon,
                t1.dark * t2.dark,
                t1.fairy * t2.fairy
                );

            return final;
        }

        private static dmgMult getSingleType(string t)
        {
            dmgMult type;
            switch (t)
            {
                case Consts.Bug:
                    type = bug;
                    break;
                case Consts.Dark:
                    type = dark;
                    break;

                case Consts.Dragon:
                    type = dragon;
                    break;

                case Consts.Electric:
                    type = electric;
                    break;

                case Consts.Fairy:
                    type = fairy;
                    break;

                case Consts.Fighting:
                    type = fighting;
                    break;

                case Consts.Fire:
                    type = fire;
                    break;

                case Consts.Flying:
                    type = flying;
                    break;

                case Consts.Ghost:
                    type = ghost;
                    break;

                case Consts.Grass:
                    type = grass;
                    break;

                case Consts.Ground:
                    type = ground;
                    break;

                case Consts.Ice:
                    type = ice;
                    break;

                case Consts.Normal:
                    type = normal;
                    break;
                case Consts.Poison:
                    type = poison;
                    break;

                case Consts.Psychic:
                    type = psychic;
                    break;

                case Consts.Rock:
                    type = rock;
                    break;

                case Consts.Steel:
                    type = steel;
                    break;

                case Consts.Water:
                    type = water;
                    break;

                default:
                    //Debug.LogError("No Type found with name: " + t);
                    type = normal;
                    break;
            }
            return type;
        }
    }
}
