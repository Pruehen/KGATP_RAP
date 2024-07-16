using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGaugeBarManager : SceneSingleton<EnemyGaugeBarManager>
{
    [SerializeField] GameObject prefab_EnemyGaugeBar;

    private void Awake()
    {
        EnemyManager.Instance.Register_OnSpawn(GenerateGaugeBar_OnAddEnemy);
    }

    void GenerateGaugeBar_OnAddEnemy(LCH.Enemy enemy)
    {
        GameObject obj = Instantiate(prefab_EnemyGaugeBar, this.transform);
        obj.GetComponent<EnemyGaugeBar>().Init(enemy);
    }
}
