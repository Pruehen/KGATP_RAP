using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    public Renderer playerRenderer;  // 플레이어의 렌더러를 참조합니다.
    [SerializeField] float blinkDuration = 1.0f;  // 깜빡거리는 총 시간을 설정합니다.
    [SerializeField] float blinkInterval = 0.1f;  // 깜빡거림 간격을 설정합니다.

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
        float endTime = Time.time + blinkDuration;

        while (Time.time < endTime)
        {
            playerRenderer.enabled = !playerRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }

        playerRenderer.enabled = true;  // 깜빡거림이 끝나면 다시 렌더러를 활성화합니다.
    }
}
