using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Transforms")]
    [SerializeField] private RectTransform playerValueGaugesTransform;
    [SerializeField] private RectTransform playerBuffIconsTransform;
    [SerializeField] private RectTransform playerDebuffIconsTransform;

    public RectTransform PlayerValueGaugesTransform { get { return playerValueGaugesTransform; }  }
    public RectTransform PlayerBuffIconsTransform { get { return playerBuffIconsTransform; } }
    public RectTransform PlayerDebuffIconsTransform { get { return playerDebuffIconsTransform; } }

    [Header("UI Prefabs")]
    [SerializeField] private GameObject valueBarPrefab;
    public GameObject ValueBarPrefab { get { return valueBarPrefab; } }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
}
