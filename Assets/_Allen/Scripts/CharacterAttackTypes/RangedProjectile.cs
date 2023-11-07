using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RangedProjectile : MonoBehaviour
{
    public GameObject Owner { get; private set; }

    Rigidbody rBody;
    Vector3 startPos = Vector3.zero;

    float distanceTravelled = 0f;
    float maxDistance = 1f;

    public void Init(GameObject owner, float projectileSpeed, float maxDistance)
    {
        Owner = owner;

        this.maxDistance = maxDistance;
        startPos = transform.position;

        rBody = GetComponent<Rigidbody>();

        rBody.AddForce(transform.forward * projectileSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
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
    }

}