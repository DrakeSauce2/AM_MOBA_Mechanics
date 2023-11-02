using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Stats/BaseStats")]
public class Stats : ScriptableObject
{
    private Dictionary<Stat, float> statsDictionary = new Dictionary<Stat, float>();
    [SerializeField] private List<StatInfo> statsList = new List<StatInfo>();

    public delegate void OnStatChange(Stat stat, float modifiedStat);
    public event OnStatChange onStatChange;

    public void Initialize()
    {
        onStatChange += TrySetStatValue;

        foreach(StatInfo stat in statsList)
        {
            statsDictionary.Add(stat.statType, stat.statValue);
        }
    }

    private void TrySetStatValue(Stat stat, float modifiedStat)
    {
        if (statsDictionary.ContainsKey(stat))
        {
            statsDictionary[stat] = modifiedStat;
        }
        else
        {
            Debug.LogError($"Error Trying To Retrieve {stat} Stat; Stat Does Not Exist!");
        }
    }

    public float TryGetStatValue(Stat queryStat)
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

    public void TryApplyStatus()
    {

    }
}

#region Stat Foundation

[Serializable]
public class StatInfo
{
    public Stat statType;
    public float statValue;
}

public enum Stat
{
    HEALTH,
    MANA,
    MOVEMENT_SPEED,
    PHYSICAL_DEFENSE,
    MAGICAL_DEFENSE,
    PHYSICAL_POWER,
    MAGICAL_POWER,
    EXPERIENCE,
    GOLD,
    ITEMBUY_COST,
    ITEMSELL_COST,
    MAXHEALTH,
    MAXMANA,
    LEVELUP_COST
}

#endregion


