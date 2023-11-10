using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

[CreateAssetMenu(menuName = "BasicAttacks/Ranged Attack")]
public class RangedAttack : BasicAttack
{
    [SerializeField] private GameObject attackPrefab;
    [Header("Damage")]
    [SerializeField, Range(1, 2)] private float damageScale;
    private float damage;

    private float projectileSpeed = 1000f;
    private Vector3 spawnPoint = new Vector3 (0, 0, 0.5f);

    public override void Init(GameObject owner)
    {
        Owner = owner;
        damage = Owner.GetComponent<Character>().GetStats().TryGetStatValue(Stat.BASICATTACKDAMAGE);
    }

    public override void Attack()
    {
        GameObject spawnedobj = Instantiate(attackPrefab, Owner.transform.position, Owner.transform.rotation);

        RangedProjectile projectile = spawnedobj.GetComponent<RangedProjectile>();
        projectile.Init(Owner, projectileSpeed, Range, damage);
    }

}