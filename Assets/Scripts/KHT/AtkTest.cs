using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkTest : MonoBehaviour
{
    [Range(0f, 100f)] [SerializeField] float PushPower;
    private void OnTriggerEnter(Collider other)
    {
        //if(other.TryGetComponent(out Enemy enemy))
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("РћСп");
            other.attachedRigidbody.AddForce(transform.forward * PushPower, ForceMode.Impulse);
        }
    }
}
