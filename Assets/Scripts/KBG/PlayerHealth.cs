using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    public float lastDamagedTime;
    public float invincibleTime;

    //������ �޾��� �� ����
    protected bool IsDamagedInvincible
    {
        get
        {
            return (Time.time <= lastDamagedTime + invincibleTime);
        }
    }

    //�ʻ�� ������ ����
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
            Debug.Log("�÷��̾� ����");
            return false;
        }

        lastDamagedTime = Time.time;
        invincibleTime = 2;
        currentHealth -= damageMsg.damage;
        Debug.Log("Player�� " + damageMsg.damage + "����");
        

        if (currentHealth <= 0) Die();
        return true;
    }

    public override void Die()
    {
        base.Die();
        Debug.Log("Player ����");
    }
}
