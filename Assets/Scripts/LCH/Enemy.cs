using System.Collections;
using UnityEngine;
using System;
public enum EnemyFireType
{
    PlayerTargeting,
    RotateTargeting,
    FixedPointTargeting
}
namespace LCH
{    
    public class Enemy : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float enemyHp;        

        [Header("발사 패턴 관련 속성")]
        [SerializeField] EnemyFireType enemyFireType;
        [Range(0f, 360f)][SerializeField] float autoRotateAngle = 45f;

        [SerializeField] GameObject enemyHitEffect;
        [Range(0.1f, 10f)][SerializeField] float coolTime;
        [Header("장착 무기")]
        [SerializeField] EnemyWeapon weapon;
          
        private bool _isCanFire;
        private Animator _animator;
        bool _isStun;
        float _fireDelayTime;
        float _stunTime;

        Action OnHit;
        Action OnStun;
        Action OnDead;

        private void Start()
        {
            if (target == null && Player.Instance != null) target = Player.Instance.transform;
            if (weapon == null) weapon = this.GetComponent<EnemyWeapon>();

            if (enemyFireType == EnemyFireType.RotateTargeting)
            {
                Nontarget_StartCoroutine_OnStart();
            }
            if(enemyFireType == EnemyFireType.FixedPointTargeting)
            {
                //FixedTarget_StartCoroutine_OnStart();
            }
            _animator = GetComponent<Animator>();
            _fireDelayTime = coolTime;
            EnemyManager.Instance.AddDic(this.gameObject.GetInstanceID(), this);
        }
        private void Update()
        {
            if(_isStun)
            {
                OnStun_OnUpdate();
            }

            switch (enemyFireType)
            {
                case EnemyFireType.PlayerTargeting:
                    FindPlayer_OnUpdate();
                    ShootCoolTime_OnTargetingMode_OnUpdate();
                    break;
                case EnemyFireType.RotateTargeting:
                    ShootCoolTime_NoneTargetingMode_OnUpdate();
                    break;
                case EnemyFireType.FixedPointTargeting:
                    ShootCoolTime_NoneTargetingMode_OnUpdate();
                    break;
                default:
                    break;
            }
        }

        void FindPlayer_OnUpdate()
        {
            if (_isStun == false)
            {
                Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
                transform.LookAt(targetPosition);
            }
        }
        void Nontarget_StartCoroutine_OnStart()
        {
            if (enemyFireType == EnemyFireType.RotateTargeting)
            {
                StartCoroutine(Rotate());
            }
        }
        /*void FixedTarget_StartCoroutine_OnStart()
        {
            if(enemyFireType == EnemyFireType.FixedPointTargeting)
            {
                StartCoroutine(FixedCoolTime());
            }
        }*/

        IEnumerator Rotate()
        {
            while (true)
            {
                if (_isStun == false)
                {
                    transform.Rotate(0, autoRotateAngle, 0);
                }
                yield return new WaitForSeconds(coolTime);
            }
        }


        public void Stun(float duration)
        {
            _isStun = true;
            _stunTime = duration;
            OnStun?.Invoke();
        }        

        /// <summary>
        /// 피격 메서드
        /// </summary>
        /// <param name="dmg"></param>
        public void Hit(int dmg)
        {
            enemyHp -= dmg;
            OnHit?.Invoke();
            //OnHpChange?.Invoke(Hp);
            EffectManager.Instance.EffectGenerate(enemyHitEffect, this.gameObject.transform.position);
            if (enemyHp <= 0)
            {
                Dead();
            }
        }

        void Dead()
        {
            OnDead?.Invoke();
            GetComponent<CapsuleCollider>().enabled = false;
            _animator.enabled = false;
            StopAllCoroutines();
            Debug.Log("적 사망");
            EffectManager.Instance.EffectGenerate(EffectType.EnemyDie, this.gameObject.transform.position);
            EnemyManager.Instance.DestroyDic(this.gameObject.GetInstanceID());
            Destroy(this);
        }

        void ShootCoolTime_NoneTargetingMode_OnUpdate()
        {
            if (_isStun == false)
            {
                _fireDelayTime -= Time.deltaTime;
            }
            if(_fireDelayTime <= 0)
            {
                _fireDelayTime = coolTime;
                weapon.CommandFire(this.transform.forward + this.transform.position);
            }            
        }
        void ShootCoolTime_OnTargetingMode_OnUpdate()
        {
            if (_isStun == false)
            {
                _fireDelayTime -= Time.deltaTime;
            }
            if (_fireDelayTime <= 0)
            {
                _fireDelayTime = coolTime;
                weapon.CommandFire(target);
            }
        }

        void OnStun_OnUpdate()
        {            
            _stunTime -= Time.deltaTime;
            if(_stunTime <= 0)
            {
                _isStun = false;
            }
        }

        public float GetFireDelayRatio()
        {
            return 1 - (_fireDelayTime / coolTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Hit(10);
        }
    }
}