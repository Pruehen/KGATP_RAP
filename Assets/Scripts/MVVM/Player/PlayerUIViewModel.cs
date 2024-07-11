using System.ComponentModel;

public class PlayerUIViewModel
{
    int _hp;
    float _skillValueRatio;
    float _evadeValueRatio;
    public int Hp
    {
        get { return _hp; }
        set
        {
            if (_hp == value) return;
            _hp = value;
            OnPropertyChanged(nameof(Hp));
        }
    }
    public float SkillValueRatio
    {
        get { return _skillValueRatio; }
        set
        {
            if (_skillValueRatio == value) return;
            _skillValueRatio = value;
            OnPropertyChanged(nameof(SkillValueRatio));
        }
    }
    public float EvadeValueRatio
    {
        get { return _evadeValueRatio; }
        set
        {
            if (_evadeValueRatio == value) return;
            _evadeValueRatio = value;
            OnPropertyChanged(nameof(EvadeValueRatio));
        }
    }

    #region PropChanged
    public event PropertyChangedEventHandler PropertyChanged;    

    protected virtual void OnPropertyChanged(string propertyName)//값이 변경되었을 때 이벤트를 발생시키기 위한 용도 (데이터 바인딩)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}
