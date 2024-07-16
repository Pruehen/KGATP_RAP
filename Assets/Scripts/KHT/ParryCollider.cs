using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryCollider : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] kjh.PlayerSkill playerSkill;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Bullet bullet))
        {
            if (bullet.IsCanParry)
            {
                playerSkill.Command_Parrying();
                player.OnParrying();
                this.gameObject.SetActive(false);
            }

        }
    }
}
