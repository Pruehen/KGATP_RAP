using System;
using UnityEngine;
using System.Collections;
public class PlayerTest : MonoBehaviour
{
    [Range(1f, 100f)][SerializeField] float MoveSpeed;
    [Range(1f, 10f)][SerializeField] float evasion_power;
    [Range(0.1f, 1f)] [SerializeField] float evasion_duration;

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
    float evasion_powerValue = 1;
    float evasion_timeRemaining;
    bool isEvading;
    bool iscombo1=false;
    bool iscombo2=false;
    private Coroutine currentCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        OnZClick += OnClick_Z;
        OnXClick += OnClick_X;

        Gauge_Max = 100;
        Gauge_RecoverySec = 1;
        evasion_coolTime = 1.5f;
        isEvading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEvading) 
        {
            InputCheck_OnUpdate();
        }
        InputCheck_OnUpdate_Test();

        if (isEvading)
        {
            evasion_timeRemaining -= Time.deltaTime;
            if (evasion_timeRemaining <= 0)
            {
                isEvading = false;
                evasion_powerValue = 1;
            }
        }

        _rigidbody.velocity = new Vector3(_moveCommandVector.x, 0, _moveCommandVector.y) * evasion_powerValue;
        //evasion_powerValue = Mathf.Lerp(evasion_powerValue, 1, Time.deltaTime);

        evasion_coolTimeValue -= Time.deltaTime;

        if (_moveCommandVector != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(_moveCommandVector.x, _moveCommandVector.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }
    }

    /// <summary>
    /// 피격 메서드
    /// </summary>
    /// <param name="dmg"></param>
    public void Hit(int dmg)
    {
        Hp -= dmg;
        OnHit?.Invoke();

        if(Hp <= 0)
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

    void InputCheck_OnUpdate_Test()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Hit(1);
        }
    }

    void OnClick_X()
    {
        Debug.Log("X 버튼 클릭");
        
        if(!iscombo1)
        {
            StartNewCoroutine(Atk1());
        }
        else if(iscombo1 &&!iscombo2)
        {
            StartNewCoroutine(Atk2());
        }
        else if (iscombo2)
        {
            StartNewCoroutine(Atk3());
        }
    }

    void OnClick_Z()
    {
        Debug.Log("Z 버튼 클릭");

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
        isEvading = true;
        evasion_timeRemaining = evasion_duration;
    }

    void Attack1()
    {
        Debug.Log("기본공격1");
        iscombo1 = true;

    }
    void Attack2()
    {
        Debug.Log("기본공격2");
        iscombo2 = true;
    }
    void Attack3()
    {
        Debug.Log("기본공격3");
    }

    void ComboReset()
    {
        Debug.Log("<color=red>콤보리셋.</color>");
        iscombo1 = false;
        iscombo2 = false;
    }
    IEnumerator Atk1()
    {
        Attack1();
        yield return new WaitForSeconds(1f);
        ComboReset();
    }
    IEnumerator Atk2()
    {
        Attack2();
        yield return new WaitForSeconds(1f);
        ComboReset();
    }
    IEnumerator Atk3()
    {
        Attack3();
        yield return new WaitForSeconds(1f);
        ComboReset();
    }
    void StartNewCoroutine(IEnumerator coroutine)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(coroutine);
    }
}
