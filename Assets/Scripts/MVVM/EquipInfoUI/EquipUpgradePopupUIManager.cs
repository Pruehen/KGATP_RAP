using UnityEngine;

public class EquipUpgradePopupUIManager : MonoBehaviour
{
    //[Header("장비 강화 정보 필드")]
    //[SerializeField] TextMeshProUGUI TMP_DeltaLevel;
    //[SerializeField] TextMeshProUGUI TMP_DeltaMainState;
    //[SerializeField] TextMeshProUGUI TMP_AddedSubState;
    ////[SerializeField] EquipIcon EquipIcon_ViewIcon;

    //EquipUpgradePopupUIManagerViewModel _vm;

    //int _preLevel;
    //int _affterLevel;
    //IncreaseableStateType _mainStateType;
    ////List<UserHaveEquipData.EquipStateSet> _addedSubStateList = new List<UserHaveEquipData.EquipStateSet>();

    //private void OnEnable()
    //{
    //    if (_vm == null)
    //    {
    //        _vm = new EquipUpgradePopupUIManagerViewModel();
    //        _vm.PropertyChanged += OnPropertyChanged;
    //        //_vm.RegisterEventsOnEnable();            
    //    }
    //}
    //private void OnDisable()
    //{
    //    if (_vm != null)
    //    {
    //        //_vm.UnRegisterEventsOnDisable();
    //        _vm.PropertyChanged -= OnPropertyChanged;
    //        _vm = null;
    //    }
    //}

    //public void ViewResult(UserHaveEquipData before, UserHaveEquipData affter)
    //{
    //    _vm.RefreshVielModel(before, affter);
    //}

    //void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    //{
    //    switch (e.PropertyName)
    //    {
    //        case nameof(_vm.TableKey):
    //            EquipIcon_ViewIcon.SetSprite(_vm.TableKey);
    //            break;
    //        case nameof(_vm.Prelevel):
    //            _preLevel = _vm.Prelevel;
    //            break;
    //        case nameof(_vm.AffterLevel):
    //            _affterLevel = _vm.AffterLevel;
    //            break;
    //        case nameof(_vm.MainStateType):
    //            _mainStateType = _vm.MainStateType;
    //            break;
    //        case nameof(_vm.SubStateList):
    //            _addedSubStateList = _vm.SubStateList;
    //            break;
    //    }
    //    TextSet_UpgradeResult();
    //}

    //void TextSet_UpgradeResult()
    //{
    //    TMP_DeltaLevel.text = $"+{_preLevel} -> +{_affterLevel}";
    //    TMP_DeltaMainState.text = $"{GetStateName(_mainStateType)} +{GetStateValue(_mainStateType, _preLevel + 5)} -> +{GetStateValue(_mainStateType, _affterLevel + 5)}";

    //    string subStateText = string.Empty;
    //    foreach (var item in _addedSubStateList)
    //    {
    //        subStateText += $"<color=#FFFF00>추가 옵션!</color>    {GetStateName(item._stateType)} +{GetStateValue(item._stateType, item.GetLevel())}\n";
    //    }
    //    TMP_AddedSubState.text = subStateText;
    //}

    //string GetStateName(IncreaseableStateType stateType)
    //{
    //    return StateType_StateMultipleTable.GetStateText(stateType);
    //}
    //string GetStateValue(IncreaseableStateType stateType, int level)
    //{
    //    return StateType_StateMultipleTable.GetStateText(stateType, level)[1];
    //}
}
