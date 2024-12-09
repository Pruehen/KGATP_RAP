using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : Bullet
{
    [SerializeField] int bounce_num;
    int _bounce_count;

    public override void Shoot(Vector3 initPos, Vector3 projectionVector, float value1, float value2, int dmg)
    {
        _bounce_count = 0;
        base.Shoot(initPos, projectionVector, value1, value2, dmg);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        _bounce_count++;        
        if(_bounce_count == bounce_num)
        {
            ProjectileDestroy(collision.contacts[0].point);
        }

        if(collision.gameObject.TryGetComponent(out Player player))
        {
            player.Hit(dmg);
            ProjectileDestroy(collision.contacts[0].point);
        }
    }

    public override void ProjectileDestroy(Vector3 destroyPos)
    {
        EffectManager.Instance.EffectGenerate(EffectType.BulletDestroy, destroyPos);
        ObjectPoolManager.Instance.EnqueueObject(this.gameObject);
    }
}
