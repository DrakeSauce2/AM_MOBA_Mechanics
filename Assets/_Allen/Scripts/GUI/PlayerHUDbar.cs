using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDbar : MonoBehaviour
{
    [SerializeField] private Image characterImage;

    [SerializeField] private ValueGauge healthGauge;
    [SerializeField] private ValueGauge manaGauge;
    [SerializeField] private ValueGauge expGauge;

    [SerializeField] private List<AbilitySlot> abilitySlotList = new List<AbilitySlot>(4);

    public ValueGauge HealthGauge { get { return healthGauge; } }
    public ValueGauge ManaGauge { get { return manaGauge; } }
    public ValueGauge ExpGauge { get { return expGauge; } }
    public List<AbilitySlot> AbilitySlotList { get { return abilitySlotList; } }

    public void SetCharacterIcon(Sprite characterSprite)
    {
        characterImage.sprite = characterSprite;
    }

}
