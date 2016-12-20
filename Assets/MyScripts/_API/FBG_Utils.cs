using UnityEngine;
using System.Collections;

namespace FatBobbyGaming
{
    public static class FBG_Utils
    {

        /// <summary>
        /// this is only to check if this affects the pokemon moving
        /// </summary>
        /// <param name="self">the pokemon to check</param>
        /// <returns></returns>
        public static bool checkPlayerNVStatus(FBG_Pokemon self)
        {
            if (self.status_A != nonVolitileStatusEffects.none)
            {
                if (self.status_A == nonVolitileStatusEffects.paralized)
                {
                    int rnd = UnityEngine.Random.Range(1, 4);
                    if (rnd == 1)
                    {
                        string text = self.Name + " is Paralized!";
                        //c_Queue.AddCoroutineToQueue(DisplayText(text));
                        return true;
                    }
                }
                if (self.status_A == nonVolitileStatusEffects.sleep)
                {
                    self.nonVolDuration--;
                    string text = "";
                    if (self.nonVolDuration == 0)
                    {
                        text = self.Name + " woke up!";
                        //c_Queue.AddCoroutineToQueue(DisplayText(text));
                    }
                    else
                    {
                        text = self.Name + " is fast asleep!";
                        //c_Queue.AddCoroutineToQueue(DisplayText(text));
                        return true;
                    }

                }
                if (self.status_A == nonVolitileStatusEffects.frozen)
                {
                    int rnd = UnityEngine.Random.Range(1, 10);
                    if (rnd >= 2)
                    {
                        string text = self.Name + " is Frozen!";
                        //c_Queue.AddCoroutineToQueue(DisplayText(text));
                        return true;
                    }
                    else
                    {
                        self.status_A = nonVolitileStatusEffects.none;
                        string text = self.Name + " thawed out!";
                        //c_Queue.AddCoroutineToQueue(DisplayText(text));

                    }
                }
            }
            return false;
        }

        public static AttackJsonData getAttack(AttackData dex, int num)
        {
            for(int i = 0; i < dex.attacks.Length; i++)
            {
                if(dex.attacks[i].num == num)
                {
                    return dex.attacks[i];
                }
            }
            return null;
        }

        public static AttackJsonData getAttack(AttackData dex, string name)
        {
            for (int i = 0; i < dex.attacks.Length; i++)
            {
                if (dex.attacks[i].id == name.ToLower())
                {
                    return dex.attacks[i];
                }
            }
            return null;
        }

    }
}
