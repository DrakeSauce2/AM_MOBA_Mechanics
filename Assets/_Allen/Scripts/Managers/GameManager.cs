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

    internal void AwardKill(GameObject instigator, GameObject victim)
    {
        Character instigatingCharacter = instigator.GetComponent<Character>();
        Stats instigatingStats = instigatingCharacter.GetStats();

        Character victimCharacter = instigator.GetComponent<Character>();
        Stats victimStats = victimCharacter.GetStats();

        float experienceToGive = victimStats.TryGetStatValue(Stat.LEVEL) * 12f;

        instigatingStats.TryAddStatValue(Stat.GOLD, 150f);
        instigatingStats.TrySetStatValue(Stat.EXPERIENCE, instigatingStats.TryGetStatValue(Stat.EXPERIENCE) + experienceToGive);

    }

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
            player.InitializePlayerInfo(newPlayerInfo);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}

// Contains All the Variables Created At Start To Send To Player Character On Initialization
public class PlayerInfo 
{
    public UIManager _UIManager { get; private set; }
    public PlayerHUDbar _HUDbar { get; private set; }

    public ValueGauge HealthGauge { get; private set; }
    public ValueGauge ManaGauge { get; private set; }
    public ValueGauge ExpGauge { get; private set; }

    public PlayerStatsUI playerStatsUI { get; private set; }

    private Stats ownerStats;

    public PlayerInfo(PlayerCharacter player)
    {
        _UIManager = UIManager.Instance;
        playerStatsUI = _UIManager.PlayerStatsUI.GetComponent<PlayerStatsUI>();
        ownerStats = player.GetStats();

        _HUDbar = _UIManager.PlayerHUDbar.GetComponent<PlayerHUDbar>();

        ownerStats.onValueChanged -= UpdateUI;
        ownerStats.onValueChanged += UpdateUI;

        HealthGauge = _HUDbar.HealthGauge;
        ManaGauge = _HUDbar.ManaGauge;
        ExpGauge = _HUDbar.ExpGauge;

        HealthGauge.Initialize(ownerStats.TryGetStatValue(Stat.MAXHEALTH), ownerStats.TryGetStatValue(Stat.MAXHEALTH), Color.red);
        ManaGauge.Initialize(ownerStats.TryGetStatValue(Stat.MAXMANA), ownerStats.TryGetStatValue(Stat.MAXMANA), Color.blue);
        ExpGauge.Initialize(0, ownerStats.TryGetStatValue(Stat.LEVELUP_COST), Color.yellow);

        playerStatsUI.SetValues(ownerStats);
    }

    // TODO: Change logic, probs not the best to check each stat on each component
    public void UpdateUI(Stat statChanged, float value)
    {
        if (statChanged == Stat.HEALTH)
        {
            HealthGauge.SetValue(value);
            return;
        }

        if (statChanged == Stat.MANA)
        {
            ManaGauge.SetValue(value);
            return;
        }

        if (statChanged == Stat.EXPERIENCE)
        {
            ExpGauge.SetValue(value);
            return;
        }

        playerStatsUI.SetValues(ownerStats);
    }

    private ValueGauge CreateValueGauge(Transform transform)
    {
        GameObject valueGuageInstance = UIManager.Instance.CreateValueGauge(transform);
        return valueGuageInstance.GetComponent<ValueGauge>();
    }



}
