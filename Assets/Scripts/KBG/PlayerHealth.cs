using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    public float lastDamagedTime;
    public float invincibleTime;

    //데미지 받았을 때 무적
    protected bool IsDamagedInvincible
    {
        get
        {
            return (Time.time <= lastDamagedTime + invincibleTime);
        }
    }

    //필살기 등으로 무적
    protected bool IsInvincible;


    protected override void OnEnable()
    {
        //maxHealth = DataManager.Instance.LoadedPlayerCharacterList[101].HP;
        maxHealth = 10;
        lastDamagedTime = 0;
        invincibleTime = 0;
        base.OnEnable();
    }


    public override bool ApplyDamage(DamageMessage damageMsg)
    {
        if(!base.ApplyDamage(damageMsg)) return false;


        if (IsInvincible || IsDamagedInvincible)
        {
            Debug.Log("플레이어 무적");
            return false;
        }

        lastDamagedTime = Time.time;
        invincibleTime = 2;
        currentHealth -= damageMsg.damage;
        Debug.Log("Player에 " + damageMsg.damage + "피해");
        

        if (currentHealth <= 0) Die();
        return true;
    }

    public override void Die()
    {
        base.Die();
        Debug.Log("Player 죽음");
    }
}
