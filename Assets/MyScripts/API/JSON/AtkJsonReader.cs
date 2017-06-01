using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Diagnostics;

namespace FBG.JSON
{
    public static class AtkJsonReader
    {

        /*
        List of flags and their descriptions:
        authentic: Ignores a target's substitute.
        bite: Power is multiplied by 1.5 when used by a Pokemon with the Ability Strong Jaw.
        bullet: Has no effect on Pokemon with the Ability Bulletproof.
        charge: The user is unable to make a move between turns.
        contact: Makes contact.
        defrost: Thaws the user if executed successfully while the user is frozen.
        distance: Can target a Pokemon positioned anywhere in a Triple Battle.
        gravity: Prevented from being executed or selected during Gravity's effect.
        heal: Prevented from being executed or selected during Heal Block's effect.
        mirror: Can be copied by Mirror Move.
        nonsky: Prevented from being executed or selected in a Sky Battle.
        powder: Has no effect on Grass-type Pokemon, Pokemon with the Ability Overcoat, and Pokemon holding Safety Goggles.
        protect: Blocked by Detect, Protect, Spiky Shield, and if not a Status move, King's Shield.
        pulse: Power is multiplied by 1.5 when used by a Pokemon with the Ability Mega Launcher.
        punch: Power is multiplied by 1.2 when used by a Pokemon with the Ability Iron Fist.
        recharge: If this move is successful, the user must recharge on the following turn and cannot make a move.
        reflectable: Bounced back to the original user by Magic Coat or the Ability Magic Bounce.
        snatch: Can be stolen from the original user and instead used by another Pokemon using Snatch.
        sound: Has no effect on Pokemon with the Ability Soundproof.
        */

        public static AttackData createAttackDex()
        {
            TextAsset jsonFile = loadTextFile();
            Stopwatch sw = new Stopwatch();
            sw.Start();

            AttackData a = JsonUtility.FromJson<AttackData>(jsonFile.text);
            debugJson(a);
            sw.Stop();
            UnityEngine.Debug.Log("time to create atk json in ms: " + sw.ElapsedMilliseconds);
            return a;
        }

        private static void debugJson(AttackData a)
        {
            int index = 0;
            //print(json.pokemon[0].name);
            for (int i = 0; i < a.attacks.Length; i++)
            {
                //UnityEngine.Debug.Log(string.Format("{0} {1}",i, a.attacks[i].name));
                if (a.attacks[i].priority != 0)
                {
                    UnityEngine.Debug.Log(string.Format("num: {0} name: {1} priority: {2}", a.attacks[i].num, a.attacks[i].name, a.attacks[i].priority));
                    index++;
                }
            }
        }

        private static TextAsset loadTextFile()
        {
            object o;
            TextAsset t;
            string path = "JSON/AttackJSON";
            o = Resources.Load(path);
            if (o == null)
            {
                UnityEngine.Debug.Log("JSON NOT FOUND");
            }
            t = o as TextAsset;
            return t;
        }
    }

    [Serializable]
    public class AttackData
    {
        public AttackJsonData[] attacks;
    }

    [Serializable]
    public class AttackJsonData
    {
        public int num;
        public int accuracy;
        public int basePower;
        public string category;
        public string desc;
        public string shortDesc;
        public string id;
        public string name;
        public int pp;
        public int priority;
        public string[] flags;
        public int critRatio;
        public string target;
        public string type;
        public string contestType;
    }
}