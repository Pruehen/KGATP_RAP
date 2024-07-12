using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public int maxHealth = 999;
    public int currentHealth { get; protected set; }
    public bool dead {  get; protected set; }
    public event Action OnDeath;

    public bool IsDead
    {
        get
        {
            if (currentHealth <= 0) return true;
            else return false;
        }
    }

    protected virtual void OnEnable()
    {
        dead = false;
        currentHealth = maxHealth;
    }

    public virtual bool ApplyDamage(DamageMessage damageMsg)
    {
        //데미지를 적용하는 자신이 죽었거나, 데미지를 주는 damager가 자신이었으면 false
        if (damageMsg.damager == gameObject || dead) return false;
        currentHealth -= damageMsg.damage;
        if (currentHealth <= 0) Die();
        return true;
    }

    public virtual void Die()
    {
        if(OnDeath != null) OnDeath();
        dead = true;
    }
}
