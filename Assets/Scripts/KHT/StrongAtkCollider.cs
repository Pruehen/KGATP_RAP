using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongAtkCollider : MonoBehaviour
{
    [Range(1f, 10f)] [SerializeField] int atk = 1;
    [Range(1f, 10f)] [SerializeField] float stun = 2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out LCH.Enemy enemy))
        {
            enemy.Hit(atk);
            enemy.Stun(stun);
            Debug.Log("½ê°Ô¶§¸²");
        }
    }
}
