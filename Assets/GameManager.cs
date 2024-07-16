using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SceneSingleton<GameManager>
{


    public void GameOver_OnPlayerDead()
    {
        Debug.Log("플레이어죽음");
    }

    public void StageClear()
    {
        Debug.Log("스테이지클리어");
    }
}
