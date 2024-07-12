using System.Collections;
using UnityEngine;

public enum EffectType
{ 
    Explosion,
    BulletDestroy
}
public class EffectManager : SceneSingleton<EffectManager>
{
    [SerializeField] GameObject Prefab_ExplosionEffect;
    [SerializeField] GameObject Prefab_BulletDestroyEffect;

    public void EffectGenerate(GameObject prefab, Vector3 position)
    {
        GameObject effect = ObjectPoolManager.Instance.DequeueObject(prefab);
        effect.transform.position = position;
        effect.transform.rotation = Quaternion.identity;        
        effect.GetComponent<ParticleSystem>().Play();

        StartCoroutine(EffectEnqueue(effect, 5));
    }

    public void EffectGenerate(EffectType effectType, Vector3 pos)
    {
        switch (effectType)
        {
            case EffectType.Explosion:
                EffectGenerate(Prefab_ExplosionEffect, pos);
                break;
            case EffectType.BulletDestroy:
                EffectGenerate(Prefab_BulletDestroyEffect, pos);
                break;
            default:
                break;
        }
    }

    IEnumerator EffectEnqueue(GameObject item, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        ObjectPoolManager.Instance.EnqueueObject(item);
    }
}
