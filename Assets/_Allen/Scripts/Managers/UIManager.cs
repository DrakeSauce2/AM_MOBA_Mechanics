using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Prefabs")]
    [SerializeField] private GameObject playerHUDbar;
    [SerializeField] private GameObject playerStatsUI;
    public GameObject PlayerStatsUI { get { return playerStatsUI; } }
    public GameObject PlayerHUDbar { get { return playerHUDbar; } }

    [Header("UI Transforms")]
    [SerializeField] private RectTransform playerHealthGaugeTransform;
    [SerializeField] private RectTransform playerManaGaugeTransform;
    [SerializeField] private RectTransform playerExpGaugeTransform;
    [SerializeField] private RectTransform playerBuffIconsTransform;
    [SerializeField] private RectTransform playerDebuffIconsTransform;

    public RectTransform PlayerHealthGaugeTransform { get { return playerHealthGaugeTransform; }  }
    public RectTransform PlayerManaGaugeTransform { get { return playerManaGaugeTransform; } }
    public RectTransform PlayerExpGaugeTransform { get { return playerExpGaugeTransform; } }
    public RectTransform PlayerBuffIconsTransform { get { return playerBuffIconsTransform; } }
    public RectTransform PlayerDebuffIconsTransform { get { return playerDebuffIconsTransform; } }

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(Instance);

    }

    public GameObject CreateValueGauge(RectTransform transform)
    {
        return Instantiate(GameManager.Instance.ValueBarPrefab, transform);
    }

}
