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
        [SerializeField] bool isStun;
        [Range(0.1f, 10f)][SerializeField] float coolTime;
        [Header("장착 무기")]
        [SerializeField] EnemyWeapon weapon;
          
        private bool _isCanFire;
        private Animator _animator;

        Action OnHit;
        Action OnDead;

        private void Start()
        {
            if (target == null) target = Player.Instance.transform;
            if (weapon == null) weapon = this.GetComponent<EnemyWeapon>();

            if (!isTargeting)
            {
                Nontarget_StartCoroutine_OnStart();
            }
            else
            {
                StartCoroutine(ShootCoolTime());
            }
            _animator = GetComponent<Animator>();
        }
        private void Update()
        {
            if (isTargeting)
            {
                FindPlayer_OnUpdate();
            }

            if(Input.GetKeyDown(KeyCode.F))
            {
                weapon.CommandFire(target.transform.position);
            }
        }

        void FindPlayer_OnUpdate()
        {
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(targetPosition);


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
               transform.Rotate(0, 40, 0);
                
               weapon.CommandFire(this.gameObject.transform.forward + this.transform.position);
               yield return new WaitForSeconds(coolTime);
            }  
        }
        
        void Stun_OnUpdate()
        {

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

        IEnumerator ShootCoolTime()
        {
            while (true)
            {
                weapon.CommandFire(target);
                yield return new WaitForSeconds(coolTime);
            }
        }
        
    }
}