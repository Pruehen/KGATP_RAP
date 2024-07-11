using System.ComponentModel;

public class PlayerUIViewModel
{
    int _hp;
    float _gauge;

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
    public float Gauge
    {
        get { return _gauge; }
        set
        {
            if (_gauge == value) return;
            _gauge = value;
            OnPropertyChanged(nameof(Gauge));
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
