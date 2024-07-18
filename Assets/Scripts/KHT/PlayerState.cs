using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum KeyName
{
    Z,
    X
}
public interface IState
{
    void EnterState();
    void ExitState();
    void ExcuteOnUpdate();
    void OnInput(KeyName InputName);
    void OnAnimationComplete(string animationName);
}
public abstract class StateBase : IState
{
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void ExcuteOnUpdate() { }
    public virtual void OnInput(KeyName InputName) { }
    public virtual void OnAnimationComplete(string animationName) { }
}

//Idle
public class IdleState : StateBase
{
    private readonly Player _player;

    public IdleState(Player player)
    {
        _player = player;
    }

    public override void EnterState()
    {

    }
    public override void ExitState()
    {
        
    }
    public override void ExcuteOnUpdate()
    {
        _player.StopPlayer();
    }
    public override void OnInput(KeyName InputName)
    {
        switch (InputName)
        {
            case KeyName.X:
                if (_player.SkillGauge >= _player.SkillGauge_Max)
                {
                    _player.ChangeState(new SpecialAtkState(_player));
                }
                else
                {
                    _player.ChangeState(new Atk1State(_player));
                }
                break;
            case KeyName.Z:
                _player.ChangeState(new EvasionState(_player));
                break;
        }
    }
}

//Move
public class MoveState : StateBase
{
    private readonly Player _player;

    public MoveState(Player player)
    {
        _player = player;
    }

    public override void EnterState()
    {
        _player.animator.SetBool("Run", true);
    }
    public override void ExitState()
    {
        _player.animator.SetBool("Run", false);
        
    }
    public override void ExcuteOnUpdate()
    {
        Vector2 moveInput = _player.GetMoveInput();
        _player.Move();
        if(moveInput == Vector2.zero)
        {
            _player.ChangeState(new IdleState(_player));
        }
    }
    public override void OnInput(KeyName InputName)
    {
        switch (InputName)
        {
            case KeyName.X:
                if (_player.SkillGauge >= _player.SkillGauge_Max)
                {
                    _player.ChangeState(new SpecialAtkState(_player));
                    _player.StopPlayer();
                }
                else
                {
                    _player.ChangeState(new Atk1State(_player));
                    _player.StopPlayer();
                }
                break;
            case KeyName.Z:
                _player.ChangeState(new EvasionState(_player));
                break;
        }
    }
}


//Evasition
public class EvasionState : StateBase
{
    private readonly Player _player;

    public EvasionState(Player player)
    {
        _player = player;
    }

    public override void EnterState()
    {
        _player.animator.SetTrigger("Evasion");
        _player.EvasionStart();
    }
    public override void ExitState()
    {
        //_player.animator.SetBool("Evasion", false);
        _player.animator.SetTrigger("Stop");
    }
    public override void OnInput(KeyName InputName)
    {
        if (InputName == KeyName.X)
        {
            _player.ChangeState(new StrongAtkState(_player));
            _player.EvasionStop();
            
        }
    }
}


//EvastionDelay
public class EvasionDelayState : StateBase
{
    private readonly Player _player;

    public EvasionDelayState(Player player)
    {
        _player = player;
    }

    public override void EnterState()
    {
        _player.Delay();
    }
    public override void ExitState()
    {

    }

}
//Atk1
public class Atk1State : StateBase
{
    private readonly Player _player;
    bool iscombo = false;
    public Atk1State(Player player)
    {
        _player = player;
    }

    public override void EnterState()
    {
        _player.animator.SetTrigger("Atk1");
        _player.CallCollider(AtkCollider.Atk1);
    }
    public override void ExitState()
    {
        iscombo = false;
    }
    public override void OnInput(KeyName InputName)
    {
        switch (InputName)
        {
            case KeyName.X:
                iscombo = true;
                break;
            case KeyName.Z:
                _player.animator.SetTrigger("Stop");
                _player.ChangeState(new EvasionState(_player));
                break;
        }

    }
    public override void OnAnimationComplete(string animationName)
    {
        if(iscombo)
        {
            _player.ChangeState(new Atk2State(_player));
            
        }
        else
        {
            _player.animator.SetTrigger("Stop");
            _player.ChangeState(new IdleState(_player));
        }
    }
}

//Atk2
public class Atk2State : StateBase
{
    private readonly Player _player;
    bool iscombo = false;
    public Atk2State(Player player)
    {
        _player = player;
    }

    public override void EnterState()
    {
        _player.animator.SetTrigger("Atk2");
        _player.CallCollider(AtkCollider.Atk2);
    }
    public override void ExitState()
    {

        iscombo = false;
        
    }
    public override void OnInput(KeyName InputName)
    {
        switch (InputName)
        {
            case KeyName.X:
                iscombo = true;
                break;
            case KeyName.Z:
                _player.animator.SetTrigger("Stop");
                _player.ChangeState(new EvasionState(_player));
                break;
        }
    }
    public override void OnAnimationComplete(string animationName)
    {
        if (iscombo)
        {

            _player.ChangeState(new Atk3State(_player));
            
        }
        else
        {
            _player.animator.SetTrigger("Stop");
            _player.ChangeState(new IdleState(_player));
        }

    }
}

//Atk3
public class Atk3State : StateBase
{
    private readonly Player _player;

    public Atk3State(Player player)
    {
        _player = player;
    }

    public override void EnterState()
    {
        _player.animator.SetTrigger("Atk3");
        _player.CallCollider(AtkCollider.Atk3);
    }
    public override void ExitState()
    {

    }
    public override void OnInput(KeyName InputName)
    {
        switch (InputName)
        {
            case KeyName.Z:
                _player.animator.SetTrigger("Stop");
                _player.ChangeState(new EvasionState(_player));
                break;
        }
    }
    public override void OnAnimationComplete(string animationName)
    {
        _player.animator.SetTrigger("Stop");
        _player.ChangeState(new IdleState(_player));
    }
}

//StrongAtk
public class StrongAtkState : StateBase
{
    private readonly Player _player;

    public StrongAtkState(Player player)
    {
        _player = player;
    }

    public override void EnterState()
    {
        _player.animator.SetTrigger("Strong");
        _player.CallCollider(AtkCollider.StrongAtk);
    }
    public override void ExitState()
    {
        
    }
    public override void ExcuteOnUpdate()
    {
        _player.StopPlayer();
    }
    public override void OnAnimationComplete(string animationName)
    {
        _player.ChangeState(new IdleState(_player));
    }
}

//SpecialAtk
public class SpecialAtkState : StateBase
{
    private readonly Player _player;

    public SpecialAtkState(Player player)
    {
        _player = player;
    }

    public override void EnterState()
    {
        _player.animator.SetTrigger("SpecialAtk");
        _player.SpecialAttack();
    }
    public override void ExitState()
    {
        
    }
    public override void ExcuteOnUpdate()
    {
        _player.StopPlayer();
    }
    public override void OnAnimationComplete(string animationName)
    {
        _player.ChangeState(new IdleState(_player));
    }
}