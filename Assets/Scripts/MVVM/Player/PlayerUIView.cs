using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using ViewModel.Extensions;


public class PlayerUIView : MonoBehaviour
{
    [Header("¿Â∫Ò ∫‰ « µÂ")]
    [SerializeField] List<GameObject> HpIconList;
    [SerializeField] Slider Slider_SkillBar;

    PlayerUIViewModel _vm;

    private void Awake()
    {
        _vm = new PlayerUIViewModel();
        _vm.PropertyChanged += OnPropertyChanged;
    }

    private void OnEnable()
    {
        _vm.Register_OnEnable();
    }
    private void OnDisable()
    {
        _vm.UnRegister_OnDisable();
    }

    void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.Hp):
                SetHpIcon(_vm.Hp);
                break;
            case nameof(_vm.Gauge):
                Slider_SkillBar.value = _vm.Gauge;
                break;
        }
    }

    void SetHpIcon(int validHp)
    {
        for (int i = 0; i < HpIconList.Count; i++)
        {
            if(i < validHp)
            {
                HpIconList[i].SetActive(true);
            }
            else
            {
                HpIconList[i].SetActive(false);
            }
        }
    }
}
