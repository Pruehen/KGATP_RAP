using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum KeyName
{
    Z,
    X

}

public class Player : MonoBehaviour
{
    public static Player Instance;
    [Range(1f, 100f)][SerializeField] float MoveSpeed;
    [Range(0.1f, 5f)] [SerializeField] float evasion_duration;
    [Range(0f, 5f)] [SerializeField] float evasion_coolTime = 1;
    [Range(0f, 100f)] [SerializeField] float evasion_Velocity = 5;
    [Range(0f, 5f)] [SerializeField] float evasion_delay = 0.5f;
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

    float evasion_coolTimeValue;
    bool isEvading;

    [SerializeField]GameObject Atk1Collider;
    [SerializeField] GameObject Atk2Collider;
    [SerializeField] GameObject Atk3Collider;
    public Animator animator;

    private IState _curState;
    private Vector2 moveInput;

    private Coroutine evasionCoroutine;

    public float lastDamagedTime;
    public float invincibleTime;
    public bool IsDamagedInvincible
    {
        get { return Time.time <= lastDamagedTime + invincibleTime; }
    }

    [SerializeField] Text Text_TemporalState;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        OnZClick += OnClick_Z;
        OnXClick += OnClick_X;
        SkillGauge = 100;
        SkillGauge_Max = 100;
        SkillGauge_RecoverySec = 1;

        MoveSpeed = 10f;
        evasion_duration = 0.2f;
        evasion_coolTime = 1.5f;
        isEvading = false;

        ChangeState(new IdleState(this));
    }


    void Update()
    {
        _curState?.ExcuteOnUpdate();

        InputCheck_OnUpdate();
        InputCheck_OnUpdate_Test();
        EvasionCoolTime_OnUpdate();
        MoveCheck_OnUpdate();



        GaugeRecovery_OnUpdate();
    }
    //스킬 게이지 업데이트
    void GaugeRecovery_OnUpdate()
    {
        SkillGauge += Time.deltaTime * SkillGauge_RecoverySec;
        if (SkillGauge > SkillGauge_Max)
        {
            SkillGauge = SkillGauge_Max;
        }

        OnGaugeChange?.Invoke(SkillGauge / SkillGauge_Max);
    }
    //회피 쿨타임 업데이트
    void EvasionCoolTime_OnUpdate()
    {
        if (evasion_coolTimeValue > 0)
        {
            evasion_coolTimeValue -= Time.deltaTime;
            OnEvasionGaugeChange?.Invoke(evasion_coolTimeValue / evasion_coolTime);
        }
    }
    void MoveCheck_OnUpdate()
    {
        moveInput = _moveCommandVector;
        if (moveInput != Vector2.zero)
        {
            ChangeState(new MoveState(this));
        }
    }
    //이동 및 캐릭터 회전
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
        if (IsDamagedInvincible) { Debug.Log("피격무적"); return; }
        lastDamagedTime = Time.time;
        invincibleTime = 8;
        Debug.Log("데미지");
        BlinkEffect blinkEffect = GetComponent<BlinkEffect>();
        if (blinkEffect != null) { blinkEffect.StartBlinking(); }

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
        }
    }


    //회피 코루틴 시작 함수
    public void EvasionStart()
    {
        if (evasionCoroutine != null)
        {
            StopCoroutine(evasionCoroutine);
        }
        evasionCoroutine = StartCoroutine(EvasionCoroutine());
    }
    public void EvasionStop()
    {
        if (evasionCoroutine != null)
        {
            StopCoroutine(evasionCoroutine);
            evasionCoroutine = null;

            // 현재 위치에 고정
            isEvading = false;
            ChangeLayer(this.gameObject, 8); // 레이어 8 Player
            ChangeState(new EvasionDelayState(this));
        }
    }
    //더킹 코루틴
    private IEnumerator EvasionCoroutine()
    {
        isEvading = true;
        ChangeLayer(this.gameObject, 13);//레이어 13 Evasion
        //Vector3 start = transform.position;
        //Vector3 end = transform.position + transform.forward * evasion_Velocity;

        float elapsedTime = 0f;

        while(elapsedTime < evasion_duration)
        {
            if (!isEvading) yield break;
            _rigidbody.velocity = this.transform.forward * evasion_Velocity;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        isEvading = false;
        ChangeLayer(this.gameObject, 8);//레이어 8 Player
        ChangeState(new EvasionDelayState(this));
    }

    //플레이어 상태변경
    public void ChangeState(IState newState)
    {
        if ((_curState is Atk1State || _curState is Atk2State || _curState is Atk3State|| _curState is EvasionDelayState) && newState is MoveState)
        {
            Debug.Log("Cannot transition from AtkState to MoveState");
            return;
        }
        _curState?.ExitState();
        _curState = newState;
        //Text_TemporalState.text = _curState.ToString(); 스테이트 체크용 디버그 텍스트.
        _curState.EnterState();
    }

    //애니메이션 완료 함수
    public void OnAnimationComplete(string animationName)
    {
        Debug.Log($"{animationName}1234");
        _curState.OnAnimationComplete(animationName);
    }

    //이동 입력체크
    public Vector2 GetMoveInput()
    {
        return moveInput;
    }
    
    //회피 딜레이 시작 함수
    public void Delay()
    {
        StartCoroutine(EvasionDelay());
    }
    //회피 딜레이 코루틴
    private IEnumerator EvasionDelay()
    {
        yield return new WaitForSeconds(evasion_delay);
        ChangeState(new IdleState(this));
    }

    //회피 레이어 변경 함수
    private void ChangeLayer(GameObject player, int newLayer)
    {
        player.layer = newLayer;

        foreach (Transform child in player.transform)
        {
            ChangeLayer(child.gameObject, newLayer);
        }
    }
    //
    public void SpecialAttack()
    {
        SkillGauge = 0;
    }
}
