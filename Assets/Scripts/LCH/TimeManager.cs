using System.Collections;
using UnityEngine;

public class TimeManager : SceneSingleton<TimeManager>
{ 
    public void CommandBulletTime(float targetTimeScale, float duration)
    {
        StartCoroutine(BulletTime(targetTimeScale, duration));
    }

    IEnumerator BulletTime(float targetTimeScale, float duration)
    {
        Time.timeScale = targetTimeScale;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
    }
}
