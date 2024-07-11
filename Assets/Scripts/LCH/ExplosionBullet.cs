using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bullet
{
    [SerializeField] GameObject explosionPrefab;
    public override void Shoot(Transform target, Vector3 initPos)
    {
        base.Shoot(target, initPos);
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = Instantiate(explosionPrefab);
        
    }
}
