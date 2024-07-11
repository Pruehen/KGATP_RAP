using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{    
    [SerializeField] private float bulletSpeed;    
    [SerializeField] private float bulletSize;
    [SerializeField] private bool isCanParry;

   

    public float BulletSpeed
    {
        get { return bulletSpeed; }
        set { bulletSpeed = value; }
    }

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


    //projectionVector의 방향으로 발사
    public void Shoot(Transform target, Vector3 initPos, Vector3 projectionVector)
    {


        this.transform.position = initPos;
        
        this.GetComponent<Rigidbody>().velocity = projectionVector.normalized * bulletSpeed;
    }
    //타겟 위치로 발사
    public virtual void Shoot(Transform target, Vector3 initPos)
    {


        this.transform.position = initPos;

        this.GetComponent<Rigidbody>().velocity = (target.position - this.transform.position).normalized * bulletSpeed;
    }
}
