using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempGameManager : MonoBehaviour
{
    public static TempGameManager Instance {  get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        NewDataManager.ReadClearedStage();
        SceneManager.sceneLoaded += OnSceneLoaded;

        StartCoroutine(LoadNextSceneAfterDelay(5f));
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);
        Debug.Log($"{NewDataManager.LoadedClearedStage}");

    }
    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // 여기서 "NextSceneName"을 전환하려는 씬의 이름으로 변경하세요.
        SceneManager.LoadScene("TempScene");
    }

}
