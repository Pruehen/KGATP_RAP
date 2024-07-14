using UnityEngine;

public class CommonBulletCopy : Bullet
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealth health))
        {
            DamageMessage dmgMessage = new DamageMessage();
            dmgMessage.damage = 1;

            health.ApplyDamage(dmgMessage);
        }
        ProjectileDestroy(collision.contacts[0].point);
    }

    void ProjectileDestroy(Vector3 destroyPos)
    {
        EffectManager.Instance.EffectGenerate(EffectType.BulletDestroy, destroyPos);
        ObjectPoolManager.Instance.EnqueueObject(this.gameObject);
    }
}
