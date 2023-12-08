using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPerSecond : MonoBehaviour
{
    private Stats stats;
    bool isWaiting = false;

    public void Init(Stats stats)
    {
        this.stats = stats;
    }

    private void Update()
    {
        if (!isWaiting)
        {
            StartCoroutine(StatPerSecond());
        }
    }

    private IEnumerator StatPerSecond()
    {
        isWaiting = true;

        stats.TryAddStatValue(Stat.HEALTH, stats.TryGetStatValue(Stat.HP5));
        stats.TryAddStatValue(Stat.MANA, stats.TryGetStatValue(Stat.MP5));
        stats.TryAddStatValue(Stat.GOLD, stats.TryGetStatValue(Stat.GP5));

        if (stats.TryGetStatValue(Stat.HEALTH) > stats.TryGetStatValue(Stat.MAXHEALTH))
        {
            stats.TrySetStatValue(Stat.HEALTH, stats.TryGetStatValue(Stat.MAXHEALTH));
        }

        if (stats.TryGetStatValue(Stat.MANA) > stats.TryGetStatValue(Stat.MAXMANA))
        {
            stats.TrySetStatValue(Stat.MANA, stats.TryGetStatValue(Stat.MAXMANA));
        }

        yield return new WaitForSeconds(5f);

        isWaiting = false;
    }

}
