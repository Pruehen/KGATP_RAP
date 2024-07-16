using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SceneSingleton<EnemyManager>
{
    Dictionary<int, LCH.Enemy> EnemyDic =  new Dictionary<int, LCH.Enemy>(); 

    Action<LCH.Enemy> OnSpawn;
    public void Register_OnSpawn(Action<LCH.Enemy> callBack) { OnSpawn += callBack; }    
    Action OnDestroy;

    public void AddDic(int instanceID, LCH.Enemy enemy)
    {
       EnemyDic.Add(instanceID, enemy);
       OnSpawn?.Invoke(enemy);
       Debug.Log(EnemyDic.Count);
    }

    public void DestroyDic(int instanceID)
    {
        EnemyDic.Remove(instanceID);
        OnDestroy?.Invoke();
        Debug.Log(EnemyDic.Count);
        if (EnemyDic.Count == 0)
        {
            GameManager.Instance.StageClear();
        }
    }
}
