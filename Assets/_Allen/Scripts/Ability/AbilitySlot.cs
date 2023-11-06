using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{
    [SerializeField] Image abilityImage;
    [SerializeField] TextMeshProUGUI cooldownTimeText;
    [SerializeField] Image abilityCooldownFill;
    [SerializeField] List<GameObject> levelCount = new List<GameObject>();

    public void Init(Sprite imageSprite)
    {
        abilityImage.sprite = imageSprite;
        cooldownTimeText.text = $"";
        abilityCooldownFill.fillAmount = 0;

        foreach (GameObject level in levelCount)
        {
            level.SetActive(false);
        }
    }

    public void SetCooldown(float cooldown, float maxCooldown)
    {
        abilityCooldownFill.fillAmount = 1 - (cooldown / maxCooldown);
    }

}
