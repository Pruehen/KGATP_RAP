
using Unity.VisualScripting;
using UnityEngine;


public class DestroybleObject : MonoBehaviour
{
    private int _hp = 4;

    void DestroyObject()
    {        
        this.GetComponent<BoxCollider>().enabled = false;
        while (this.transform.childCount > 0)
        {
            Transform child = this.transform.GetChild(0);
            child.SetParent(null);

            child.GetComponent<BoxCollider>().enabled = true;
            Rigidbody childRb = child.AddComponent<Rigidbody>();
            childRb.useGravity = true;            
            childRb.AddForce(UnityEngine.Random.onUnitSphere * 2f, ForceMode.VelocityChange);
            childRb.angularDrag = 0;
            childRb.AddTorque(UnityEngine.Random.onUnitSphere * 0.2f, ForceMode.VelocityChange);
            Destroy(child.gameObject, UnityEngine.Random.Range(5f, 8f));
        }

        Destroy(this.gameObject, 8);
    }
    public void Hit(int dmg)
    {
        _hp -= dmg;
        EffectManager.Instance.EffectGenerate(EffectType.Hit, this.transform.position);
        if (_hp <= 0)
        {
            DestroyObject();
        }
    }
}
