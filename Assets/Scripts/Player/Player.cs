using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnPlayerDeathEvent;

    [Header("Run")]
    [SerializeField] private float _baseRunSpeed = 5f;
    [Header("Slide")]
    [SerializeField] private float _slideDuration = 1f;
    [Header("Jump")]
    [SerializeField] private float _jumpForce = 7f;
    [Header("Respawn")]
    [SerializeField] private float _verticalDistance = 25f;
    [SerializeField] private float _immunityTime = 1f;
    [Header("Death")]
    [SerializeField] private Vector3 _knockbackForce = new Vector3(0, 4, -3);

    public float baseRunSpeed => _baseRunSpeed;
    public float gravity => _gravity;
    public float slideDuration => _slideDuration;
    public float JumpForce => _jumpForce;
    public float VerticalDistance => _verticalDistance;
    public float ImmunityTime => _immunityTime;
    public Vector3 KnockbackForce => _knockbackForce;
    public Vector3 moveVector { get; set; }
    public float verticalVelocity { get; set; }
    public bool isGrounded { get; set; }
    public int currentLane { get; set; }
    public CharacterController Controller => _controller;
    public Animator Anim => _anim;

    private float _gravity = 14f;
    private float _terminalVelocity = 20f;
    private float _baseSidewaySpeed = 10f;
    private float _distanceInBetweenLanes = 3f;
    private bool _pause = true;
    private CharacterController _controller;
    private Animator _anim;
    private IPlayerState _currentState;
    private Dictionary<Type, IPlayerState> _statesMap;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();

        InitPlayerState();
    }

    private void Update()
    {
        if (!_pause)
            UpdateMotor();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string hitLayerMask = LayerMask.LayerToName(hit.gameObject.layer);

        if (hitLayerMask == "Death")
        {
            SetStateDeath();
            OnPlayerDeathEvent?.Invoke();
        }

    }

    public void RespawnPlayer()
    {
        SetStateRespawn();
    }

    public void ApplyGravity()
    {
        verticalVelocity -= _gravity * Time.deltaTime;

        if (verticalVelocity < -_terminalVelocity)
            verticalVelocity = -_terminalVelocity;
    }

    public void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, -1, 1);
    }

    public float SnapToLane()
    {
        float r = 0f;

        if (transform.position.x != (currentLane * _distanceInBetweenLanes))
        {
            float deltaToDesiredPosition = (currentLane * _distanceInBetweenLanes) - transform.position.x;
            r = (deltaToDesiredPosition > 0) ? 1 : -1;
            r *= _baseSidewaySpeed;

            float actualDistance = r * Time.deltaTime;

            if (Mathf.Abs(actualDistance) > Mathf.Abs(deltaToDesiredPosition))
                r = deltaToDesiredPosition * (1 / Time.deltaTime);
        }
        else
        {
            r = 0;
        }

        return r;
    }

    public void ResetPlayer()
    {
        _pause = true;
        currentLane = 0;
        transform.position = Vector3.zero;
        _anim?.SetTrigger("Idle");
        _anim?.SetFloat("Speed", 0);
    }

    public void PausePlayer()
    {
        _pause = true;
    }

    public void ResumePlayer()
    {
        _pause = false;
    }

    private void UpdateMotor()
    {
        isGrounded = _controller.isGrounded;

        if (_currentState != null)
        {
            _currentState.ProcessMotion(this);

            //Trying to change state
            _currentState.Transition(this);
        }

        _anim?.SetBool("IsGrounded", isGrounded);
        _anim?.SetFloat("Speed", Mathf.Abs(moveVector.z));

        //Move the player
        _controller.Move(moveVector * Time.deltaTime);
    }

    #region STATE

    public void SetStateRun()
    {
        var state = GetPlayerState<PlayerStateRunning>();
        SetState(state);
    }
    public void SetStateJump()
    {
        var state = GetPlayerState<PlayerStateJumping>();
        SetState(state);
    }
    public void SetStateFall()
    {
        var state = GetPlayerState<PlayerStateFalling>();
        SetState(state);
    }
    public void SetStateRespawn()
    {
        var state = GetPlayerState<PlayerStateRespawn>();
        SetState(state);
    }
    public void SetStateDeath()
    {
        var state = GetPlayerState<PlayerStateDeath>();
        SetState(state);
    }
    public void SetStateSlide()
    {
        var state = GetPlayerState<PlayerStateSliding>();
        SetState(state);
    }
    public void SetStateIdle()
    {
        var state = GetPlayerState<PlayerStateIdle>();
        SetState(state);
    }

    private void InitPlayerState()
    {
        _statesMap = new Dictionary<Type, IPlayerState>();

        CreateState<PlayerStateIdle>();
        CreateState<PlayerStateRunning>();
        CreateState<PlayerStateFalling>();
        CreateState<PlayerStateJumping>();
        CreateState<PlayerStateRespawn>();
        CreateState<PlayerStateDeath>();
        CreateState<PlayerStateSliding>();
    }

    private void SetState(IPlayerState newState)
    {
        if (_currentState != null)
            _currentState.Destruct(this);

        _currentState = newState;
        _currentState.Construct(this);
    }

    private void SetStateByDefault()
    {
        SetStateIdle();
    }

    private IPlayerState GetPlayerState<T>() where T : IPlayerState
    {
        var type = typeof(T);
        return _statesMap[type];
    }

    private void CreateState<T>() where T : IPlayerState, new()
    {
        var state = new T();
        var type = typeof(T);
        _statesMap[type] = state;
    }

    #endregion
}


