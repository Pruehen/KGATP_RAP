using System.Collections;
using UnityEngine;
using System;

namespace LCH
{    
    public class Enemy : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float enemyHp;
        [SerializeField] float atkDamage;        
        [SerializeField] bool isTargeting;        
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
            if (target == null) target = Player.Instance.transform;
            if (weapon == null) weapon = this.GetComponent<EnemyWeapon>();

            if (!isTargeting)
            {
                Nontarget_StartCoroutine_OnStart();
            }
            _animator = GetComponent<Animator>();
            _fireDelayTime = coolTime;
        }
        private void Update()
        {
            if(_isStun)
            {
                OnStun_OnUpdate();
            }

            if (isTargeting)
            {
                FindPlayer_OnUpdate();
                ShootCoolTime_OnTargetingMode_OnUpdate();
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
            if (!isTargeting)
            {
                StartCoroutine(CoolTime());
            }
        }

        IEnumerator CoolTime()
        {
            while (true)
            {
                if (_isStun == false)
                {
                    transform.Rotate(0, 40, 0);

                    weapon.CommandFire(this.gameObject.transform.forward + this.transform.position);
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

            if (enemyHp <= 0)
            {
                Dead();
            }
        }

        void Dead()
        {
            OnDead?.Invoke();
            _animator.enabled = false;
            StopAllCoroutines();
            Debug.Log("적 사망");
            EffectManager.Instance.EffectGenerate(EffectType.Explosion, this.gameObject.transform.position);
            Destroy(this);
        }

        void ShootCoolTime_OnTargetingMode_OnUpdate()
        {
            if (_isStun == false)
            {
                _fireDelayTime -= Time.deltaTime;
            }
            if(_fireDelayTime <= 0)
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
    }
}