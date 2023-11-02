using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Transforms")]
    [SerializeField] private RectTransform playerValueGaugesTransform;
    [SerializeField] private RectTransform playerBuffIconsTransform;
    [SerializeField] private RectTransform playerDebuffIconsTransform;

    public RectTransform PlayerValueGaugesTransform { get { return playerValueGaugesTransform; }  }
    public RectTransform PlayerBuffIconsTransform { get { return playerBuffIconsTransform; } }
    public RectTransform PlayerDebuffIconsTransform { get { return playerDebuffIconsTransform; } }

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(Instance);

    }

    private void Start()
    {
        if(playerValueGaugesTransform == null)
            playerValueGaugesTransform = GameObject.FindGameObjectWithTag("PlayerValueGauge").GetComponent<RectTransform>();
    }

    public GameObject CreateValueGauge()
    {
        return Instantiate(GameManager.Instance.ValueBarPrefab, PlayerValueGaugesTransform);
    }

}
