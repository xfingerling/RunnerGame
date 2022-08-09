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
    private BaseState _state;
    private bool _isPaused;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
        _state = GetComponent<RunningState>();
        _state.Construct();

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
            ChangeState(GetComponent<DeathState>());
    }

    public void RespawnPlayer()
    {
        ChangeState(GetComponent<RespawnState>());
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

    public void ChangeState(BaseState state)
    {
        _state.Destruct();
        _state = state;
        _state.Construct();
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
    }

    public void ResumePlayer()
    {
        _isPaused = false;
    }

    private void UpdateMotor()
    {
        isGrounded = _controller.isGrounded;

        moveVector = _state.ProcessMotion();

        //Trying to change state
        _state.Transition();

        _anim?.SetBool("IsGrounded", isGrounded);
        _anim?.SetFloat("Speed", Mathf.Abs(moveVector.z));

        //Move the player
        _controller.Move(moveVector * Time.deltaTime);
    }
}
