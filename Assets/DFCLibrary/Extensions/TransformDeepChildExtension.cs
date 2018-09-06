using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class TransformDeepChildExtension
{
    //Breadth-first search
    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        var result = aParent.Find(aName);
        if (result != null)
            return result;
        foreach (Transform child in aParent)
        {
            result = child.FindDeepChild(aName);
            if (result != null)
                return result;
        }
        return null;
    }

    public static List<Transform> FindDeepChildren(this Transform aParent, string aName)
    {
        List<Transform> list = new List<Transform>();

        Transform[] all = aParent.GetComponentsInChildren<Transform>();
        for(int i = 0; i < all.Length; i++)
        {
            if(all[i].name == aName)
            {
                list.Add(all[i]);
            }
        }
        return list;
    }
}
