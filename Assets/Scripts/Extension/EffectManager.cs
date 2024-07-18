using System.Collections;
using UnityEngine;

public enum EffectType
{ 
    Explosion,
    BulletDestroy,
    EnemyDie,
    Hit
}
public class EffectManager : SceneSingleton<EffectManager>
{
    [SerializeField] GameObject Prefab_ExplosionEffect;
    [SerializeField] GameObject Prefab_BulletDestroyEffect;
    [SerializeField] GameObject Prefab_EnemyDie_Effect;
    [SerializeField] GameObject Prefab_Hit_Effect;

    public void EffectGenerate(GameObject prefab, Vector3 position)
    {
        GameObject effect = ObjectPoolManager.Instance.DequeueObject(prefab);
        effect.transform.position = position;
        effect.transform.rotation = Quaternion.identity;        
        effect.GetComponent<ParticleSystem>().Play();

        StartCoroutine(EffectEnqueue(effect, 5));
    }
    public GameObject EffectGenerate(GameObject prefab, Transform targetTrf)
    {
        GameObject effect = ObjectPoolManager.Instance.DequeueObject(prefab);
        effect.transform.SetParent(targetTrf);
        effect.transform.position = targetTrf.position;
        effect.transform.rotation = Quaternion.identity;
        effect.GetComponent<ParticleSystem>().Play();

        StartCoroutine(EffectEnqueue(effect, 5));
        return effect;
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
            case EffectType.EnemyDie:
                EffectGenerate(Prefab_EnemyDie_Effect, pos);
                break;
            case EffectType.Hit:
                EffectGenerate(Prefab_Hit_Effect, pos);
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
