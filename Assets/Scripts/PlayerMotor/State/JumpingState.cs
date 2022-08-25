using UnityEngine;

public class JumpingState : BaseState
{
    [SerializeField] private float _jumpForce = 7f;

    public override void Construct()
    {
        motor.Anim?.SetTrigger("Jump");
        motor.verticalVelocity = _jumpForce;
    }

    public override Vector3 ProcessMotion()
    {
        motor.ApplyGravity();

        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = motor.verticalVelocity;
        m.z = motor.baseRunSpeed;

        return m;
    }

    public override void Transition()
    {
        if (InputManager.Instance.SwipeLeft)
            motor.ChangeLane(-1);

        if (InputManager.Instance.SwipeRight)
            motor.ChangeLane(1);

        if (motor.verticalVelocity < 0)
            motor.ChangeState(GetComponent<FallingState>());
    }
}
