using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    public Renderer playerRenderer;  // 플레이어의 렌더러를 참조합니다.
    [SerializeField] float blinkInterval = 0.1f;  // 깜빡거림 간격을 설정합니다.
    [SerializeField] Player player;
    private void Start()
    {
        if (playerRenderer == null)
        {
            playerRenderer = GetComponent<Renderer>();
        }
    }

    public void StartBlinking()
    {
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        //float endTime = Time.time + blinkDuration;
        float startTime = 0f;
        while (startTime <= player.Damagedinvincible)
        {
            playerRenderer.enabled = !playerRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            startTime += blinkInterval;
        }

        playerRenderer.enabled = true;  // 깜빡거림이 끝나면 다시 렌더러를 활성화합니다.
    }
}
