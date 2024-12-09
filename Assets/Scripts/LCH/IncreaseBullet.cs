using UnityEngine;

public class IncreaseBullet : Bullet
{
    float _size_IncreasePercent_PerSec;
    float _size_MaxPercent;

    Vector3 initScale;
    float activeTime = 0;

    public override void Shoot(Vector3 initPos, Vector3 projectionVector, float value1, float value2, int dmg)
    {
        base.Shoot(initPos, projectionVector, value1, value2, dmg);
        this.gameObject.transform.localScale = Vector3.one;
        initScale = this.transform.localScale;
        activeTime = 0;

        _size_IncreasePercent_PerSec = value1;
        _size_MaxPercent = value2;
    }
    private void Update()
    {
        activeTime += Time.deltaTime;
        this.gameObject.transform.localScale = initScale * (1 + (Mathf.Clamp(activeTime * _size_IncreasePercent_PerSec, 0, _size_MaxPercent) / 100));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.Hit(dmg);            
        }

        ProjectileDestroy(collision.contacts[0].point);
    }
    public override void ProjectileDestroy(Vector3 destroyPos)
    {
        EffectManager.Instance.EffectGenerate(EffectType.BulletDestroy, destroyPos);
        ObjectPoolManager.Instance.EnqueueObject(this.gameObject);
    }
}
