using System;
using UnityEngine;

public class Player : SceneSingleton<Player>
{    
    [Range(1f, 100f)][SerializeField] float MoveSpeed;
    [Range(1f, 10f)][SerializeField] float evasion_power;
    [Range(1f, 10f)][SerializeField] float evasion_damper;

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
    public void Register_OnGaugeChange(Action<float> callBack) { OnGaugeChange += callBack; }
    public void UnRegister_OnGaugeChange(Action<float> callBack) { OnGaugeChange -= callBack; }

    public int Hp { get; private set; }
    public int Atk { get; private set; }
    public float Gauge { get; private set; }
    public float Gauge_Max { get; private set; }
    public float Gauge_RecoverySec { get; private set; }    
    public float evasion_coolTime { get; private set; }

    float evasion_coolTimeValue;
    float evasion_powerValue;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        OnZClick += OnClick_Z;
        OnXClick += OnClick_X;

        Gauge_Max = 100;
        Gauge_RecoverySec = 1;

        Hp = 4;
        Atk = 1;
    }

    // Update is called once per frame
    void Update()
    {
        InputCheck_OnUpdate();
        InputCheck_OnUpdate_Test();

        GaugeRecovery_OnUpdate();

        _rigidbody.velocity = new Vector3(_moveCommandVector.x, 0, _moveCommandVector.y) * evasion_powerValue;
        evasion_powerValue = Mathf.Lerp(evasion_powerValue, 1, Time.deltaTime);

        evasion_coolTimeValue -= Time.deltaTime;
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

    void GaugeRecovery_OnUpdate()
    {
        Gauge += Time.deltaTime * Gauge_RecoverySec;
        if(Gauge > Gauge_Max)
        {
            Gauge = Gauge_Max;
        }

        OnGaugeChange?.Invoke(Gauge / Gauge_Max);
    }

    void InputCheck_OnUpdate_Test()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Hit(1);
        }
    }

    void OnClick_Z()
    {
        Debug.Log("Z 버튼 클릭");
    }

    void OnClick_X()
    {
        Debug.Log("X 버튼 클릭");

        if(evasion_coolTimeValue <= 0)
        {
            evasion_coolTimeValue = evasion_coolTime;
            Evasion();
        }
    }

    void Evasion()
    {
        Debug.Log("회피 조작");
        evasion_powerValue = evasion_power;
    }
}
