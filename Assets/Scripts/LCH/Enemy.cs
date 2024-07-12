using System.Collections;
using UnityEngine;
namespace LCH
{    
    public class Enemy : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float enemyHp;
        [SerializeField] float atkDamage;
        [SerializeField] bool isCanFire;
        [SerializeField] bool isTargeting;
        [SerializeField] bool isStun;
        [Range(0.1f, 10f)][SerializeField] float coolTime;
        [Header("ÀåÂø ¹«±â")]
        [SerializeField] EnemyWeapon weapon;

        private void Start()
        {
            if (!isTargeting)
            {
                Nontarget_StartCoroutine_OnStart();
            }
            StartCoroutine(ShootCoolTime());
        }
        private void Update()
        {
            if (isTargeting)
            {
                FindPlayer_OnUpdate();
            }
            if (enemyHp <= 0)
            {
                Die_OnUpdate();
            }

            if(Input.GetKeyDown(KeyCode.F))
            {
                weapon.CommandFire(target.transform.position);
            }
        }

        void FindPlayer_OnUpdate()
        {
            transform.LookAt(target);

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
                transform.Rotate(0, 15, 0);
                yield return new WaitForSeconds(5);
            }
        }
        void Die_OnUpdate()
        {
            //ragdoll
        }
        void Stun_OnUpdate()
        {

        }
        IEnumerator ShootCoolTime()
        {
            while (true)
            {
                weapon.CommandFire(target.transform.position);
                yield return new WaitForSeconds(coolTime);
            }
            
        }
    }
}