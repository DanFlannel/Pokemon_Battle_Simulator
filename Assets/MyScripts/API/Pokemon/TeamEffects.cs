﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FBG.Base
{
    public class TeamEffects
    {
        public bool hasMist;
        public bool hasReflect;
        public bool hasLightScreen;
        public bool isBound;
        public bool hasLeechSeed;

        private int lightScreenDur;
        private int reflectDur;
        private int mistDur;

        public int bindDuration;
        public float bindDamage;

        public void addMist()
        {
            if (hasMist) { return; }
            hasMist = true;
            mistDur = 5;
        }

        public void addLightScreen(int dur)
        {
            if (hasLightScreen) { return; }

            hasLightScreen = true;
            lightScreenDur = dur;
        }

        public void addReflect(int dur)
        {
            if (hasReflect) { return; }

            hasReflect = true;
            reflectDur = dur;
        }

        public void addBind(int dur, float dmg)
        {
            if (isBound) { return; }

            isBound = true;
            bindDuration = dur;
            bindDamage = dmg;
        }



        private void reduceLightScreen()
        {
            if (!hasReflect) { return; }
            reflectDur--;
            if (reflectDur <= 0)
            {
                reflectDur = 0;
                hasReflect = false;
            }
        }

        private void reduceMist()
        {
            if (!hasMist) { return; }
            mistDur--;

            if (mistDur <= 0)
            {
                mistDur = 0;
                hasMist = false;
            }
        }

        private void reduceReflect()
        {
            if (!hasLightScreen) { return; }
            lightScreenDur--;
            if (lightScreenDur <= 0)
            {
                lightScreenDur = 0;
                hasLightScreen = false;
            }
        }

        private void reduceBind()
        {
            if (!isBound) { return; }
            bindDuration--;
            if (bindDuration <= 0)
            {
                bindDuration = 0;
                bindDamage = 0;
                isBound = false;
            }
        }

        public void SetupTeamEffects()
        {
            hasMist = false;
            hasReflect = false;
            hasLightScreen = false;
            isBound = false;
        }

        public void EndOfTurn()
        {
            reduceLightScreen();
            reduceMist();
            reduceReflect();
            reduceBind();
            //we do the leech seed damage and healing in the battleSim so we can add text and effects
            //along with the bind as well
        }

    }
}