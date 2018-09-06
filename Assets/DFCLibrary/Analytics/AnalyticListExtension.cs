using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnalyticListExtension
{
    public static AnalyticData FindByID(this List<AnalyticData> list, int id)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (id == list[i].ID)
            {
                return list[i];
            }
        }
        return null;
    }

    public static void replaceByID(this List<AnalyticData> list, int id, AnalyticData data)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (id == list[i].ID)
            {
                list[i] = data;
            }
        }
    }
}