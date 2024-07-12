using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : Bullet
{
    [SerializeField] int bounce_num;

    public override void Shoot(Vector3 initPos, Vector3 projectionVector)
    {
        base.Shoot(initPos, projectionVector);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        bounce_num--;
        Debug.Log(bounce_num);
        if(bounce_num == 0)
        {
            Debug.Log("바운스그만");
            this.gameObject.SetActive(false);
        }
    }
}
