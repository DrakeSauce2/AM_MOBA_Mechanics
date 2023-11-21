using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RangedProjectile : MonoBehaviour
{
    public GameObject Owner { get; private set; }

    [SerializeField] private GameObject hitParticlePrefab;

    Rigidbody rBody;
    Vector3 startPos = Vector3.zero;

    float damage = 0;
    float distanceTravelled = 0f;
    float maxDistance = 1f;

    bool isInitialized = false;

    public void Init(GameObject owner, float projectileSpeed, float maxDistance, float damage)
    {
        Owner = owner;
        this.damage = damage;

        this.maxDistance = maxDistance;
        startPos = transform.position;

        rBody = GetComponent<Rigidbody>();

        rBody.AddForce(transform.forward * projectileSpeed * Time.fixedDeltaTime, ForceMode.Impulse);

        isInitialized = true;
    }

    private void FixedUpdate()
    {
       // if (isInitialized == false) return;
 
        distanceTravelled = Vector3.Distance(startPos, transform.position);

        if (distanceTravelled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Owner) return;
        Destroy(gameObject);

        Character hitCharacter = other.GetComponent<Character>();
        if (!hitCharacter)
        {
            Debug.Log($"Character Not Found On Hit Object");
            Destroy(gameObject);
            return;
        }

        Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
        Debug.Log($"{hitCharacter} Found! Applying Damage");

        UIManager.Instance.CreateDamagePopUp(other.gameObject.transform, damage);
        hitCharacter.ApplyDamage(Owner, damage);
    }

}