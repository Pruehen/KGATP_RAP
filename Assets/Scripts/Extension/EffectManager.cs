using System.Collections;
using UnityEngine;

public class EffectManager : SceneSingleton<EffectManager>
{
    public GameObject ExplosionEffect;    

    public void EffectGenerate(GameObject item, Vector3 position, float size)
    {
        GameObject effect = ObjectPoolManager.Instance.DequeueObject(item);
        effect.transform.position = position;
        effect.transform.rotation = Quaternion.identity;
        effect.transform.localScale *= size;
        effect.GetComponent<ParticleSystem>().Play();

        StartCoroutine(EffectEnqueue(effect, 10));
    }

    public void ExplosionEffectGenerate(Vector3 position, float size)
    {
        GameObject effect = ObjectPoolManager.Instance.DequeueObject(ExplosionEffect);
        effect.transform.position = position;
        effect.transform.rotation = Quaternion.identity;
        effect.transform.localScale = new Vector3(size, size, size);
        effect.transform.GetChild(0).localScale = new Vector3(size, size, size);
        effect.GetComponent<ParticleSystem>().Play();

        StartCoroutine(EffectEnqueue(effect, 10));
    }

    IEnumerator EffectEnqueue(GameObject item, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        ObjectPoolManager.Instance.EnqueueObject(item);
    }
}
