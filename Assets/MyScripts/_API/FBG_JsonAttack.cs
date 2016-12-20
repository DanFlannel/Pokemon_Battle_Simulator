using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;

namespace FatBobbyGaming
{
    public static class FBG_JsonAttack
    {
        public static AttackData createAttackDex()
        {
            TextAsset jsonFile = loadTextFile();
            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            AttackData a = JsonUtility.FromJson<AttackData>(jsonFile.text);
            debugJson(a);
            //sw.Stop();
            //print("time to create json in ms: " + sw.ElapsedMilliseconds);
            return a;
        }

        private static void debugJson(AttackData a)
        {
            //int index = 0;
            //print(json.pokemon[0].name);
            for (int i = 0; i < a.attacks.Length; i++)
            {
                //UnityEngine.Debug.Log(string.Format("{0} {1}",i, a.attacks[i].name));
                if(a.attacks[i].critRatio != 0)
                {
                    UnityEngine.Debug.Log(string.Format("{0} {1}", a.attacks[i].num, a.attacks[i].name));
                }
            }
            //print(index);
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
