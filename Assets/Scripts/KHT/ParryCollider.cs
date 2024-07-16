using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryCollider : MonoBehaviour
{
    [SerializeField] kjh.PlayerSkill playerSkill;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Bullet bullet))
        {
            playerSkill.Command_Parrying();
            Player.Instance.evasion_coolTimeValue = 0;
            this.gameObject.SetActive(false);
        }
    }
}
