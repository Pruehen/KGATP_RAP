using UnityEngine;

public class PlayerSkillProjectile : MonoBehaviour
{
    Rigidbody rigidbody;
    float _accumulateDistance;
    float _range;
    float _velocity;
    public void Init(Vector3 initPos, Quaternion initRotation, float velocity, float range)
    {
        _accumulateDistance = 0;
        _range = range;
        _velocity = velocity;

        this.transform.position = initPos;
        this.transform.rotation = initRotation;
        if(rigidbody == null) rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = this.transform.forward * velocity;
    }

    private void FixedUpdate()
    {
        _accumulateDistance += _velocity * Time.fixedDeltaTime;

        if (_accumulateDistance > _range)
        {
            DestroyObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        if(other.gameObject.layer == 7)//Enemy
        {
            if(other.TryGetComponent(out LCH.Enemy enemy))
            {
                enemy.Stun(3);
            }
        }
        if(other.gameObject.layer == 9)//NonDestroyObject
        {
            DestroyObject();
        }
        if (other.gameObject.layer == 6)//Projectile
        {
            if(other.TryGetComponent(out Bullet bullet))
            {
                bullet.ProjectileDestroy();
            }
        }
    }

    void DestroyObject()
    {
        ObjectPoolManager.Instance.EnqueueObject(this.gameObject);
    }
}
