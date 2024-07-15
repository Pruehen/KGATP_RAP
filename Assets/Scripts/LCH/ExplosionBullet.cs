using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bullet
{    
    public override void Shoot(Vector3 initPos, Vector3 projectionVector)
    {
        base.Shoot(initPos, projectionVector);
    }
    private void OnCollisionEnter(Collision collision)
    {
        ProjectileDestroy(collision.contacts[0].point);
    }
    public override void ProjectileDestroy(Vector3 destroyPos)
    {
        EffectManager.Instance.EffectGenerate(EffectType.Explosion, destroyPos);
        ObjectPoolManager.Instance.EnqueueObject(this.gameObject);
    }
}
