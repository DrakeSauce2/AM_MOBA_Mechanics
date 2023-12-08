using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public GameObject Owner { get; private set; }
    private float damage;
    private float duration;

    public void Init(GameObject owner, float damage, float duration)
    {
        Owner = owner;
        this.damage = damage;
        this.duration = duration;
        StartScan();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Owner) return;

        Character character = other.GetComponent<Character>();
        if (character == null) return;

        Debug.Log(character);

        character.ApplyDamage(Owner, damage);
    }

    public void StartScan()
    {
        StartCoroutine(ScanCoroutine());
    }

    IEnumerator ScanCoroutine()
    {
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }

}
