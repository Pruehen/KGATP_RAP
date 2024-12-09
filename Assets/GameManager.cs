using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SceneSingleton<GameManager>
{


    public void GameOver_OnPlayerDead()
    {
        Debug.Log("�÷��̾�����");
        StartCoroutine(LoadScene());
    }

    public void GameClear()
    {
        StartCoroutine(LoadScene());
        Debug.Log("��������Ŭ����");
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
