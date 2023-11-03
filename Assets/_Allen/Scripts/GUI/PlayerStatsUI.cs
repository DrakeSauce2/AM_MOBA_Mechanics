using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private List<StatUISlot> statUISlots = new List<StatUISlot>();

    public void SetValues(Stats stats)
    {
        foreach (StatUISlot compareStatSlot in statUISlots)
        {
            foreach (StatInfo stat in stats.StatsList)
            {
                if (stat.statType == compareStatSlot.GetStatType())
                {
                    compareStatSlot.SetValue(stat.statType, stats.TryGetStatValue(stat.statType));
                }
            }
        }       
    }
}

[Serializable]
public class StatUISlot
{
    [SerializeField] Stat stat;
    [SerializeField] TextMeshProUGUI statText;
    [SerializeField] float statValue;

    public Stat GetStatType()
    {
        return stat;
    }

    public void SetValue(Stat stat, float value)
    {
        if(this.stat == stat)
        {
            statText.text = $"{value}";
        }

    }

}
