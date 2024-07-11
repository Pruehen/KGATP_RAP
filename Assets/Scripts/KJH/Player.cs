using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(1f, 10f)][SerializeField] float MoveSpeed;

    Rigidbody _rigidbody;
    Vector2 _moveCommandVector = Vector2.zero;

    Action OnZClick;
    Action OnXClick;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        OnZClick += OnClick_Z;
        OnXClick += OnClick_X;
    }

    // Update is called once per frame
    void Update()
    {
        InputCheck_OnUpdate();

        _rigidbody.velocity = _moveCommandVector;
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

    void OnClick_Z()
    {
        Debug.Log("Z 버튼 클릭");
    }

    void OnClick_X()
    {
        Debug.Log("X 버튼 클릭");
    }
}
