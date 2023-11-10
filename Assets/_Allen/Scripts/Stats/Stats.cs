using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Stats/BaseStats")]
public class Stats : ScriptableObject
{
    
    // Maybe Not Make 3 Dictionaries, Not Sure How Expensive Procedure Is

    private Dictionary<Stat, float> statsDictionary = new Dictionary<Stat, float>();
    private Dictionary<Stat, List<float>> incrementStatsDictionary = new Dictionary<Stat, List<float>>();
    private Dictionary<Stat, int> incrementIndexDictionary = new Dictionary<Stat, int>();

    [SerializeField] private List<StatInfo> statsList = new List<StatInfo>();
    public List<StatInfo> StatsList { get { return statsList; } }

    public delegate void OnValueChanged(Stat statChanged, float value);
    public event OnValueChanged onValueChanged;

    public void Initialize()
    {
        foreach(StatInfo stat in statsList)
        {
            statsDictionary.Add(stat.statType, stat.statValue);
            incrementStatsDictionary.Add(stat.statType, stat.incrementValues);
            incrementIndexDictionary.Add(stat.statType, stat.incrementIndexValue);
        }
    }

    public void TrySetStatValue(Stat queryStat, float value)
    {
        if (statsDictionary.ContainsKey(queryStat))
        {
            statsDictionary[queryStat] = value;
            onValueChanged?.Invoke(queryStat, value);
        }
        else
        {
            Debug.LogError($"Error Trying To Set {queryStat} Stat; Stat Does Not Exist On This Object!");
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
            // Alternatively might want to direct this error somewhere else if Stat cannot be found, If Stat cannot be Reached then send IMMUNE
            Debug.LogError($"Error Trying To Retrieve {queryStat} Stat; Stat Does Not Exist On This Object!");
            return 0;
        }
    }

    public void TryApplyStatus()
    {
        // Not Sure How I Want To Approach This Yet
    }

    public void TryIncrementStat(Stat queryStat)
    {
        if (incrementStatsDictionary.ContainsKey(queryStat))
        {
            if (incrementStatsDictionary[queryStat].Count <= 0 || incrementStatsDictionary[queryStat].Count >= 20) return;

            incrementIndexDictionary[queryStat]++;
            statsDictionary[queryStat] = incrementStatsDictionary[queryStat][incrementIndexDictionary[queryStat]];
        }
        else
        {
            Debug.LogError($"Error Trying To Increment {queryStat} Stat; Stat Does Not Exist On This Object!");
        }
    }

}

#region Stat Foundation

[Serializable]
public class StatInfo
{
    public Stat statType;
    public float statValue;
    public List<float> incrementValues = new List<float>(20);
    public int incrementIndexValue = 0;
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
    LEVELUP_COST,
    ATTACKSPEED,
    BASICATTACKDAMAGE,
    LEVEL
}

#endregion


