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

    float maxCooldown, cooldown;
    bool onCooldown = false;

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

    private void Update()
    {
        if (!onCooldown) { return; }

        cooldown += Time.deltaTime;
        abilityCooldownFill.fillAmount = 1 - (cooldown / maxCooldown);
        cooldownTimeText.text = (maxCooldown - cooldown).ToString("F0");

        if (abilityCooldownFill.fillAmount > 0)
        {
            abilityCooldownFill.gameObject.SetActive(true);
            
        }
        else
        {
            abilityCooldownFill.gameObject.SetActive(false);
            onCooldown = false;
        }       

    }

    public void StartCooldown(float maxCooldown)
    {
        if (onCooldown) return;

        onCooldown = true;
        this.maxCooldown = maxCooldown;
        cooldown = 0;
    }

}
