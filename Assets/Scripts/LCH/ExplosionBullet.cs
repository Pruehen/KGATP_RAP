using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bullet
{    
    public override void Shoot(Vector3 initPos, Vector3 projectionVector, float value1, float value2)
    {
        base.Shoot(initPos, projectionVector, value1, value2);
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
