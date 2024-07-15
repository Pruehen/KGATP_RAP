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
        float fixedDeltaTimeTamp = Time.fixedDeltaTime;
        Time.fixedDeltaTime *= targetTimeScale;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
        Time.fixedDeltaTime = fixedDeltaTimeTamp;
    }
}
