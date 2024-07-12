using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bullet
{
    [SerializeField] GameObject explosionPrefab;
    public override void Shoot(Vector3 initPos, Vector3 projectionVector)
    {
        base.Shoot(initPos, projectionVector);
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = Instantiate(explosionPrefab);
        
    }
}
