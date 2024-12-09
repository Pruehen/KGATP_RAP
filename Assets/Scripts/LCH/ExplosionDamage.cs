using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ExplosionDamage : MonoBehaviour
{
    [Header("���� ����")]
    [Range(0, 3)][SerializeField] float size_Effect = 0;
    private void Start()
    {
        this.gameObject.transform.localScale = new Vector3(size_Effect, size_Effect,size_Effect);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            player.Hit(1);
            Debug.Log("���ߵ�");
            Debug.Log(player.Hp);
        }
    }
}
