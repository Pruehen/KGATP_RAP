using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk1Collider : MonoBehaviour
{
    [Range(1f, 10f)] [SerializeField] int atk = 1;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out LCH.Enemy enemy))
        {
            enemy.Hit(atk);
            Debug.Log("¶§¸²1");
        }
    }
}
