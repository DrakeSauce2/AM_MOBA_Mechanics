using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Canvas")]
    [SerializeField] private Canvas canvasPrefab;
    public Canvas CanvasPrefab { get { return canvasPrefab; } }

    [Header("UI Prefabs")]
    [SerializeField] private GameObject playerHUDbar;
    [SerializeField] private GameObject playerStatsUI;
    [SerializeField] private GameObject shopPrefab;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject itemLinePrefab;
    [SerializeField] private TextMeshProUGUI damagePopUp;
    [SerializeField] private SpriteRenderer lineSpriteRenderer;

    public GameObject PlayerStatsUI { get { return playerStatsUI; } }
    public GameObject PlayerHUDbar { get { return playerHUDbar; } }
    public GameObject ShopPrefab { get {  return shopPrefab; } }
    public TextMeshProUGUI DamagePopUp { get {  return damagePopUp; } }
    public GameObject ItemSlotPrefab { get { return itemSlotPrefab; } }
    public GameObject ItemLinePrefab { get { return itemLinePrefab; } }
    public SpriteRenderer LineSpriteRenderer { get { return lineSpriteRenderer; } }


    [Header("UI Transforms")]
    [SerializeField] private RectTransform playerHealthGaugeTransform;
    [SerializeField] private RectTransform playerManaGaugeTransform;
    [SerializeField] private RectTransform playerExpGaugeTransform;
    [SerializeField] private RectTransform playerBuffIconsTransform;
    [SerializeField] private RectTransform playerDebuffIconsTransform;
    [SerializeField] private RectTransform shopTreeTransform;
    [SerializeField] private RectTransform linePrefabTransform;
   
    public RectTransform PlayerHealthGaugeTransform { get { return playerHealthGaugeTransform; }  }
    public RectTransform PlayerManaGaugeTransform { get { return playerManaGaugeTransform; } }
    public RectTransform PlayerExpGaugeTransform { get { return playerExpGaugeTransform; } }
    public RectTransform PlayerBuffIconsTransform { get { return playerBuffIconsTransform; } }
    public RectTransform PlayerDebuffIconsTransform { get { return playerDebuffIconsTransform; } }
    public RectTransform ShopTreeTransform { get { return shopTreeTransform; } }
    public RectTransform LinePrefabTransform { get { return linePrefabTransform; } }

    public Transform CanvasTransform { get; private set; }

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(Instance);

        CanvasTransform = canvasPrefab.transform;
    }

    public GameObject CreateValueGauge(Transform transform)
    {
        return Instantiate(GameManager.Instance.ValueBarPrefab, transform);
    }

    public void CreateDamagePopUp(Transform targetTransform, float damage)
    {
        TextMeshProUGUI instancedDamagePopUp = Instantiate(damagePopUp, CanvasTransform);
        instancedDamagePopUp.text = damage.ToString("F0");

        UIAttachComponent uiAttachComp = instancedDamagePopUp.gameObject.AddComponent<UIAttachComponent>();
        uiAttachComp.Init(uiAttachComp.GetRandomPosition(targetTransform.position, 1, 1));

        Destroy(instancedDamagePopUp.gameObject, 1f);
    }

}
