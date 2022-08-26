using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [HideInInspector] public Vector3 moveVector;
    [HideInInspector] public float verticalVelocity;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public int currentLane;

    public float distanceInBetweenLanes = 3f;
    public float baseRunSpeed = 5f;
    public float baseSidewaySpeed = 10f;
    public float gravity = 14f;
    public float terminalVelocity = 20f;
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
        verticalVelocity -= gravity * Time.deltaTime;

        if (verticalVelocity < -terminalVelocity)
            verticalVelocity = -terminalVelocity;
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

        if (transform.position.x != (currentLane * distanceInBetweenLanes))
        {
            float deltaToDesiredPosition = (currentLane * distanceInBetweenLanes) - transform.position.x;
            r = (deltaToDesiredPosition > 0) ? 1 : -1;
            r *= baseSidewaySpeed;

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
