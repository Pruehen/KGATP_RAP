using UnityEngine;

public class IncreaseBullet : Bullet
{
    [SerializeField] private float size_IncreasePercent_PerSec;//10
    [SerializeField] private float size_MaxPercent;//100

    Vector3 initScale;
    float activeTime = 0;

    public override void Shoot(Vector3 initPos, Vector3 projectionVector)
    {
        base.Shoot(initPos, projectionVector);
        this.gameObject.transform.localScale = Vector3.one;
        initScale = this.transform.localScale;
        activeTime = 0;
    }
    private void Update()
    {
        activeTime += Time.deltaTime;
        this.gameObject.transform.localScale = initScale * (1 + (Mathf.Clamp(activeTime * size_IncreasePercent_PerSec, 0, size_MaxPercent) / 100));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
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
