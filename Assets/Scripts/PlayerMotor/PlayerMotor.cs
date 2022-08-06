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

    public CharacterController controller;

    private BaseState _state;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        _state = GetComponent<RunningState>();
        _state.Construct();
    }

    private void Update()
    {
        UpdateMotor();
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

    private void UpdateMotor()
    {
        isGrounded = controller.isGrounded;

        moveVector = _state.ProcessMotion();

        _state.Transition();

        //Move the player
        controller.Move(moveVector * Time.deltaTime);
    }
}
