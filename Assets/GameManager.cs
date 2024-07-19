using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SceneSingleton<GameManager>
{


    public void GameOver_OnPlayerDead()
    {
        Debug.Log("플레이어죽음");
        StartCoroutine(LoadScene());
    }

    public void GameClear()
    {
        StartCoroutine(LoadScene());
        Debug.Log("스테이지클리어");
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
