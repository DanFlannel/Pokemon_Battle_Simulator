using FBG.Attack;
using FBG.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace FBG.Battle
{
    public class TurnOrder
    {
        public List<TurnInformation> order;
        public List<TurnInformation> speedDetermined;

        public TurnOrder(List<TurnInformation> info)
        {
            order = determineOrder(info);
        }

        private List<TurnInformation> determineOrder(List<TurnInformation> info)
        {
            info = adjustSwitching(info);
            //we determine the order all specifically by speed and store that
            speedDetermined = speedAll(info);
            //we then take into account priorities and re order the pokemon
            return combineSpeed_Priority(info, speedDetermined);
        }

        private List<TurnInformation> adjustSwitching (List<TurnInformation> info)
        {
            for(int i = 0; i < info.Count; i++)
            {
                if (info[i].isSwapping)
                {
                    info[i].priority = 6;
                }
            }
            return info;
        }

        private List<TurnInformation> combineSpeed_Priority(List<TurnInformation> info, List<TurnInformation> speed)
        {
            //create an upper and lower list (for positive and negative priorities)
            List<TurnInformation> upper = new List<TurnInformation>();
            List<TurnInformation> lower = new List<TurnInformation>();
            //Add to those lists based on priority
            for (int i = 0; i < info.Count; i++)
            {
                if (info[i].priority > 0)
                {
                    upper.Add(info[i]);
                }
                else if (info[i].priority < 0)
                {
                    lower.Add(info[i]);
                }
            }

            //Sort those lists
            upper = priorityOnlySort(upper);
            lower = priorityOnlySort(lower);

            //remove pokemon with priority moves from the speed sorted list so we dont have them in there twice
            speed = remove(speed, upper);
            speed = remove(speed, lower);

            //add the lists together
            List<TurnInformation> final = add(upper, speed);
            add(final, lower);
            return final;
        }

        //ORDERING

        //sorts all the pokemon
        private List<TurnInformation> speedAll(List<TurnInformation> info)
        {
            //do a basic speed ordering so our for loop works....
            info = basicSpeedSort(info);
            List<TurnInformation> final = new List<TurnInformation>();
            for(int i = 0; i < info.Count; i++)
            {
                List<TurnInformation> tmp = shuffle(searchForSpeed(info[i].speed, info));
                add(final, tmp);
                //add to i based on the length of the list, if there is a tie... then we dont want to count the next pokemon who is tied with the current one
                UnityEngine.Debug.Log(tmp.Count - 1);
                i += tmp.Count - 1;
            }
            return final;
        }

        //sorts only pokemon with a priority move
        private List<TurnInformation> priorityOnlySort(List<TurnInformation> info)
        {
            info = basicPrioritySort(info);
            List<TurnInformation> final = new List<TurnInformation>();
            for (int i = 0; i < info.Count; i++)
            {
                List<TurnInformation> tmp = shuffle(searchForPrioties(info[i].priority, order));
                if (info[i].priority != 0)
                {
                    add(final, tmp);
                }
                else
                {
                    UnityEngine.Debug.Log("Found priority 0 in priority search");
                }
                i += tmp.Count - 1;
            }
            return final;
        }

        //BASIC SEARCH

        private List<TurnInformation> searchForPrioties(int priority, List<TurnInformation> info)
        {
            List<TurnInformation> result = new List<TurnInformation>();
            for (int i = 0; i < info.Count; i++)
            {
                if (info[i].priority == priority)
                {
                    result.Add(info[i]);
                }
            }
            return result;
        }

        private List<TurnInformation> searchForSpeed(int speed, List<TurnInformation> info)
        {
            List<TurnInformation> result = new List<TurnInformation>();
            for(int i = 0; i < info.Count; i++)
            {
                if(info[i].speed == speed)
                {
                    result.Add(info[i]);
                }
            }
            return result;
        }


        //BASIC SORT

        private List<TurnInformation> basicPrioritySort(List<TurnInformation> info)
        {
            TurnInformation[] tmp = info.ToArray();
            for (int i = 0; i < tmp.Length; i++)
            {
                for (int n = 0; n < tmp.Length; n++)
                {
                    if (tmp[i].priority >= tmp[n].priority)
                    {
                        swap(tmp, i, n);
                    }
                }
            }
            return tmp.ToList();
        }

        private List<TurnInformation> basicSpeedSort(List<TurnInformation> info)
        {
            TurnInformation[] tmp = info.ToArray();
            for (int i = 0; i < tmp.Length; i++)
            {
                for (int n = 0; n < tmp.Length; n++)
                {
                    if (tmp[i].speed >= tmp[n].speed)
                    {
                        swap(tmp, i, n);
                    }
                }
            }
            return tmp.ToList();
        }

        //HELPERS

        //http://stackoverflow.com/questions/273313/randomize-a-listt
        private List<TurnInformation> shuffle(List<TurnInformation> list)
        {
            Random rng = new Random();
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                TurnInformation value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

        //adds t1 and t2 together
        private List<TurnInformation> add(List<TurnInformation> t1, List<TurnInformation> t2){
            for(int i = 0; i < t2.Count; i++)
            {
                t1.Add(t2[i]);
            }
            return t1;
        }

        //removes all of the instances of t2 in t1
        private List<TurnInformation> remove(List<TurnInformation> t1, List<TurnInformation> t2)
        {
            List<TurnInformation> tmp = new List<TurnInformation>();
            for(int i = 0; i < t1.Count; i++)
            {
                if (!t2.Contains(t1[i]))
                {
                    tmp.Add(t1[i]);
                }
            }
            return tmp;
        }

        private void swap(TurnInformation[] arr, int a, int b)
        {
            TurnInformation temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }
    }
}
