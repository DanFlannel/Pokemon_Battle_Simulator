using FBG.Attack;
using FBG.Base;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FBG.Battle
{
    public class TurnOrder
    {
        public List<TurnInformation> order;

        public TurnOrder(List<TurnInformation> info)
        {
            order = determineOrder(info);
        }

        private List<TurnInformation> determineOrder(List<TurnInformation> info)
        {
            List<TurnInformation> speed = new List<TurnInformation>();
            List<TurnInformation> priorities = new List<TurnInformation>();

            for (int i = 0; i < info.Count; i++)
            {
                if (info[i].priority != 0)
                {
                    priorities.Add(info[i]);
                }
                else
                {
                    speed.Add(info[i]);
                }
            }
            priorities = prioritiesOrder(priorities);
            speed = speedOrder(speed);
            return add(priorities, speed);
        }

        private List<TurnInformation> prioritiesOrder(List<TurnInformation> info)
        {
            TurnInformation[] tmp = info.ToArray();

            for (int i = 0; i < tmp.Length; i++)
            {
                for (int n = 0; n < tmp.Length; n++)
                {
                    if (tmp[i].priority <= tmp[n].priority)
                    {
                        if (tmp[i].speed < tmp[n].speed)
                        {
                            swap(tmp, i, n);
                        }
                        else if (tmp[i].speed == tmp[n].speed)
                        {
                            //speed tie
                            if (Utilities.probability(1, 2))
                            {
                                swap(tmp, i, n);
                            }
                        }
                    }
                }
            }
            return tmp.ToList();
        }

        private List<TurnInformation> speedOrder(List<TurnInformation> info)
        {
            TurnInformation[] tmp = info.ToArray();

            for (int i = 0; i < tmp.Length; i++)
            {
                for (int n = 0; n < tmp.Length; n++)
                {
                    if (tmp[i].speed > tmp[n].speed)
                    {
                        swap(tmp, i, n);
                    }else if(tmp[i].speed == tmp[n].speed)
                    {
                        //speed tie
                        if(Utilities.probability(1, 2))
                        {
                            swap(tmp, i, n);
                        }
                    }
                }
            }
            return tmp.ToList();
        }

        private static List<TurnInformation> add(List<TurnInformation> t1, List<TurnInformation> t2){
            for(int i = 0; i < t2.Count; i++)
            {
                t1.Add(t2[i]);
            }
            return t1;
        }

        private void swap(TurnInformation[] arr, int a, int b)
        {
            TurnInformation temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }
    }
}
