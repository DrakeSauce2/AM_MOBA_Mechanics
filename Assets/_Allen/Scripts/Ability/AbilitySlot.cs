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

    public void Init(ActiveAbility ability, Sprite imageSprite)
    {
        abilityImage.sprite = imageSprite;
        cooldownTimeText.text = $"";
        abilityCooldownFill.fillAmount = 0;

        ability.onCooldownStarted += StartCooldown;
    }

    private void StartCooldown(float cooldownDuration)
    {
        StartCoroutine(CooldownCoroutine(cooldownDuration));
    }

    IEnumerator CooldownCoroutine(float cooldownDuration)
    {
        float cooldownTimeLeft = cooldownDuration;
        while (cooldownTimeLeft > 0)
        {
            cooldownTimeLeft -= Time.deltaTime;

            abilityCooldownFill.fillAmount = 1 - (cooldownTimeLeft / cooldownDuration);
            cooldownTimeText.text = (cooldownTimeLeft).ToString("F1");

            abilityCooldownFill.gameObject.SetActive(cooldownTimeLeft > 0.1);

            yield return new WaitForEndOfFrame();
        }
    }

}
