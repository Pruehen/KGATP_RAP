using System;
using UnityEngine;

public class Player : SceneSingleton<Player>
{    
    [Range(1f, 100f)][SerializeField] float MoveSpeed;
    [Range(1f, 10f)][SerializeField] float evasion_power;
    [Range(0.1f, 1f)][SerializeField] float evasion_duration;

    Rigidbody _rigidbody;
    Vector2 _moveCommandVector = Vector2.zero;

    Action OnZClick;
    Action OnXClick;
    Action OnHit;
    Action OnDead;

    Action<int> OnHpChange;
    public void Register_OnHpChange(Action<int> callBack) { OnHpChange += callBack; }
    public void UnRegister_OnHpChange(Action<int> callBack) { OnHpChange -= callBack; }

    Action<float> OnGaugeChange;
    public void Register_OnSkillGaugeChange(Action<float> callBack) { OnGaugeChange += callBack; }
    public void UnRegister_OnSkillGaugeChange(Action<float> callBack) { OnGaugeChange -= callBack; }

    Action<float> OnEvasionGaugeChange;
    public void Register_OnEvasionGaugeChange(Action<float> callBack) { OnEvasionGaugeChange += callBack; }
    public void UnRegister_OnEvasionGaugeChange(Action<float> callBack) { OnEvasionGaugeChange -= callBack; }


    public int Hp { get; private set; }
    public int Atk { get; private set; }
    public float SkillGauge { get; private set; }
    public float SkillGauge_Max { get; private set; }
    public float SkillGauge_RecoverySec { get; private set; }    
    public float evasion_coolTime { get; private set; }

    float _evasion_coolTimeValue;
    float _evasion_powerValue;
    float _evasion_timeRemaining;
    bool _isEvading;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCharacter player = DataManager.Instance.LoadedPlayerCharacterList[101];

        _rigidbody = GetComponent<Rigidbody>();

        OnZClick += OnClick_Z;
        OnXClick += OnClick_X;

        SkillGauge_Max = 100;
        SkillGauge_RecoverySec = 1;
        evasion_coolTime = player.Evasion.Cooltime;
        _evasion_powerValue = 1;
        _isEvading = false;

        Hp = player.HP;
        Atk = player.Atk;
    }

    // Update is called once per frame
    void Update()
    {
        InputCheck_OnUpdate();
        InputCheck_OnUpdate_Test();

        GaugeRecovery_OnUpdate();

        EvasionLogic_OnUpdate();
        MoveLogic_OnUpdate();

        RotateForward_OnUpdate();
    }

    /// <summary>
    /// 피격 메서드
    /// </summary>
    /// <param name="dmg"></param>
    public void Hit(int dmg)
    {
        Hp -= dmg;
        OnHit?.Invoke();
        OnHpChange?.Invoke(Hp);

        if (Hp <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        OnDead?.Invoke();
        Debug.Log("플레이어 사망");
    }

    void InputCheck_OnUpdate()
    {
        if (_isEvading == false)
        {
            _moveCommandVector = Vector2.zero;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                _moveCommandVector += new Vector2(MoveSpeed, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _moveCommandVector += new Vector2(-MoveSpeed, 0);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                _moveCommandVector += new Vector2(0, MoveSpeed);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                _moveCommandVector += new Vector2(0, -MoveSpeed);
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                OnZClick?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                OnXClick?.Invoke();
            }
        }
    }

    void GaugeRecovery_OnUpdate()
    {
        SkillGauge += Time.deltaTime * SkillGauge_RecoverySec;
        if(SkillGauge > SkillGauge_Max)
        {
            SkillGauge = SkillGauge_Max;
        }

        OnGaugeChange?.Invoke(SkillGauge / SkillGauge_Max);
    }

    void InputCheck_OnUpdate_Test()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Hit(1);
        }
    }

    void EvasionLogic_OnUpdate()
    {
        if (_isEvading)
        {
            _evasion_timeRemaining -= Time.deltaTime;
            if (_evasion_timeRemaining <= 0)
            {
                _isEvading = false;
                _evasion_powerValue = 1;
            }
        }        

        if (_evasion_coolTimeValue > 0)
        {
            _evasion_coolTimeValue -= Time.deltaTime;
            OnEvasionGaugeChange?.Invoke(_evasion_coolTimeValue / evasion_coolTime);
        }
    }

    void MoveLogic_OnUpdate()
    {
        _rigidbody.velocity = new Vector3(_moveCommandVector.x, 0, _moveCommandVector.y) * _evasion_powerValue;
    }

    void RotateForward_OnUpdate()
    {
        if (_moveCommandVector != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(_moveCommandVector.x, _moveCommandVector.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }
    }


    void OnClick_Z()
    {
        Debug.Log("Z 버튼 클릭");
    }

    void OnClick_X()
    {
        Debug.Log("X 버튼 클릭");

        if(_evasion_coolTimeValue <= 0)
        {
            _evasion_coolTimeValue = evasion_coolTime;
            OnEvasionGaugeChange?.Invoke(_evasion_coolTimeValue / evasion_coolTime);
            Evasion();
        }
    }

    void Evasion()
    {
        Debug.Log("회피 조작");
        _evasion_powerValue = evasion_power;
        _isEvading = true;
        _evasion_timeRemaining = evasion_duration;
    }
}
