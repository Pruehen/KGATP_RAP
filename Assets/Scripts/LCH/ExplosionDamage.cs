using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            player.Hit(1);
            Debug.Log("Æø¹ßµ©");
            Debug.Log(player.Hp);
        }
    }
}
