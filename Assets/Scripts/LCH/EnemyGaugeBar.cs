using System.Collections;
using System.Collections.Generic;
using UI.Extension;
using UnityEngine;
using UnityEngine.UI;
public class EnemyGaugeBar : MonoBehaviour
{
    [SerializeField] Slider enemyHpbar_slider;
    [SerializeField] RectTransform _rect;
    [SerializeField] Transform _targetTrf;

    private void Update()
    {
        if(_targetTrf != null)
        {
            _rect.SetUIPos_WorldToScreenPos(_targetTrf.position + new Vector3(0, 2, 0));
        }
    }
}
