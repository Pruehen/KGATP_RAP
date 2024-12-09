using UnityEngine;

public class PlayerSkillProjectile : MonoBehaviour
{
    [SerializeField] GameObject Prefab_Effect;
    [Range(0f, 1f)][SerializeField] float minRadius = 1f;
    [Range(1f, 10f)][SerializeField] float targetRadius = 3f;
    [Range(0f, 2f)][SerializeField] float offset = 1f;
    SphereCollider _sphereCollider;

    Rigidbody rigidbody;
    float _lifeTime;
    GameObject _effect;    
    public void Init(Vector3 initPos, Quaternion initRotation, float velocity)
    {
        _lifeTime = 0;

        if(_sphereCollider == null)
        {
            _sphereCollider = this.GetComponent<SphereCollider>();            
        }

        _sphereCollider.radius = 0;

        this.transform.position = initPos;
        this.transform.rotation = initRotation;
        this.transform.position += this.transform.forward * offset;
        if(rigidbody == null) rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = this.transform.forward * velocity;

        _effect = EffectManager.Instance.EffectGenerate(Prefab_Effect, this.transform);
    }

    private void Update()
    {
        _lifeTime += Time.deltaTime;
        _sphereCollider.radius = Mathf.Lerp(minRadius, targetRadius, _lifeTime * 2);

        if (_lifeTime > 0.5f)
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
        if(other.gameObject.layer == 10)//CanDestroyObject
        {
            if(other.TryGetComponent(out DestroybleObject obj))
            {
                obj.Hit(4);
            }
        }
    }

    void DestroyObject()
    {
        _effect.transform.SetParent(null);
        ObjectPoolManager.Instance.EnqueueObject(this.gameObject);
    }
}
