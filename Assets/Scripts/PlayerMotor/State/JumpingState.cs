using UnityEngine;

public class JumpingState : IBaseState
{
    [SerializeField] private float _jumpForce = 7f;

    public void Construct(PlayerMotor motor)
    {
        motor.Anim?.SetTrigger("Jump");
        motor.verticalVelocity = _jumpForce;
    }

    public void Destruct(PlayerMotor motor)
    {

    }

    public void ProcessMotion(PlayerMotor motor)
    {
        motor.ApplyGravity();

        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = motor.verticalVelocity;
        m.z = motor.baseRunSpeed;

        motor.moveVector = m;
    }

    public void Transition(PlayerMotor motor)
    {
        if (InputManager.Instance.SwipeLeft)
            motor.ChangeLane(-1);

        if (InputManager.Instance.SwipeRight)
            motor.ChangeLane(1);

        if (motor.verticalVelocity < 0)
            motor.ChangeState(new FallingState());
    }
}
