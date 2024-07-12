using UnityEngine;

public class IncreaseBullet : Bullet
{
    [SerializeField] private float size_IncreasePercent_PerSec;//10
    [SerializeField] private float size_MaxPercent;//100

    Vector3 initScale;
    float activeTime = 0;

    public override void Shoot(Transform target, Vector3 initPos, float bulletSpeed)
    {
        initScale = this.transform.localScale;
        activeTime = 0;
    }
    private void Update()
    {
        activeTime += Time.deltaTime;
        this.gameObject.transform.localScale = initScale * (1 + (Mathf.Clamp(activeTime * size_IncreasePercent_PerSec, 0, size_MaxPercent) / 100));
    }
}
