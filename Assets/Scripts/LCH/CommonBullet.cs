using UnityEngine;

public class CommonBullet : Bullet
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.Hit(1);
        }
        ProjectileDestroy(collision.contacts[0].point);
    }

    void ProjectileDestroy(Vector3 destroyPos)
    {
        EffectManager.Instance.EffectGenerate(EffectType.BulletDestroy, destroyPos);
        ObjectPoolManager.Instance.EnqueueObject(this.gameObject);
    }
}