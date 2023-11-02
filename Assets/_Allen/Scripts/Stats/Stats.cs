using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Stats/BaseStats")]
public class Stats : ScriptableObject
{
    private Dictionary<Stat, float> statsDictionary = new Dictionary<Stat, float>();
    private Dictionary<Stat, List<float>> incrementStatsDictionary = new Dictionary<Stat, List<float>>();
    private Dictionary<Stat, int> incrementIndexDictionary = new Dictionary<Stat, int>();

    [SerializeField] private List<StatInfo> statsList = new List<StatInfo>();
    public List<StatInfo> StatsList { get { return statsList; } }

    public void Initialize()
    {
        foreach(StatInfo stat in statsList)
        {
            statsDictionary.Add(stat.statType, stat.statValue);
            incrementStatsDictionary.Add(stat.statType, stat.incrementValues);
            incrementIndexDictionary.Add(stat.statType, stat.incrementIndexValue);
        }
    }

    public void TrySetStatValue(Stat stat, float modifiedStat)
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
            Debug.LogError($"Error Trying To Retrieve {queryStat} Stat; Stat Does Not Exist!");
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
    LEVELUP_COST
}

#endregion


