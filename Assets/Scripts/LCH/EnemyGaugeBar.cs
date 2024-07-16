using System.Collections;
using System.Collections.Generic;
using UI.Extension;
using UnityEngine;
using UnityEngine.UI;
public class EnemyGaugeBar : MonoBehaviour
{
    [SerializeField] Slider enemyHpbar_slider;
    [SerializeField] RectTransform _rect;

    LCH.Enemy _targetEnemy;
    Transform _targetEnemyTransform;

    public void Init(LCH.Enemy targetEnemy)
    {
        _targetEnemy = targetEnemy;
        _targetEnemyTransform = _targetEnemy.transform;
    }
    public void GaugeUpdate_OnUpdate()
    {
        enemyHpbar_slider.value = _targetEnemy.GetFireDelayRatio();
    }
    private void Update()
    {
        if(_targetEnemyTransform != null)
        {
            _rect.SetUIPos_WorldToScreenPos(_targetEnemyTransform.position + new Vector3(0, 2, 0));
        }
        if(_targetEnemy != null)
        {
            GaugeUpdate_OnUpdate();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
