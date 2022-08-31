using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private float _distanceInBetweenLanes = 3f;
    [SerializeField] private float _baseSidewaySpeed = 10f;
    [SerializeField] private float _gravity = 14f;
    [SerializeField] private float _terminalVelocity = 20f;
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

    public float BaseRunSpeed => _baseRunSpeed;
    public float Gravity => _gravity;
    public float SlideDuration => _slideDuration;
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

    private CharacterController _controller;
    private Animator _anim;
    private IBaseState _state;
    private bool _isPaused;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
        _state = new RunningState();
        _state.Construct(this);

        _isPaused = true;
    }

    private void Update()
    {
        if (!_isPaused)
            UpdateMotor();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string hitLayerMask = LayerMask.LayerToName(hit.gameObject.layer);

        if (hitLayerMask == "Death")
            ChangeState(new DeathState());
    }

    public void RespawnPlayer()
    {
        ChangeState(new RespawnState());
        GameManager.Instance.ChangeCamera(GameCamera.Respawn);
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

    public void ChangeState(IBaseState state)
    {
        _state.Destruct(this);
        _state = state;
        _state.Construct(this);
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

    public void PausePlayer()
    {
        _isPaused = true;
        _anim?.SetFloat("Speed", 0);
    }

    public void ResumePlayer()
    {
        _isPaused = false;
    }

    public void ResetPlayer()
    {
        currentLane = 0;
        PausePlayer();
        transform.position = Vector3.zero;
        _anim?.SetTrigger("Idle");
        ChangeState(new RunningState());
    }

    private void UpdateMotor()
    {
        isGrounded = _controller.isGrounded;

        _state.ProcessMotion(this);

        //Trying to change state
        _state.Transition(this);

        _anim?.SetBool("IsGrounded", isGrounded);
        _anim?.SetFloat("Speed", Mathf.Abs(moveVector.z));

        //Move the player
        _controller.Move(moveVector * Time.deltaTime);
    }
}
