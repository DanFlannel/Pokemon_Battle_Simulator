using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnalyticData
{
    public int ID { get; private set; }
    public string eventQuery { get; private set; }
    public string eventName { get; private set; }
    public string eventValue { get; private set; }

    public bool sucess;
    public bool isPosting;
    public bool backedUp;

    public AnalyticData(int ID, string eventQuery, string eventName, string eventValue)
    {
        this.ID = ID;

        sucess = false;
        isPosting = false;
        backedUp = false;

        this.eventQuery = eventQuery;
        this.eventName = eventName;
        this.eventValue = eventValue;
    }
}