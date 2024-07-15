using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : Bullet
{
    [SerializeField] int bounce_num;

    public override void Shoot(Vector3 initPos, Vector3 projectionVector)
    {
        base.Shoot(initPos, projectionVector);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        bounce_num--;        
        if(bounce_num == 0)
        {
            ProjectileDestroy(collision.contacts[0].point);
        }

        if(collision.gameObject.TryGetComponent(out Player player))
        {
            player.Hit(1);
            ProjectileDestroy(collision.contacts[0].point);
        }
    }

    public override void ProjectileDestroy(Vector3 destroyPos)
    {
        EffectManager.Instance.EffectGenerate(EffectType.BulletDestroy, destroyPos);
        ObjectPoolManager.Instance.EnqueueObject(this.gameObject);
    }
}
