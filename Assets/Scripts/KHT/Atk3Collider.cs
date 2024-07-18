using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk3Collider : MonoBehaviour
{
    [Range(1f, 10f)] [SerializeField] int atk = 1;
    [Range(1f, 10f)] [SerializeField] float stun = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out LCH.Enemy enemy))
        {
            enemy.Hit(atk);
            enemy.Stun(stun);
            Debug.Log("¶§¸²3");
            Player.Instance.playerSound.Play_AttackSound(3);
        }
        else if (other.TryGetComponent(out DestroybleObject obj))
        {
            obj.Hit(atk);
            Debug.Log("obj");
            Player.Instance.playerSound.Play_AttackSound(3);
        }
    }
}
