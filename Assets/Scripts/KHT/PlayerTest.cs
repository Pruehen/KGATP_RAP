using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum KeyName
{
    Z,
    X

}

public class PlayerTest : MonoBehaviour
{
    [Range(1f, 100f)][SerializeField] float MoveSpeed;
    [Range(1f, 10f)][SerializeField] float evasion_power;
    [Range(0.1f, 1f)] [SerializeField] float evasion_duration;
    [Range(0f, 5f)] [SerializeField] float evasion_coolTime;
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
    //public float evasion_coolTime { get; private set; }

    float evasion_coolTimeValue;
    float evasion_powerValue = 1;
    float evasion_timeRemaining;
    bool isEvading;
    [SerializeField]GameObject Atk1Collider;
    [SerializeField] GameObject Atk2Collider;
    [SerializeField] GameObject Atk3Collider;
    public Animator animator;

    private IState _curState;
    private Vector2 moveInput;


    [SerializeField] Text Text_TemporalState;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        OnZClick += OnClick_Z;
        OnXClick += OnClick_X;

        Gauge_Max = 100;
        Gauge_RecoverySec = 1;

        MoveSpeed = 10f;
        evasion_power = 5f;
        evasion_duration = 0.2f;
        evasion_coolTime = 1.5f;
        isEvading = false;

        ChangeState(new IdleState(this));
    }

    // Update is called once per frame
    void Update()
    {
        InputCheck_OnUpdate();
        //InputCheck_OnUpdate_Test();

        if (isEvading)
        {
            animator.SetBool("Evasion", true);
            _rigidbody.AddForce(transform.forward*evasion_powerValue, ForceMode.Impulse);

            evasion_timeRemaining -= Time.deltaTime;
            if (evasion_timeRemaining <= 0)
            {
                animator.SetBool("Evasion", false);
                isEvading = false;
                evasion_powerValue = 1;
            }
        }


        

        evasion_coolTimeValue -= Time.deltaTime;

        _curState?.ExcuteOnUpdate();
        moveInput = _moveCommandVector;
        if(moveInput != Vector2.zero)
        {
            ChangeState(new MoveState(this));
        }
    }

    public void Move()
    {
        _rigidbody.velocity = new Vector3(_moveCommandVector.x, 0, _moveCommandVector.y);
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
        _curState.OnInput(KeyName.X);
    }

    void OnClick_Z()
    {
        Debug.Log("Z 버튼 클릭");
        _curState.OnInput(KeyName.Z);

        if (evasion_coolTimeValue <= 0)
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
    public void ChangeState(IState newState)
    {
        if ((_curState is Atk1State || _curState is Atk2State || _curState is Atk3State) && newState is MoveState)
        {
            Debug.Log("Cannot transition from AtkState to MoveState");
            return;
        }
        _curState?.ExitState();
        _curState = newState;
        Text_TemporalState.text = _curState.ToString();
        _curState.EnterState();
    }
    public void OnAnimationComplete(string animationName)
    {
        _curState.OnAnimationComplete(animationName);
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }
    public void Atk1()
    {
        animator.SetTrigger("Atk1");
        Atk1Collider.SetActive(true);
    }
    public void Atk2()
    {
        animator.SetTrigger("Atk2");
        Atk2Collider.SetActive(true);
    }
    public void Atk3()
    {
        animator.SetTrigger("Atk3");
        Atk3Collider.SetActive(true);
    }
    public void AtkEnd()
    {
        //animator.SetTrigger("Stop");
        Atk1Collider.SetActive(false);
        Atk2Collider.SetActive(false);
        Atk3Collider.SetActive(false);
    }
}
