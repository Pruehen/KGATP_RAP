using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    public Renderer playerRenderer;  // �÷��̾��� �������� �����մϴ�.
    [SerializeField] float blinkInterval = 0.1f;  // �����Ÿ� ������ �����մϴ�.
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

        playerRenderer.enabled = true;  // �����Ÿ��� ������ �ٽ� �������� Ȱ��ȭ�մϴ�.
    }
}
