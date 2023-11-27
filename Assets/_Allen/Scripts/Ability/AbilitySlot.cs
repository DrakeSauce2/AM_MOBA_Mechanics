using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
        /*
        foreach (GameObject level in levelCount)
        {
            level.SetActive(false);
        }
        */
    }

    public void SetCooldownTime(float cooldownTime, float maxCooldown)
    {
        abilityCooldownFill.fillAmount = 1 - (cooldownTime / maxCooldown);
        cooldownTimeText.text = (maxCooldown - cooldownTime).ToString("F0");

        abilityCooldownFill.gameObject.SetActive(abilityCooldownFill.fillAmount > 0);
    }

}
