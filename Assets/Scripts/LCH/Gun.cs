using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] GameObject bulletPrefab;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = ObjectPoolManager.Instance.DequeueObject(bulletPrefab);
            obj.GetComponent<CommonBullet>().Shoot(Target, this.transform.position);
        }
    }
}
