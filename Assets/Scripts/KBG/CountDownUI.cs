using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownUI : MonoBehaviour
{
    [Header("Count 321")]
    [SerializeField] GameObject Panel_CountDown;
    [SerializeField] List<Sprite> Sprites_Count;
    [SerializeField] Sprite Sprite_Go;
    [SerializeField] Image Img_Count;
    [SerializeField][Range(0f, 1f)] float MinScale;

    [Header("Go!!")]
    [SerializeField][Range(.1f, 1f)] float flyInDuration;
    [SerializeField][Range(1f, 100f)] float maxImageScale;
    [SerializeField] FlyInDirection flyInDirection = FlyInDirection.FromTop;
    public enum FlyInDirection { FromLeft, FromRight, FromTop, FromBottom }

    private bool isPaused = false;

    private void Start()
    {
        StartCountDown();
    }

    public void SetCountDownPanel(bool setActive)
    {
        this.gameObject.SetActive(setActive);
    }

    public void StartCountDown()
    {
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        TimeManager.Instance.CommandBulletTime(0f, 3f);
        Player.Instance.isPaused = true;

        int timeRemaining = Sprites_Count.Count;
        int spriteIndex = Sprites_Count.Count - 1;

        while (timeRemaining > 0 && spriteIndex >= 0)
        {
            Img_Count.sprite = Sprites_Count[spriteIndex];
            StartCoroutine(ScaleDownImage());

            yield return new WaitForSecondsRealtime(1f); // ���� �ð� �������� ���

            timeRemaining--;
            spriteIndex--;
        }

        StartCoroutine(FlyInGoImage());
    }

    private IEnumerator ScaleDownImage()
    {
        Vector3 originalScale = Img_Count.transform.localScale;
        Vector3 targetScale = new Vector3(MinScale, MinScale, MinScale);

        float duration = 1f; // 1�� ���� ���
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Img_Count.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / duration);
            elapsed += Time.unscaledDeltaTime; // Time.unscaledDeltaTime ���
            yield return null;
        }

        Img_Count.transform.localScale = originalScale;
    }

    private IEnumerator FlyInGoImage()
    {
        Img_Count.sprite = Sprite_Go;
        Vector3 originalPosition = Img_Count.transform.position;
        Vector3 offScreenPosition = GetOffScreenPosition(originalPosition);
        Img_Count.transform.position = offScreenPosition;

        Vector3 originalScale = Img_Count.transform.localScale;
        Vector3 maxScale = originalScale * maxImageScale;
        Img_Count.transform.localScale = maxScale;

        float elapsed = 0f;

        while (elapsed < flyInDuration)
        {
            Img_Count.transform.position = Vector3.Lerp(offScreenPosition, originalPosition, elapsed / flyInDuration);
            Img_Count.transform.localScale = Vector3.Lerp(maxScale, originalScale, elapsed / flyInDuration);
            elapsed += Time.unscaledDeltaTime; // Time.unscaledDeltaTime ���
            yield return null;
        }

        Img_Count.transform.position = originalPosition;
        Img_Count.transform.localScale = originalScale;

        Player.Instance.isPaused = false;

        yield return new WaitForSecondsRealtime(1f); // ���� �ð� �������� ���

        // ī��Ʈ�ٿ� �г� ��Ȱ��ȭ
        SetCountDownPanel(false);

    }

    private Vector3 GetOffScreenPosition(Vector3 originalPosition)
    {
        Vector3 offScreenPosition = originalPosition;
        RectTransform canvasRectTransform = Img_Count.canvas.GetComponent<RectTransform>();
        float canvasWidth = canvasRectTransform.rect.width;
        float canvasHeight = canvasRectTransform.rect.height;

        switch (flyInDirection)
        {
            case FlyInDirection.FromLeft:
                offScreenPosition = new Vector3(-Img_Count.rectTransform.rect.width, canvasHeight / 2, originalPosition.z);
                break;
            case FlyInDirection.FromRight:
                offScreenPosition = new Vector3(canvasWidth + Img_Count.rectTransform.rect.width, canvasHeight / 2, originalPosition.z);
                break;
            case FlyInDirection.FromTop:
                offScreenPosition = new Vector3(canvasWidth / 2, canvasHeight + Img_Count.rectTransform.rect.height, originalPosition.z);
                break;
            case FlyInDirection.FromBottom:
                offScreenPosition = new Vector3(canvasWidth / 2, -Img_Count.rectTransform.rect.height, originalPosition.z);
                break;
        }

        return offScreenPosition;
    }

}
