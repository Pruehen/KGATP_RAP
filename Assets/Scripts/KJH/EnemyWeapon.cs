using System.Collections;
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
    [Header("����ü ���� �ʵ�")]
    [SerializeField] GameObject Prefab_Projectile;
    [Header("���� źȯ �Ű� ���� �ʵ�")]
    [SerializeField] float size_IncreasePercent_PerSec = 33;
    [SerializeField] float size_MaxPercent = 200;

    [Header("�߻� Ÿ�� �ʵ�")]
    [SerializeField] ProjectionType projectionType;

    [Header("�߻� �Ӽ� �Ϲ� �ʵ�")]
    [Range(0, 500)][SerializeField] float speed_Projectile = 10;    

    [Header("����, ��ä�� �ʵ�")]
    [Range(0, 360)][SerializeField] float projection_Angle = 30;
    [Range(1, 100)][SerializeField] int projection_ea = 5;

    [Header("���� �ʵ�")]
    [Range(0, 0.99f)][SerializeField] float projectionSpeed_RandomGain = 0.1f;

    [Header("�������� �ʵ�")]
    [Range(0.1f, 5)][SerializeField] float bulletChargingTime = 1f;
    [Range(0.5f, 5)][SerializeField] float lineWidth = 1f;

    [SerializeField] Transform Transform_FirePoint;
    LineRenderer lineRenderer;

    public void CommandFire(Vector3 targetPosition)
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.enabled = false;
        }

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
    public void CommandFire(Transform targetTransform)
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.enabled = false;
        }

        switch (projectionType)
        {
            case ProjectionType.Common:
                Fire_Common(targetTransform.position);
                break;
            case ProjectionType.Shogun:
                Fire_ShotGun(targetTransform.position);
                break;
            case ProjectionType.Scatter:
                Fire_Scatter(targetTransform.position);
                break;
            case ProjectionType.Sniping:
                Fire_Sniping(targetTransform);
                break;
            default:
                Fire_Common(targetTransform.position);
                break;
        }
    }

    void Fire_Common(Vector3 target)
    {
        GameObject obj = ObjectPoolManager.Instance.DequeueObject(Prefab_Projectile, Transform_FirePoint.position);

        Vector3 projectionVector = (target - Transform_FirePoint.position).normalized * speed_Projectile;
        obj.GetComponent<Bullet>().Shoot(Transform_FirePoint.position, projectionVector, size_IncreasePercent_PerSec, size_MaxPercent);
    }
    void Fire_ShotGun(Vector3 target)
    {
        for (int i = 0; i < projection_ea; i++)
        {
            GameObject obj = ObjectPoolManager.Instance.DequeueObject(Prefab_Projectile, Transform_FirePoint.position);

            float randomProjectionAngle = Random.Range(-projection_Angle * 0.5f, projection_Angle * 0.5f);
            float randomProjecitonVelocity = speed_Projectile * (1 + Random.Range(-projectionSpeed_RandomGain, projectionSpeed_RandomGain));
            Quaternion angle = Quaternion.Euler(0, randomProjectionAngle, 0);

            Vector3 projectionVector = (target - Transform_FirePoint.position).normalized * randomProjecitonVelocity;
            projectionVector = angle * projectionVector;

            obj.GetComponent<Bullet>().Shoot(Transform_FirePoint.position, projectionVector, size_IncreasePercent_PerSec, size_MaxPercent);
        }
    }
    void Fire_Scatter(Vector3 target)
    {
        for (int i = 0; i < projection_ea; i++)
        {
            GameObject obj = ObjectPoolManager.Instance.DequeueObject(Prefab_Projectile, Transform_FirePoint.position);

            float projectionAngle = ((float)i / (float)projection_ea) * projection_Angle - projection_Angle * 0.5f;

            Quaternion angle = Quaternion.Euler(0, projectionAngle, 0);

            Vector3 projectionVector = (target - Transform_FirePoint.position).normalized * speed_Projectile;
            projectionVector = angle * projectionVector;

            obj.GetComponent<Bullet>().Shoot(Transform_FirePoint.position, projectionVector, size_IncreasePercent_PerSec, size_MaxPercent);
        }
    }
    void Fire_Sniping(Vector3 targetPos)
    {
        StartCoroutine(BulletCharging_Sniping(targetPos));
    }
    void Fire_Sniping(Transform targetTrf)
    {
        StartCoroutine(BulletCharging_Sniping(targetTrf));
    }

    IEnumerator BulletCharging_Sniping(Transform targetTrf)
    {
        float chargeTime = 0;
        lineRenderer.enabled = true;
        while (chargeTime < bulletChargingTime)
        {
            Vector3 origin = this.transform.position + new Vector3(0, 0.5f, 0);
            Vector3 targetPos = targetTrf.position + new Vector3(0, 0.5f, 0);
            Vector3 dir = (targetPos - origin).normalized;

            lineRenderer.SetPosition(0, origin);
            Ray ray = new Ray(origin, dir);
            
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit,1000, LayerMask.GetMask("NonDestroyObject", "Player")))
            {

                lineRenderer.SetPosition(1, hit.point);
                int hitLayer = hit.collider.gameObject.layer;
                //Debug.Log("Hit layer: " + LayerMask.LayerToName(hitLayer));
            }
            else
            {
                lineRenderer.SetPosition(1, targetPos);
            }         
            
            lineRenderer.widthMultiplier = (1 - (chargeTime / bulletChargingTime)) * lineWidth;
            chargeTime += Time.deltaTime;
            yield return null;
        }

        lineRenderer.enabled = false;
        Fire_Common(targetTrf.position);
    }
    IEnumerator BulletCharging_Sniping(Vector3 targetPos)
    {
        float chargeTime = 0;
        Vector3 origin = this.transform.position + new Vector3(0, 0.5f, 0);
        targetPos = targetPos + new Vector3(0, 0.5f, 0);
        Vector3 dir = (targetPos - origin).normalized;

        lineRenderer.SetPosition(0, origin);
        Ray ray = new Ray(origin, dir);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            lineRenderer.SetPosition(1, hit.point);
            int hitLayer = hit.collider.gameObject.layer;
            Debug.Log("Hit layer: " + LayerMask.LayerToName(hitLayer));
        }
        else
        {
            lineRenderer.SetPosition(1, targetPos);
        }
        while (chargeTime < bulletChargingTime)
        {
            lineRenderer.widthMultiplier = (1 - (chargeTime / bulletChargingTime)) * lineWidth;
            chargeTime += Time.deltaTime;
            yield return null;
        }

        lineRenderer.enabled = false;
        Fire_Common(targetPos);
    }
}
