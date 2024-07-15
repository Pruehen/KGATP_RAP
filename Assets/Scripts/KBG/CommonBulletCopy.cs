using UnityEngine;

public class CommonBulletCopy : Bullet
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerCopy2 player))
        {
            /*
            DamageMessage dmgMessage = new DamageMessage();
            dmgMessage.damage = 1;

            health.ApplyDamage(dmgMessage);
            */
            player.Hit(1);
        }
        ProjectileDestroy(collision.contacts[0].point);
    }

    public override void ProjectileDestroy(Vector3 destroyPos)
    {
        EffectManager.Instance.EffectGenerate(EffectType.BulletDestroy, destroyPos);
        ObjectPoolManager.Instance.EnqueueObject(this.gameObject);
    }
}
