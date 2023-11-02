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

    public PlayerInfo(PlayerCharacter player)
    {
        _UIManager = UIManager.Instance;

        healthGauge = CreateValueGauge();
        manaGauge = CreateValueGauge();
        expGauge = CreateValueGauge();

        healthGauge.Initialize(player.GetStats().TryGetStatValue(Stat.MAXHEALTH), player.GetStats().TryGetStatValue(Stat.MAXHEALTH), Color.green);
        manaGauge.Initialize(player.GetStats().TryGetStatValue(Stat.MAXMANA), player.GetStats().TryGetStatValue(Stat.MAXMANA), Color.blue);
        expGauge.Initialize(0, player.GetStats().TryGetStatValue(Stat.LEVELUP_COST), Color.yellow);
    }

    private ValueGauge CreateValueGauge()
    {
        GameObject valueGuageInstance = UIManager.Instance.CreateValueGauge();
        return valueGuageInstance.GetComponent<ValueGauge>();
    }

}
