using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/BaseStats")]
public class Stats : ScriptableObject
{
    private Dictionary<Stat, int> statsDictionary = new Dictionary<Stat, int>();
    [SerializeField] private List<StatInfo> statsList = new List<StatInfo>();

    public void Initialize()
    {
        foreach(StatInfo stat in statsList)
        {
            statsDictionary.Add(stat.statType, stat.statValue);
        }
    }

    public int TryGetStatValue(Stat queryStat)
    {
        if (statsDictionary.ContainsKey(queryStat))
        {
            return statsDictionary[queryStat];
        }
        else
        {
            Debug.LogError($"Error Trying To Retrieve {queryStat} Stat; Stat Does Not Exist!");
            return 0;
        }
    }

}

#region Stat Foundation

[Serializable]
public class StatInfo
{
    public Stat statType;
    public int statValue;
}

public enum Stat
{
    HEALTH,
    MOVEMENT_SPEED,
    PHYSICAL_DEFENSE,
    MAGICAL_DEFENSE,
    PHYSICAL_POWER,
    MAGICAL_POWER
}

#endregion


