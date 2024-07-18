using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryCollider : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] kjh.PlayerSkill_Parrying playerSkill;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Bullet bullet))
        {
            if (bullet.IsCanParry)
            {
                player.OnParrying();
                this.gameObject.SetActive(false);
                Player.Instance.playerSound.Play_ParryingSound();
            }
        }
    }
}
