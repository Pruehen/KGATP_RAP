using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bullet
{
    [Header("Æø¹ß ¹üÀ§")]
    [Range(0, 10)][SerializeField] float size_Effect = 3;
    public override void Shoot(Vector3 initPos, Vector3 projectionVector, float value1, float value2, int dmg)
    {
        base.Shoot(initPos, projectionVector, value1, value2, dmg);
    }
    private void OnCollisionEnter(Collision collision)
    {
        ProjectileDestroy(collision.contacts[0].point);
    }
    public override void ProjectileDestroy(Vector3 destroyPos)
    {
        Collider[] colliders = Physics.OverlapSphere(destroyPos, size_Effect);
        foreach (Collider collider in colliders)
        {
            if(collider.TryGetComponent(out Player player))
            {
                player.Hit(dmg);
                break;
            }
        }

        EffectManager.Instance.EffectGenerate(EffectType.Explosion, destroyPos);
        ObjectPoolManager.Instance.EnqueueObject(this.gameObject);
    }
}
