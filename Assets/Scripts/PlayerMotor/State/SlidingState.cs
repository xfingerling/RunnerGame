using UnityEngine;

public class SlidingState : BaseState
{
    public float slideDuration = 1f;

    private Vector3 _initialCenter;
    private float _initialSize;
    private float _slideStart;

    public override void Construct()
    {
        _slideStart = Time.time;

        _initialSize = motor.Controller.height;
        _initialCenter = motor.Controller.center;

        motor.Controller.height = _initialSize * 0.5f;
        motor.Controller.center = _initialCenter * 0.5f;
    }

    public override void Destruct()
    {
        motor.Controller.height = _initialSize;
        motor.Controller.center = _initialCenter;
    }

    public override Vector3 ProcessMotion()
    {
        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = -1f;
        m.z = motor.baseRunSpeed;

        return m;
    }

    public override void Transition()
    {
        if (InputManager.Instance.SwipeLeft)
            motor.ChangeLane(-1);

        if (InputManager.Instance.SwipeRight)
            motor.ChangeLane(1);

        if (!motor.isGrounded)
            motor.ChangeState(GetComponent<FallingState>());

        if (InputManager.Instance.SwipeUp)
            motor.ChangeState(GetComponent<JumpingState>());

        if (Time.time - _slideStart > slideDuration)
            motor.ChangeState(GetComponent<RunningState>());
    }
}
