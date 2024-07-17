using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZoomType
{
    FastSlow,
    SlowFast
}

public class CameraZoom : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float zoomInFovLimit;
    [SerializeField] float zoomOutFovLimit;
    [Header("Zoom In Parameter")]
    [SerializeField][Range(0.1f, 5)] float zoomInTime;
    [SerializeField] ZoomType zoomInType;
    [SerializeField][Range(2, 100)] float fastSlowInForce;
    [SerializeField][Range(2, 10)] float slowFastInForce;

    [Header("Zoom Out Parameter")]
    [SerializeField][Range(0.1f, 5)] float zoomOutTime;
    [SerializeField] ZoomType zoomOutType;
    [SerializeField][Range(2, 100)] float fastSlowOutForce;
    [SerializeField][Range(2, 10)] float slowFastOutForce;

    private Coroutine zoomCoroutine;
    private float velocity = 0.0f;


    //테스트용 나중에 지워야함.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartZoomIn();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartZoomOut();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpecialZoom();
        }

    }

    public void SpecialZoom()
    {
        if(zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
        }
        zoomCoroutine = StartCoroutine(ZoomInOutCoroutine());
    }

    private IEnumerator ZoomInOutCoroutine()
    {
        switch (zoomInType)
        {
            case ZoomType.FastSlow:
                yield return StartCoroutine(ZoomInLogCoroutine());
                break;
            case ZoomType.SlowFast:
                yield return StartCoroutine(ZoomInPowCoroutine());
                break;
        }
        switch (zoomOutType)
        {
            case ZoomType.FastSlow:
                yield return StartCoroutine(ZoomOutLogCoroutine());
                break;
            case ZoomType.SlowFast:
                yield return StartCoroutine(ZoomOutPowCoroutine());
                break;
        }
    }

    public void StartZoomIn()
    {
        if(zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
        }
        switch (zoomInType)
        {
            case ZoomType.FastSlow:
                zoomCoroutine = StartCoroutine(ZoomInLogCoroutine());
                break;
            case ZoomType.SlowFast:
                zoomCoroutine = StartCoroutine(ZoomInPowCoroutine());
                break;
        }

    }

    private IEnumerator ZoomInLogCoroutine()
    {
        float startFov = cam.fieldOfView;
        float elapsedTime = 0f;

        while (elapsedTime < zoomInTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / zoomInTime; // 0 to 1
            float logT = Mathf.Log(t * (fastSlowInForce - 1) + 1, fastSlowInForce); // Transforming to logarithmic scale and flipping
            cam.fieldOfView = Mathf.Lerp(startFov, zoomInFovLimit, logT);
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, zoomInFovLimit, zoomOutFovLimit);
            yield return null;
        }
        cam.fieldOfView = zoomInFovLimit; // Ensure it reaches the exact limit at the end
    }

    private IEnumerator ZoomInPowCoroutine()
    {
        float startFov = cam.fieldOfView;
        float elapsedTime = 0f;

        while (elapsedTime < zoomInTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / zoomInTime; // 0 to 1
            float expT = Mathf.Pow(t, slowFastInForce); // Transforming to exponential scale
            cam.fieldOfView = Mathf.Lerp(startFov, zoomInFovLimit, expT);
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, zoomInFovLimit, zoomOutFovLimit);
            yield return null;
        }

        cam.fieldOfView = zoomInFovLimit; // Ensure it reaches the exact limit at the end
    }

    public void StartZoomOut()
    {
        if(zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
        }
        switch (zoomOutType)
        {
            case ZoomType.FastSlow:
                zoomCoroutine = StartCoroutine(ZoomOutLogCoroutine());
                break;
            case ZoomType.SlowFast:
                zoomCoroutine = StartCoroutine(ZoomOutPowCoroutine());
                break;
        }
    }

    private IEnumerator ZoomOutLogCoroutine()
    {
        float startFov = cam.fieldOfView;
        float elapsedTime = 0f;

        while (elapsedTime < zoomOutTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / zoomOutTime; // 0 to 1
            float logT = Mathf.Log(t * (fastSlowOutForce - 1) + 1, fastSlowOutForce); // Transforming to logarithmic scale
            cam.fieldOfView = Mathf.Lerp(startFov, zoomOutFovLimit, logT);
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, zoomInFovLimit, zoomOutFovLimit);
            yield return null;
        }

        cam.fieldOfView = zoomOutFovLimit; // Ensure it reaches the exact limit at the end
    }

    private IEnumerator ZoomOutPowCoroutine()
    {
        float startFov = cam.fieldOfView;
        float elapsedTime = 0f;

        while (elapsedTime < zoomOutTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / zoomOutTime; // 0 to 1
            float expT = Mathf.Pow(t, slowFastOutForce); // Transforming to exponential scale
            cam.fieldOfView = Mathf.Lerp(startFov, zoomOutFovLimit, expT);
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, zoomInFovLimit, zoomOutFovLimit);
            yield return null;
        }
        cam.fieldOfView = zoomOutFovLimit; // Ensure it reaches the exact limit at the end
    }
}



