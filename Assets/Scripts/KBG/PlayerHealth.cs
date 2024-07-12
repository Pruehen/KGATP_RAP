using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    protected override void OnEnable()
    {
        maxHealth = DataManager.Instance.LoadedPlayerCharacterList[101].HP;
        base.OnEnable();
    }

    public override bool ApplyDamage(DamageMessage damageMsg)
    {
        if(!base.ApplyDamage(damageMsg)) return false;

        Debug.Log("Player¿¡ " + damageMsg.damage + "ÇÇÇØ");
        return true;
    }

    public override void Die()
    {
        base.Die();
        Debug.Log("Player Á×À½");
    }
}
