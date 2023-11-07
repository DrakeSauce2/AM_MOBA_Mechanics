using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

[CreateAssetMenu(menuName = "BasicAttacks/Ranged Attack")]
public class RangedAttack : BasicAttack
{
    [SerializeField] private GameObject attackPrefab;

    private float projectileSpeed = 1000f;
    private Vector3 spawnPoint = new Vector3 (0, 0, 0.5f);

    public override void Init(GameObject owner)
    {
        Owner = owner;
    }

    public override void Attack()
    {
        GameObject spawnedobj = Instantiate(attackPrefab, Owner.transform.position + spawnPoint, Owner.transform.rotation);

        RangedProjectile projectile = spawnedobj.GetComponent<RangedProjectile>();
        projectile.Init(Owner, projectileSpeed, Range);

    }

}