using UnityEngine;
using System.Collections;

namespace FatBobbyGaming
{
    public class FBG_PokemonTeam
    {
        public bool hasMist;
        public bool hasReflect;
        public bool hasLightScreen;
        public bool canSwap;

        private int lightScreenDur;
        private int reflectDur;
        private int mistDur;


        public FBG_PokemonTeam()
        {
            init();
        }

        public void init()
        {
            hasMist = false;
            hasReflect = false;
            hasLightScreen = false;
            canSwap = true;
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

        public void EndOfTurn()
        {
            reduceLightScreen();
            reduceMist();
            reduceReflect();
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

    }
}
