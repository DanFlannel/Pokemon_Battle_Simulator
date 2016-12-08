using UnityEngine;
using System.Collections;

namespace FatBobbyGaming
{
    public class FBG_PokemonTeam
    {
        public bool hasMist;
        public bool hasReflect;
        public bool hasLightScreen;
        public bool isBound;

        private int lightScreenDur;
        private int reflectDur;
        private int mistDur;

        private int bindDuration;
        private float bindDamage;


        public FBG_PokemonTeam()
        {
            init();
        }

        public void init()
        {
            hasMist = false;
            hasReflect = false;
            hasLightScreen = false;
            isBound = false;
        }

        public void addMist()
        {
            hasMist = true;
            mistDur = 5;
        }

        public void addLightScreen(int dur)
        {
            hasLightScreen = true;
            lightScreenDur = dur;
        }

        public void addReflect(int dur)
        {
            hasReflect = true;
            reflectDur = dur;
        }

        public void addBind(int dur, float dmg)
        {
            isBound = true;
            bindDuration = dur;
            bindDamage = dmg;
        }

        public void EndOfTurn()
        {
            reduceLightScreen();
            reduceMist();
            reduceReflect();
            reduceBind();
        }

        private void reduceLightScreen()
        {
            if (!hasReflect) return;
            reflectDur--;
            if (reflectDur <= 0)
            {
                reflectDur = 0;
                hasReflect = false;
            }
        }

        private void reduceMist()
        {
            if (!hasMist) return;
            mistDur--;

            if (mistDur <= 0)
            {
                mistDur = 0;
                hasMist = false;
            }
        }

        private void reduceReflect()
        {
            if (!hasLightScreen) return;
            lightScreenDur--;
            if (lightScreenDur <= 0)
            {
                lightScreenDur = 0;
                hasLightScreen = false;
            }
        }

        private void reduceBind()
        {
            if (!isBound) return;
            bindDuration--;
            if(bindDuration <= 0)
            {
                bindDuration = 0;
                bindDamage = 0;
                isBound = false;
            }
        }

    }
}
