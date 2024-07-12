using UnityEngine;

public enum ProjectionType
{
    Common,
    Shogun,
    Scatter,
    Sniping
}
public class EnemyWeapon : MonoBehaviour
{
    [Header("투사체 종류 필드")]
    [SerializeField] GameObject Prefab_Projectile;

    [Header("발사 타입 필드")]
    [SerializeField] ProjectionType projectionType;

    [Header("발사 속성 필드")]
    [Range(0, 500)][SerializeField] float speed_Projectile;
    [Range(0, 180)][SerializeField] float projection_Angle;
    [Range(1, 50)][SerializeField] int projection_ea;
    [Range(0, 0.99f)][SerializeField] float projectionSpeed_RandomGain;

    public void CommandFire(Vector3 targetPosition)
    {        
        switch (projectionType)
        {
            case ProjectionType.Common:
                Fire_Common(targetPosition);
                break;
            case ProjectionType.Shogun:
                Fire_ShotGun(targetPosition);
                break;
            case ProjectionType.Scatter:
                Fire_Scatter(targetPosition);
                break;
            case ProjectionType.Sniping:
                Fire_Sniping(targetPosition);
                break;
            default:
                Fire_Common(targetPosition);
                break;
        }
    }

    void Fire_Common(Vector3 target)
    {
        GameObject obj = ObjectPoolManager.Instance.DequeueObject(Prefab_Projectile);

        Vector3 projectionVector = (target - this.transform.position).normalized * speed_Projectile;
        obj.GetComponent<Bullet>().Shoot(this.transform.position, projectionVector);
    }
    void Fire_ShotGun(Vector3 target)
    {
        for (int i = 0; i < projection_ea; i++)
        {
            GameObject obj = ObjectPoolManager.Instance.DequeueObject(Prefab_Projectile);

            float randomProjectionAngle = Random.Range(-projection_Angle * 0.5f, projection_Angle * 0.5f);
            float randomProjecitonVelocity = speed_Projectile * (1 + Random.Range(-projectionSpeed_RandomGain, projectionSpeed_RandomGain));
            Quaternion angle = Quaternion.Euler(0, randomProjectionAngle, 0);

            Vector3 projectionVector = (target - this.transform.position).normalized * randomProjecitonVelocity;
            projectionVector = angle * projectionVector;

            obj.GetComponent<Bullet>().Shoot(this.transform.position, projectionVector);
        }
    }
    void Fire_Scatter(Vector3 target)
    {
        for (int i = 0; i < projection_ea; i++)
        {
            GameObject obj = ObjectPoolManager.Instance.DequeueObject(Prefab_Projectile);

            float projectionAngle = ((float)i / (float)projection_ea) * projection_Angle - projection_Angle * 0.5f;

            Quaternion angle = Quaternion.Euler(0, projectionAngle, 0);

            Vector3 projectionVector = (target - this.transform.position).normalized * speed_Projectile;
            projectionVector = angle * projectionVector;

            obj.GetComponent<Bullet>().Shoot(this.transform.position, projectionVector);
        }
    }
    void Fire_Sniping(Vector3 target)
    {
        GameObject obj = ObjectPoolManager.Instance.DequeueObject(Prefab_Projectile);

        Vector3 projectionVector = (target - this.transform.position).normalized * speed_Projectile;
        obj.GetComponent<Bullet>().Shoot(this.transform.position, projectionVector);

    }
}
