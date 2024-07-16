using System.Collections;
using UnityEngine;

public class SoundPlayerManager : SceneSingleton<SoundPlayerManager>
{
    public void PlaySound(GameObject prefab, Vector3 position)
    {
        GameObject sound = ObjectPoolManager.Instance.DequeueObject(prefab);
        sound.transform.position = position;        
        sound.GetComponent<AudioSource>().Play();

        StartCoroutine(SoundEnqueue(sound, 5));
    }


    IEnumerator SoundEnqueue(GameObject item, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        ObjectPoolManager.Instance.EnqueueObject(item);
    }
}
