using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{    
    [SerializeField] private float bulletSize;
    [SerializeField] private bool isCanParry;
    [SerializeField] protected GameObject Prefab_DestroyEffect;
    protected int dmg;

    public float BulletSize
    {
        get { return bulletSize; }
        set { bulletSize = value; }
    }

    public bool IsCanParry
    {
        get { return isCanParry; }
        set { isCanParry = value; }
    }


    //projectionVector�� �������� �߻�
    //public void Shoot(Transform target, Vector3 initPos, Vector3 projectionVector, float bulletSpeed)
    //{


    //    this.transform.position = initPos;
        
    //    this.GetComponent<Rigidbody>().velocity = projectionVector.normalized * bulletSpeed;
    //}
    //Ÿ�� ��ġ�� �߻�
    public virtual void Shoot(Vector3 initPos, Vector3 projectionVector, float value1, float value2, int dmg)
    {
        this.dmg = dmg;
        this.transform.position = initPos;
        this.GetComponent<Rigidbody>().velocity = projectionVector;
    }

    public abstract void ProjectileDestroy(Vector3 destroyPos);
    public void ProjectileDestroy()
    {
        ProjectileDestroy(this.transform.position);
    }
}
