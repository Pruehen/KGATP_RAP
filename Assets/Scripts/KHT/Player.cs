using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class Player : MonoBehaviour
{
    public static Player Instance;
    public PlayerSound playerSound;
    [Range(1f, 100f)][SerializeField] float MoveSpeed;
    [Header("회피")]
    [Range(0.1f, 5f)][SerializeField] float evasion_duration;
    [Range(0f, 5f)][SerializeField] float evasion_coolTime = 1;
    [Range(0f, 100f)][SerializeField] float evasion_Velocity = 30;
    [Range(0f, 5f)][SerializeField] float evasion_delay = 0.5f;
    [SerializeField] ParticleSystem evasion_Pac;
    [Header("공격시간")]
    [Range(0f, 1f)][SerializeField] float atkcollider_active = 0.2f;
    [Range(0f, 1f)][SerializeField] float atk1delay_second = 0.1f;
    [Range(0f, 1f)][SerializeField] float atk2delay_second = 0.1f;
    [Range(0f, 1f)][SerializeField] float atk3delay_second = 0.3f;
    [Range(0f, 1f)][SerializeField] float parrydelay_second;
    [Header("무적시간")]
    [Range(0f, 5f)] [SerializeField] float damagedinvincible = 1f;
    [Range(0f, 5f)] [SerializeField] float parryinvincible = 2f;
    [Range(0f, 5f)] [SerializeField] float specialinvincible = 3f;

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
    public float Damagedinvincible { get { return damagedinvincible; } }
    float evasion_coolTimeValue;
    bool isEvading;
    [Header("공격콜리더")]
    [SerializeField] GameObject Atk1Collider;
    [SerializeField] GameObject Atk2Collider;
    [SerializeField] GameObject Atk3Collider;
    [SerializeField] GameObject StrongAtkCollider;
    [SerializeField] GameObject ParryCollider;
    public Animator animator;

    private IState _curState;
    private Vector2 moveInput;

    private Coroutine evasionCoroutine;
    private Coroutine atkCoroutine;
    private Coroutine invincibleCoroutine;

    bool IsInvincible = false;
    [Header("스킬스크립트")]
    [SerializeField] kjh.PlayerSkill_Parrying playerSkill1;
    [SerializeField] kjh.PlayerSkill_Special playerSkill2;

    [Header("카메라 이벤트")]
    [SerializeField] CameraZoom camera;

    public bool isPaused;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        OnZClick += OnClick_Z;
        OnXClick += OnClick_X;
        SkillGauge_Max = 100;
        SkillGauge_RecoverySec = 1;
        Hp = 4;
        MoveSpeed = 10f;
        evasion_duration = 0.5f;
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
        _rigidbody.velocity = Vector3.ClampMagnitude(new Vector3(_moveCommandVector.x, 0, _moveCommandVector.y), MoveSpeed);
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
        if (IsInvincible) { Debug.Log("무적"); return; }
        StartInvincible(damagedinvincible);

        Debug.Log("데미지");
        BlinkEffect blinkEffect = GetComponent<BlinkEffect>();
        if (blinkEffect != null) { blinkEffect.StartBlinking(); }

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
        animator.enabled = false;
        this.enabled = false;
        GameManager.Instance.GameOver_OnPlayerDead();
    }

    void InputCheck_OnUpdate()
    {
        if(isPaused) return;

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


        if (evasion_coolTimeValue <= 0)
        {
            _curState.OnInput(KeyName.Z);
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
        if (_moveCommandVector != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(_moveCommandVector.x, _moveCommandVector.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }
        evasionCoroutine = StartCoroutine(EvasionCoroutine());
    }
    //회피 정지
    public void EvasionStop()
    {
        if (evasionCoroutine != null)
        {
            StopCoroutine(evasionCoroutine);
            evasionCoroutine = null;

            // 현재 위치에 고정
            _rigidbody.velocity = Vector3.zero;
            isEvading = false;
            ChangeLayer(this.gameObject, 8); // 레이어 8 Player
        }
    }
    //더킹 코루틴
    private IEnumerator EvasionCoroutine()
    {
        playerSound.Play_EvasionSound();
        evasion_Pac.Play();
        isEvading = true;
        ChangeLayer(this.gameObject, 13);//레이어 13 Evasion

        float elapsedTime = 0f;

        while (elapsedTime < evasion_duration)
        {
            if (!isEvading) yield break;
            _rigidbody.velocity = this.transform.forward * evasion_Velocity;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _rigidbody.velocity = Vector3.zero;
        isEvading = false;
        ChangeLayer(this.gameObject, 8);//레이어 8 Player
        ChangeState(new EvasionDelayState(this));
    }

    //플레이어 상태변경
    public void ChangeState(IState newState)
    {
        if ((_curState is SpecialAtkState || _curState is Atk1State || _curState is Atk2State || _curState is Atk3State || _curState is StrongAtkState || _curState is EvasionState || _curState is EvasionDelayState || _curState is SpecialAtkState) && newState is MoveState)
        {

            return;
        }
        _curState?.ExitState();
        _curState = newState;
        _curState.EnterState();
    }

    //애니메이션 완료 함수
    public void OnAnimationComplete(string animationName)
    {
        _curState.OnAnimationComplete(animationName);
    }

    //이동 입력체크
    public Vector2 GetMoveInput()
    {
        return moveInput;
    }

    public void StopPlayer()
    {
        _rigidbody.velocity = Vector2.zero;
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
    }
    //스페셜어택
    public void SpecialAttack()
    {
        camera.StartZoomIn();
        StartInvincible(specialinvincible);
        SkillGauge = 0;

        StartCoroutine(SpecialDelay());
    }

    //공격 코루틴 호출 함수
    public void CallCollider(AtkCollider col)
    {

        if (atkCoroutine != null)
        {
            StopCoroutine(atkCoroutine);
        }

        switch (col)
        {
            case AtkCollider.Atk1:
                atkCoroutine = StartCoroutine(ActiveCollider(Atk1Collider, atk1delay_second));
                break;

            case AtkCollider.Atk2:
                atkCoroutine = StartCoroutine(ActiveCollider(Atk2Collider, atk2delay_second));
                break;

            case AtkCollider.Atk3:
                atkCoroutine = StartCoroutine(ActiveCollider(Atk3Collider, atk3delay_second));
                break;

            case AtkCollider.StrongAtk:
                atkCoroutine = StartCoroutine(ActiveCollider(StrongAtkCollider, 0f));
                StartCoroutine(ActiveCollider(ParryCollider, parrydelay_second));
                break;
        }

    }

    //공격 콜리더 활성,비활성화 코루틴
    private IEnumerator ActiveCollider(GameObject collider, float second)
    {
        yield return new WaitForSeconds(second);
        collider.SetActive(true);
        yield return new WaitForSeconds(atkcollider_active);
        collider.SetActive(false);
    }

    //패리
    public void OnParrying()
    {
        StartInvincible(parryinvincible);
        playerSkill1.Command_Parrying();
        evasion_coolTimeValue = 0;
        OnEvasionGaugeChange?.Invoke(evasion_coolTimeValue / evasion_coolTime);
        SkillGauge += 10;
    }

    //무적 시작 코루틴
    private void StartInvincible(float time)
    {
        if(invincibleCoroutine != null)
        {
            StopCoroutine(invincibleCoroutine);
            invincibleCoroutine = StartCoroutine(InvincibleMode(time));
        }
        else
        {
            invincibleCoroutine = StartCoroutine(InvincibleMode(time));
        }
    }
    //무적 코루틴
    private IEnumerator InvincibleMode(float time)
    {
        Debug.Log($"무적시작, {time} 초");
        IsInvincible = true;
        yield return new WaitForSeconds(time);
        IsInvincible = false;
        Debug.Log("무적끝");
    }
    private IEnumerator SpecialDelay()
    {
        yield return new WaitForSeconds(1);
        TimeManager.Instance.CommandBulletTime(0.2f, 0.7f);
        playerSkill2.Command_Special();
        playerSound.Play_SpecialAttackSound();
        camera.StartZoomOut();
    }

}
public enum AtkCollider
{
    Atk1,
    Atk2,
    Atk3,
    StrongAtk,
    Parry

}