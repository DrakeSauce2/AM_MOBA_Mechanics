using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 

    [SerializeField] private List<PlayerCharacter> playerList = new List<PlayerCharacter>();

    [SerializeField] private GameObject valueBarPrefab;
    public GameObject ValueBarPrefab { get { return valueBarPrefab; } }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);  
    }

    private void Start()
    {
        foreach (PlayerCharacter player in playerList)
        {
            player.Initialize();
            PlayerInfo newPlayerInfo = new PlayerInfo(player);
            player.SetPlayerInfo(newPlayerInfo);
        }
    }
}

// Contains All the Variables Created At Start To Send To Player Character On Initialization
public class PlayerInfo 
{
    public UIManager _UIManager { get; private set; }
    public ValueGauge healthGauge { get; private set; }
    public ValueGauge manaGauge { get; private set; }
    public ValueGauge expGauge { get; private set; }
    public PlayerStatsUI playerStatsUI { get; private set; }

    private Stats ownerStats;

    public PlayerInfo(PlayerCharacter player)
    {
        _UIManager = UIManager.Instance;
        playerStatsUI = _UIManager.PlayerStatsUI.GetComponent<PlayerStatsUI>();
        ownerStats = player.GetStats();

        ownerStats.onValueChanged -= UpdateUI;
        ownerStats.onValueChanged += UpdateUI;

        healthGauge = CreateValueGauge(_UIManager.PlayerHealthGaugeTransform);
        manaGauge = CreateValueGauge(_UIManager.PlayerManaGaugeTransform);
        expGauge = CreateValueGauge(_UIManager.PlayerExpGaugeTransform);

        healthGauge.Initialize(ownerStats.TryGetStatValue(Stat.MAXHEALTH), ownerStats.TryGetStatValue(Stat.MAXHEALTH), Color.green);
        manaGauge.Initialize(ownerStats.TryGetStatValue(Stat.MAXMANA), ownerStats.TryGetStatValue(Stat.MAXMANA), Color.blue);
        expGauge.Initialize(0, ownerStats.TryGetStatValue(Stat.LEVELUP_COST), Color.yellow);

        playerStatsUI.SetValues(ownerStats);
    }

    // TODO: Change logic, probs not the best to check each stat on each component
    public void UpdateUI(Stat statChanged, float value)
    {
        if (statChanged == Stat.HEALTH)
        {
            healthGauge.SetValue(value);
            return;
        }

        if (statChanged == Stat.MANA)
        {
            manaGauge.SetValue(value);
            return;
        }

        if (statChanged == Stat.EXPERIENCE)
        {
            expGauge.SetValue(value);
            return;
        }
    }

    private ValueGauge CreateValueGauge(RectTransform transform)
    {
        GameObject valueGuageInstance = UIManager.Instance.CreateValueGauge(transform);
        return valueGuageInstance.GetComponent<ValueGauge>();
    }



}
